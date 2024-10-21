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

        public EditSiswa(int Nis)
        {
            _siswaDal = new SiswaDal();
            _jurusanDal = new JurusanDal();
            _kelasDal = new KelasDal();

            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            RegisterEvent();

            var jurusan = _jurusanDal.ListData();
            jurusanCombo.DataSource = jurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
            


            GetDataSiswa(Nis);
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
                
        private void RegisterEvent()
        {
            btnSave_FormSiswa.Click += BtnSave_FormSiswa_Click;
        }

        private void BtnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            if (new MesQuestionYN("Update data ?",1).ShowDialog() == DialogResult.Yes)
            {
                Update();
                this.Close();
                this.DialogResult = DialogResult.OK;
            }    
        }

        private void Update()
        {
            

            var jenisKelamin= string.Empty;
            if (lakiRadio.Checked == true) jenisKelamin = "L";
            if (perempuanRadio.Checked == true) jenisKelamin = "P";

            var Tingkat = string.Empty;
            if (XRadio.Checked == true) Tingkat = "X";
            if (XIRadio.Checked == true) Tingkat = "XI";
            if (XIIRadio.Checked == true) Tingkat = "XII";

            string NamaKelas = $"{Tingkat} {jurusanCombo.Text} {rombelCombo.Text}";

            int IdKelas = Convert.ToInt32(_kelasDal.GetIdKelas(NamaKelas));

            var siswa = new SiswaModel
            {
                Nis = Convert.ToInt32(txtNIS_FormSiswa.Text),
                Persensi = Convert.ToInt32(txtPersensi_FormSiswa.Text),
                Nama = txtNama_FormSiswa.Text,
                JenisKelamin = jenisKelamin,
                IdKelas = IdKelas,
                Tahun = txtTahun_FormSiswa.Text
                
            };

            _siswaDal.Update(siswa);
        }
    }
}
