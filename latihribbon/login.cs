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
        private readonly RiwayatLoginDal _riwayatLoginDal;
        private readonly MesBox _mesBox = new MesBox();
        public login()
        {
            _riwayatLoginDal = new RiwayatLoginDal();
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

            // Initialize database access layer
            DbDal dbDal = new DbDal();

            IEnumerable<UserModel> Users = dbDal.GetUsers();

            // Check if the user exists with the provided username and password
            UserModel user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                //InsertHistori();
                // If user exists, check the role and open the appropriate dashboard
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
                // If user does not exist, show an error message
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       /* private void InsertHistori()
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
        }*/

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
            if (string.IsNullOrWhiteSpace(tx_Username.Text))
                LabelWarning.Visible = true;
            else
                LabelWarning.Visible = false;
        }
    }
}
