using latihribbon.Dal;
using latihribbon.Helper;
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
        private readonly MesBox mesBox;

        private Form mainForm;
        public Pemakai(Form mainForm)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ControlBox = true;
            this.KeyPreview = true;
            this.mainForm = mainForm;
            _dbDal = new DbDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            mesBox = new MesBox();
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
                mesBox.MesInfo("Harap masukkan angka yang valid untuk NIS!");
                tx_NIS.Text = "";
                return;
            }

            // Cari NIS di tabel siswa 
            var siswa = siswaDal.GetData(nis);
            if (siswa == null)
            {
                MessageBox.Show("NIS tidak ditemukan.", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tx_NIS.Text = "";
            }
            else
            {
                DialogResult result = MessageBox.Show($"NIS: {siswa.Nis} Dengan Nama: {siswa.Nama}", "Data ditemukan", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.No)
                {
                    ResetForm();
                    return;
                }
                string NIS, nama, kelas;
                NIS = siswa.Nis.ToString();
                nama = siswa.Nama;
                kelas = kelasDal.GetData(siswa.IdKelas).NamaKelas ?? string.Empty;
                FormMilih formMilih = new FormMilih(mainForm,NIS,nama,kelas);
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

        private void Pemakai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                mainForm.Show();
                this.Close();

                // Keluar dari aplikasi saat kombinasi tombol Ctrl + Alt + K ditekan
                //Application.Exit();
            }
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            mainForm.Opacity = 1;
            this.Close();
        }
    }
}
