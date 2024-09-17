using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace latihribbon
{
    public class RiwayatLoginDal
    {
        public IEnumerable<RiwayatLoginModel> ListData()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = "SELECT * FROM RiwayatLogin";

                return Conn.Query<RiwayatLoginModel>(sql);
            };
        }

/*        public void Insert(RiwayatLoginModel riwayat)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                        INSERT INTO RiwayatLogin 
                            (UserLogin, Tanggal, Waktu)
                        VALUES 
                            (@UserLogin, @Tanggal, @Waktu)";

                var Dp = new DynamicParameters();
                Dp.Add("@UserLogin", riwayat.UserLogin, DbType.String);
                Dp.Add("@Tanggal", riwayat.Tanggal, DbType.DateTime);
                Dp.Add("@Waktu", riwayat.Waktu, DbType.Time);

                Conn.Execute(sql, Dp);
            }
        }*/

        public IEnumerable<RiwayatLoginModel> GetSiswaFilter(string sql , object query)
        {
            using ( var Conn = new SqlConnection(conn.connstr()))
            {
                var  filterLogin = Conn.Query<RiwayatLoginModel>(sql, query);
                return filterLogin;
            }
        }
    }
}