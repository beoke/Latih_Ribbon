using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class EditSiswa : Form
    {
        private readonly SiswaDal _siswaDal;
        private readonly JurusanDal _jurusanDal;
        private readonly KelasDal _kelasDal;
        private int Nis = 0;


        public EditSiswa(int Nis)
        {
            _siswaDal = new SiswaDal();
            _jurusanDal = new JurusanDal();
            _kelasDal = new KelasDal();

            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Nis = Nis;
            RegisterEvent();

            var jurusan = _jurusanDal.ListData();
            jurusanCombo.DataSource = jurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
            jurusanCombo.KeyPress += (s, e) => e.Handled = true; // versi singkat event
            jurusanCombo.MouseDown += (s, e) => jurusanCombo.DroppedDown = true;

            rombelCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            GetDataSiswa(Nis);
        }   
        private void RegisterEvent()
        {
            btnSave_FormSiswa.Click += BtnSave_FormSiswa_Click;
            txtNIS_FormSiswa.TextChanged += TxtNIS_FormSiswa_TextChanged;

            XRadio.CheckedChanged += radio_CheckedChange;
            XIRadio.CheckedChanged += radio_CheckedChange;
            XIIRadio.CheckedChanged += radio_CheckedChange;
            jurusanCombo.SelectedIndexChanged += radio_CheckedChange;

            txtNIS_FormSiswa.KeyPress += input_KeyPress;
            txtPersensi_FormSiswa.KeyPress += input_KeyPress;
            txtTahun_FormSiswa.KeyPress += input_KeyPress;
        }

        private void TxtNIS_FormSiswa_TextChanged(object sender, EventArgs e)
        {
            string nis = txtNIS_FormSiswa.Text;
            if (nis.Length >= 5)
                CekNis(Convert.ToInt32(nis));
            else
                lblNisSudahAda.Visible = false;
        }

        private void input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void radio_CheckedChange(object sender, EventArgs e)
        {
            string tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            string jurusan = ((JurusanModel)jurusanCombo.SelectedItem)?.Id.ToString() ?? string.Empty;
            if (tingkat == string.Empty || jurusan == "")
            {
                rombelCombo.DataSource = null;
                return;
            }
            var cari = _kelasDal.GetDataRombel(Convert.ToInt32(jurusan), tingkat);
            rombelCombo.DataSource = cari.Select(item => item.Rombel).ToList();
        }
        public void CekNis(int nis)
        {
            var cekNis = _siswaDal.GetData(nis);
            if (cekNis != null && nis != this.Nis)
                lblNisSudahAda.Visible = true;
            else
                lblNisSudahAda.Visible = false;
        }
        private void GetDataSiswa(int nis)
        {
            var get = _siswaDal.GetData(nis);
            if (get == null) return;

            txtNIS_FormSiswa.Text = get.Nis.ToString();
            txtNama_FormSiswa.Text = get.Nama;
            txtPersensi_FormSiswa.Text = get.Persensi.ToString();

            if (get.JenisKelamin == "L") lakiRadio.Checked = true;
            if (get.JenisKelamin == "P") perempuanRadio.Checked = true;

            string[] NamaKelas = get.NamaKelas.Split(' ');
            if (NamaKelas[0] == "XI") XIRadio.Checked = true;
            if (NamaKelas[0] == "XI") XIRadio.Checked = true;
            if (NamaKelas[0] == "XII") XIIRadio.Checked = true;


            txtTahun_FormSiswa.Text = get.Tahun;

            foreach (var item in jurusanCombo.Items)
                if (item is JurusanModel jurusan)
                    if (jurusan.NamaJurusan == NamaKelas[1])
                        jurusanCombo.SelectedItem = jurusan;

            
            rombelCombo.DataSource = _kelasDal.GetDataRombel((int)jurusanCombo.SelectedValue, NamaKelas[0]);
            rombelCombo.DisplayMember = "Rombel";
            rombelCombo.ValueMember = "Id";
        }
                
        private void BtnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            Update();  
        }

        private void Update()
        {
            string jenisKelamin = string.Empty;
            if (lakiRadio.Checked == true) jenisKelamin = "L";
            if (perempuanRadio.Checked == true) jenisKelamin = "P";

            var Tingkat = string.Empty;
            if (XRadio.Checked == true) Tingkat = "X";
            if (XIRadio.Checked == true) Tingkat = "XI";
            if (XIIRadio.Checked == true) Tingkat = "XII";

            string NamaKelas = $"{Tingkat} {jurusanCombo.Text} {rombelCombo.Text}".Trim();

            int IdKelas = Convert.ToInt32(_kelasDal.GetIdKelas(NamaKelas));

            var siswa = new SiswaModel
            {
                Nis = txtNIS_FormSiswa.Text==""?0:Convert.ToInt32(txtNIS_FormSiswa.Text),
                Persensi =txtPersensi_FormSiswa.Text == "" ? 0 : Convert.ToInt32(txtPersensi_FormSiswa.Text),
                Nama = txtNama_FormSiswa.Text,
                JenisKelamin = jenisKelamin,
                IdKelas = IdKelas,
                Tahun = txtTahun_FormSiswa.Text
            };
            if(siswa.Nis == 0 || siswa.Persensi == 0 || siswa.Nama == "" || siswa.Tahun == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog();
                return;
            }
            if(lblNisSudahAda.Visible == true)
            {
                new MesWarningOK("Nis Sudah Ada!").ShowDialog();
                return;
            }
            if (new MesWarningYN($"Update Data?\nJika di Update, Maka Data Yang Terhubung Akan Ikut Terupdate!",2).ShowDialog() != DialogResult.Yes) return;
            _siswaDal.Update(siswa);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
