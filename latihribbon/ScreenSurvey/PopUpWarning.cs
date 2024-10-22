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
    public partial class PopUpWarning : Form
    {
        public PopUpWarning()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.Load += LoadForm;
            this.Text = "";
        }
        private async void LoadForm(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            for (double opacity = 1; opacity >= 0; opacity -= 0.2)
            {
                this.Opacity = opacity;
                await Task.Delay(1);
            }
            this.Close();
        }

    }
}
