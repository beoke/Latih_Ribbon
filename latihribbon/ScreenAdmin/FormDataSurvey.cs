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
            GridListSurvey.DataSource = ListData();
            buf();

            GridListSurvey.RowEnter += GridListSurvey_RowEnter;
        }

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



        private void GridListSurvey_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != null)
            {
                var surveyId = GridListSurvey.Rows[e.RowIndex].Cells[0].Value;

                GetDataFromGrid(Convert.ToInt32(surveyId));

            }
        }

        private void GetDataFromGrid(int surveyId)
        {
            var data = GetData(surveyId);

            if (data == null) return;

            var hasil = data.HasilSurvey == 1 ? "Puas" : "Tidak Puas";

            TextSurveyId.Text = data.SurveyId.ToString();
            TexthasilSurvey.Text = hasil;
            TextTanggal.Text = data.Tanggal.ToString();
            TextWaktu.Text = data.Waktu.ToString();


        }


    

        #region DAL
        private IEnumerable<SurveyModel> ListData()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM Survey";

                return Conn.Query<SurveyModel>(sql);
            }

        }

        private SurveyModel GetData(int surveyId)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM Survey WHERE SurveyId = @SurveyId";

                return Conn.QueryFirstOrDefault<SurveyModel>(sql , new {SurveyId = surveyId});
            }
        }
        #endregion
    }
}
