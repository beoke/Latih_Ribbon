﻿using latihribbon.Dal;
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

namespace latihribbon.ScreenAdmin
{
    public partial class FormJurusan : Form
    {
        private readonly JurusanDal _jurusanDal;
        private readonly KelasDal kelasDal;
        private string NamaJurusanGlobal;
        public FormJurusan()
        {
            InitializeComponent();
            buf();
            _jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            LoadData();
            InitEvent();
            this.Load += FormJurusan_Load;
        }

        private void FormJurusan_Load(object sender, EventArgs e)
        {
            GridListJurusan.Focus();
        }

        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            GridListJurusan,
            new object[] { true });
        }
        private void LoadData()
        {
            GridListJurusan.DataSource = _jurusanDal.ListData().Select((x,index) => new 
            {
                IdJurusan = x.Id,
                No = index+1,
                NamaJurusan = x.NamaJurusan,
                Kode = x.Kode
            }).ToList();

            GridListJurusan.Columns[0].Visible = false;
            GridListJurusan.Columns[1].Width = 60;
            GridListJurusan.Columns[2].Width = 150;
            GridListJurusan.Columns[2].Width = 200;
            GridListJurusan.Columns["NamaJurusan"].HeaderText = "Nama Jurusan";

            GridListJurusan.EnableHeadersVisualStyles = false;
            GridListJurusan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            GridListJurusan.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            GridListJurusan.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            GridListJurusan.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            GridListJurusan.RowTemplate.Height = 30;
            GridListJurusan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GridListJurusan.ColumnHeadersHeight = 35;
        }

        private void InitEvent()
        {
            btnSaveJurusan.Click += BtnSaveJurusan_Click;
            GridListJurusan.CellMouseClick += DataGridView1_CellMouseClick;
            EditMenuStrip.Click += EditMenuStrip_Click;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;
        }

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            var jurusanKode = GridListJurusan.CurrentRow.Cells["Kode"].Value;
            if (new MesWarningYN($"Anda yakin ingin menghapus data \"{jurusanKode}\" ? \n Jika Dihapus, maka data yang terhubung akan ikut Terhapus", 2).ShowDialog() != DialogResult.Yes) return;

            var id = GridListJurusan.CurrentRow.Cells[0].Value;

            _jurusanDal.Delete(Convert.ToInt32(id));
            LoadData();
        }

        private void EditMenuStrip_Click(object sender, EventArgs e)
        {
            int jurusanId = Convert.ToInt32(GridListJurusan.CurrentRow.Cells[0].Value);
            string kodeJurusan = GridListJurusan.CurrentRow.Cells["Kode"].Value?.ToString() ?? string.Empty;
            if (new EditJurusan(jurusanId,kodeJurusan).ShowDialog() == DialogResult.Yes)
                LoadData();
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                GridListJurusan.ClearSelection();
                GridListJurusan.CurrentCell = GridListJurusan[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

                    
        private void BtnSaveJurusan_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            var namaJurusan = txtNamaJurusan.Text.Trim();
            var kode = txtKode.Text.Trim();
            if(kode == string.Empty || namaJurusan == string.Empty)
            {
                new MesWarningOK("Seluruh data wajib di isi!").ShowDialog();
                return;
            }
            if(_jurusanDal.GetIdJurusan(kode, namaJurusan) != 0)
            {
                new MesError($"Jurusan {namaJurusan} Sudah Tersedia.").ShowDialog(this);
                return;
            }
            if (new MesQuestionYN("Input Data?").ShowDialog() != DialogResult.Yes) return;
            _jurusanDal.Insert(kode , namaJurusan);
            LoadData();
            Cleardata();
        }

        private void Cleardata()
        {
            txtNamaJurusan.Text= string.Empty;
        }

        private void txtIdJurusan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}





