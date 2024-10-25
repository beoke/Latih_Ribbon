﻿  using Dapper;
using latihribbon.Conn;
using latihribbon.Dal;
using OfficeOpenXml.Sparkline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            InitializeComponent();
            buf();
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            LoadRiwayatLogin();
            InitialEvent();
            DeleteOtomatis();
            LoadData();
            LoadUser();
            LoadRiwayatLogin();
        }

        private void DeleteOtomatis()
        {
            var tanggal = DateTime.Today.AddDays(-30);
            _riwayatLoginDal.DeleteOtomatis(tanggal);
        }


        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            GridListRiwayatLogin,
            new object[] { true });
        }

        private void LoadUser()
        {

            GridListUser.DataSource = _riwayatLoginDal.ListUser()
                .Select (x => new 
                {
                    x.Id, 
                    Username = x.username,
                    Password = x.password,

                }).ToList();

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

                GridListUser.Columns["Id"].Visible = false;
                GridListUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                GridListUser.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                GridListUser.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void LoadRiwayatLogin()
        {

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

                
                GridListRiwayatLogin.Columns[0].Width = 80;
                GridListRiwayatLogin.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                GridListRiwayatLogin.Columns[2].Width = 200;
                GridListRiwayatLogin.Columns[3].Width = 150;

            }
        }


        bool SqlGlobal = false;
        private string FilterData(string userLogin)
        {
            List<string> filter = new List<string>();
            string sql = string.Empty;

            if (userLogin != "") filter.Add("UserLogin LIKE '%' + @UserLogin + '%'");
            if (SqlGlobal) filter.Add("Tanggal BETWEEN @tgl_1 AND @tgl_2");

            if (filter.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", filter);
            }
            return sql;
        }

        int Page = 1;
        int totalPage;
        private void LoadData()
        {
            string userLogin = TextUserName.Text;
            DateTime tgl_1 = PickerRentan_1.Value;
            DateTime tgl_2 = PickerRentan_2.Value;

            var dp = new DynamicParameters();
            if (userLogin != "") dp.Add("@UserLogin",userLogin);
            if (SqlGlobal) 
            {
                dp.Add("@tgl_1",tgl_1);
                dp.Add("@tgl_2",tgl_2);
            }
            string sqlc = FilterData(userLogin);

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = _riwayatLoginDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;

            dp.Add("@Offset",inRowPage);
            dp.Add("@Fetch",RowPerPage);
            GridListRiwayatLogin.DataSource = _riwayatLoginDal.GetSiswaFilter(sqlc, dp);
        }

        private void InitialEvent()
        {
            TextUserName.TextChanged += TextUserName_TextChanged;
            PickerRentan_1.ValueChanged += PickerRentan_ValueChanged;
            PickerRentan_2.ValueChanged += PickerRentan_ValueChanged;

            ButtonSaveUser.Click += ButtonSaveUser_Click;
            GridListUser.CellMouseClick += GridListUser_CellMouseClick;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;
            //EditMenuStrip.Click += EditMenuStrip_Click;
        }

        private void EditMenuStrip_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(GridListUser.CurrentRow.Cells["Id"].Value);

            EditUser user = new EditUser(userId);

            if (user.ShowDialog() == DialogResult.Yes)
            {
                LoadData();
                LoadUser();
            }
        }
               

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            var idUser = Convert.ToInt32(GridListUser.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Hapus Data User ?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _riwayatLoginDal.DeleteUser(idUser);
                LoadUser();
            }
        }

        private void GridListUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                GridListUser.ClearSelection();
                GridListUser.CurrentCell = GridListUser[e.ColumnIndex, e.RowIndex];
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        private void ButtonSaveUser_Click(object sender, EventArgs e)
        {
            var username = TextNameUser.Text;
            if (MessageBox.Show($"Tambahkan \"{username}\" sebagai admin ?", "pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int idUser = TextIdUser.Text == string.Empty ? 0 : Convert.ToInt32(TextIdUser.Text);
            SaveUser(idUser);

        }   

        private void PickerRentan_ValueChanged(object sender, EventArgs e)
        {
            SqlGlobal = true;
            LoadData();
        }

        private void TextUserName_TextChanged(object sender, EventArgs e)
        {
            if (TextUserName.Text == string.Empty)
                LoadRiwayatLogin();
            else
                LoadData();
        }
        string Userlama = string.Empty;
      

        private int SaveUser(int idUser)
        {
            var user = new UserModel
            {
                Id = idUser,
                username = TextNameUser.Text,
                password = EncriptPassword(TextPassword.Text),
                Role = "admin",
            };

            if (idUser == 0)
            {
                _riwayatLoginDal.Insert(user);
                LabelAddUser.Text = "Add User";
                ClearUser();
                LoadData();
                LoadUser();
                LoadRiwayatLogin();
            }
            else
            {
                _riwayatLoginDal.Update(user);
                _riwayatLoginDal.UpdateUserRiwayat(user.username,Userlama);
                LoadData();
                LoadUser();
                LoadRiwayatLogin();
            }
            return idUser;
        }

        private void ClearUser()
        {
            TextIdUser.Clear();
            TextNameUser.Clear();
            TextPassword.Clear();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            TextUserName.Clear();
            PickerRentan_1.Value = DateTime.Now;
            PickerRentan_2.Value = DateTime.Now;
            SqlGlobal = false;
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Page < totalPage)
            {
                Page++;
                LoadData();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(Page > 1)
            {
                Page--;
                LoadData();
            }
        }



        
        public static string EncriptPassword (string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                
                return builder.ToString();
            }
        }
      
    }
}
