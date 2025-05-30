﻿using latihribbon.Dal;
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
        string Nama = string.Empty;
        public PopUpKelas(string nama, string history)
        {
            _jurusanDal = new JurusanDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            historyDal = new HistoryDal();
            InitializeComponent();
            InitEvent();
            this.Nama = nama;
            this.NamaKelas = history;
            InitComponent();
            InitIsian();
        }

        private  void InitComponent()
        {
            var listJurusan = _jurusanDal.ListData();
            ComboJurusanPopUp.DataSource = listJurusan
                .Select(x => new JurusanModel
                {
                    Id = x.Id,
                    NamaJurusan = $"{x.Kode} - {x.NamaJurusan}",
                    Kode = x.Kode
                }).ToList();
            ComboJurusanPopUp.DisplayMember = "NamaJurusan";
        }

        private void InitIsian()
        {
            if (string.IsNullOrEmpty(NamaKelas)) return;
            string[] kelas = NamaKelas.Trim().Split(' ');
            if (kelas.Length < 2) return;
  
            string tingkat = kelas[0];
            string kode = kelas[1];
            string rombel = kelas.Length > 2 ? kelas[2] : string.Empty;
            SetRombel();
            if (tingkat == "X") Radio_X.Checked = true;
            if (tingkat == "XI") Radio_XI.Checked = true;
            if (tingkat == "XII") Radio_XII.Checked = true;
            
            foreach (var item in ComboJurusanPopUp.Items)
                if (item is JurusanModel j)
                    if (j.Kode == kode)
                        ComboJurusanPopUp.SelectedItem = j;
            if (rombel == string.Empty) return;
            comboRombel.SelectedItem = rombel;
            SetHasil();
        }

        private void SetRombel()
        {
            string rombel = comboRombel.SelectedItem?.ToString() ?? string.Empty;
            string tingkat = Radio_X.Checked ? "X":Radio_XI.Checked ? "XI" : Radio_XII.Checked ? "XII" : string.Empty;
            string idJurusan = ((JurusanModel)ComboJurusanPopUp.SelectedItem)?.Id.ToString() ?? string.Empty;
            if (tingkat != string.Empty|| idJurusan != string.Empty || rombel != string.Empty) 
            {
                var listRombel = kelasDal.GetDataRombel(Convert.ToInt32(idJurusan), tingkat);
                List<string> list = new List<string>();
                foreach (var item in listRombel)
                    list.Add(item.Rombel);
                comboRombel.DataSource = list;
                comboRombel.SelectedItem = rombel;
            }
        }

        private void SetHasil()
        {
            string tingkat = Radio_XII.Checked ? "XII" : Radio_XI.Checked ? "XI" : Radio_X.Checked ?"X":string.Empty;
            string kode = ((JurusanModel)ComboJurusanPopUp.SelectedItem)?.Kode ?? string.Empty;
            if (kode == string.Empty) return;
            string rombel = comboRombel.Items.Count <= 0 ? "" : " "+comboRombel.SelectedItem;
            if (tingkat == string.Empty || kode == string.Empty) return;
            string NamaKelas = $"{tingkat} {kode}{rombel}";
            txtHasil.Text = NamaKelas.Trim();
        }

        private void InitEvent()
        {
            Button_Atur.Click += Button_Atur_Click;
            Radio_X.CheckedChanged += Change;
            Radio_XI.CheckedChanged += Change;
            Radio_XII.CheckedChanged += Change;
            ComboJurusanPopUp.SelectedIndexChanged += Change;
        }

        private void Button_Atur_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            historyDal.Update(Nama, txtHasil.Text);
            this.Close();
        }

        private void Change(object sender, EventArgs e)
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
