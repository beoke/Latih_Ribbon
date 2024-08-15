using Dapper;
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
        private const string _connString = "Server=(local);Database=RekapSiswa;Trusted_Connection=True;TrustServerCertificate=True";

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

        public IEnumerable<SiswaModel> GetSiswaFilter(string sql, object mbuh) 
        {
            using (var koneksi = new SqlConnection(_connString))
            {
                var users = koneksi.Query<SiswaModel>(sql,mbuh);
                return users;
            }
        }

        public IEnumerable<KeluarModel> GetKeluarFilter(string sql, object mbuh)
        {
            using (var koneksi = new SqlConnection(_connString))
            {
                var users = koneksi.Query<KeluarModel>(sql, mbuh);
                return users;
            }
        }

        // Function untuk mendapatkan data siswa dari database
        public IEnumerable<SiswaModel> GetSiswa()
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                var siswa = connection.Query<SiswaModel>("SELECT * FROM siswa");
                return siswa;
            }
        }
        // Function untuk mendapatkan data siswa dari database
        public SiswaModel GetSiswaByNis(int nis)
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                var siswa = connection.QuerySingleOrDefault<SiswaModel>("SELECT * FROM siswa WHERE nis = @nis", new { Nis = nis });
                return siswa;
            }
        }


    }
}
