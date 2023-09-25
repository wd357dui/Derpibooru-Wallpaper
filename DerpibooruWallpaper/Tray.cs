using System.ComponentModel;
using System.Diagnostics;
using static DerpibooruWallpaper.Localization.Localization;

namespace DerpibooruWallpaper
{
    public static class Tray
    {
        private static Container? TrayContainer;
        private static NotifyIcon? SystemTray;
        private static ContextMenuStrip? RightClickMenu;
        private static ToolStripMenuItem? ChangeWallpaperButton;
        private static ToolStripMenuItem? OpenImageFolderButton;
        private static ToolStripMenuItem? LaunchOnSystemStartupButton;
        private static ToolStripMenuItem? SettingsButton;
        private static ToolStripMenuItem? AboutButton;
        private static ToolStripMenuItem? ExitButton;
        public static void Show()
        {
            TrayContainer = new();
            SystemTray = new(TrayContainer)
            {
                Text = "Derpibooru Wallpaper",
                Icon = Resources.Resources.Derpibooru
            };

            ChangeWallpaperButton = new()
            {
                Name = "ChangeWallpaperButton",
                Text = ChangeWallpaper
            };
            ChangeWallpaperButton.Click += ChangeWallpaperButton_Click;
            OpenImageFolderButton = new()
            {
                Name = "OpenImageFolderButton",
                Text = OpenImageFolder
            };
            OpenImageFolderButton.Click += OpenImageFolderButton_Click;
            LaunchOnSystemStartupButton = new()
            {
                Name = "LaunchOnSystemStartupButton",
                Text = LaunchOnSystemStartup
            };
            LaunchOnSystemStartupButton.Click += LaunchOnSystemStartupButton_Click;
            SettingsButton = new()
            {
                Name = "SettingsButton",
                Text = Localization.Localization.Settings
            };
            SettingsButton.Click += SettingsButton_Click;
            AboutButton = new()
            {
                Name = "AboutButton",
                Text = About
            };
            AboutButton.Click += AboutButton_Click;
            ExitButton = new()
            {
                Name = "ExitButton",
                Text = Exit
            };
            ExitButton.Click += ExitButton_Click;

            RightClickMenu = new();
            RightClickMenu.Items.Add(ChangeWallpaperButton);
            RightClickMenu.Items.Add(OpenImageFolderButton);
            RightClickMenu.Items.Add(LaunchOnSystemStartupButton);
            RightClickMenu.Items.Add(SettingsButton);
            RightClickMenu.Items.Add(AboutButton);
            RightClickMenu.Items.Add(ExitButton);
            RightClickMenu.Opening += RightClickMenu_Opening;

            SystemTray.MouseDoubleClick += SystemTray_MouseDoubleClick;
            SystemTray.ContextMenuStrip = RightClickMenu;
            SystemTray.Visible = true;

            new Thread(() =>
            {
                try
                {
                    if (!Settings.APIKeyValid)
                    {
                        if (Settings.SettingsMutex.WaitOne(0))
                        {
                            using Settings SettingsWindow = new();
                            SettingsWindow.ShowDialog();
                            Settings.SettingsMutex.ReleaseMutex();
                        }
                        else Settings.FocusInstance();
                    }
                }
                catch { }
            }).Start();
        }

        private static void SystemTray_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            new Thread(() =>
            {
                if (Settings.SettingsMutex.WaitOne(0))
                {
                    using Settings SettingsWindow = new();
                    SettingsWindow.ShowDialog();
                    Settings.SettingsMutex.ReleaseMutex();
                }
                else Settings.FocusInstance();
            }).Start();
        }

        private static void RightClickMenu_Opening(object? sender, CancelEventArgs e)
        {
            if (LaunchOnSystemStartupButton is not null)
            {
                LaunchOnSystemStartupButton.Checked = Settings.LaunchOnSystemStartup;
            }
        }

        private static void ChangeWallpaperButton_Click(object? sender, EventArgs e)
        {
            new Thread(() =>
            {
                try
                {
                    if (API.ChangingWallpaperMutex.WaitOne(0))
                    {
                        API.ChangeWallpaper(true);
                    }
                }
                catch (APIException ex)
                {
                    if (!ex.MessageBoxAlreadyShown) MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    _ = new OurException("external exception", ex);
                    MessageBox.Show("stack trace: " + ex.StackTrace + "\r\n" +
                                    ErrorMessage + ": " + ex.Message + "\r\n",
                                    Error,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }).Start();
        }

        private static void OpenImageFolderButton_Click(object? sender, EventArgs e)
        {
            Process.Start("explorer", '\"' + Directory.CreateDirectory("images").FullName + '\"');
        }

        private static void LaunchOnSystemStartupButton_Click(object? sender, EventArgs e)
        {
            if (LaunchOnSystemStartupButton is not null)
            {
                try
                {
                    Settings.LaunchOnSystemStartup = !LaunchOnSystemStartupButton.Checked;
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
        }

        private static void SettingsButton_Click(object? sender, EventArgs e)
        {
            new Thread(() =>
            {
                if (Settings.SettingsMutex.WaitOne(0))
                {
                    using Settings SettingsWindow = new();
                    SettingsWindow.ShowDialog();
                    Settings.SettingsMutex.ReleaseMutex();
                }
                else Settings.FocusInstance();
            }).Start();
        }

        private static void AboutButton_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(MadeBy, About,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void ExitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        public static void Hide()
        {
            if (SystemTray is not null)
            {
                SystemTray.Visible = false;
            }
        }
    }
}
