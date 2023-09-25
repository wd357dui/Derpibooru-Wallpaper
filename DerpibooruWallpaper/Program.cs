using System.Globalization;

namespace DerpibooruWallpaper
{
    internal static class Program
    {
        static bool CreatedNew = false;
        static readonly Mutex M = new(false, nameof(DerpibooruWallpaper), out CreatedNew);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            bool LogOnStart = args.Any((str) => { return str.Equals("LogOnStart", StringComparison.InvariantCultureIgnoreCase); });
            if (!CreatedNew)
            {
                if (LogOnStart) _ = new OurException("another instance already exists, exiting current instance...");
                return;
            }
            if (LogOnStart) _ = new OurException("program starting");

            string exePath = Application.ExecutablePath;
            Environment.CurrentDirectory = exePath.Remove(exePath.LastIndexOf('\\')) + '\\';

            string UA = API.UserAgent;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            Tray.Show();
            using System.Threading.Timer timer = new(Tick, null, 0, 1000 * 60);

            if (LogOnStart) _ = new OurException("program running");

            Application.Run();

            Tray.Hide();
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            M.WaitOne();
            M.Dispose();
        }

        static void Tick(object? _)
        {
            M.WaitOne();
            try
            {
                Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
                DateTimeFormatInfo Info = new();
                Info.SetAllDateTimePatterns(new string[] { "yyyy/MM/dd HH:mm:ss" }, 'T');
                string LastUpdateString = Settings.LastUpdate;
                int? Day = Settings.UpdateIntervalDay;
                int? Hour = Settings.UpdateIntervalHour;
                int? Minute = Settings.UpdateIntervalMinute;
                if (LastUpdateString.Length > 0)
                {
                    DateTime LastTime = DateTime.Parse(Settings.LastUpdate, Info);
                    DateTime Now = DateTime.Now;
                    if (Minute is not null && (Now - LastTime).Minutes >= Minute)
                    {
                        if (API.ChangingWallpaperMutex.WaitOne(0))
                        {
                            API.ChangeWallpaper(true);
                        }
                    }
                    else if (Hour is not null && (Now - LastTime).Hours >= Hour)
                    {
                        if (API.ChangingWallpaperMutex.WaitOne(0))
                        {
                            API.ChangeWallpaper(true);
                        }
                    }
                    else if (Day is not null && (Now.Date - LastTime.Date).Days >= Day)
                    {
                        if (API.ChangingWallpaperMutex.WaitOne(0))
                        {
                            API.ChangeWallpaper(true);
                        }
                    }
                }
                else if (Minute is not null || Hour is not null || Day is not null)
                {
                    if (API.ChangingWallpaperMutex.WaitOne(0))
                    {
                        API.ChangeWallpaper(true);
                    }
                }
            }
            catch (Exception e) { _ = new OurException("external exception", e); }
            GC.Collect();
            M.ReleaseMutex();
        }
    }
}