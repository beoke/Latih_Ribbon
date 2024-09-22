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
        public login()
        {
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            InitializeComponent();

            tx_Username.TextChanged += Tx_Username_TextChanged;
            LabelWarning.Visible = false;
        }

      

        private void btn_Login_Click(object sender, EventArgs e)
        {
            ENTER();
        }

        public void ENTER()
        {
            string username = tx_Username.Text;
            string password = tx_Password.Text;

            DbDal dbDal = new DbDal();

            IEnumerable<UserModel> Users = dbDal.GetUsers();

            UserModel user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                InsertHistori();
                if (user.Role == "admin")
                {
                    Form1 adminDashboard = new Form1();
                    adminDashboard.Show();
                }
                else if (user.Role == "siswa")
                {
                    Pemakai userDashboard = new Pemakai();
                    userDashboard.Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        }

        private void tx_Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tx_Password.Focus();
        }

        private void tx_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ENTER();
        }

        private void Tx_Username_TextChanged(object sender, EventArgs e)
        {
            string user = tx_Username.Text;

            if (user.EndsWith(" "))
                LabelWarning.Visible = true;
            
            if (user.StartsWith(" "))
                LabelWarning.Visible = true;

            else
                LabelWarning.Visible = false;
        }
    }
}
