using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class MesClose : Form
    {
        public MesClose(string message, int row = 1)
        {
            InitializeComponent();
            KeyPreview = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            lblMessage.Text = message;
            int width = lblMessage.Width + 114;
            this.Width = width;
            if (this.Width < 246) this.Width = 246;
            if (row == 1)
                lblMessage.Location = new Point(lblMessage.Location.X, 41);
            else
                lblMessage.Location = new Point(lblMessage.Location.X, 32);

            this.KeyDown += MesClose_KeyDown;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MesClose_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                Application.Exit();
            }   
        }
    }
}
