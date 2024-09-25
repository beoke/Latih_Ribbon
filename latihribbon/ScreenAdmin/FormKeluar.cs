﻿using Dapper;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace latihribbon
{
    public partial class FormKeluar : Form
    {
        private readonly SiswaDal siswaDal;
        private readonly KeluarDal keluarDal;
        private readonly MesBox mesBox = new MesBox();
        int globalId = 0;
        public FormKeluar()
        {
            InitializeComponent();
            siswaDal = new SiswaDal();
            keluarDal = new KeluarDal();
            LoadData();
            InitComponent();
        }

        public void InitComponent()
        {

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

            //TextBox
            txtNIS1.MaxLength = 9;
            txtTujuan1.MaxLength = 60;
        }

        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string nis, nama, kelas;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tgl1 = tglsatu.Value.Date;
            tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL(nis, nama, kelas);
            var dp = new DynamicParameters();
            dp.Add("@NIS", nis);
            dp.Add("@Nama", nama);
            dp.Add("@Kelas", kelas);
            dp.Add("@tgl1", tgl1);
            dp.Add("@tgl2", tgl2);

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = keluarDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = keluarDal.ListData(sqlc, dp);


        }

        string tglchange = string.Empty;
        private string FilterSQL(string nis, string nama, string kelas)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (nis != "") fltr.Add("l.NIS LIKE @NIS+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE '%'+@Nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE '%'+@Kelas+'%'");
            if (tglchange != "") fltr.Add("k.Tanggal BETWEEN @tgl1 AND @tgl2");
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);
            return sqlc;
        }

        private void GetData()
        {
            var Id = dataGridView1.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            globalId = Id==string.Empty ? 0 : int.Parse(Id); // set global varibel
            var data = keluarDal.GetData(Convert.ToInt32(Id));
            if (data == null) return;
            txtNIS1.Text = data.Nis.ToString();
            txtNama1.Text = data.Nama;
            txtKelas1.Text = data.Kelas;
            tglDT.Value = data.Tanggal;
            jamKeluarDT.Value = DateTime.Today.Add(data.JamKeluar);
            jamMasukDT.Value = DateTime.Today.Add(data.JamMasuk);
            txtTujuan1.Text = data.Tujuan;
        }

        private void ClearInput()
        {
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = new DateTime(2000,01,01);
            jamKeluarDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            jamMasukDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            txtTujuan1.Clear();
        }

        private void SaveData()
        {
            string nis, nama, kelas, tujuan;
            DateTime tgl;
            TimeSpan jamKeluar, jamMasuk;
            nis = txtNIS1.Text;
            nama = txtNama1.Text;
            kelas = txtKelas1.Text;
            tujuan = txtTujuan1.Text;
            tgl = tglDT.Value;
            jamKeluar = jamKeluarDT.Value.TimeOfDay;
            jamMasuk = jamMasukDT.Value.TimeOfDay;

            if (nis == "" || nama == "" || tujuan == "" || jamMasuk == TimeSpan.Zero || jamKeluar == TimeSpan.Zero) 
            {
                MessageBox.Show("Seluruh Data Wajib Diisi!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (jamMasuk == jamKeluar)
            {
                mesBox.MesInfo("Jam Keluar & Jam Masuk Tidak Valid!");
                return;
            }

            var keluar = new KeluarModel
            {
                Id = globalId,
                Nis = Convert.ToInt32(nis),
                Nama = nama,
                Kelas = kelas,
                Tanggal= tgl,
                JamKeluar = jamKeluar,
                JamMasuk = jamMasuk,
                Tujuan = tujuan
            };

            if(keluar.Id == 0)
            {
                if (MessageBox.Show("Input Data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    keluarDal.Insert(keluar);
                    LoadData();
                }

            }
            else
            {
                if (MessageBox.Show("Update Data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    keluarDal.Update(keluar);
                    LoadData();
                }
            }
        }

        private void ControlInsertUpdate()
        {
            if(globalId == 0)
            {
                lblInfo.Text = "INSERT";
                txtNIS1.ReadOnly = false;
            }
            else
            {
                lblInfo.Text = "UPDATE";
                txtNIS1.ReadOnly = true;
            }
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
            Page = 1;
            LoadData();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = "0";
            LoadData();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = "0";
            LoadData();
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();
            globalId = 0;
            ControlInsertUpdate();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            GetData();
            ControlInsertUpdate();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = string.Empty;
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Page < totalPage)
            {
                Page++;
                LoadData();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Page > totalPage)
            {
                Page--;
                LoadData();
            }
        }
    }
}
