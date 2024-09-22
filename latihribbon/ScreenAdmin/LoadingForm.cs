using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    public partial class LoadingForm : Form
    {
        private Timer timer;  // Deklarasi Timer
        private int angle = 0;
        public LoadingForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 100; // Kecepatan animasi (dalam milidetik)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            angle += 10; // Ubah sudut (semakin besar nilai, semakin cepat animasinya)
            if (angle >= 360) angle = 0;
            this.Invalidate(); // Merefresh form untuk menggambar ulang
        }

        // Override fungsi OnPaint untuk menggambar animasi
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Tentukan pusat dan ukuran lingkaran
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            int radius = 30;

            // Gambar lingkaran berputar
            for (int i = 0; i < 12; i++)
            {
                int alpha = (i + (angle / 30)) % 12 * 20;
                using (Brush brush = new SolidBrush(Color.FromArgb(alpha, Color.Blue)))
                {
                    double theta = (i * 30) * Math.PI / 180;
                    int x = (int)(centerX + Math.Cos(theta) * radius);
                    int y = (int)(centerY + Math.Sin(theta) * radius);
                    g.FillEllipse(brush, x - 5, y - 5, 10, 10); // Gambar lingkaran kecil
                }
            }
        }
    }
}
