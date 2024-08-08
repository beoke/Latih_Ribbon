﻿using System;
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
        public static string NIS; //hooh yoga
        public static string nama;
        public static string kelas;
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
                // Munculkan pesan bahwa data ditemukan dengan pilihan Yes dan No
                DialogResult result = MessageBox.Show($"NIS: {siswa.Nis} Dengan Nama: {siswa.Nama}", "Data ditemukan", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // Jika pengguna memilih No, reset form dan kembalikan fokus ke input NIS
                if (result == DialogResult.No)
                {
                    ResetForm();
                    return;
                }

                // Jika pengguna memilih Yes, lanjutkan ke FormMilih
                FormMilih formMilih = new FormMilih(this);

                // Atur nilai label dengan data siswa di FormMilih
                formMilih.LabelNis.Text = "NIS: " + siswa.Nis.ToString();
                formMilih.LabelNama.Text = "Nama: " + siswa.Nama;
                formMilih.LabelKelas.Text = "Kelas: " + siswa.Kelas;

                //yoga iki
                NIS = siswa.Nis.ToString();
                nama = siswa.Nama;
                kelas = siswa.Kelas;

                // Tampilkan FormMilih dan sembunyikan form ini
                formMilih.Show();
                this.Hide();
            }
        }
        private void ResetForm()
        {
            // Mengosongkan TextBox dan mengembalikan fokus ke TextBox NIS
            tx_NIS.Clear();
            tx_NIS.Focus();
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

