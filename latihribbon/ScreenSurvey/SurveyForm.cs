﻿using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class SurveyForm : Form
    {
        private int _data;
        private Form mainForm;

        private bool PuasOn = false;
        private bool TidakPuasOn = false;

        public SurveyForm(Form mainForm)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.mainForm = mainForm;
            this.KeyPreview = true;
            this.TopMost = true;
            this.ControlBox = true;
            InitialPicture();
            ControlEvent();
        }

        private void InitialPicture()
        {
            PictureBoxTidakPuas.BackgroundImage = Properties.Resources.TidakPuas_Polos;
            PictureBoxTidakPuas.BackgroundImageLayout = ImageLayout.Stretch;

            PictureBoxPuas.BackgroundImage = Properties.Resources.Puas_Polos;
            PictureBoxPuas.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void ControlEvent()
        {
            PictureBoxPuas.Click += PictureBoxPuas_Click;
            PictureBoxTidakPuas.Click += PictureBoxTidakPuas_Click;

            ButtonKirim.Click += ButtonSave_Click;
            btn_kembali.Click += btn_kembali_Click;
            //this.Load += LoadForm;
            this.KeyDown += Kepuasan_KeyDown;
        }
        private async void LoadForm(object sender,EventArgs e)
        {
            this.Opacity = 0;
            for (double i = 0; i <= 1; i += 0.1)
            {
                this.Opacity = i;
                await Task.Delay(3);
            }
        }
        private async void btn_kembali_Click(object sender,EventArgs e)
        {
            mainForm.Opacity = 1;

            this.Opacity = 1;
            for (double i = 0; i >= 0; i -= 0.1)
            {
                this.Opacity = i;
                await Task.Delay(5);
            }
            this.Close();
        }

        private void Kepuasan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                mainForm.Show();
                this.Close();
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (PuasOn == false && TidakPuasOn == false)
            {
                this.TopMost = false;
                PopUpWarning p = new PopUpWarning();
                p.ShowDialog();
                this.TopMost = true;
                return;
            }

            var puas = new SurveyModel
            {
                HasilSurvey = _data,
                Tanggal = DateTime.Today,
                Waktu = DateTime.Now.TimeOfDay,
            };
            SaveData(puas);

            if (_data == 1)
                MessageBox.Show("Terima kasih,  respon anda sangat berharga bagi kami :)", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("🖕 🐶 🖕", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mainForm.Opacity = 1;
            this.Close();

            clear();
        }

        private void PictureBoxTidakPuas_Click(object sender, EventArgs e)
        {
            if (TidakPuasOn)
            {
                PictureBoxTidakPuas.BackgroundImage = Properties.Resources.TidakPuas_Polos;
                PictureBoxTidakPuas.BackgroundImageLayout = ImageLayout.Stretch;

                TidakPuasOn = false;
            }
            else
            {
                clear();

                PictureBoxTidakPuas.BackgroundImage = Properties.Resources.TidakPuas_Warna;
                PictureBoxTidakPuas.BackgroundImageLayout = ImageLayout.Stretch;

                TidakPuasOn = true;
            }
            
            _data = 0;
        }

        private void PictureBoxPuas_Click(object sender, EventArgs e)
        {
            if (PuasOn)
            {
                PictureBoxPuas.BackgroundImage = Properties.Resources.Puas_Polos;
                PictureBoxPuas.BackgroundImageLayout = ImageLayout.Stretch;

                PuasOn = false;
            }
            else
            {
                clear();
                PictureBoxPuas.BackgroundImage = Properties.Resources.Puas_Warna;
                PictureBoxPuas.BackgroundImageLayout = ImageLayout.Stretch;

                PuasOn = true;
            }

            _data = 1;
        }

        private void clear()
        {
            PictureBoxPuas.BackgroundImage = Properties.Resources.Puas_Polos;
            PictureBoxTidakPuas.BackgroundImage = Properties.Resources.TidakPuas_Polos;
        }

        private void SaveData(SurveyModel puas)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                INSERT INTO Survey (HasilSurvey, Tanggal, Waktu)
                VALUES (@HasilSurvey, @Tanggal, @Waktu)";


                var Dp = new DynamicParameters();
                Dp.Add("@HasilSurvey", puas.HasilSurvey, DbType.Int16);
                Dp.Add("@Tanggal", puas.Tanggal, DbType.DateTime);
                Dp.Add("@Waktu", puas.Waktu, DbType.Time);

                Conn.Execute(sql, Dp);
            }
        }
    }
}