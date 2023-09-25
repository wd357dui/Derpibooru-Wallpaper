using System.Diagnostics;

namespace DerpibooruWallpaper
{
    public class OurException : Exception
    {
        public OurException(string Message) : base(Message)
        {
            Caller = new StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "(method name unknown)";
            Log(this);
        }
        public OurException(string Message, Exception InnerException) : base(Message, InnerException)
        {
            Caller = new StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "(method name unknown)";
            Log(this);
        }
        protected OurException(string Message, string MethodName) : base(Message)
        {
            Caller = MethodName;
            Log(this);
        }
        protected OurException(string Message, Exception InnerException, string MethodName) : base(Message, InnerException)
        {
            Caller = MethodName;
            Log(this);
        }

        public static readonly Mutex LogMutex = new(false, nameof(DerpibooruWallpaper) + "." + nameof(Log));
        public static void Log(OurException exception)
        {
            LogMutex.WaitOne();
            using (StreamWriter writer = new("ErrorLog.log", true, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(DateTime.Now);
                writer.WriteLine(">>> method: " + exception.Caller + " <<<");
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    writer.WriteLine(">>> message <<<");
                    writer.WriteLine(exception.Message);
                }
                if (!string.IsNullOrEmpty(exception.ExtraInfo))
                {
                    writer.WriteLine(">>> extra info <<<");
                    writer.WriteLine(exception.ExtraInfo);
                }
                if (exception.InnerException is Exception inner)
                {
                    writer.WriteLine(">>> inner exception stack trace <<<");
                    writer.WriteLine(inner.StackTrace);
                    writer.WriteLine(">>> inner exception message <<<");
                    writer.WriteLine(inner.Message);
                }
                writer.WriteLine();
            }
            LogMutex.ReleaseMutex();
        }

        public string ExtraInfo = "";
        public string Caller = "";
    }
    public class APIException : OurException
    {
        public bool MessageBoxAlreadyShown = false;
        public APIException(string Message) : base(Message,
            new StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "(method name unknown)")
        { }
        public APIException(string Message, string Data) : base(Message,
            new StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "(method name unknown)")
        { ExtraInfo = Data; }
    }
}
