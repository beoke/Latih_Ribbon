using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
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
                // If user exists, check the role and open the appropriate dashboard
                if (user.Role == "admin")
                {
                    Form1 adminDashboard = new Form1();
                    adminDashboard.Show();
                }
                else if (user.Role == "user")
                 {
                     User userDashboard = new User();
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
    }
}
