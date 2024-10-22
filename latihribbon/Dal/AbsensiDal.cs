using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.Dal
{
    public class AbsensiDal
    {
        public void Insert(AbsensiModel absensi)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Persensi(Nis,Tanggal,Keterangan) VALUES(@Nis,@Tanggal,@Keterangan)";
                var dp = new DynamicParameters();
                dp.Add("@Id", absensi.Id, System.Data.DbType.Int32);
                dp.Add("@Nis", absensi.Nis, System.Data.DbType.Int32);
                dp.Add("@Tanggal", absensi.Tanggal, System.Data.DbType.Date);
                dp.Add("@Keterangan", absensi.Keterangan, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
            }
        }

        public void Update(AbsensiModel absensi)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Persensi SET Nis=@Nis,Tanggal=@Tanggal,Keterangan=@Keterangan WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id",absensi.Id,System.Data.DbType.Int32);
                dp.Add("@Nis",absensi.Nis,System.Data.DbType.Int32);
                dp.Add("@Tanggal",absensi.Tanggal,System.Data.DbType.Date);
                dp.Add("@Keterangan",absensi.Keterangan,System.Data.DbType.String);

                 koneksi.Execute(sql,dp);
            }
        }

        public IEnumerable<AbsensiModel> ListData(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT p.ID,p.NIS,s.Nama,s.Persensi,kls.NamaKelas,p.Tanggal,p.Keterangan
                                     FROM Persensi p 
                                     INNER JOIN siswa s ON p.NIS=s.NIS
                                     INNER JOIN Kelas kls ON s.IdKelas = kls.Id 
                                     {sqlc} 
                                     ORDER BY Tanggal DESC OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                return koneksi.Query<AbsensiModel>(sql, dp);
            }
        }

        public AbsensiModel GetData(int ID)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT p.ID,p.NIS,s.Nama,s.Persensi,kls.NamaKelas,p.Tanggal,p.Keterangan
                                     FROM Persensi p 
                                     INNER JOIN siswa s ON p.NIS=s.NIS
                                     INNER JOIN Kelas kls ON s.IdKelas = kls.Id
                                     WHERE p.ID = @ID";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, new {ID=ID});
            }
        }

        public void Delete(int Id)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Persensi WHERE Id=@Id";
                koneksi.Execute(sql, new {Id=Id});
            }
        }

        public IEnumerable<AbsensiModel> Filter(string sql, object param)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                return koneksi.Query<AbsensiModel>(sql,param);
            }
        }

         public int CekRows(string sqlcRow, object dp)
         {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = @"SELECT COUNT(*)
                                    FROM Persensi p
                                    INNER JOIN Siswa s ON p.NIS = s.NIS
                                    INNER JOIN Kelas kls ON s.IdKelas = kls.Id";
                if (sqlcRow != string.Empty) sql += sqlcRow;
                return koneksi.QuerySingle<int>(sql,dp);
            }
         }

        public AbsensiModel GetByCondition(string condition,object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT p.ID,p.NIS,s.Nama,s.Persensi,p.Tanggal,p.Keterangan
                                     FROM siswa s 
                                     INNER JOIN Persensi p ON s.Nis = p.NIS {condition}";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, dp);
            }
        }

        public AbsensiModel GetByAbsensiKelas(string NamaKelas, int Persensi)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT s.NIS, s.Nama FROM siswa s INNER JOIN Kelas k ON k.Id = s.IdKelas WHERE s.Persensi = @Persensi AND k.NamaKelas = @NamaKelas";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, new { Persensi = Persensi, NamaKelas = NamaKelas });
            }
        }
    }
}
