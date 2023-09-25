using Microsoft.Win32;
using System.Globalization;
using static DerpibooruWallpaper.Localization.Localization;

namespace DerpibooruWallpaper
{
    public partial class Settings : Form
    {
        public static readonly Mutex SettingsMutex = new(false, nameof(DerpibooruWallpaper) + "." + nameof(Settings));
        static Settings? Instance;
        public static void FocusInstance()
        {
            Instance?.Invoke(() => { Instance?.Activate(); });
        }
        public Settings()
        {
            InitializeComponent();
            Instance = this;

            int? day = UpdateIntervalDay;
            int? hour = UpdateIntervalHour;
            int? minute = UpdateIntervalMinute;
            if (DaysRadioButton.Checked = day is not null)
            {
                WallpaperChangeIntervalTextBox.Text = day?.ToString() ?? "";
                WallpaperChangeIntervalSaved = true;
            }
            if (HoursRadioButton.Checked = hour is not null)
            {
                WallpaperChangeIntervalTextBox.Text = hour?.ToString() ?? "";
                WallpaperChangeIntervalSaved = true;
            }
            if (MinutesRadioButton.Checked = minute is not null)
            {
                WallpaperChangeIntervalTextBox.Text = minute?.ToString() ?? "";
                WallpaperChangeIntervalSaved = true;
            }

            APIKeyTextBox.Text = APIKey;
            SearchParamTextBox.Text = SearchQuery;
            APIKeySaved = true;
            SearchParamSaved = true;
            OverrideExistingImageFilesCheckBox.Checked = OverrideExistingImageFiles;
            LaunchOnSystemStartupCheckBox.Checked = LaunchOnSystemStartup;
            UseHttpsCheckBox.Checked = UseHTTPS;
            UseSystemProxyCheckBox.Checked = UseSystemProxy;
            UseTrixiebooruMirrorCheckBox.Checked = UseTrixiebooruMirror;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                if (!APIKeyValid)
                {
                    MessageBox.Show(this,
                        SpecifyYourAPIKey + "\r\n" +
                        "(" + WhereToCheckYourAPIKey + ")",
                        Attention,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void APIKeyTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            APIKeyTextBox.UseSystemPasswordChar = !APIKeyTextBox.UseSystemPasswordChar;
        }

        private bool APIKeySaved = true;
        private void APIKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            APIKeySaved = false;
        }

        private bool SearchParamSaved = true;
        private void SearchParamTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchParamSaved = false;
        }

        private bool WallpaperChangeIntervalSaved = true;
        private void WallpaperChangeIntervalTextBox_TextChanged(object sender, EventArgs e)
        {
            WallpaperChangeIntervalSaved = false;
        }

        private void DaysRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            WallpaperChangeIntervalSaved = false;
        }

        private void HoursRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            WallpaperChangeIntervalSaved = false;
        }

        private void MinutesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            WallpaperChangeIntervalSaved = false;
        }

        bool NotSafeToClose = false;
        private void SaveAPIKeyButton_Click(object sender, EventArgs e)
        {
            NotSafeToClose = true;
            SaveAPIKeyButton.Text = Wait;
            SaveAPIKeyButton.Enabled = false;
            APIKeyTextBox.Focus();
            string NewKey = APIKeyTextBox.Text;
            void SaveKey()
            {
                APIKey = NewKey;
                APIKeySaved = APIKeyTextBox.Text == NewKey;
            }
            void End()
            {
                SaveAPIKeyButton.Text = Save;
                SaveAPIKeyButton.Enabled = true;
                NotSafeToClose = false;
            }
            if (NewKey.Length > 0)
            {
                new Thread(() =>
                {
                    try
                    {
                        if (APIKeyValid = API.VerifyKey(NewKey))
                        {
                            Invoke(SaveKey);
                            Invoke(End);
                        }
                        else
                        {
                            Invoke(() =>
                            {
                                if (MessageBox.Show(this,
                                    NotValidAPIKey + ",\r\n" +
                                    WhereToCheckYourAPIKey + ".\r\n" +
                                    string.Format(PressToUseKeyAnyway, OK),
                                    Error,
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                    Invoke(SaveKey);
                                Invoke(End);
                            });
                        }
                    }
                    catch (OurException ex)
                    {
                        Invoke(() =>
                        {
                            if (MessageBox.Show(this, ex.Message + "\r\n" +
                                    string.Format(PressToUseKeyAnyway, OK),
                                    Error,
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                Invoke(SaveKey);
                            Invoke(End);
                        });
                    }
                    catch (Exception ex)
                    {
                        _ = new OurException("external exception", ex);
                        Invoke(() =>
                        {
                            if (MessageBox.Show(this, ErrorVerifyingAPIKey + ":\r\n" +
                                    "stack trace: " + ex.StackTrace + "\r\n" +
                                    ErrorMessage + ": " + ex.Message + "\r\n" +
                                    string.Format(PressToUseKeyAnyway, OK),
                                    Error,
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                Invoke(SaveKey);
                            Invoke(End);
                        });
                    }
                }).Start();
            }
            else
            {
                if (MessageBox.Show(this,
                    WhereToCheckYourAPIKey + "\r\n" +
                    string.Format(PressToSaveKeyAnyway, OK),
                    EmptyAPIKey,
                    MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    Invoke(SaveKey);
                Invoke(End);
            }
        }

        private void SaveSearchButton_Click(object sender, EventArgs e)
        {
            SearchQuery = SearchParamTextBox.Text;
            SearchParamSaved = true;
        }

        private void SaveWallpaperChangeIntervalButton_Click(object sender, EventArgs e)
        {
            int? Num = null;
            if (int.TryParse(WallpaperChangeIntervalTextBox.Text, out int n))
            {
                Num = n;
            }
            if (Num is null)
            {
                UpdateIntervalDay = UpdateIntervalHour = UpdateIntervalMinute = null;
                WallpaperChangeIntervalTextBox.Text = string.Empty;
                WallpaperChangeIntervalSaved = true;
            }
            else if (Num is int)
            {
                if (DaysRadioButton.Checked)
                {
                    UpdateIntervalDay = n;
                    UpdateIntervalHour = null;
                    UpdateIntervalMinute = null;
                    WallpaperChangeIntervalSaved = true;
                }
                else if (HoursRadioButton.Checked)
                {
                    UpdateIntervalDay = null;
                    UpdateIntervalHour = n;
                    UpdateIntervalMinute = null;
                    WallpaperChangeIntervalSaved = true;
                }
                else if (MinutesRadioButton.Checked)
                {
                    UpdateIntervalDay = null;
                    UpdateIntervalHour = null;
                    UpdateIntervalMinute = n;
                    WallpaperChangeIntervalSaved = true;
                }
                else
                {
                    MessageBox.Show(SelectUnitOfTimeFirst,
                        Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OverrideExistingFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OverrideExistingImageFiles = OverrideExistingImageFilesCheckBox.Checked;
        }

        private void LaunchOnSystemStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LaunchOnSystemStartup = LaunchOnSystemStartupCheckBox.Checked;
            }
            catch (OurException ex)
            {
                MessageBox.Show(ex.Message, Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                _ = new OurException("external exception", exc);
                MessageBox.Show("stack trace: " + exc.StackTrace + "\r\n" +
                                ErrorMessage + ": " + exc.Message + "\r\n",
                                Error,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UseHttpsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UseHTTPS = UseHttpsCheckBox.Checked;
        }

        private void UseSystemProxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UseSystemProxy = UseSystemProxyCheckBox.Checked;
        }

        private void UseTrixiebooruMirrorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UseTrixiebooruMirror = UseTrixiebooruMirrorCheckBox.Checked;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NotSafeToClose)
            {
                e.Cancel = true;
                MessageBox.Show(this, NotSafeToCloseYet, Warning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (!APIKeySaved)
            {
                e.Cancel = MessageBox.Show(this, UnsavedAPIKey + "\r\n" +
                    CloseWithoutSavingWarning, Warning,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK;
            }
            else if (!SearchParamSaved)
            {
                e.Cancel = MessageBox.Show(this, UnsavedSearchParams + "\r\n" +
                    CloseWithoutSavingWarning, Warning,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK;
            }
            else if (!WallpaperChangeIntervalSaved)
            {
                e.Cancel = MessageBox.Show(this, UnsavedWallpaperChangeInterval + "\r\n" +
                    CloseWithoutSavingWarning, Warning,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK;
            }
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
        }

        #region Settings
        private static bool? WasAPIKeyValid { get => GetSettingsBool(nameof(WasAPIKeyValid)); set => SetSettings(nameof(WasAPIKeyValid), value); }
        public static bool APIKeyValid
        {
            get
            {
                bool Valid;
                if (WasAPIKeyValid is null)
                {
                    if (APIKey.Length > 0)
                    {
                        WasAPIKeyValid = Valid = API.VerifyKey(APIKey);
                    }
                    else WasAPIKeyValid = Valid = false;
                }
                else Valid = WasAPIKeyValid == true;
                return Valid;
            }
            set => WasAPIKeyValid = value;
        }
        public static string APIKey { get => GetSettings(nameof(APIKey), ""); set => SetSettings(nameof(APIKey), value); }
        public static string SearchQuery { get => GetSettings(nameof(SearchQuery), "wallpaper, safe, -video, -webm, -sound, -animated, (aspect_ratio.gte:1.6, aspect_ratio.lte:1.7), (width.gte:1920, height.gte:1080)"); set => SetSettings(nameof(SearchQuery), value); }
        public static bool OverrideExistingImageFiles { get => GetSettingsBool(nameof(OverrideExistingImageFiles), false); set => SetSettings(nameof(OverrideExistingImageFiles), value); }
        public static bool UseHTTPS { get => GetSettingsBool(nameof(UseHTTPS), true); set => SetSettings(nameof(UseHTTPS), value); }
        public static bool UseSystemProxy { get => GetSettingsBool(nameof(UseSystemProxy), true); set => SetSettings(nameof(UseSystemProxy), value); }
        public static bool UseTrixiebooruMirror { get => GetSettingsBool(nameof(UseTrixiebooruMirror), CultureInfo.CurrentUICulture.Name == "zh-CN"); set => SetSettings(nameof(UseTrixiebooruMirror), value); }
        public static string LastUpdate { get => GetSettings(nameof(LastUpdate), ""); set => SetSettings(nameof(LastUpdate), value); }
        public static int? UpdateIntervalMinute { get => GetSettingsInt(nameof(UpdateIntervalMinute)); set => SetSettings(nameof(UpdateIntervalMinute), value); }
        public static int? UpdateIntervalHour { get => GetSettingsInt(nameof(UpdateIntervalHour)); set => SetSettings(nameof(UpdateIntervalHour), value); }
        public static int? UpdateIntervalDay { get => GetSettingsInt(nameof(UpdateIntervalDay)); set => SetSettings(nameof(UpdateIntervalDay), value); }
        static readonly Mutex SettingsFileMutex = new(false, nameof(DerpibooruWallpaper) + "." + nameof(Settings) + ".File");
        public static string GetSettings(string SettingsName, string DefaultValue)
        {
            string? Result = null;
            FileStreamOptions options = new()
            {
                Mode = FileMode.OpenOrCreate,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read,
            };
            SettingsFileMutex.WaitOne();
            using (StreamReader sr = new("Settings.txt", options))
            {
                while (sr.ReadLine() is string line)
                {
                    if (line.StartsWith(SettingsName + "="))
                    {
                        Result = line[(SettingsName.Length + "=".Length)..];
                    }
                }
            }
            SettingsFileMutex.ReleaseMutex();
            if (Result is null)
            {
                Result = DefaultValue;
                SetSettings(SettingsName, DefaultValue);
            }
            return Result;
        }
        public static T? GetSettings<T>(string SettingsName, T? DefaultValue, Func<string, T> Parser) where T : struct
        {
            string Value = GetSettings(SettingsName, DefaultValue?.ToString() ?? "");
            T? Result = null;
            try
            {
                Result = Parser(Value);
            }
            catch (FormatException) { }
            return Result;
        }
        public static bool GetSettingsBool(string SettingsName, bool DefaultValue)
        {
            return GetSettings(SettingsName, DefaultValue, bool.Parse) ?? DefaultValue;
        }
        public static bool? GetSettingsBool(string SettingsName)
        {
            return GetSettings(SettingsName, null, bool.Parse);
        }
        public static int? GetSettingsInt(string SettingsName)
        {
            return GetSettings(SettingsName, null, int.Parse);
        }

        public static void SetSettings<T>(string SettingsName, T Value)
        {
            FileStreamOptions options = new()
            {
                Mode = FileMode.Open,
                Access = FileAccess.Read,
                Share = FileShare.Read,
            };
            string[] lines;
            SettingsFileMutex.WaitOne();
            using (StreamReader reader = new("Settings.txt", options))
            {
                lines = reader.ReadToEnd().Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            SettingsFileMutex.ReleaseMutex();
            options.Access = FileAccess.ReadWrite;
            SettingsFileMutex.WaitOne();
            using (StreamWriter writer = new("Settings.txt", options))
            {
                bool Found = false;
                foreach (string line in lines)
                {
                    if (line.StartsWith(SettingsName + "="))
                    {
                        writer.WriteLine(SettingsName + "=" + Value);
                        Found = true;
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
                if (!Found)
                {
                    writer.WriteLine(SettingsName + "=" + Value);
                }
            }
            SettingsFileMutex.ReleaseMutex();
        }

        public static bool LaunchOnSystemStartup
        {
            get
            {
                const string HKCU = "HKEY_CURRENT_USER";
                const string RUN_KEY = @"SOFTWARE\\Microsoft\Windows\CurrentVersion\Run";
                string exePath = '"' + Application.ExecutablePath + '"';
                string? Value = null;
                try
                {
                    Value = Registry.GetValue(HKCU + "\\" + RUN_KEY, nameof(DerpibooruWallpaper), null) as string;
                }
                catch { }
                return Value == exePath;
            }
            set
            {
                const string RUN_KEY = @"SOFTWARE\\Microsoft\Windows\CurrentVersion\Run";
                string exePath = '"' + Application.ExecutablePath + '"';
                RegistryKey? key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true);
                if (key is RegistryKey Key)
                {
                    if (value)
                    {
                        Key.SetValue(nameof(DerpibooruWallpaper), exePath);
                    }
                    else Key.DeleteValue(nameof(DerpibooruWallpaper), false);
                }
                else throw new APIException(FailedToOpenRegistryKeyForLaunchOnStartup,
                    RUN_KEY);
            }
        }
        #endregion
    }
}
