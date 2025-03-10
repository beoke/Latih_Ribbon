﻿using latihribbon.Dal;
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
    public partial class EditTerlambat : Form
    {
        private readonly MasukDal masukDal;
        private int globalId = 0;
        private DateTime globalTgl;
        public EditTerlambat(int Id)
        {
            InitializeComponent();
            masukDal = new MasukDal();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            globalId = Id;
            GetData(Id);
            RegisterEvent();

            CultureInfo culture = new CultureInfo("id-ID");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        private void GetData(int Id)
        {
            var masuk = masukDal.GetData(Id);
            if (masuk is null) return;
            txtNIS.Text = masuk.NIS.ToString();
            txtNama.Text = masuk.Nama;
            txtKelas.Text = masuk.NamaKelas;
            tglDT.Value = masuk.Tanggal.Date;
            jamMasuk.Value = DateTime.Today.Add(TimeSpan.Parse(masuk.JamMasuk));
            txtAlasan.Text = masuk.Alasan;
            globalTgl = masuk.Tanggal.Date;
        }

        #region EVENT
        private void RegisterEvent()
        {
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var masuk = new MasukModel()
            {
                Id = globalId,
                NIS = Convert.ToInt32(txtNIS.Text),
                Nama = txtNama.Text,
                NamaKelas = txtKelas.Text,
                Tanggal = tglDT.Value.Date,
                JamMasuk = jamMasuk.Value.ToString(@"hh\:mm"),
                Alasan = txtAlasan.Text
            };

            if(masuk.Alasan == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog(this);
                return;
            }
            if (new MesQuestionYN("Update Data?").ShowDialog(this) != DialogResult.Yes) return;
            masukDal.Update(masuk);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        #endregion
    }
}
