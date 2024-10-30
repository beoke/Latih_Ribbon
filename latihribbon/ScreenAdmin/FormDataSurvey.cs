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
    public partial class FormDataSurvey : Form
    {

        public FormDataSurvey()
        {
            InitializeComponent();
            buf();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ControlBox = true;
            this.KeyPreview = true;

            ControlEvent();
            InitCombo();
            LoadData();
            InitGrid();
        }

        #region EVENT
        private void ControlEvent()
        {
            comboPerPage.SelectedIndexChanged += ComboPerPage_SelectedIndexChanged;
            GridListSurvey.CellMouseClick += GridListSurvey_CellMouseClick;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;

            ComboFilter.SelectedValueChanged += ComboFilter_SelectedValueChanged;
            PickerRentan_1.ValueChanged += PickerRentan_ValueChanged;
            PickerRentan_2.ValueChanged += PickerRentan_ValueChanged;

            ButtonResetFilter.Click += ButtonResetFilter_Click;

            ButtonPrevious.Click += ButtonPrevious_Click;
            ButtonNext.Click += ButtonNext_Click;

            this.Load += FormDataSurvey_Load;
        }

        private void FormDataSurvey_Load(object sender, EventArgs e)
        {
            ComboFilter.Focus();
        }

        private void InitCombo()
        {
            List<int> list = new List<int>() { 10, 20, 50, 100, 200 };
            comboPerPage.DataSource = list;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;

            //init Combo
            ComboFilter.Items.Add("Semua");
            ComboFilter.Items.Add("Hari ini");
            ComboFilter.SelectedIndex = 0;
            ComboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ComboPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void GridListSurvey_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                GridListSurvey.ClearSelection();
                GridListSurvey.CurrentCell = GridListSurvey[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hapus data ?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
               
            var id = GridListSurvey.CurrentRow.Cells[0].Value;
            Delete(Convert.ToInt16(id));
            LoadData();
           
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (pageNow < totalPage)
            {
                pageNow++;
                LoadData();
            }
        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            if (pageNow > 1)
            {
                pageNow--;
                LoadData();
            }
        }

        private void InitGrid()
        {
            GridListSurvey.ReadOnly = true;
            GridListSurvey.EnableHeadersVisualStyles = false;
            GridListSurvey.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            GridListSurvey.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            GridListSurvey.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            GridListSurvey.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            GridListSurvey.RowTemplate.Height = 30;
            GridListSurvey.ColumnHeadersHeight = 35;

            GridListSurvey.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        bool TanggalCanged = false;
        int pageNow = 1;
        int totalPage;
        private void LoadData()
        {
            DateTime FilterToday = DateTime.Today;
            DateTime tgl1 = PickerRentan_1.Value;
            DateTime tgl2 = PickerRentan_2.Value;

            var dp = new DynamicParameters();
            string sqlc = string.Empty;
            List<string> list = new List<string>();




            if (ComboFilter.SelectedIndex != 0)
            {
                list.Add("Tanggal = @FilterToday");
                dp.Add("@FilterToday", FilterToday);
            }
            if (TanggalCanged)
            {
                list.Add("Tanggal BETWEEN @tgl1 AND @tgl2");
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }
            if (list.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", list);



          
            int rowPerPage = (int)comboPerPage.SelectedValue;

            int rowInPage = (pageNow - 1) * rowPerPage;
            int totalData = rowCount(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)totalData / rowPerPage);

            dp.Add("@Offset", rowInPage);
            dp.Add("@Fetch", rowPerPage);

            string text = "Halaman";
            text += $"{pageNow.ToString()}/{totalPage.ToString()}";
            LabelHalaman.Text = text;



            GridListSurvey.DataSource = ListData(sqlc, "OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY", dp)
                                        .Select(x => new
                                        {
                                            Id = x.SurveyId,
                                            Jawaban_Survey = x.HasilSurvey == 1 ? "Puas" : "Tidak Puas",
                                            Tanggal = x.Tanggal.ToString("dd/MM/yyyy"),
                                            Waktu = x.Waktu.ToString(@"hh\:mm")
                                        }).ToList();

            TextTotalPuas.Text = ListData(sqlc, string.Empty, dp).Count(x => x.HasilSurvey == 1).ToString();
            TextTotalTidakPuas.Text = ListData(sqlc, string.Empty, dp).Count(x => x.HasilSurvey == 0).ToString();
        }


        private void ButtonResetFilter_Click(object sender, EventArgs e)
        {
            ComboFilter.SelectedIndex = 0;
            PickerRentan_1.Value = DateTime.Today;
            PickerRentan_2.Value = DateTime.Today;
            TanggalCanged = false;
            LoadData();
        }

        private void PickerRentan_ValueChanged(object sender, EventArgs e)
        {
            TanggalCanged = true;
            LoadData();
        }


        private void ComboFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

      
        #endregion

        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            GridListSurvey,
            new object[] { true });
        }

        #region DAL
        private IEnumerable<SurveyModel> ListData(string Filter, string Pagination, object dp)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                string sql = $@"
                        SELECT *  FROM Survey  {Filter}
                        ORDER BY SurveyId DESC {Pagination}";

                return Conn.Query<SurveyModel>(sql, dp);
            }

        }


        private int rowCount (string Filter, object dp)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                string sql = $@"
                    SELECT COUNT(*) FROM Survey {Filter}";

                return Conn.QuerySingle<int>(sql, dp);
            }
        }
       

        private SurveyModel GetData(int surveyId)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM Survey WHERE SurveyId = @SurveyId";

                return Conn.QueryFirstOrDefault<SurveyModel>(sql, new { SurveyId = surveyId });
            }
        }


        private void Delete(int SurveyId)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = "DELETE FROM Survey WHERE SurveyId = @SurveyId";

                Conn.Execute(sql, new { SurveyId = SurveyId });
            }
        }

        #endregion

        private void TextTotalPuas_TextChanged(object sender, EventArgs e)
        {

        }
    }
}