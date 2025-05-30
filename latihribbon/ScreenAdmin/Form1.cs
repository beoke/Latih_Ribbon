﻿using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class Form1 : RibbonForm
    {
        private Form _mainForm;
        private Form _indexForm;
        public Form1(Form mainForm, Form indexForm, string role)
        {
            InitializeComponent();
            this._mainForm = mainForm;
            this._indexForm = indexForm;
            this.WindowState = FormWindowState.Maximized;

            this.MinimumSize = new Size(1300, 700);

            if(role == "Admin") //disable ribbon if role admin
                ribbonPanel14.Visible = false;
        }
        private void ShowFormInPanel(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(form);
            form.Show();
        }
        private void ribbonSiswaAbsensi_Click(object sender, EventArgs e)
        {
            FormSIswa fs = new FormSIswa();
            fs.Bounds = this.ClientRectangle;
            ShowFormInPanel(fs);

            ClearCheckRibbon();
            ribbonSiswaAbsensi.CheckOnClick = true;
            ribbonSiswaAbsensi.Checked = true;
        }

        private void ribbon_terlambat_Click(object sender, EventArgs e)
        {
            FormTerlambat formTerlambat = new FormTerlambat();
            formTerlambat.Bounds = this.ClientRectangle; // sudah mencakup (Width,Height, X, Y)
            ShowFormInPanel(formTerlambat);
            ClearCheckRibbon();
            ribbon_terlambat.CheckOnClick = true;
            ribbon_terlambat.Checked = true;
        }

        private void ribbon_keluar_Click(object sender, EventArgs e)
        {
            FormKeluar formkeluar = new FormKeluar();
            formkeluar.Bounds = this.ClientRectangle;
            ShowFormInPanel(formkeluar);

            ClearCheckRibbon();
            ribbon_keluar.CheckOnClick = true;
            ribbon_keluar.Checked = true;
        }
        private void ribbon_Siswa_Click(object sender, EventArgs e)
        {
            FormSIswa dataSiswa = new FormSIswa();
            dataSiswa.Bounds = this.ClientRectangle;
            ShowFormInPanel(dataSiswa);

            ClearCheckRibbon();
            ribbon_Siswa.CheckOnClick = true;
            ribbon_Siswa.Checked = true;
        }

        private void ribbonAbsensi_Click(object sender, EventArgs e)
        {
            FormAbsensi dataSiswa = new FormAbsensi();
            dataSiswa.Bounds = this.ClientRectangle;
            ShowFormInPanel(dataSiswa);

            ClearCheckRibbon();
            ribbonAbsensi.CheckOnClick = true;
            ribbonAbsensi.Checked = true;
        }

        private void ribbonRekapPersensi_Click(object sender, EventArgs e)
        {
            FormRekapPersensi p = new FormRekapPersensi();
            p.Bounds = this.ClientRectangle;
            ShowFormInPanel(p);

            ClearCheckRibbon();
            ribbonRekapPersensi.CheckOnClick = true;
            ribbonRekapPersensi.Checked = true;
        }

        private void ribbonUserLogin_Click(object sender, EventArgs e)
        {
            FormUser_RiwayatLogin formUser_RiwayatLogin = new FormUser_RiwayatLogin();
            formUser_RiwayatLogin.Bounds = this.ClientRectangle;
            ShowFormInPanel(formUser_RiwayatLogin);

            ClearCheckRibbon();
            ribbonUserLogin.CheckOnClick = true;
            ribbonUserLogin.Checked = true;
        }

        private void ribbonKelas_Click(object sender, EventArgs e)
        {
            FormKelas formKelas = new FormKelas();
            formKelas.Bounds = this.ClientRectangle;
            ShowFormInPanel(formKelas);

            ClearCheckRibbon();
            ribbonKelas.CheckOnClick = true;
            ribbonKelas.Checked = true;
        }

        private void ribbonJurusan_Click(object sender, EventArgs e)
        {
            FormJurusan formJurusan = new FormJurusan();
            formJurusan.Bounds = this.ClientRectangle;
            ShowFormInPanel(formJurusan);

            ClearCheckRibbon();
            ribbonJurusan.CheckOnClick = true;
            ribbonJurusan.Checked = true;
        }

        private void ribbonButtonSurvey_Click(object sender, EventArgs e)
        {
            FormDataSurvey survey = new FormDataSurvey();
            survey.Bounds = this.ClientRectangle;
            ShowFormInPanel(survey);

            ClearCheckRibbon();
            ribbonButtonSurvey.CheckOnClick = true;
            ribbonButtonSurvey.Checked = true;
        }

        private void ClearCheckRibbon()
        {
            ribbonJurusan.CheckOnClick = false;
            ribbonKelas.CheckOnClick = false;
            ribbonUserLogin.CheckOnClick = false;
            ribbonRekapPersensi.CheckOnClick = false;
            ribbonAbsensi.CheckOnClick = false;
            ribbon_Siswa.CheckOnClick = false;
            ribbon_keluar.CheckOnClick = false;
            ribbon_terlambat.CheckOnClick = false;
            ribbonSiswaAbsensi.CheckOnClick = false;
            ribbonButtonSurvey.CheckOnClick = false;

            ribbonJurusan.Checked = false;
            ribbonKelas.Checked = false;
            ribbonUserLogin.Checked = false;
            ribbonRekapPersensi.Checked = false;
            ribbonAbsensi.Checked = false;
            ribbon_Siswa.Checked = false;
            ribbon_keluar.Checked = false;
            ribbon_terlambat.Checked = false;
            ribbonSiswaAbsensi.Checked = false;
            ribbonButtonSurvey.Checked = false;
        }

        bool Closing = true;
        private void ButtonLogOut_Click(object sender, EventArgs e)
        {
            if (new MesQuestionYN("Apakah Anda Ingin Logout?").ShowDialog(this) == DialogResult.Yes)
            {
                Closing = false;
                FormIndex formIndex = new FormIndex(this);
                formIndex.WindowState = FormWindowState.Maximized;
                formIndex.Show();
                this.Close();
            }
        }

        private bool isExiting = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExiting) return;
            if (Closing)
            {
                var result = new MesQuestionYN("Apakah Anda Ingin Menutup Aplikasi ?").ShowDialog();
                if (result == DialogResult.Yes)
                {
                    isExiting = true;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}