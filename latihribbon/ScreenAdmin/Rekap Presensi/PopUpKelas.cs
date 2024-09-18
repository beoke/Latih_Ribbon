﻿using latihribbon.Dal;
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
        private readonly HistoryDal historyDal;
        string NamaKelas = string.Empty;
        public PopUpKelas(string message)
        {
            _jurusanDal = new JurusanDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            historyDal = new HistoryDal();
            InitializeComponent();
            InitEvent();

            NamaKelas = message;
            InitComponent();
            InitIsian();
        }

        private  void InitComponent()
        {
            var listJurusan = _jurusanDal.ListData();
            ComboJurusanPopUp.DataSource = listJurusan;
            ComboJurusanPopUp.DisplayMember = "NamaJurusan";
        }

        private void InitIsian()
        {
            string[] kelas = NamaKelas.Split(' ');
            string tingkat = kelas[0];
            string jurusan = kelas[1];
            string rombel = kelas.Length > 2 ? kelas[2] : string.Empty;
            SetRombel();
            if (tingkat == "X") Radio_X.Checked = true;
            if (tingkat == "XI") Radio_XI.Checked = true;
            if (tingkat == "XII") Radio_XII.Checked = true;

            foreach (var item in ComboJurusanPopUp.Items)
                if (item is JurusanModel j)
                    if (j.NamaJurusan == jurusan)
                        ComboJurusanPopUp.SelectedItem = j;
            if (rombel == string.Empty) return;
            comboRombel.SelectedItem = rombel;
            SetHasil();
        }


        private string SetRombel()
        {
            string[] kelas = NamaKelas.Split(' ');
            string tingkat = kelas[0];
            string rombel = kelas.Length > 2 ? kelas[2] : string.Empty;
            string idJurusan = ((JurusanModel)ComboJurusanPopUp.SelectedItem).Id.ToString() ?? string.Empty;
            if (idJurusan != string.Empty || rombel != string.Empty) 
            {
                var listRombel = kelasDal.GetDataRombel(Convert.ToInt32(idJurusan), tingkat);
                List<string> list = new List<string>();
                foreach (var item in listRombel)
                    list.Add(item.Rombel);
                comboRombel.DataSource = list;
            }
            return rombel;
        }

        private void SetHasil()
        {
            string tingkat = Radio_X.Checked ? "X" : Radio_XI.Checked ? "XI" : "XII";
            string jurusan = ((JurusanModel)ComboJurusanPopUp.SelectedItem).NamaJurusan;
            string rombel = comboRombel.Items.Count <= 0 ? "" : " "+comboRombel.SelectedItem;
            string NamaKelas = $"{tingkat} {jurusan}{rombel}";
            txtHasil.Text = NamaKelas;
        }

        private void InitEvent()
        {
            Button_Atur.Click += Button_Atur_Click;
        }


        private void Button_Atur_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Radio_X_CheckedChanged(object sender, EventArgs e)
        {
            SetRombel();
            SetHasil();
        }

        private void Radio_XI_CheckedChanged(object sender, EventArgs e)
        {
            SetRombel();
            SetHasil();
        }

        private void Radio_XII_CheckedChanged(object sender, EventArgs e)
        {
            SetRombel();
            SetHasil();
        }

        private void ComboJurusanPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRombel();
            SetHasil();
        }

        private void comboRombel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetHasil();
        }
    }
}