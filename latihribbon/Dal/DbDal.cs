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
    public class DbDal
    {

        // Function untuk mendapatkan user dari database
        public UserModel GetUsers(string username)
        {
                using (var connection = new SqlConnection(conn.connstr()))
                {
                    connection.Open();
                    var users = connection.QueryFirstOrDefault<UserModel>("SELECT * FROM Users WHERE username=@username", new {username=username});
                    return users;
                }
        }


        public IEnumerable<SiswaModel> ListTahun()
        {
            using (var koneksi = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT DISTINCT Tahun
                                 FROM siswa
                                 ORDER BY Tahun ASC";
                return koneksi.Query<SiswaModel>(sql);
            }
        }

    }
}
