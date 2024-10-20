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
    public partial class MesWarning : Form
    {
        public MesWarning(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
        }


        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
