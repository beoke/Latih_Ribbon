using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.ScreenAdmin;
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
    public partial class PopUpKelas : Form
    {
        private readonly JurusanDal _jurusanDal;

        public PopUpKelas()
        {
            _jurusanDal = new JurusanDal();

            InitializeComponent();
            InitCombo();
            InitEvent();
        }

        private  void InitCombo()
        {
            var jurusan = _jurusanDal.ListData();
            ComboJurusanPopUp.DataSource = jurusan;
            ComboJurusanPopUp.DisplayMember = "NamaJurusan";
            ComboJurusanPopUp.ValueMember = "Id";
        }


        private void InitEvent()
        {
            Button_Atur.Click += Button_Atur_Click;
        }

        public string KelasText { get; private set; }

        private void Button_Atur_Click(object sender, EventArgs e)
        {
            var Tingkat = Radio_X.Checked ? "X" : Radio_XI.Checked ? "XI" : "XII";
            var jurusan = ComboJurusanPopUp.Text;
            var rombel = TextRombel.Text;

            if (Tingkat == "" || jurusan == "" || rombel == "")
            {
                MessageBox.Show("Pastikan semua data terisi");
                return;
            }

            KelasText = $"{Tingkat} {jurusan} {rombel}";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
