using DocumentFormat.OpenXml.Drawing;
using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();


        [STAThread]
        static void Main()
        {

            SetProcessDPIAware();

            using (Graphics graphic = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpi_x = graphic.DpiX;

                if (dpi_x > 96)
                {
                    DialogResult result = MessageBox.Show(
                        "Skala tampilan layar Anda lebih dari 150%. " +
                        "Aplikasi tidak akan berjalan dalam kondisi ini. \nApakah Anda ingin membuka pengaturan tampilan?",
                        "Peringatan Skala Layar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            Process.Start("ms-settings:display");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Gagal membuka pengaturan skala layar. Silakan buka pengaturan secara manual.",
                                        "Peringatan",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        }
                    }
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FirstForm());
        }
    }
}
