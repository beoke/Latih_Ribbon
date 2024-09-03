using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class MasukDal
    {
        public IEnumerable<MasukModel> ListData()
        {
            using (var koneksi = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT m.id, m.NIS, s.Nama, s.Kelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m INNER JOIN siswa s ON m.NIS = s.NIS";
                return koneksi.Query<MasukModel>(sql);
            }
        }

        public IEnumerable<MasukModel> GetTerlambatFilter(string sql, object mbuh)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                var users = koneksi.Query<MasukModel>(sql, mbuh);
                return users;
            }
        }

        private void Insert(MasukModel masuk)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Masuk(Nis,Tanggal,JamMasuk,Alasan)
                                VALUES(@Nis,@Tanggal,@JamMasuk,@Alasan)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", masuk.NIS, System.Data.DbType.Int32);
                dp.Add("@Tanggal", masuk.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamMasuk", masuk.JamMasuk, System.Data.DbType.Time);
                dp.Add("@Alasan", masuk.Alasan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        private void Update(MasukModel masuk)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Keluar SET Nis=@Nis,Tanggal=@Tanggal,
                                    JamMasuk=@JamMasuk,Alasan=@Alasan";
                var dp = new DynamicParameters();
                dp.Add("@Nis", masuk.NIS, System.Data.DbType.Int32);
                dp.Add("@Tanggal", masuk.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamMasuk", masuk.JamMasuk, System.Data.DbType.Time);
                dp.Add("@Alasan", masuk.Alasan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        private void Delete(int IdMasuk)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Masuk WHERE Id=@Id";
                koneksi.Execute(sql, new { Id=IdMasuk });
            }
        }
    }
}
