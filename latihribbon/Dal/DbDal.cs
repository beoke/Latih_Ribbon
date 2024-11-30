using Dapper;
using latihribbon.Conn;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon
{
    public class DbDal
    {

        // Function untuk mendapatkan user dari database
        public UserModel GetUsers(string username)
        {
                using (var connection = new SQLiteConnection(conn.connstr()))
                {
                    connection.Open();
                    var users = connection.QueryFirstOrDefault<UserModel>("SELECT * FROM Users WHERE username=@username", new {username=username});
                    return users;
                }
        }


        public IEnumerable<SiswaModel> ListTahun()
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT DISTINCT Tahun
                                 FROM siswa
                                 ORDER BY Tahun ASC";
                return koneksi.Query<SiswaModel>(sql);
            }
        }

    }
}
