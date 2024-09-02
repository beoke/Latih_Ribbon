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
    public class KeluarDal
    {
        public IEnumerable<KeluarModel> ListData()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT k.Id,k.Nis,s.Nama,s.Kelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k INNER JOIN siswa s ON k.Nis=s.Nis";
                return koneksi.Query<KeluarModel>(sql);
            }
        }

        public IEnumerable<KeluarModel> GetKeluarFilter(string sql, object mbuh)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                var users = koneksi.Query<KeluarModel>(sql, mbuh);
                return users;
            }
        }
    }
}
