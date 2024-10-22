using latihribbon.Dal;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class EditKeluar : Form
    {
        private readonly KeluarDal _keluarDal;
        private int globalID = 0;
        public EditKeluar(int keluarId)
        {
            _keluarDal = new KeluarDal();
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            GetData(keluarId);

            btnSave_FormSiswa.Click += BtnSave_FormSiswa_Click;
            globalID = keluarId;

            CultureInfo culture = new CultureInfo("id-ID");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void BtnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            if (new MesWarningYN("Update Data ?").ShowDialog() != DialogResult.Yes) return;
            this.DialogResult = DialogResult.OK;
            Update();
            this.Close();
        }

        private void Update()
        {
            var data = new KeluarModel
            {
                Id = globalID,
                Nis = Convert.ToInt32(txtNIS1.Text),
                Tanggal = tglDT.Value,
                JamKeluar = jamKeluarDT.Value.TimeOfDay,
                JamMasuk = jamMasukDT.Value.TimeOfDay,
                Tujuan = txtTujuan1.Text
            };

            _keluarDal.Update(data);
        }

        private void GetData(int keluarId)
        {
            var data = _keluarDal.GetData(keluarId);
            if (data == null) return;
            txtNIS1.Text = data.Nis.ToString();
            txtNama1.Text = data.Nama;
            txtKelas1.Text = data.NamaKelas;
            tglDT.Value = data.Tanggal;
            jamKeluarDT.Value = DateTime.Today.Add(data.JamKeluar);
            jamMasukDT.Value = DateTime.Today.Add(data.JamMasuk);
            txtTujuan1.Text = data.Tujuan;
        }

        
    }
}
