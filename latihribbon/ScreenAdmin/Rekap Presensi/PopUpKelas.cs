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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class PopUpKelas : Form
    {
        private readonly JurusanDal _jurusanDal;
        private readonly SiswaDal siswaDal;
        private readonly KelasDal kelasDal;

        public PopUpKelas(string message)
        {
            _jurusanDal = new JurusanDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();

            InitializeComponent();
            InitEvent();

            txtHasil.Text = message;
            InitComponent();
        }

        private  void InitComponent()
        {
            var listJurusan = _jurusanDal.ListData();
            ComboJurusanPopUp.DataSource = listJurusan;
            ComboJurusanPopUp.DisplayMember = "NamaJurusan";
            
            //set
            string[] kelas = txtHasil.Text.Split(' ');
            string tingkat = kelas[0];
            string jurusan = kelas[1];
            string rombel = kelas.Length > 2 ? kelas[2] : string.Empty;

            if (tingkat == "X") Radio_X.Checked = true;
            if (tingkat == "XI") Radio_XI.Checked = true;
            if (tingkat == "XII") Radio_XII.Checked = true;
        }


        private void SetRombel()
        {
            string[] kelas = txtHasil.Text.Split(' ');
            string tingkat = kelas[0];
            string rombel = kelas.Length > 2 ? kelas[2] : string.Empty;
            string idJurusan = ((JurusanModel)ComboJurusanPopUp.SelectedItem).Id.ToString() ?? string.Empty;
            if (idJurusan == string.Empty || rombel == string.Empty) return;
            var listRombel = kelasDal.GetDataRombel(Convert.ToInt32(idJurusan), tingkat);
            if (!listRombel.Any()) return;
            List<string> list = new List<string>();
            foreach (var item in listRombel)
                list.Add(item.Rombel);
            comboRombel.DataSource = list;

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

        private void Radio_X_CheckedChanged(object sender, EventArgs e)
        {
            SetRombel();
        }

        private void Radio_XI_CheckedChanged(object sender, EventArgs e)
        {
            SetRombel();
        }

        private void Radio_XII_CheckedChanged(object sender, EventArgs e)
        {
            SetRombel();
        }

        private void ComboJurusanPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRombel();
        }
    }
}
