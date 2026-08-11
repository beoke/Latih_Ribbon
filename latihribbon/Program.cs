using DocumentFormat.OpenXml.Drawing;
using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Verifikasi dan selaraskan kolom tabel database saat pertama kali aplikasi dibuka
            latihribbon.Conn.conn.EnsureTableColumns();

            Application.Run(new FirstForm());

        }
    }
}
