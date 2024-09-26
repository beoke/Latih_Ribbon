﻿using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormSurveyKepuasan : Form
    {
        private PictureBox[] Bintang;
        private int cekBintangClick = 0;
        public FormSurveyKepuasan()
        {
            InitializeComponent();
            InitialBintang();
            ControlEvent();
        }

        private void InitialBintang()
        { 
            Bintang = new PictureBox[5] 
            {
                PictureBintang_1,  
                PictureBintang_2, 
                PictureBintang_3, 
                PictureBintang_4, 
                PictureBintang_5
            };


            for (int i = 0; i < Bintang.Length; i++)
            {
                Bintang[i].BackgroundImage = Properties.Resources.Bintang_Kosong;
                Bintang[i].BackgroundImageLayout = ImageLayout.Stretch;
                Bintang[i].Tag = i + 1;
            }
        }


        private  void ControlEvent()
        {
            PictureBintang_1.Click += PictureBintang_Click;
            PictureBintang_2.Click += PictureBintang_Click;
            PictureBintang_3.Click += PictureBintang_Click;
            PictureBintang_4.Click += PictureBintang_Click;
            PictureBintang_5.Click += PictureBintang_Click;

            ButtonKirim.Click += ButtonKirim_Click;
            ButtonBatal.Click += ButtonBatal_Click;
            TextKritikSaran.TextChanged += TextKritikSaran_TextChanged;
        }

        private void TextKritikSaran_TextChanged(object sender, EventArgs e)
        {
            var lengh = TextKritikSaran.Text.Length;
            LabelLenghText.Text = $"{lengh}/40";
        }


        private void ButtonBatal_Click(object sender, EventArgs e)
        {
           if ( MessageBox.Show("Anda yakin ingin membatalkan pengisian ?", "Pertanyaan", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
            {
                InitialBintang();
                TextKritikSaran.Text = string.Empty;
            }
        }


        private void ButtonKirim_Click(object sender, EventArgs e)
        {

            if (cekBintangClick == 0)
            {
                MessageBox.Show("Pilih bintang terlebih dahulu");
            }
         
            else
            {
                using (var Conn = new SqlConnection(conn.connstr()))
                {
                    const string sql = @"
                INSERT INTO Rating 
                    (Bintang,
                    Pesan)
                VALUES
                    (@Bintang,
                    @Pesan)";

                    var Dp = new DynamicParameters();
                    Dp.Add("@Bintang", cekBintangClick, DbType.Int32);
                    Dp.Add("@Pesan", TextKritikSaran.Text, DbType.String);

                    Conn.Execute(sql, Dp);
                }
            }
        }


        private void PictureBintang_Click(object sender, EventArgs e)
        {
            PictureBox bintangClick = (PictureBox)sender;

            int tagBintang = (int)bintangClick.Tag;
            UpdateBintang(tagBintang);
        }


        private void UpdateBintang(int tagBintang)
        {
            cekBintangClick = tagBintang;

            for (int i = 0; i < Bintang.Length; i++)
            {
                if (i < tagBintang)
                {
                    Bintang[i].BackgroundImage = Properties.Resources.Bintang_Full;
                    Bintang[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    Bintang[i].BackgroundImage = Properties.Resources.Bintang_Kosong;
                    Bintang[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }



























        /* private void InitialiBintang()
         {
             Bintang = new PictureBox[5] { PictureBintang_1, PictureBintang_2, PictureBintang_3, PictureBintang_4, PictureBintang_5 };
         }

         private void InitialTag()
         {
             PictureBintang_1.Tag = 1;
             PictureBintang_2.Tag = 2;
             PictureBintang_3.Tag = 3;
             PictureBintang_4.Tag = 4;
             PictureBintang_5.Tag = 5;

             PictureBintang_1.BackgroundImage = Properties.Resources.Bintang_Kosong;
             PictureBintang_2.BackgroundImage = Properties.Resources.Bintang_Kosong;
             PictureBintang_3.BackgroundImage = Properties.Resources.Bintang_Kosong;
             PictureBintang_4.BackgroundImage = Properties.Resources.Bintang_Kosong;
             PictureBintang_5.BackgroundImage = Properties.Resources.Bintang_Kosong;

             foreach (var style in Bintang)
             {
                 style.BackgroundImageLayout = ImageLayout.Stretch;
             }
         }

         private void ControlEvent()
         {
             PictureBintang_1.Click += PictureBintang_Click;
             PictureBintang_2.Click += PictureBintang_Click;
             PictureBintang_3.Click += PictureBintang_Click;
             PictureBintang_4.Click += PictureBintang_Click;
             PictureBintang_5.Click += PictureBintang_Click;
         }

         private void PictureBintang_Click(object sender, EventArgs e)
         {
             PictureBox BintangClick = (PictureBox)sender;

             int indexBintang = (int)BintangClick.Tag;
             UpdateBintang(indexBintang);
         }

         private void UpdateBintang(int indexBintang)
         {
             cekBintangClick = indexBintang;

             for (int i = 0; i < Bintang.Length; i++)
             {
                 if (i < indexBintang)
                 {
                     Bintang[i].BackgroundImage = Properties.Resources.Bintang_Full;
                     Bintang[i].BackgroundImageLayout = ImageLayout.Stretch;
                 }
                 else
                 {
                     Bintang[i].BackgroundImage = Properties.Resources.Bintang_Kosong;
                     Bintang[i].BackgroundImageLayout = ImageLayout.Stretch;

                 }
             }
         }*/
    }
}
