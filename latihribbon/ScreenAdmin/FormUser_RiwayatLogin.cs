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
    public partial class FormUser_RiwayatLogin : Form
    {

        private readonly RiwayatLogin_UserDal _riwayatLoginDal;

        public FormUser_RiwayatLogin()
        {
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            InitializeComponent();
            LoadRiwayatLogin();
            InitialEvent();
            ClearUser();
            LoadUser();

         
        }
        

        private void LoadUser()
        {

            GridListUser.DataSource = _riwayatLoginDal.ListUser();
            if (GridListUser.Rows.Count > 0)
            {
                GridListUser.ReadOnly = true;
                GridListUser.EnableHeadersVisualStyles = false;
                GridListUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                GridListUser.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                GridListUser.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                GridListUser.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                GridListUser.RowTemplate.Height = 30;
                GridListUser.ColumnHeadersHeight = 35;

                GridListUser.Columns["password"].Visible = false;
                GridListUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                GridListUser.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                GridListUser.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                GridListUser.Columns[2].Width = 50;
            }
        }

        private void LoadRiwayatLogin()
        {
            GridListRiwayatLogin.DataSource = _riwayatLoginDal.ListData();
            if (GridListRiwayatLogin.Rows.Count > 0)
            {
                GridListRiwayatLogin.ReadOnly = true;
                GridListRiwayatLogin.EnableHeadersVisualStyles = false;
                GridListRiwayatLogin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                GridListRiwayatLogin.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                GridListRiwayatLogin.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                GridListRiwayatLogin.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                GridListRiwayatLogin.RowTemplate.Height = 30;
                GridListRiwayatLogin.ColumnHeadersHeight = 35;


                GridListRiwayatLogin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                GridListRiwayatLogin.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                GridListRiwayatLogin.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                GridListRiwayatLogin.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                GridListRiwayatLogin.Columns[3].Width = 150;

            }
        }


        string SqlGlobal = string.Empty;
        private string FilterData(string userLogin , DateTime tgl_1, DateTime tgl_2)
        {
            List<string> filter = new List<string>();
            string sql = "SELECT IdLogin, UserLogin , Tanggal FROM RiwayatLogin";

            if (!string.IsNullOrEmpty(userLogin)) filter.Add("UserLogin LIKE '%' + @UserLogin + '%'");
            if (!string.IsNullOrEmpty(SqlGlobal)) filter.Add("Tanggal BETWEEN @tgl_1 AND @tgl_2");

            if (filter.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", filter);
            }

            SqlGlobal = string.Empty; 
            return sql;
        }

        private void FilterData2()
        {
            string userLogin = TextUserName.Text;
            DateTime tgl_1 = PickerRentan_1.Value;
            DateTime tgl_2 = PickerRentan_2.Value;

            string sql = FilterData(userLogin, tgl_1, tgl_2);
            var filter = _riwayatLoginDal.GetSiswaFilter(sql, new { userLogin = userLogin, tgl_1 = tgl_1, tgl_2 = tgl_2 });
            GridListRiwayatLogin.DataSource = filter;

        }

        private void InitialEvent()
        {
            TextUserName.TextChanged += TextUserName_TextChanged;
            PickerRentan_1.ValueChanged += PickerRentan_ValueChanged;
            PickerRentan_2.ValueChanged += PickerRentan_ValueChanged;

            ButtonNewUser.Click += ButtonNewUser_Click;
            ButtonDeleteUser.Click += ButtonDeleteUser_Click;
            ButtonSaveUser.Click += ButtonSaveUser_Click;
            GridListUser.SelectionChanged += GridListUser_SelectionChanged;

        }

        private void GridListUser_SelectionChanged(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(GridListUser.CurrentRow.Cells[0].Value);
            GetUser(idUser);
            LabelAddUser.Text = "Update User";


        }

        private void ButtonSaveUser_Click(object sender, EventArgs e)
        {
            int idUser = TextIdUser.Text == string.Empty ? 0 : Convert.ToInt32(TextIdUser.Text);
            SaveUser(idUser);

            LoadUser();
        }

        private void ButtonDeleteUser_Click(object sender, EventArgs e)
        {
            var idUser = Convert.ToInt32(GridListUser.CurrentRow.Cells[0].Value);
            _riwayatLoginDal.DeleteUser(idUser);


            LoadUser();
        }

        private void ButtonNewUser_Click(object sender, EventArgs e)
        {
            ClearUser();
            LabelAddUser.Text = "Add User";

        }

        private void PickerRentan_ValueChanged(object sender, EventArgs e)
        {
            SqlGlobal = "0"; 
            FilterData2();
        }

        private void TextUserName_TextChanged(object sender, EventArgs e)
        {
            if (TextUserName.Text == string.Empty)
                LoadRiwayatLogin();
            else
                FilterData2();
        }

        private void GetUser(int idUser)
        {

            var user = _riwayatLoginDal.GetUser(idUser);

            if (user != null)
            {
                TextIdUser.Text = idUser.ToString();
                TextNameUser.Text = user.Username;
                TextPassword.Text = user.Password;
                TextRole.Text = user.Role;
            }

            return;
            
        }

        private int SaveUser(int idUser)
        {
            var user = new UserModel
            {
                Id = idUser,
                Username = TextNameUser.Text,
                Password = TextPassword.Text,
                Role = TextRole.Text,
            };

            if (idUser == 0)
            {
                _riwayatLoginDal.Insert(user);
            }
            else
            {
                _riwayatLoginDal.Update(user);
            }

            return idUser;
        }

        private void ClearUser()
        {
            TextIdUser.Clear();
            TextNameUser.Clear();
            TextPassword.Clear();
            TextRole.Clear();
        }

        private void FormUser_RiwayatLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
