using Dapper;
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
        private Form indexForm;

        private bool PuasOn = false;
        private bool TidakPuasOn = false;

        public SurveyForm(Form mainForm,Form indexForm)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.mainForm = mainForm;
            this.indexForm = indexForm;
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
        }

        private async void btn_kembali_Click(object sender, EventArgs e)
        {
            indexForm.Opacity = 1;
            this.Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (PuasOn == false && TidakPuasOn == false)
            {
                PopUpWarning p = new PopUpWarning();
                p.ShowDialog(this);
                return;
            }
            PopUp popUp = new PopUp(); 
            popUp.ShowDialog(this);

            var puas = new SurveyModel
            {
                HasilSurvey = _data,
                Tanggal = DateTime.Today,
                Waktu = DateTime.Now.TimeOfDay,
            };
            SaveData(puas);

            indexForm.Opacity = 1;
            this.Close();
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