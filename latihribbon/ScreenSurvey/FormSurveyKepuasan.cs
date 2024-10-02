using Dapper;
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
using System.Windows.Input;

namespace latihribbon
{
    public partial class FormSurveyKepuasan : Form
    {
        private PictureBox[] Bintang;
        private int cekBintangClick = 0;
        public FormSurveyKepuasan()
        {
            InitializeComponent();
            InitialProperti();
            ControlEvent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = true;
            this.TopMost = true;
            this.KeyPreview = true;
        }

        private void InitialProperti()
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

            TextKritikSaran.MaxLength = 40;
            TextKritikSaran.Text = string.Empty;

            PictureBintang_RataRata.BackgroundImage = Properties.Resources.Bintang_Full;
            PictureBintang_RataRata.BackgroundImageLayout = ImageLayout.Stretch;

            cekBintangClick = 0;
            RataRataBintang();
        }

        private void RataRataBintang()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                    SELECT 
                        CAST (ROUND ( AVG(Bintang), 1 ) AS FLOAT)
                    FROM 
                        Rating
                    WHERE
                        Bintang BETWEEN 1 AND 5";

                var rataRata = Conn.ExecuteScalar<Decimal>(sql);

                LabelRataRataRating.Text = rataRata.ToString();
            }
        }




        #region Event
        private void ControlEvent()
        {
            PictureBintang_1.Click += PictureBintang_Click;
            PictureBintang_2.Click += PictureBintang_Click;
            PictureBintang_3.Click += PictureBintang_Click;
            PictureBintang_4.Click += PictureBintang_Click;
            PictureBintang_5.Click += PictureBintang_Click;


            ButtonKirim.Click += ButtonKirim_Click;
            ButtonBatal.Click += ButtonBatal_Click;
            TextKritikSaran.TextChanged += TextKritikSaran_TextChanged;
            TextKritikSaran.TextChanged += TextKritikSaran_TextChanged1; ;
            TextKritikSaran.Leave += TextKritikSaran_Leave;
            LabelKritikSaran.Click += LabelKritikSaran_Click;
            this.KeyDown += FormSurveyKepuasan_KeyDown; 
        }

        private void TextKritikSaran_TextChanged1(object sender, EventArgs e)
        {
            if (TextKritikSaran.Text.Length > 0)
            {
                LabelKritikSaran.Visible = false;
            }
            else
            {
                LabelKritikSaran.Visible = true;
            }
        }

        private void LabelKritikSaran_Click(object sender, EventArgs e)
        {
            TextKritikSaran.Focus();
        }


        private void TextKritikSaran_Leave(object sender, EventArgs e)
        {
            LabelKritikSaran.Visible = true;
        }




        private void FormSurveyKepuasan_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                login login = new login();
                login.Show();
                this.Close();
            }
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
                cekBintangClick = 0;
                InitialProperti();
                TextKritikSaran.Text = string.Empty;
            }
        }


        private void ButtonKirim_Click(object sender, EventArgs e)
        {

            if (cekBintangClick == 0)
            {
                LabelKritikSaran.Visible = false;
                MessageBox.Show("Pilih bintang terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
                InitialProperti();
                MessageBox.Show("Terima kasih telah memberi penilaian", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void PictureBintang_Click(object sender, EventArgs e)
        {
            PictureBox bintangClick = (PictureBox)sender;

            int tagBintang = (int)bintangClick.Tag;
            UpdateBintang(tagBintang);
        }

        #endregion



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


       

        
    }
}
