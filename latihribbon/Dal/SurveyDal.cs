using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace latihribbon
{
    public class SurveyDal
    {
        public void Insert(SurveyModel hasil)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"
                        INSERT INTO Survey (HasilSurvey, Tanggal, Waktu, CreatedAt, CreatedBy)
                        VALUES (@HasilSurvey, @Tanggal, @Waktu, @CreatedAt, @CreatedBy);
                        SELECT last_insert_rowid();";

                        var Dp = new DynamicParameters();
                        Dp.Add("@HasilSurvey", hasil.HasilSurvey, DbType.Int16);
                        Dp.Add("@Tanggal", hasil.Tanggal, DbType.DateTime);
                        Dp.Add("@Waktu", hasil.Waktu, DbType.String);
                        Dp.Add("@CreatedAt", DateTime.Now);
                        Dp.Add("@CreatedBy", UserSession.CurrentUser ?? "Siswa");

                        int newId = Conn.QuerySingle<int>(sql, Dp, trans);
                        hasil.SurveyId = newId;

                        LoggingHelper.WriteLog(Conn, trans, "Survey", "INSERT", newId, null, hasil);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "SurveyDal.Insert");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<SurveyModel> ListData(string Filter, string Pagination, object dp)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"
                        SELECT * FROM Survey {Filter}
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
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeData = Conn.QueryFirstOrDefault<SurveyModel>("SELECT * FROM Survey WHERE SurveyId = @SurveyId", new { SurveyId = SurveyId }, trans);

                        const string sql = "DELETE FROM Survey WHERE SurveyId = @SurveyId";
                        Conn.Execute(sql, new { SurveyId = SurveyId }, trans);

                        LoggingHelper.WriteLog(Conn, trans, "Survey", "DELETE", SurveyId, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "SurveyDal.Delete");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<SurveyModel> GetDataExport(DateTime tgl1, DateTime tgl2)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"
                        SELECT * FROM Survey
                        WHERE date(Tanggal) BETWEEN date(@tgl1) AND date(@tgl2)       
                        ORDER BY SurveyId ASC";

                var dp = new DynamicParameters();
                dp.Add("@tgl1", tgl1, DbType.Date);
                dp.Add("@tgl2", tgl2, DbType.Date);

                return Conn.Query<SurveyModel>(sql, dp);
            }
        }
    }
}
