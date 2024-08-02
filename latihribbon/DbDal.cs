using Dapper;
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
        private const string _connString = "Data Source=DESKTOP-B10RQSP;Initial Catalog=RekapSiswa;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        // Function untuk mendapatkan user dari database
        public IEnumerable<UserModel> GetUsers()
        {
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    var users = connection.Query<UserModel>("SELECT * FROM Users");
                    return users;
                }
        }
    }
}
