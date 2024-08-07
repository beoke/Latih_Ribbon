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
        public Pemakai()
        {
            InitializeComponent();
            _dbDal = new DbDal();
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            // Validasi input untuk memastikan hanya angka yang bisa dimasukkan
            int nis;
            if (!int.TryParse(tx_NIS.Text, out nis))
            {
                MessageBox.Show("Harap masukkan angka yang valid untuk NIS.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Cari NIS di tabel siswa
            var siswa = _dbDal.GetSiswaByNis(nis);
            if (siswa == null)
            {
                MessageBox.Show("NIS tidak ditemukan di tabel siswa.", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Buat instance dari FormMilih
                FormMilih formMilih = new FormMilih();

                // Atur nilai label dengan data siswa
              /*  formMilih.Label3.Text = "NIS: " + siswa.Nis.ToString();
                formMilih.Label4.Text = "Nama: " + siswa.Nama;*/

                // Tampilkan FormMilih
                formMilih.Show();
                this.Hide();
            }
        }

        private void tx_NIS_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Memastikan hanya angka yang bisa diinputkan
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

