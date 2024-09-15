using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon
{
    public class RiwayatLoginDal
    {
        public IEnumerable<RiwayatLoginModel> ListData()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM RiwayatLogin";

                return Conn.Query<RiwayatLoginModel>(sql);
            };
        }
    }
}
