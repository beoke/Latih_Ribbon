using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class KeluarDal
    {
        public IEnumerable<KeluarModel> ListData(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT k.Id,k.Nis,s.Nama,kls.NamaKelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k 
                            INNER JOIN siswa s ON k.Nis=s.Nis
                            INNER JOIN kelas kls ON s.IdKelas = kls.Id 
                            {sqlc} 
                            ORDER BY k.Tanggal OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                return koneksi.Query<KeluarModel>(sql,dp);
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
                const string sql = @"SELECT k.Id,k.Nis,s.Nama,kls.NamaKelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k 
                            INNER JOIN siswa s ON k.Nis=s.Nis
                            INNER JOIN kelas kls ON s.IdKelas = kls.Id 
                            WHERE k.Id = @Id";
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

        public void Delete(int IdKeluar)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Keluar WHERE Id=@Id";
                koneksi.Execute(sql, new { Id=IdKeluar });
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = @"SELECT COUNT(*) FROM Keluar k
                                INNER JOIN siswa s ON k.Nis=s.Nis
                                INNER JOIN kelas kls ON s.IdKelas = kls.Id";
                if (sqlc != string.Empty) sql += sqlc;
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }
    }
}
