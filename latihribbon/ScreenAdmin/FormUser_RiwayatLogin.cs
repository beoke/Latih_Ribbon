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
using System.Threading;

namespace latihribbon
{
    public partial class FormUser_RiwayatLogin : Form
    {

        private readonly RiwayatLogin_UserDal _riwayatLoginDal;
        private ToolTip toolTip;
        private System.Threading.Timer timer;

        public FormUser_RiwayatLogin()
        {
            InitializeComponent();
            buf();
            _riwayatLoginDal = new RiwayatLogin_UserDal();
            toolTip = new ToolTip();
            InitCombo();
            InitialEvent();
            DeleteOtomatis();
            LoadData();
            LoadUser();

            this.Load += FormUser_RiwayatLogin_Load;
            StyleGrids();
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


        private void StyleGrids()
        {
            //Riwayat Login
            StyleComponent.StyleGrid(GridListRiwayatLogin);

            GridListRiwayatLogin.Columns[0].Width = 80;
            GridListRiwayatLogin.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridListRiwayatLogin.Columns[2].Width = 200;
            GridListRiwayatLogin.Columns[3].Width = 150;
            GridListRiwayatLogin.Columns[4].Visible = false;
            GridListRiwayatLogin.Columns["UserLogin"].HeaderText = "User Login";


            //Daftar UserLogin
            StyleComponent.StyleGrid(GridListUser);

            GridListUser.Columns[0].Visible = false;
            GridListUser.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridListUser.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            GridListUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            GridListUser.Columns[1].FillWeight = 10;
            GridListUser.Columns[2].FillWeight = 60;
            GridListUser.Columns[3].FillWeight = 30;

            toolTip.SetToolTip(btnResetFilter, "Reset Filter");
        }

        private void LoadUser()
        {
            GridListUser.DataSource = _riwayatLoginDal.ListUser()
                .Select ((x,index) => new 
                {
                    x.Id,
                    No = index+1,
                    Username = x.username,
                    Role = x.Role,

                }).ToList();
        }
        bool SqlGlobal = false;
        private string FilterData(string userLogin)
        {
            List<string> filter = new List<string>();
            string sql = string.Empty;

            if (userLogin != "") filter.Add("UserLogin LIKE '%' || @UserLogin || '%'");
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
            GridListRiwayatLogin.DataSource = _riwayatLoginDal.GetSiswaFilter(sqlc, dp)
                .Select((x,index) => new
                {
                    No = inRowPage+index+1,
                    x.UserLogin,
                    x.Tanggal,
                    Jam = x.Waktu,
                    x.IdLogin
                }).ToList();
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
                GridListRiwayatLogin.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
            else if(GridListRiwayatLogin.Width >= 610 && lastSizeMode != 2)
            {
                GridListRiwayatLogin.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            SaveUser();
        }   

        private void PickerRentan_ValueChanged(object sender, EventArgs e)
        {
            Page = 1;
            SqlGlobal = true;
            LoadData();
        }

        private void TextUserName_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            timer = new System.Threading.Timer(x =>
            {
                this.Invoke(new Action(LoadData));
            },null,400,Timeout.Infinite);
        }
        string Userlama = string.Empty;
      

        private void SaveUser()
        {
            string username = TextNameUser.Text;
            string password = TextPassword.Text;
            string role = radioSuperAdmin.Checked ? "Super Admin" :
                radioAdmin.Checked ? "Admin"
                : string.Empty;

            bool valid = username != string.Empty &&
                password != string.Empty &&
                role != string.Empty;

            if (!valid)
            {
                new MesWarningOK("Harap melengkapi data!").ShowDialog(this);
                return;
            }

            if (_riwayatLoginDal.ExistUsername(username))
            {
                new MesError($"Username {username} sudah terdaftar!").ShowDialog(this);
                return;
            }

            if (new MesQuestionYN("Input Data?").ShowDialog(this) != DialogResult.Yes) return;


            var user = new UserModel
            {
                username = username,
                password = HashPassword(password),
                Role = role,
            };
            _riwayatLoginDal.Insert(user);
            ClearUser();
            LoadData();
            LoadUser();
        }

        private void ClearUser()
        {
            TextNameUser.Clear();
            TextPassword.Clear();
            radioAdmin.Checked = false;
            radioSuperAdmin.Checked = false;
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

        #region HASH PASSWORD
        public static string HashPassword(string password)
        {
            // Generate salt secara kriptografis
            byte[] salt = GenerateSalt();

            // Buat instance Argon2
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,  // Jumlah thread
                MemorySize = 32768,       // Penggunaan memori (32 MB)
                Iterations = 2            // Jumlah iterasi
            };

            // Generate hash
            byte[] hashBytes = argon2.GetBytes(32); // Panjang hash 32 byte
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Pisahkan salt dan hash dari string yang disimpan
            var parts = hashedPassword.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hashToCompare = Convert.FromBase64String(parts[1]);

            // Buat instance Argon2 dengan salt yang sama
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,  // Jumlah thread
                MemorySize = 32768,       // Penggunaan memori (32 MB)
                Iterations = 2            // Jumlah iterasi
            };

            // Generate hash dari password yang dimasukkan
            byte[] hashBytes = argon2.GetBytes(32);

            // Bandingkan hash
            return hashBytes.SequenceEqual(hashToCompare);
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        #endregion
    }
}
