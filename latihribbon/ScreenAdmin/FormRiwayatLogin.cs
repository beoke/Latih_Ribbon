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
    public partial class FormRiwayatLogin : Form
    {

        private readonly RiwayatLoginDal _riwayatLoginDal;
        public FormRiwayatLogin()
        {
            _riwayatLoginDal = new RiwayatLoginDal();
            InitializeComponent();
            InitialGrid();
            InitialEvent();
        }

        private void InitialGrid()
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
                GridListRiwayatLogin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                GridListRiwayatLogin.ColumnHeadersHeight = 35;
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
        }

        private void PickerRentan_ValueChanged(object sender, EventArgs e)
        {
            SqlGlobal = "0"; 
            FilterData2();
        }

        private void TextUserName_TextChanged(object sender, EventArgs e)
        {
            if (TextUserName.Text == string.Empty)
                InitialGrid();
            else
                FilterData2();
        }
    }
}
