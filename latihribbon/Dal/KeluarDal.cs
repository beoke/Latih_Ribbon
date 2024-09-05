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

        public KeluarModel GetData(int Id)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT k.Id,k.Nis,s.Nama,s.Kelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k INNER JOIN siswa s ON k.Nis=s.Nis WHERE k.Id = @Id";
                return koneksi.QueryFirstOrDefault<KeluarModel>(sql, new {Id=Id});
            }
        }

        public void Insert(KeluarModel keluar)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Keluar(Nis,Tanggal,JamKeluar,JamMasuk,Tujuan)
                                VALUES(@Nis,@Tanggal,@JamKeluar,@JamMasuk,@Tujuan)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", keluar.Nis, System.Data.DbType.Int32);
                dp.Add("@Tanggal", keluar.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamKeluar", keluar.JamKeluar, System.Data.DbType.Time);
                dp.Add("@JamMasuk", keluar.JamMasuk, System.Data.DbType.Time);
                dp.Add("@Tujuan", keluar.Tujuan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        public void Update(KeluarModel keluar)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Keluar SET Nis=@Nis,Tanggal=@Tanggal,JamKeluar=@JamKeluar,
                                    JamMasuk=@JamMasuk,Tujuan=@Tujuan WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id", keluar.Id, System.Data.DbType.Int32);
                dp.Add("@Nis", keluar.Nis, System.Data.DbType.Int32);
                dp.Add("@Tanggal", keluar.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamKeluar", keluar.JamKeluar, System.Data.DbType.Time);
                dp.Add("@JamMasuk", keluar.JamMasuk, System.Data.DbType.Time);
                dp.Add("@Tujuan", keluar.Tujuan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        private void Delete(int IdKeluar)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Keluar WHERE Id=@Id";
                koneksi.Execute(sql, new { Id=IdKeluar });
            }
        }
    }
}
