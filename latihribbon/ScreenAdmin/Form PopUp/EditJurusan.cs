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

        public EditJurusan(int JurusanId, string NamaJurusan)
        {
            InitializeComponent();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
       
            txtId.Text = JurusanId.ToString();
            txtNamaJurusan.Text = NamaJurusan;
            namaJurusanGlobal = NamaJurusan;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int jurusanId = Convert.ToInt16(txtId.Text);
            string namaJurusan = txtNamaJurusan.Text;
            if(txtNamaJurusan.Text == string.Empty)
            {
                new MesWarningOK("Data Wajib Diisi!").ShowDialog();
                return;
            }
            if (new MesWarningYN($"Update Data? \n Kelas dengan Jurusan {namaJurusanGlobal} dan semua yang berhubungan, akan berubah menjadi {namaJurusan}", 2).ShowDialog() != DialogResult.Yes)
                return;

            var jurusan = new JurusanModel()
            {
                Id = jurusanId,
                NamaJurusan = namaJurusan
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
