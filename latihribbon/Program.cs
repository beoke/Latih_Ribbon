using latihribbon.Dal;
using System;
using System.Threading;
using System.Windows.Forms;

namespace latihribbon
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Register global unhandled exception handlers
            Application.ThreadException += (sender, args) =>
            {
                AppLogger.LogError(args.Exception, "Application.ThreadException");
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                {
                    AppLogger.LogError(ex, "AppDomain.UnhandledException");
                }
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Run database migration to ensure metadata columns and audit log tables exist
            DbMigrationDal.EnsureDatabaseUpToDate();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Verifikasi dan selaraskan kolom tabel database saat pertama kali aplikasi dibuka
            latihribbon.Conn.conn.EnsureTableColumns();

            Application.Run(new FirstForm());
        }
    }
}

