using latihribbon.Dal;
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
    public partial class Pemakai : Form
    {
        private DbDal _dbDal;
        private readonly SiswaDal siswaDal;
        private readonly KelasDal kelasDal;

        private Form mainForm;
        private Form indexForm;
        public Pemakai(Form mainForm, Form indexForm)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = true;
            this.ControlBox = true;
            this.mainForm = mainForm;
            this.indexForm = indexForm;
            _dbDal = new DbDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();

            btn_kembali.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn_kembali.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_kembali.Enter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.Leave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
            btn_kembali.MouseEnter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.MouseLeave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            ENTER();
        }

        public void ResetForm()
        {
            tx_NIS.Clear();
            tx_NIS.Focus();
        }

        private void tx_NIS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ENTER();
            }
        }

        public void ENTER()
        {
            int nis;
           
            if (!int.TryParse(tx_NIS.Text, out nis))
            {
                new MesWarningOK("Harap masukkan angka yang valid untuk NIS!").ShowDialog(this);
                tx_NIS.Text = "";
                return;
            }

            // Cari NIS di tabel siswa 
            var siswa = siswaDal.GetData(nis);
            if (siswa == null)
            {
                new MesWarningOK("NIS tidak ditemukan.").ShowDialog(this);
                tx_NIS.Text = "";
            }
            else
            {
                string NIS, nama, kelas;
                NIS = siswa.Nis.ToString();
                nama = siswa.Nama;
                kelas = kelasDal.GetData(siswa.IdKelas)?.NamaKelas ?? string.Empty;
                FormMilih formMilih = new FormMilih(mainForm,indexForm,NIS,nama,kelas);
                formMilih.Show();
                this.Close();
            } 
        }

        private void tx_NIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            indexForm.Opacity = 1;
            this.Close();
        }
    }
}
