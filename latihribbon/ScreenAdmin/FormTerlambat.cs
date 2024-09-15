using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
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
    public partial class FormTerlambat : Form
    {
        private  MasukDal masukDal;
        private SiswaDal siswaDal;
        int globalId = 0;
        public FormTerlambat()
        {
            InitializeComponent();
            masukDal = new MasukDal();
            siswaDal = new SiswaDal();
            LoadData();
            InitComponen();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = masukDal.ListData();
        }

        public void InitComponen()
        {
            //textBox MaxLength
            txtNIS1.MaxLength = 10;
            txtAlasan1.MaxLength = 60;
            
            // DataGrid
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersHeight = 35;
            }
        }
        private void btn_terlambat_Click(object sender, EventArgs e)
        {
            PrintTerlambat telat = new PrintTerlambat();
            telat.Show();
        }

        string sqlglobal=string.Empty;
        public string Filter(string nis, string nama, string kelas, DateTime tgl1, DateTime tgl2)
        {
            List<string> fltr = new List<string>();
            string sql = @"SELECT m.id, m.NIS, s.Nama, s.Kelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m INNER JOIN siswa s ON m.NIS = s.Nis";

            if (nis != "") fltr.Add("m.NIS LIKE @nis+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE @nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE @kelas+'%'");
            if (sqlglobal != "") fltr.Add("m.Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", fltr);
            }
            sqlglobal = "";
            return sql;

        }

        public void Filter2()
        {
            string nis, nama, kelas;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;

            tgl1 = tglsatu.Value;
            tgl2 = tgldua.Value;


            string sql = Filter(nis, nama, kelas,  tgl1, tgl2);
            var select = masukDal.GetTerlambatFilter(sql, new { nis = nis, nama = nama, kelas = kelas, tgl1 = tgl1, tgl2 = tgl2 });
            dataGridView1.DataSource = select;
        }

        private void GetData()
        {
            var Id = dataGridView1.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            globalId = Id == string.Empty ? 0 : int.Parse(Id); // set global varibel
            var data = masukDal.GetData(Convert.ToInt32(Id));
            if (data == null) return;
            txtNIS1.Text = data.NIS.ToString();
            txtNama1.Text = data.Nama;
            txtKelas1.Text = data.Kelas;
            tglDT.Value = data.Tanggal;
            jamMasukDT.Value = DateTime.Today.Add(data.JamMasuk);
            txtAlasan1.Text = data.Alasan;
        }

        private void ClearInput()
        {
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = new DateTime(2000, 01, 01);
            jamMasukDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            txtAlasan1.Clear();
        }

        private void SaveData()
        {
            string nis, nama, kelas, alasan;
            DateTime tgl;
            TimeSpan jamMasuk;
            nis = txtNIS1.Text;
            nama = txtNama1.Text;
            kelas = txtKelas1.Text;
            alasan = txtAlasan1.Text;
            tgl = tglDT.Value;
            jamMasuk = jamMasukDT.Value.TimeOfDay;

            if (nis == "" || nama == "" || alasan == "")
            {
                MessageBox.Show("Seluruh Data Wajib Diisi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var masuk = new MasukModel
            {
                Id = globalId,
                NIS = Convert.ToInt32(nis),
                Nama = nama,
                Kelas = kelas,
                Tanggal = tgl,
                JamMasuk = jamMasuk,
                Alasan = alasan
            };

            if (masuk.Id == 0)
            {
                if (MessageBox.Show("Input Data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    masukDal.Insert(masuk);
                    LoadData();
                }

            }
            else
            {
                if (MessageBox.Show("Update Data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    masukDal.Update(masuk);
                    LoadData();
                }
            }

        }

        private void Delete()
        {
         
        }

        private void CekNis()
        {
            var siswa = siswaDal.GetData(Convert.ToInt32(txtNIS1.Text));
            if (siswa == null)
            {
                lblNisTidakDitemukan.Visible = true;
                txtNama1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
            }
            else
            {
                lblNisTidakDitemukan.Visible = false;
                txtNama1.Text = siswa.Nama;
                txtKelas1.Text = siswa.Kelas;
            }
        }

        #region EVENT FILTER
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            sqlglobal = "0";
            Filter2();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            sqlglobal = "0";
            Filter2();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();
            globalId = 0;
            lblInfo.Text = "INSERT";
            txtNIS1.ReadOnly = false;

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           
           
        }

        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void txtNIS1_TextChanged(object sender, EventArgs e)
        {
            if (txtNIS1.Text.Length >= 5)
            {
                CekNis();
            }
            else
            {
                txtNama1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
                lblNisTidakDitemukan.Visible = false;
            }
        }

        private void txtNIS1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnDelete_FormSiswa_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            GetData();
            lblInfo.Text = "UPDATE";
            txtNIS1.ReadOnly = true;
        }
    }
}
