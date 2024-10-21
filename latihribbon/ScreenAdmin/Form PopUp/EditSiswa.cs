using latihribbon.Dal;
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
    public partial class EditSiswa : Form
    {
        private readonly SiswaDal _siswaDal;

        public EditSiswa(int Nis)
        {
            _siswaDal = new SiswaDal();
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            RegisterEvent();

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


        }

        private void RegisterEvent()
        {
            btnSave_FormSiswa.Click += BtnSave_FormSiswa_Click;
        }

        private void BtnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            new MesQuestionYN("Sya amaka aia aua aua aa aua aua a aa aua aua wuw wuw wuw wuwwwww uww",1).ShowDialog();
        }
    }
}
