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
    public class TerlambatDal
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
    }
}
