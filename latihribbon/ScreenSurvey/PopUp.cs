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
    public partial class PopUp : Form
    {
        public PopUp()
        {
            InitializeComponent();

            PictureGif.Image = Properties.Resources.verify;
            PictureGif.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private async void PuasPupUp_Load(object sender, EventArgs e)
        {
            await Task.Delay(2000);

            for (double i = 1; i >= 0; i -= 0.2)
            {
                this.Opacity = i;
                await Task.Delay(2);
            }
            this.Close();

        }
    }
}