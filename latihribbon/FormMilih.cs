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
    public partial class FormMilih : Form
    {
        public Label Label3 { get; private set; }
        public Label Label4 { get; private set; }
        public FormMilih()
        {
            InitializeComponent();

        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            SuratMasuk masuk = new SuratMasuk();
            masuk.Show();
            this.Hide();
        }

        private void btn_Keluar_Click(object sender, EventArgs e)
        {
            SuratKeluarcs keluar = new SuratKeluarcs();
            keluar.Show();
            this.Hide();
        }
    }
}
