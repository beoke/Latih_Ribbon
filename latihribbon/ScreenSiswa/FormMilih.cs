using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormMilih : Form
    {
        private string NIS;
        private string nama;
        private string kelas;                                               
        private Form mainForm;
        private Form indexForm;
        public FormMilih(Form mainForm,Form indexForm,string NIS,string nama,string kelas)
        {
            InitializeComponent();

            this.NIS = NIS;
            this.nama = nama;
            this.kelas = kelas;
            txtNIS.Text += " " + NIS;
            txtNama.Text += " " + nama;
            txtKelas.Text += " " + kelas;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true;  
            this.ControlBox = true;    
            this.mainForm = mainForm;
            this.indexForm = indexForm;
            this.Resize += FormMilih_Resize;

            btn_kembali.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btn_kembali.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btn_kembali.Enter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.Leave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
            btn_kembali.MouseEnter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.MouseLeave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
        }

        private void FormMilih_Resize(object sender, EventArgs e)
        {
            int parentWidth = this.Width;
            int btnWidth = (parentWidth - (btn_masuk.Width * 2)) / 3;
            panel3.Location = new Point((parentWidth-panel3.Width)/2,panel3.Location.Y);
            btn_masuk.Location = new Point(btnWidth,btn_masuk.Location.Y);
            btn_Keluar.Location = new Point(parentWidth-(btnWidth+btn_Keluar.Width),btn_Keluar.Location.Y);
        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            SuratMasuk suratMasuk = new SuratMasuk(mainForm,indexForm, NIS,nama,kelas);
            suratMasuk.Show();
            this.Close();
        }

        private void btn_Keluar_Click(object sender, EventArgs e)
        {
            SuratKeluarcs Keluar = new SuratKeluarcs(mainForm,indexForm, NIS,nama,kelas);
            Keluar.Show();
            this.Close();
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            Pemakai p = new Pemakai(mainForm,indexForm);
            p.Show();
            this.Close();
        }
    }
}
