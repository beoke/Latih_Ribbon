using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class EditUser : Form
    {
        private readonly RiwayatLogin_UserDal _userDal;
       
        public EditUser(int userId)
        {
            InitializeComponent();
            _userDal = new RiwayatLogin_UserDal();
            
            GetData(userId);
            ButtonSaveUser.Click += ButtonSaveUser_Click;
        }

        private void ButtonSaveUser_Click(object sender, EventArgs e)
        {
            /*if (new MesQuestionYN("Update Data ?").ShowDialog() != DialogResult.Yes) return;

            var username = TextNameUser.Text;
            var password = FormUser_RiwayatLogin.VerifyPassword(TextPassword.Text,TextPassword.Text);
            if (username == null || password == null) return;

            var data = new UserModel
            {
                Id = Convert.ToInt32(TextIdUser.Text),
                username = username,
                password = password
            };

            _userDal.Update(data);
            this.Close();
            this.DialogResult = DialogResult.Yes;*/
        }

        private void GetData(int userId)
        {
            var user = _userDal.GetUser(userId);
            TextIdUser.Text = userId.ToString();
            TextNameUser.Text = user.username;
            TextPassword.Text = user.password;
        }
    }
}