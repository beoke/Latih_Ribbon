using latihribbon.Conn;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using System;

namespace latihribbon
{
    public class SurveyDal
    {
        public void Insert(SurveyModel hasil)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                INSERT INTO Survey (HasilSurvey, Tanggal, Waktu)
                VALUES (@HasilSurvey, @Tanggal, @Waktu)";


                var Dp = new DynamicParameters();
                Dp.Add("@HasilSurvey", hasil.HasilSurvey, DbType.Int16);
                Dp.Add("@Tanggal", hasil.Tanggal, DbType.DateTime);
                Dp.Add("@Waktu", hasil.Waktu, DbType.String);

                Conn.Execute(sql, Dp);
            }
        }
        public IEnumerable<SurveyModel> ListData(string Filter, string Pagination, object dp)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"
                        SELECT * FROM Survey  {Filter}
                        ORDER BY SurveyId DESC {Pagination}";

                return Conn.Query<SurveyModel>(sql, dp);
            }
        }


        public int rowCount(string Filter, object dp)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"
                    SELECT COUNT(*) FROM Survey {Filter}";

                return Conn.QuerySingle<int>(sql, dp);
            }
        }


        public SurveyModel GetData(int surveyId)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM Survey WHERE SurveyId = @SurveyId";

                return Conn.QueryFirstOrDefault<SurveyModel>(sql, new { SurveyId = surveyId });
            }
        }


        public void Delete(int SurveyId)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = "DELETE FROM Survey WHERE SurveyId = @SurveyId";

                Conn.Execute(sql, new { SurveyId = SurveyId });
            }
        }

        public IEnumerable<SurveyModel> GetDataExport(DateTime tgl1, DateTime tgl2)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"
                        SELECT * FROM Survey
                        WHERE Tanggal BETWEEN @tgl1 AND @tgl2       
                        ORDER BY SurveyId ASC";

                var dp = new DynamicParameters();
                dp.Add("@tgl1",tgl1, DbType.Date);
                dp.Add("@tgl2",tgl2, DbType.Date);

                return Conn.Query<SurveyModel>(sql, dp);
            }
        }
    }
}
