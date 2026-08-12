using System;
using System.IO;
using System.Text;

namespace latihribbon
{
    public static class AppLogger
    {
        private static readonly object _lockObj = new object();

        public static string GetLogsDirectory()
        {
            try
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string logDir = Path.Combine(localAppData, "SIM RESI", "Logs");
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
                return logDir;
            }
            catch
            {
                string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(baseDir))
                {
                    Directory.CreateDirectory(baseDir);
                }
                return baseDir;
            }
        }

        public static void LogError(Exception ex, string moduleOrForm = "General", string extraInfo = "")
        {
            try
            {
                string logDir = GetLogsDirectory();
                string fileName = $"{DateTime.Now:yyyy-MM-dd}.log";
                string filePath = Path.Combine(logDir, fileName);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"================================================================================");
                sb.AppendLine($"[TIMESTAMP] : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"[USER]      : {UserSession.CurrentUser ?? "System"}");
                sb.AppendLine($"[MODULE]    : {moduleOrForm}");
                if (!string.IsNullOrWhiteSpace(extraInfo))
                {
                    sb.AppendLine($"[INFO]      : {extraInfo}");
                }
                if (ex != null)
                {
                    sb.AppendLine($"[EX TYPE]   : {ex.GetType().FullName}");
                    sb.AppendLine($"[MESSAGE]   : {ex.Message}");
                    sb.AppendLine($"[STACKTRACE]:");
                    sb.AppendLine(ex.StackTrace ?? string.Empty);
                    if (ex.InnerException != null)
                    {
                        sb.AppendLine($"[INNER EX]  : {ex.InnerException.Message}");
                    }
                }
                sb.AppendLine();

                lock (_lockObj)
                {
                    File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                }
            }
            catch
            {
                // Fail-safe: Pastikan error logging tidak menyebabkan aplikasi crash
            }
        }
    }
}
