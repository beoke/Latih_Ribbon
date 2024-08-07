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
        public Label LabelNis { get; private set; }
        public Label LabelNama { get; private set; }
        public Label LabelKelas { get; private set; }
        public FormMilih()
        {
            InitializeComponent();
            InitializeLabels();
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
        private void InitializeLabels() // untuk menambahkan label yang konek dengan database
        {
            LabelNis = new Label
            {
                Location = new System.Drawing.Point(30, 30),
                Name = "LabelNis",
                Size = new System.Drawing.Size(200, 23),
                Text = "NIS: ",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            this.Controls.Add(LabelNis);

            LabelNama = new Label
            {
                Location = new System.Drawing.Point(30, 70),
                Name = "LabelNama",
                Size = new System.Drawing.Size(200, 23),
                Text = "Nama: ",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            this.Controls.Add(LabelNama);

            LabelKelas = new Label
            {
                Location = new System.Drawing.Point(30, 110),
                Name = "LabelKelas",
                Size = new System.Drawing.Size(200, 23),
                Text = "Kelas: ",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            this.Controls.Add(LabelKelas);
        }
    }
}
