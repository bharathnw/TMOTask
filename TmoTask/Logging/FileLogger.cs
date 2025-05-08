namespace TmoTask.Logging
{
    public class FileLogger:ILogger
    {
        private readonly string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
            var directory = Path.GetDirectoryName(_logFilePath);
            if (directory == null) { 
                throw new ArgumentNullException(nameof(directory));
            }
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                return logLevel >= LogLevel.Debug;
            }
            return logLevel >= LogLevel.Information;
        }



        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var logMessage = formatter(state, exception);

            if (string.IsNullOrEmpty(logMessage)) return;

            var logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} || {logLevel} || {logMessage}";

            if (exception != null)
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    logLine += $"\nException: {exception.Message}\n{exception.StackTrace}";
                }
                else
                {
                    logLine += $"\nException: Something Went Wrong!";
                }
            }
            File.AppendAllText(_logFilePath, logLine + "\n");
        }
    }
}
