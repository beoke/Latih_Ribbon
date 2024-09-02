using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class SiswaDal
    {
        public IEnumerable<SiswaModel> ListData()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT * FROM Siswa";
                return koneksi.Query<SiswaModel>(sql);  
            }
        }

        public IEnumerable<SiswaModel> GetSiswaFilter(string sql, object mbuh)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                var users = koneksi.Query<SiswaModel>(sql, mbuh);
                return users;
            }
        }
    }
}
