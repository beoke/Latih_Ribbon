using Dapper;
using latihribbon.Conn;
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
        public IEnumerable<SiswaModel> ListData(string sqlc,object dp)
        {
                using (var koneksi = new SqlConnection(Conn.conn.connstr()))
                {
                    string sql = $@"SELECT s.Nis,s.Nama,s.JenisKelamin,s.Persensi,k.NamaKelas,s.Tahun FROM siswa s 
                                INNER JOIN Kelas k ON s.IdKelas = k.Id {sqlc} 
                                ORDER BY  
                                    CASE
                                        WHEN k.NamaKelas LIKE 'X %' THEN 1
                                        WHEN k.NamaKelas LIKE 'XI %' THEN 2
                                        WHEN k.NamaKelas LIKE 'XII %' THEN 3
                                        ELSE 4
                                    END,
                                    SUBSTRING( k.NamaKelas, CHARINDEX(' ', k.NamaKelas) + 1, LEN(k.NamaKelas)) ASC ,
                                    s.IdKelas ASC, s.Persensi ASC
                                    OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                    return koneksi.Query<SiswaModel>(sql, dp);
                }
        }

       

        public SiswaModel GetData(int Nis)
        {
                using (var koneksi = new SqlConnection(Conn.conn.connstr()))
                {
                    const string sql = @"SELECT s.Nis,s.Nama,s.JenisKelamin,s.Persensi,s.IdKelas,k.NamaKelas,s.Tahun FROM siswa s 
                                INNER JOIN Kelas k ON s.IdKelas = k.Id
                                WHERE s.Nis=@Nis";
                    return koneksi.QueryFirstOrDefault<SiswaModel>(sql, new { Nis = Nis });
                }
        }

        public void Insert(SiswaModel siswa)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO siswa(Nis,Nama,JenisKelamin,Persensi,IdKelas,Tahun)
                                VALUES(@Nis,@Nama,@JenisKelamin,@Persensi,@IdKelas,@Tahun)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                dp.Add("@JenisKelamin", siswa.JenisKelamin, System.Data.DbType.String);
                dp.Add("@Persensi", siswa.Persensi, System.Data.DbType.Int16);
                dp.Add("@IdKelas", siswa.IdKelas, System.Data.DbType.Int32);
                dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
            }
        }





        public void Update(SiswaModel siswa, int oldNis)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var transaction = koneksi.BeginTransaction())
                {
                    try
                    {
                        string sql = $@"UPDATE siswa SET Nama = @Nama, JenisKelamin = @JenisKelamin, Persensi = @Persensi, IdKelas = @IdKelas, Tahun = @Tahun, Nis = @Nis
                               WHERE Nis = {oldNis}";
                        var dp = new DynamicParameters();
                        dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                        dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                        dp.Add("@JenisKelamin", siswa.JenisKelamin, System.Data.DbType.String);
                        dp.Add("@Persensi", siswa.Persensi, System.Data.DbType.Int16);
                        dp.Add("@IdKelas", siswa.IdKelas, System.Data.DbType.Int32);
                        dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);

                        koneksi.Execute(sql, dp, transaction);
                          
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public void Delete(int siswaNis)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM siswa WHERE Nis=@Nis";
                koneksi.Execute(sql, new { Nis = siswaNis });
            }
        }

        public int CekDataSiswa()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT COUNT(*) FROM siswa";
                return koneksi.QuerySingleOrDefault<int>(sql);
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT COUNT(*) FROM siswa s INNER JOIN Kelas k ON s.IdKelas = k.Id {sqlc}";
                return koneksi.QuerySingle<int>(sql,dp);
            }
        }
    }
}



