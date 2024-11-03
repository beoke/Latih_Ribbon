using latihribbon.Dal;
using latihribbon.Model;
using OfficeOpenXml.LoadFunctions.Params;
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
    
    public partial class EditKelas : Form
    {
        private readonly JurusanDal jurusanDal;
        private readonly KelasDal kelasDal;
        private string NamaKelasGlobal = string.Empty;
        public EditKelas(int IdKelas)
        {
            InitializeComponent();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitComponent();
            RegisterEvent();
            LoadData(IdKelas);
        }
        private void RegisterEvent()
        {
            btnSave.Click += BtnSave_Click;
            XRadio.CheckedChanged += Change_Value;
            XIRadio.CheckedChanged += Change_Value;
            XIIRadio.CheckedChanged += Change_Value;
            jurusanCombo.SelectedIndexChanged += Change_Value;
            txtRombel.TextChanged += Change_Value;
        }
        private void Change_Value(object sender, EventArgs e)
        {
            SetNamaKelas();
        }

        public void InitComponent()
        {
            var listJurusan = jurusanDal.ListData();
            if (!listJurusan.Any()) return;
            jurusanCombo.DataSource = listJurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
            jurusanCombo.KeyPress += (s, e) => e.Handled = true;
            jurusanCombo.MouseDown += (s, e) => jurusanCombo.DroppedDown = true;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (jurusanCombo.Items.Count == 0)
            { 
                new MesWarningOK("Data Jurusan Kosong!").ShowDialog();
                return;
            }
            var kelas = new KelasModel
            {
                Id = txtId.Text == string.Empty ? 0 : Convert.ToInt32(txtId.Text),
                NamaKelas = txtNamaKelas.Text.Trim(),
                Rombel = txtRombel.Text,
                IdJurusan = (int)jurusanCombo.SelectedValue,
                Tingkat = XRadio.Checked ? "X" :
                          XIRadio.Checked ? "XI" :
                          XIIRadio.Checked ? "XII" : string.Empty
            };
            if (kelas.NamaKelas == "" || kelas.Tingkat == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog();
                return;
            }
            if (new MesWarningYN($"Update Data? \n Data Dengan Kelas {NamaKelasGlobal} dan semua yang berhubungan akan berubah menjadi {kelas.NamaKelas}").ShowDialog() != DialogResult.Yes) return;
            kelasDal.Update(kelas);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        public void SetNamaKelas()
        {
            string tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            string jurusan = ((JurusanModel)jurusanCombo.SelectedItem)?.NamaJurusan ?? string.Empty;
            string rombel = txtRombel.Text.Trim();
            txtNamaKelas.Text = $"{tingkat} {jurusan} {rombel}";
        }
        public void LoadData(int Id)
        {
            var kelas = kelasDal.GetData(Id);
            if (kelas == null) return;
            txtId.Text = kelas.Id.ToString();
            txtNamaKelas.Text = kelas.NamaKelas;
            NamaKelasGlobal = kelas.NamaKelas;
            if (kelas.Tingkat == "X") XRadio.Checked = true;
            if (kelas.Tingkat == "XI") XIRadio.Checked = true;
            if (kelas.Tingkat == "XII") XIIRadio.Checked = true;
            jurusanCombo.SelectedValue = kelas.IdJurusan;

            txtRombel.Text = kelas.Rombel == string.Empty ? string.Empty : kelas.Rombel;
        }
    }
}
