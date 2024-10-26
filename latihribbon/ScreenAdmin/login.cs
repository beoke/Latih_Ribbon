using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml;
using latihribbon.Helper;
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
        private readonly MesBox _mesBox = new MesBox();

        private Form mainForm;
        private Form indexForm;
        public login(Form mainForm, Form indexForm)
        {
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            InitializeComponent();

            ControlEvent();

            tx_Username.Focus();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ControlBox = true;
            this.mainForm = mainForm;
            this.indexForm = indexForm;
        }

        #region EVENT

        private void ControlEvent()
        {
            tx_Username.TextChanged += Tx_Username_TextChanged;
            tx_Password.TextChanged += Tx_Password_TextChanged;

            tx_Username.Leave += Tx_Username_Leave;
            tx_Password.Leave += Tx_Password_Leave;
            btn_kembali.Click += btn_kembali_Click;
        }
        private void btn_kembali_Click(object sender, EventArgs e)
        {
            indexForm.Opacity = 1;
            this.Close();
        }
        private void Tx_Password_Leave(object sender, EventArgs e)
        {
            if (tx_Password.Text.Length > 0)
                LabelPassword.Visible = false;
            else
                LabelPassword.Visible = true;
        }

        private void Tx_Username_Leave(object sender, EventArgs e)
        {
            if (tx_Username.Text.Length > 0)
                LabelUsername.Visible = false; 
            else
                LabelUsername.Visible = true;
        }

        private void Tx_Password_TextChanged(object sender, EventArgs e)
        {
             if(tx_Password.Text.Length > 0)
                LabelPassword.Visible = false;
            else
                LabelPassword.Visible = true;
        }

        private void Tx_Username_TextChanged(object sender, EventArgs e)
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

            DbDal dbDal = new DbDal();
            UserModel user = dbDal.GetUsers(username);

            if(user == null)
            {
                new MesWarningOK("Username Tidak Tersedia!").ShowDialog(this);
                return;
            }
            if (!FormUser_RiwayatLogin.VerifyPassword(password, user.password))
            {
                new MesError("Username atau Password salah!").ShowDialog(this);
                return;
            }
            InsertHistori();
            Form1 admin = new Form1(mainForm,indexForm);
            admin.Show();
            this.Close();
        }

        private void InsertHistori()
        {
            string username = tx_Username.Text;
            DateTime tanggal = DateTime.Today;
            TimeSpan waktu = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var riwayat = new RiwayatLoginModel
            {
                UserLogin = username,
                Tanggal = tanggal,
                Waktu = waktu
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
    }
}
