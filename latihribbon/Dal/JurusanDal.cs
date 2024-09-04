using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class JurusanDal
    {
        public IEnumerable<Model.JurusanModel> ListData()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT * FROM Jurusan";
                return koneksi.Query<JurusanModel>(sql);
            }
        }
    }
}
