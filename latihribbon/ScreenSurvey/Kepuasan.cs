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
    public partial class Kepuasan : Form
    {
        private int Data;
        public Kepuasan()
        {
            InitializeComponent();
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

            ButtonSave.Click += ButtonSave_Click;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            var tanggal = DateTime.Now;
            var kepuasan = Data;

            SaveData(kepuasan, tanggal);
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

        private void SaveData(int kepuasan, DateTime tanggal)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                INSERT INTO Survey (Kepuasan, Tanggal)
                VALUES (@Puas , @Tanggal)";

                Conn.Execute(sql, new { Puas = kepuasan , Tanggal = tanggal });
            }
        }
    }
}
