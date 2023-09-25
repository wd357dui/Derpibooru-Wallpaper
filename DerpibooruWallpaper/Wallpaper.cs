using System.Runtime.InteropServices;

namespace DerpibooruWallpaper
{
    public static class Wallpaper
    {
        const int SPI_SETDESKWALLPAPER = 0x0014;

        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        /*
        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }
        */
        public static bool Set(string ImagePath) //, Style? style = null)
        {
            /*
            if (style is not null)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true) ??
                    throw new Exception("failed to access registry, unable to change wallpaper style");
                int? WallpaperStyle = key.GetValue(@"WallpaperStyle") as int?;
                int? TileWallpaper = key.GetValue(@"TileWallpaper") as int?;
                if (style == Style.Stretched)
                {
                    if (WallpaperStyle != 2 && TileWallpaper != 0)
                    {
                        key.SetValue(@"WallpaperStyle", 2.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }
                }
                else if (style == Style.Centered)
                {
                    if (WallpaperStyle != 1 && TileWallpaper != 0)
                    {
                        key.SetValue(@"WallpaperStyle", 1.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }
                }
                else if (style == Style.Tiled)
                {
                    if (WallpaperStyle != 1 && TileWallpaper != 1)
                    {
                        key.SetValue(@"WallpaperStyle", 1.ToString());
                        key.SetValue(@"TileWallpaper", 1.ToString());
                    }
                }
            }
            */
            return SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, ImagePath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) == 1;
        }
    }
}
