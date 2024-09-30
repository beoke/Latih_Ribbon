using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon
{
    public class SetPrintDal
    {
        public IEnumerable<KelasModel> ListKelas()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                    SELECT Namakelas FROM Kelas";

                return Conn.Query<KelasModel>(sql);
            }
        }
    }
}
