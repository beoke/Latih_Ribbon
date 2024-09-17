using DocumentFormat.OpenXml.Wordprocessing;
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

namespace latihribbon.ScreenAdmin
{
    public partial class FormKelas : Form
    {
        private readonly JurusanDal jurusanDal;
        private readonly KelasDal kelasDal;
        private readonly MesBox mesBox = new MesBox();

        public FormKelas()
        {
            InitializeComponent();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            LoadData();
            InitComponent();
            
        }

        public void LoadData()
        {
            GridListKelas.DataSource = kelasDal.listKelas();
        }

        public void InitComponent()
        {
            var listJurusan = jurusanDal.ListData();
            if (!listJurusan.Any()) return;
            jurusanCombo.DataSource = listJurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
        }

        public void SaveData()
        {
            string idKelas, NamaKelas,Tingkat, Rombel, idJurusan;
            idKelas = txtIdKelas.Text;
            NamaKelas = txtNamaKelas.Text;
            Tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            idJurusan = jurusanCombo.SelectedValue.ToString();
            Rombel = txtRombel.Text;

            if(Tingkat== "" || Rombel == string.Empty)
            {
                mesBox.MesInfo("Seluruh Data Wajib Terisi Kecuali Id Kelas!");
                return;
            }

            var kelas = new KelasModel
            {
                IdKelas = int.Parse(idKelas),
                NamaKelas = NamaKelas,
                Rombel = Rombel,
                IdJurusan = int.Parse(idJurusan)
            };

            if(idKelas == string.Empty)
            {
                kelasDal.Insert(kelas);
                LoadData();
            }
            else
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}
