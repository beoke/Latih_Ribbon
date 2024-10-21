using Dapper;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    
    public partial class FormTerlambat : Form
    {
        private readonly MasukDal masukDal;
        private readonly SiswaDal siswaDal;
        private readonly KelasDal kelasDal;
        
        private readonly MesBox mesBox;
        int globalId = 0;
        public FormTerlambat()
        {
            InitializeComponent();
            buf();
            masukDal = new MasukDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            mesBox = new MesBox();
            InitCombo();
            RegisterEvent();
            LoadData();
            InitComponen();
        }
        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            dataGridView1,
            new object[] { true });
        }
        public void InitComponen()
        {
            //textBox MaxLength
            txtNIS1.MaxLength = 10;
            txtAlasan1.MaxLength = 60;
            
            // DataGrid
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.Columns["NamaKelas"].HeaderText = "Nama Kelas";
            dataGridView1.Columns["JamMasuk"].HeaderText = "Jam Masuk";

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 350;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 110;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 300;
        }

        private void InitCombo()
        {
            comboPerPage.Items.Add(10);
            comboPerPage.Items.Add(20);
            comboPerPage.Items.Add(50);
            comboPerPage.Items.Add(100);
            comboPerPage.Items.Add(200);
            comboPerPage.SelectedIndex = 0;
        }

        bool tglchange = false;
        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string search = txtFilter.Text;
            DateTime tgl1 = tglsatu.Value.Date;
            DateTime tgl2 = tgldua.Value.Date;

            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            var dp = new DynamicParameters();

            if (search != "") 
            {
                dp.Add("@Search", search);
                if (search != "") fltr.Add("m.NIS LIKE @Search+'%' OR s.Nama LIKE '%'+@Search+'%' OR kls.NamaKelas LIKE '%'+@Search+'%'");
            }
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
                fltr.Add("m.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ",fltr);
            string text = "Halaman ";
            int RowPerPage = (int)comboPerPage.SelectedItem;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = masukDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = masukDal.ListData(sqlc, dp)
                .Select(x => new
                {
                    Id = x.Id,
                    NIS = x.NIS,
                    Nama = x.Nama,
                    NamaKelas = x.NamaKelas,
                    Tanggal = x.Tanggal,
                    JamMasuk = x.JamMasuk.ToString(@"hh\:mm"),
                    AlasanTerlambat = x.Alasan 
                }).ToList();
        }


        private void GetData()
        {
            var Id = dataGridView1.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            globalId = Id == string.Empty ? 0 : int.Parse(Id);
            var data = masukDal.GetData(Convert.ToInt32(Id));
            if (data == null) return;
            txtNIS1.Text = data.NIS.ToString();
            txtNama1.Text = data.Nama;
            txtKelas1.Text = data.NamaKelas;
            tglDT.Value = data.Tanggal;
            jamMasukDT.Value = DateTime.Today.Add(data.JamMasuk);
            txtAlasan1.Text = data.Alasan;
            ControlInsertUpdate();
        }

        private void ClearInput()
        {
            txtNIS1.ReadOnly = false;
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = DateTime.Now;
            jamMasukDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            txtAlasan1.Clear();
        }
        private void ControlInsertUpdate()
        {
            if (globalId == 0)
                lblInfo.Text = "INSERT";
            else
                lblInfo.Text = "UPDATE";
        }
        private void SaveData()
        {
            string nis = txtNIS1.Text;
            string nama = txtNama1.Text.Trim();
            string alasan = txtAlasan1.Text.Trim();
            DateTime tgl = tglDT.Value;
            TimeSpan jamMasuk = jamMasukDT.Value.TimeOfDay;

            if (nis == "" || nama == "" || alasan == "")
            {
                mesBox.MesInfo("Seluruh Data Wajib Diisi!");
                return;
            }

            var masuk = new MasukModel
            {
                Id = globalId,
                NIS = Convert.ToInt32(nis),
                Tanggal = tgl,
                JamMasuk = jamMasuk,
                Alasan = alasan
            };

            if (masuk.Id == 0)
            {
                if (!mesBox.MesKonfirmasi("Input Data?")) return;
                masukDal.Insert(masuk);
                LoadData();
                globalId = 0;
                ClearInput();
                ControlInsertUpdate();
            }
            else
            {
                if (!mesBox.MesKonfirmasi("Update Data?")) return;
                masukDal.Update(masuk);
                LoadData();
            }
        }

        private void Delete()
        {
            if(globalId == 0)
            {
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (!mesBox.MesKonfirmasi("Hapus Data?")) return;
            masukDal.Delete(globalId);
            LoadData();
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
                txtKelas1.Text = siswa.NamaKelas;
            }
        }

        #region EVENT
        private void RegisterEvent()
        {
            txtFilter.TextChanged += filter_TextChanged;
            tglsatu.ValueChanged += filter_tglChanged;
            tgldua.ValueChanged += filter_tglChanged;
            comboPerPage.SelectedIndexChanged += filter_TextChanged;
            txtFilter.Enter += TxtFilter_Enter;
            txtFilter.Leave += TxtFilter_Leave;
            lblFilter.Click += LblFilter_Click;
            this.Resize += FormTerlambat_Resize;
        }
        private void LblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }

        private void TxtFilter_Leave(object sender, EventArgs e)
        {
            lblFilter.Visible = true;
        }

        private void TxtFilter_Enter(object sender, EventArgs e)
        {
            lblFilter.Visible = false;
        }
        private void FormTerlambat_Resize(object sender, EventArgs e)
        {
            if (panel4.Height < 458)
            {
                tglDT.Width = 140;
                jamMasukDT.Width = 140;

                lblJamMasuk.Location = new Point(177, 192);
                jamMasukDT.Location = new Point(175, 213);
                lblAlasan.Location = new Point(17, 241);
                txtAlasan1.Location = new Point(15, 262);
            }
            else
            {
                tglDT.Width = 300;
                jamMasukDT.Width = 300;

                lblJamMasuk.Location = new Point(17, 240);
                jamMasukDT.Location = new Point(15, 261);
                lblAlasan.Location = new Point(17, 290);
                txtAlasan1.Location = new Point(15, 311);
            }
                
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }
        private void filter_tglChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();
            globalId = 0;
            ControlInsertUpdate();
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
            ControlInsertUpdate();
            txtNIS1.ReadOnly = true;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Clear();
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }
        #endregion

    }
}
