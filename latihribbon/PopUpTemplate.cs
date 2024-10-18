using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class PopUpTemplate : Form
    {
        public PopUpTemplate(string text, Image icon = null)
        {
            InitializeComponent();
            label1.Text = text;
            if (icon != null)
            {
                pictureBox1.Image = icon;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                // Sembunyikan PictureBox jika tidak ada gambar
                pictureBox1.Visible = false;
            }
            ResizeForm();
        }

        private void ResizeForm()
        {
            var textSize = TextRenderer.MeasureText(label1.Text, label1.Font);
            int padding = 20;
            label1.Width = textSize.Width + padding;
            label1.Height = textSize.Height + padding;

            this.Width = label1.Width + pictureBox1.Width + padding * 2;
            this.Height = label1.Height + btnYes.Height + padding * 3;

            btnYes.Location = new Point((this.Width - btnYes.Width) / 2, label1.Bottom + padding);
            if (pictureBox1.Visible)
            {
                pictureBox1.Location = new Point(padding, padding);
                label1.Location = new Point(pictureBox1.Right + padding, padding);
            }
            else
            {
                label1.Location = new Point(padding, padding);
            }
        }
    }
}
