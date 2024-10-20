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
    public partial class MesKonfirmasi : Form
    {
        public MesKonfirmasi(string message,int row = 1)
        {
            InitializeComponent();
            lblMessage.Text = message;
            int width = lblMessage.Width + 102;
            this.Width = width;
            if (row == 1)
                lblMessage.Location = new Point(lblMessage.Location.X, 41);
            else
                lblMessage.Location = new Point(lblMessage.Location.X , 32);
        }
    }
}
