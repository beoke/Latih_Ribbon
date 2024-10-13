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
    public partial class FormNaikKelas : Form
    {
        public FormNaikKelas()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
           if (MessageBox.Show("Apakah Anda yakin ingin melanjutkan proses kenaikan kelas?", "Konfirmasi", MessageBoxButtons.YesNo , MessageBoxIcon.Question)== DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
           else
            {
                DialogResult= DialogResult.Cancel;
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close ();
        }
    }
}
