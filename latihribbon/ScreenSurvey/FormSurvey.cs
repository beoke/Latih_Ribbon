using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormSurvey : Form
    {
        private int Data;
        private Form mainForm;

        public FormSurvey(Form mainForm)
        {
            InitializeComponent();
            InitialPicture();

            ControlEvent();
            this.mainForm = mainForm;

            this.KeyPreview = true;
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

            ButtonSave.Click += ButtonSave_Click;
            this.KeyDown += Kepuasan_KeyDown;
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
            var puas = new SurveyModel
            {
                HasilSurvey = Data,
                Tanggal =  DateTime.Today,
                Waktu = DateTime.Now.TimeOfDay,
            };
            SaveData(puas);

            if (Data == 1)
                MessageBox.Show("Terima kasih,  respon anda sangat berharga bagi kami :)", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("🖕 🐶 🖕", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);

            clear();
        }

        private void PictureBoxTidakPuas_Click(object sender, EventArgs e)
        {
            clear();
           
            PictureBoxTidakPuas.BackgroundImage = Properties.Resources.TidakPuas_Warna;
            PictureBoxTidakPuas.BackgroundImageLayout = ImageLayout.Stretch;
            Data = 0;
        }

        private void PictureBoxPuas_Click(object sender, EventArgs e)
        {
            clear();

            PictureBoxPuas.BackgroundImage = Properties.Resources.Puas_Warna;
            PictureBoxPuas.BackgroundImageLayout = ImageLayout.Stretch;
            Data = 1;
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

                Conn.Execute(sql,Dp);
            }
        }



       
    }
}
