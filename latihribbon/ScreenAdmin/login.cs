using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml;
using Sodium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace latihribbon
{
    public partial class login : Form
    {
        private readonly RiwayatLogin_UserDal _riwayatLoginDal;

        private Form mainForm;
        private Form indexForm;

        private readonly Image viewPassword = Properties.Resources.view__1_;
        private readonly Image hiddenPassword = Properties.Resources.hidden__1_;
        public login(Form mainForm, Form indexForm)
        {
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            InitializeComponent();


            tx_Username.Focus();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = true;
            this.mainForm = mainForm;
            this.indexForm = indexForm;

            btnMata.FlatStyle = FlatStyle.Flat;
            btnMata.FlatAppearance.BorderSize = 0;
            btnMata.FlatAppearance.MouseDownBackColor = Color.White;
            btnMata.FlatAppearance.MouseOverBackColor = Color.White;
            btnMata.BackColor = Color.White;

            btn_kembali.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn_kembali.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ControlEvent();
        }

        #region EVENT

        private void ControlEvent()
        {
            tx_Username.TextChanged += Tx_Username_LeaveChange;
            tx_Password.TextChanged += Tx_Password_LeaveChange;

            tx_Username.Leave += Tx_Username_LeaveChange;
            tx_Password.Leave += Tx_Password_LeaveChange;
            btn_kembali.Click += btn_kembali_Click;

            LabelUsername.Click += LabelUsername_Click;
            LabelPassword.Click += LabelPassword_Click;

            btnMata.Click += BtnMata_Click;

            btn_kembali.Enter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.Leave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
            btn_kembali.MouseEnter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.MouseLeave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
        }

        private void BtnMata_Click(object sender, EventArgs e)
        {

            if (tx_Password.PasswordChar == '\0')
            {
                btnMata.Image = hiddenPassword;
                tx_Password.PasswordChar = '*';
            }
            else
            {
                btnMata.Image = viewPassword;
                tx_Password.PasswordChar = '\0';
            }
        }

        private void LabelPassword_Click(object sender, EventArgs e)
        {
            tx_Password.Focus();
        }

        private void LabelUsername_Click(object sender, EventArgs e)
        {
            tx_Username.Focus();
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            indexForm.Opacity = 1;
            this.Close();
        }
        private void Tx_Password_LeaveChange(object sender, EventArgs e)
        {
            if (tx_Password.Text.Length > 0)
                LabelPassword.Visible = false;
            else
                LabelPassword.Visible = true;
        }

        private void Tx_Username_LeaveChange(object sender, EventArgs e)
        {
            if (tx_Username.Text.Length > 0)
                LabelUsername.Visible = false; 
            else
                LabelUsername.Visible = true;
        }

        #endregion

        private void btn_Login_Click(object sender, EventArgs e)
        {
            ENTER();
        }

        public void ENTER()
        {
            string username = tx_Username.Text;
            string password = tx_Password.Text;

            if (username == "" || password == "")
            {
                new MesWarningOK("Username && Password tidak boleh kosong!").ShowDialog(this);
                ClearForm();
                tx_Username.Focus();
                return;
            }

            DbDal dbDal = new DbDal();
            UserModel user = dbDal.GetUsers(username);

            if (user == null)
            {
                new MesError("Username Tidak Tersedia!").ShowDialog(this);
                ClearForm();
                tx_Username.Focus();
                return;
            }
            if (!FormUser_RiwayatLogin.VerifyPassword(password, user.password))
            {
                new MesError("Username atau Password salah!").ShowDialog(this);
                ClearForm();
                tx_Username.Focus();
                return;
            }
            InsertHistori();

            Form1 admin = new Form1(mainForm,indexForm);
            admin.Show();
            this.Close();
            indexForm.Close();
        }


        private void InsertHistori()
        {
            string username = tx_Username.Text;
            DateTime tanggal = DateTime.Now.Date;
            TimeSpan waktu = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var riwayat = new RiwayatLoginModel
            {
                UserLogin = username,
                Tanggal = tanggal,
                Waktu = waktu.ToString(@"hh\:mm")
            };
            _riwayatLoginDal.Insert(riwayat);
            _riwayatLoginDal.DeleteAfter30Days();
        }

        private void tx_Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tx_Password.Focus();
        }

        private void tx_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ENTER();
        }

        private void ClearForm ()
        {
            tx_Username.Clear();
            tx_Password.Clear();
        }
    }
}
