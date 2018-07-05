using System.Globalization;
using NLog;

namespace PayoneerLogger
{
    public static class LogManager
    {
        public static IPayoneerLogger GetLogger<T>()
        {
            var n = typeof(T).Name;
            return GetLogger(n);
        }

        public static IPayoneerLogger GetLogger(string name)
        {
            ILogger l = NLog.LogManager.GetLogger(name);
            return new PayoneerNLogImpl(l);
        }




        class PayoneerNLogImpl : IPayoneerLogger
        {
            private readonly ILogger nlogLogger;

            public PayoneerNLogImpl(ILogger nlogLogger)
            {
                this.nlogLogger = nlogLogger;
            }

            public bool IsDebugEnabled => nlogLogger.IsDebugEnabled;

            public bool IsErrorEnabled => nlogLogger.IsErrorEnabled;

            public bool IsFatalEnabled => nlogLogger.IsFatalEnabled;

            public bool IsInfoEnabled => nlogLogger.IsInfoEnabled;

            public bool IsTraceEnabled => nlogLogger.IsTraceEnabled;

            public bool IsWarnEnabled => nlogLogger.IsWarnEnabled;


            public void Trace(string message)
            {
                nlogLogger.Trace(CultureInfo.CurrentCulture, message);
            }

            public void Debug(string message)
            {
                nlogLogger.Debug(CultureInfo.CurrentCulture, message);
            }

            public void Info(string message)
            {
                nlogLogger.Info(CultureInfo.CurrentCulture, message);
            }

            public void Warn(string message)
            {
                nlogLogger.Warn(CultureInfo.CurrentCulture, message);
            }

            public void Error(string message)
            {
                nlogLogger.Error(CultureInfo.CurrentCulture, message);
            }

            public void Fatal(string message)
            {
                nlogLogger.Fatal(CultureInfo.CurrentCulture, message);
            }
        }
    }
}