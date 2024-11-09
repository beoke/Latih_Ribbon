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

        private readonly Image puasPolos = Properties.Resources.Puas_Polos;
        private readonly Image tidakPuasPolos = Properties.Resources.TidakPuas_Polos;
        private readonly Image puasWarna = Properties.Resources.Puas_Warna;
        private readonly Image tidakPuasWarna = Properties.Resources.TidakPuas_Warna;

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

            btnTidakPuas.FlatStyle = FlatStyle.Flat;
            btnTidakPuas.FlatAppearance.BorderSize = 0;
            btnTidakPuas.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnTidakPuas.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnTidakPuas.BackColor = Color.Transparent;

            btnPuas.FlatStyle = FlatStyle.Flat;
            btnPuas.FlatAppearance.BorderSize = 0;
            btnPuas.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnPuas.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnPuas.BackColor = Color.Transparent;

            btn_kembali.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn_kembali.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_kembali.Enter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.Leave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
            btn_kembali.MouseEnter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.MouseLeave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;

            InitialPicture();
            ControlEvent();
        }

        private void InitialPicture()
        {
            btnTidakPuas.BackgroundImage = tidakPuasPolos;
            btnTidakPuas.BackgroundImageLayout = ImageLayout.Stretch;

            btnPuas.BackgroundImage = puasPolos;
            btnPuas.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void ControlEvent()
        {
            btnPuas.Click += btnPuas_Click;
            btnTidakPuas.Click += btnTidakPuas_Click;

            ButtonKirim.Click += ButtonSave_Click;
            btn_kembali.Click += btn_kembali_Click;

            this.KeyDown += SurveyForm_KeyDown;
        }

        private void SurveyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LWin && e.KeyCode == Keys.RWin)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnTidakPuas_Click(object sender, EventArgs e)
        {
            btnPuas.BackgroundImage = puasPolos;
            btnTidakPuas.BackgroundImage = tidakPuasWarna;
            TidakPuasOn = true;
            PuasOn = false;
            _data = 0;
        }

        private void btnPuas_Click(object sender, EventArgs e)
        {
            btnTidakPuas.BackgroundImage = tidakPuasPolos;
            btnPuas.BackgroundImage = puasWarna;
            PuasOn = true;
            TidakPuasOn = false;
            _data = 1;
        }

        private void btn_kembali_Click(object sender, EventArgs e)
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