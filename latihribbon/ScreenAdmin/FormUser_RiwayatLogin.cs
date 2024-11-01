  using Dapper;
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
using Konscious.Security.Cryptography;

namespace latihribbon
{
    public partial class FormUser_RiwayatLogin : Form
    {

        private readonly RiwayatLogin_UserDal _riwayatLoginDal;
        private ToolTip toolTip;

        public FormUser_RiwayatLogin()
        {
            InitializeComponent();
            buf();
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            toolTip = new ToolTip();
            InitCombo();
            LoadRiwayatLogin();
            InitialEvent();
            DeleteOtomatis();
            LoadData();
            LoadUser();
            LoadRiwayatLogin();

            this.Load += FormUser_RiwayatLogin_Load;
        }

        private void FormUser_RiwayatLogin_Load(object sender, EventArgs e)
        {
            TextSearchRiwayat.Focus();
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

        private void InitCombo()
        {
            List<int> list = new List<int>() {10,20,50,100,200 };
            comboPerPage.DataSource = list;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;
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

            toolTip.SetToolTip(btnResetFilter, "Reset Filter");
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
            string userLogin = TextSearchRiwayat.Text;
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
            int RowPerPage = (int)comboPerPage.SelectedValue;
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
            comboPerPage.SelectedIndexChanged += ComboPerPage_SelectedIndexChanged;
            TextSearchRiwayat.TextChanged += TextUserName_TextChanged;
            PickerRentan_1.ValueChanged += PickerRentan_ValueChanged;
            PickerRentan_2.ValueChanged += PickerRentan_ValueChanged;

            ButtonSaveUser.Click += ButtonSaveUser_Click;
            GridListUser.CellMouseClick += GridListUser_CellMouseClick;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;
            this.Resize += FormUser_RiwayatLogin_Resize;
        }
        private int lastSizeMode = 0;
        private int SetPagination = 0;
        private void FormUser_RiwayatLogin_Resize(object sender, EventArgs e)
        {
            if (GridListRiwayatLogin.Width < 610 && lastSizeMode != 1)
            {
                GridListRiwayatLogin.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
            else if(GridListRiwayatLogin.Width >= 610 && lastSizeMode != 2)
            {
                GridListRiwayatLogin.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (panelRiwayat.Width < 523 && SetPagination != 1)
            {
               panelPage.Location = new Point(126, panelPage.Location.Y);
                panelPage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                SetPagination = 1;
            }
            else if(panelRiwayat.Width >= 523 && SetPagination != 2)
            {
                panelPage.Location = new Point(panelRiwayat.Width / 2 - panelPage.Width / 2, panelPage.Location.Y);
                panelPage.Anchor = AnchorStyles.Bottom;
                SetPagination = 2;
            }
        }

        private void ComboPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            var idUser = Convert.ToInt32(GridListUser.CurrentRow.Cells[0].Value);
            if ( new MesQuestionYN("Hapus Data User").ShowDialog(this) == DialogResult.Yes)
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
            if (new MesQuestionYN("Input Data?").ShowDialog(this) != DialogResult.Yes) return;

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
            if (TextSearchRiwayat.Text == string.Empty)
            {
                LoadRiwayatLogin();
                LoadData();
            }
            else
                LoadData();
        }
        string Userlama = string.Empty;
      

        private void SaveUser(int idUser)
        {
            var user = new UserModel
            {
                Id = idUser,
                username = TextNameUser.Text,
                password = HashPassword(TextPassword.Text),
                Role = "admin",
            };
            _riwayatLoginDal.Insert(user);
            LabelAddUser.Text = "Add User";
            ClearUser();
            LoadData();
            LoadUser();
            LoadRiwayatLogin();
        }

        private void ClearUser()
        {
            TextIdUser.Clear();
            TextNameUser.Clear();
            TextPassword.Clear();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            TextSearchRiwayat.Clear();
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

        //Hash
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            new Random().NextBytes(salt);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                MemorySize = 65536,
                Iterations = 4
            };

            byte[] hashBytes = argon2.GetBytes(32);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashBytes);
        }

        // Verifikasi kata sandi
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hashToCompare = Convert.FromBase64String(parts[1]);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                MemorySize = 65536,
                Iterations = 4
            };

            byte[] hashBytes = argon2.GetBytes(32);

            return hashBytes.Length == hashToCompare.Length && hashBytes.SequenceEqual(hashToCompare);
        }      
    }
}
