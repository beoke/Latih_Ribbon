using latihribbon.Dal;
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
    public partial class EditJurusan : Form
    {
        private readonly JurusanDal jurusanDal;
        private readonly KelasDal kelasDal;
        private string namaJurusanGlobal = string.Empty;

        public EditJurusan(int JurusanId)
        {
            InitializeComponent();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            GetData(JurusanId);
        }

        private void GetData(int id)
        {
            var data = jurusanDal.GetData(id);
            if (data is null) return;

            txtId.Text = id.ToString();
            txtNamaJurusan.Text = data.NamaJurusan;
            txtKode.Text = data.Kode;

            namaJurusanGlobal = data.NamaJurusan;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int jurusanId = Convert.ToInt16(txtId.Text);
            string namaJurusan = txtNamaJurusan.Text;
            string kode = txtKode.Text;
            if(namaJurusan == string.Empty || kode == string.Empty)
            {
                new MesWarningOK("Data Wajib Diisi!").ShowDialog();
                return;
            }
            if (jurusanDal.CekDuplikasiUpdate(jurusanId, kode, namaJurusan))
            {
                new MesError($"Jurusan {namaJurusan} atau Kode {kode} Sudah Tersedia.").ShowDialog(this);
                return;
            }

            if (new MesWarningYN($"Update Data? \n Kelas dengan Jurusan {namaJurusanGlobal} dan semua yang berhubungan, akan berubah menjadi {namaJurusan}", 2).ShowDialog() != DialogResult.Yes)
                return;

            var jurusan = new JurusanModel()
            {
                Id = jurusanId,
                NamaJurusan = namaJurusan,
                Kode = kode
            };

            jurusanDal.Update(jurusan);
            var dataKelas = kelasDal.listKelas("WHERE k.idJurusan=@idJurusan", new { idJurusan = jurusan.Id });
            foreach (var x in dataKelas)
            {
                string namaKelas = $"{x.Tingkat} {x.Kode} {x.Rombel}".Trim();
                kelasDal.UpdateNamaKelas(x.Id, namaKelas);
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
