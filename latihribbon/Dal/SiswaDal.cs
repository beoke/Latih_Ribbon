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

        private void Insert(SiswaModel siswa)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO siswa(Nis,Nama,Kelas,Tahun)
                                VALUES(@Nis,@Nama,@Kelas,@Tahun)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                dp.Add("@Kelas", siswa.Kelas, System.Data.DbType.String);
                dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
            }
        }

        private void Update(SiswaModel siswa)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE siswa SET Nis=@Nis,Nama=@Nama,Kelas=@Kelas,Tahun=@Tahun";
                var dp = new DynamicParameters();
                dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                dp.Add("@Kelas", siswa.Kelas, System.Data.DbType.String);
                dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
            }
        }

        private void Delete(int siswaNis)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM siswa WHERE Nis=@Nis";
                koneksi.Execute(sql, new { Nis = siswaNis });
            }
        }
    }
}
