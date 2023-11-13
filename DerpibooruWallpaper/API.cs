using System.Text.Json;
using System.Web;
using static DerpibooruWallpaper.Localization.Localization;

namespace DerpibooruWallpaper
{
    public static class API
    {
        static bool StopAPI = false;
        public static string UserAgent => "DerpibooruWallpaper/build_2023.5.26 (.Net " + Environment.Version + "; " + Environment.OSVersion + ") made by wd357dui";
        public static bool VerifyKey(string APIKey, TimeSpan? timeout = null)
        {
            if (StopAPI)
            {
                throw new APIException(APIRequestPaused);
            }
            string Query = "/api/v1/json/filters/user?key=" + APIKey;
            Uri Target = new(
                (Settings.UseHTTPS ? "https://" : "http://") +
                (Settings.UseTrixiebooruMirror ? "trixiebooru.org" : "derpibooru.org") +
                Query
                );
            using HttpClient Client = new(new HttpClientHandler()
            {
                Proxy = Settings.UseSystemProxy ? HttpClient.DefaultProxy : null
            });
            if (timeout is TimeSpan t) Client.Timeout = t;
            HttpRequestMessage Request = new(HttpMethod.Get, Target);
            Request.Headers.Add("User-Agent", UserAgent);
            var Response = Client.Send(Request);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return false;
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                throw new APIException(CannotVerifyAPIKey + "\r\n" +
                    YourConnectionIsFineButDerpibooru503,
                    Response.Content.ReadAsStringAsync().Result);
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                throw new APIException(CannotVerifyAPIKey + "\r\n" +
                    string.Format(TooManyRequests, (int)Response.StatusCode, Response.ReasonPhrase ?? ""),
                    Response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new APIException(FailedToVerifyAPIKey + "\r\n" +
                    string.Format(ServerResponseCode, (int)Response.StatusCode, Response.ReasonPhrase ?? ""),
                    Response.Content.ReadAsStringAsync().Result);
            }
        }

        public static readonly Mutex ChangingWallpaperMutex = new(false, nameof(DerpibooruWallpaper) + "." + nameof(API) + "." + nameof(ChangeWallpaper));
        public static void ChangeWallpaper(bool MutexAlreadyOwned)
        {
            if (!MutexAlreadyOwned)
            {
                ChangingWallpaperMutex.WaitOne();
            }
            Exception? e = null;
            try {
                var doc = SearchImages(Settings.APIKey, Settings.SearchQuery, 1, 1);
                int total = doc.RootElement.GetProperty("total").GetInt32();
                if (total > 0)
                {
                    int selected = new Random().Next(1, total);
                    Thread.Sleep(500);
                    var doc2 = SearchImages(Settings.APIKey, Settings.SearchQuery, 1, selected);
                    var images = doc2.RootElement.GetProperty("images");
                    var image = images[0];
                    string full = image.GetProperty("representations").GetProperty("full").GetString() ??
                        throw new APIException(string.Format(ServerDidNotProvideFullLink, selected) + "\r\n" +
                        "(property `images[" + 0 + "].representations.full` is null)",
                        doc.RootElement.ToString());
                    string filePath = DownloadFile(full);
                    /*
                    bool ImageBiggerThan4K = image.GetProperty("width").GetInt32() > 3840 || image.GetProperty("height").GetInt32() > 2160;
                    bool Convert = ImageBiggerThan4K;
                    if (filePath.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string newFilePath = filePath.Remove(filePath.LastIndexOf(".png")) + ".bmp";
                        using Image temp = Image.FromFile(filePath);
                        var Format = temp.PixelFormat;
                        switch (Format)
                        {
                            case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                            case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                            case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                            case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                            case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                                Convert = true;
                                break;
                        }
                        if (Convert)
                        {
                            using Bitmap temp2 = new(temp.Width, temp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                            using Graphics graphics = Graphics.FromImage(temp2);
                            graphics.Clear(Color.White);
                            graphics.DrawImage(temp, new Rectangle(0, 0, temp2.Width, temp2.Height), 0, 0, temp.Width, temp.Height, GraphicsUnit.Pixel);
                            temp2.Save(newFilePath, System.Drawing.Imaging.ImageFormat.Bmp);
                            filePath = newFilePath;
                        }
                    }
                    */
                    Wallpaper.Set(filePath);
                    Settings.LastUpdate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else throw new APIException(NoImagesFoundWithCurrentSearchParameters + "\r\n",
                    "search query:\r\n" +
                    Settings.SearchQuery);
            }
            catch (Exception ex)
            {
                e = ex;
            }
            finally {
                ChangingWallpaperMutex.ReleaseMutex();
            }
            if (e is not null) throw e;
        }
        public static JsonDocument SearchImages(string APIKey, string SearchParams, int ImgPerPage, int Page)
        {
            if (StopAPI)
            {
                throw new APIException(APIRequestPaused);
            }
            string Query = "/api/v1/json/search/images?key=" + APIKey + "&q=" + HttpUtility.UrlEncode(SearchParams) +
                "&per_page=" + ImgPerPage + "&page=" + Page;
            Uri Target = new(
                (Settings.UseHTTPS ? "https://" : "http://") +
                (Settings.UseTrixiebooruMirror ? "trixiebooru.org" : "derpibooru.org") +
                Query
                );
            using HttpClient Client = new(new HttpClientHandler()
            {
                Proxy = Settings.UseSystemProxy ? HttpClient.DefaultProxy : null
            });
            HttpRequestMessage Request = new(HttpMethod.Get, Target);
            Request.Headers.Add("User-Agent", UserAgent);
            var Response = Client.Send(Request);
            if (Response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (Response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    StopAPI = true;
                    string Banned = string.Format(BannedFromDerpibooru, (int)Response.StatusCode, Response.ReasonPhrase ?? "");
                    new Thread(() =>
                    {
                        MessageBox.Show(Banned, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }).Start();
                    throw new APIException(Banned, Response.Content.ReadAsStringAsync().Result)
                    {
                        MessageBoxAlreadyShown = true
                    };
                }
                else if (Response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    throw new APIException(string.Format(YourConnectionIsFineButDerpibooru503, (int)Response.StatusCode, Response.ReasonPhrase ?? ""), Response.Content.ReadAsStringAsync().Result);
                }
                else if (Response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    throw new APIException(string.Format(TooManyRequests, (int)Response.StatusCode, Response.ReasonPhrase ?? ""), Response.Content.ReadAsStringAsync().Result);
                }
                else if ((int)Response.StatusCode >= 400 && (int)Response.StatusCode < 500)
                {
                    StopAPI = true;
                    throw new APIException(string.Format(Unrecognized400, (int)Response.StatusCode, Response.ReasonPhrase ?? ""), Response.Content.ReadAsStringAsync().Result);
                }
                else if ((int)Response.StatusCode >= 500 && (int)Response.StatusCode < 600)
                {
                    throw new APIException(string.Format(Unrecognized500, (int)Response.StatusCode, Response.ReasonPhrase ?? ""), Response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new APIException(string.Format(UnrecognizedStatusCode, (int)Response.StatusCode, Response.ReasonPhrase ?? ""), Response.Content.ReadAsStringAsync().Result);
                }
            }
            string Result = Response.Content.ReadAsStringAsync().Result;
            return JsonDocument.Parse(Result);
        }
        public static string DownloadFile(string Path)
        {
            Directory.CreateDirectory("images");
            string filePath = "images/" + Path[(Path.LastIndexOf("/") + 1)..];
            if (File.Exists(filePath) && !Settings.OverrideExistingImageFiles)
            {
                using FileStream file = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return file.Name;
            }
            using HttpClient Client = new(new HttpClientHandler()
            {
                Proxy = Settings.UseSystemProxy ? HttpClient.DefaultProxy : null
            });
            using (Stream result = Client.GetStreamAsync(Path).Result)
            {
                using FileStream file = new(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                filePath = file.Name;
                result.CopyTo(file);
            }
            return filePath;
        }
    }
}
