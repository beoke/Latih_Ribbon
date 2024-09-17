using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
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
        private readonly SiswaDal siswaDal;
        public static string kelasName;

        public PopUpKelas(string message)
        {
            _jurusanDal = new JurusanDal();
            siswaDal = new SiswaDal();

            InitializeComponent();
            InitComponent();
            InitEvent();

            txtHasil.Text = message;
        }

        private  void InitComponent()
        {
            var listJurusan = _jurusanDal.ListData();
            ComboJurusanPopUp.DataSource = listJurusan;
            ComboJurusanPopUp.DisplayMember = "NamaJurusan";

            var siswa = siswaDal.ListKelas();
            dataGridView1.DataSource = siswa;
            return;
            


            //set
            string[] kelas = txtHasil.Text.Split(' ');
            string tingkat = kelas[0];
            string jurusan = kelas[1];
            string rombel = kelas.Length > 2 ? kelas[2] : string.Empty;

            if (tingkat == "X") Radio_X.Checked = true;
            if (tingkat == "XI") Radio_XI.Checked = true;
            if (tingkat == "XII") Radio_XII.Checked = true;

            foreach (var item in ComboJurusanPopUp.Items)
                if (jurusan == (string)item)
                    ComboJurusanPopUp.SelectedItem = item;

            /*foreach (var item in comboRombel.Items)
                if (rombel)*/

        }


        private void InitEvent()
        {
            Button_Atur.Click += Button_Atur_Click;
        }

        public string KelasText { get; private set; }

        private void Button_Atur_Click(object sender, EventArgs e)
        {
            string Tingkat = Radio_X.Checked ? "X" : Radio_XI.Checked ? "XI" : "XII";
            string jurusan = ComboJurusanPopUp.Text;
            string rombel = comboRombel.Text;
            string hasil = txtHasil.Text;
    
            KelasText = $"{Tingkat} {jurusan} {rombel}";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
