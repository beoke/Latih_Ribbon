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
                const string sql = @"SELECT Nis,Nama,JenisKelamin,Persensi,Kelas,Tahun FROM Siswa
                                    ORDER BY 
                            Persensi ASC,
                            CASE
                                WHEN Kelas LIKE 'X %' THEN 1
                                WHEN Kelas LIKE 'XI %' THEN 2
                                WHEN Kelas LIKE 'XII %' THEN 3
                                ELSE 4
                            END,
                            SUBSTRING( Kelas, CHARINDEX(' ', Kelas) + 1, LEN(Kelas)) ASC ";
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

        public SiswaModel GetData(int Nis)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT * FROM siswa WHERE Nis=@Nis";
                return koneksi.QueryFirstOrDefault<SiswaModel>(sql, new {Nis=Nis});
            }
        }

        public void Insert(SiswaModel siswa)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO siswa(Nis,Nama,JenisKelamin,PersensiKelas,Tahun)
                                VALUES(@Nis,@Nama,@JenisKelamin,@Persensi,@Kelas,@Tahun)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                dp.Add("@JenisKelamin", siswa.JenisKelamin, System.Data.DbType.String);
                dp.Add("@Persensi", siswa.Persensi, System.Data.DbType.Int16);
                dp.Add("@Kelas", siswa.Kelas, System.Data.DbType.String);
                dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
            }
        }

        public void Update(SiswaModel siswa)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE siswa SET Nama=@Nama,JenisKelamin=@JenisKelamin,Persensi=@Persensi,Kelas=@Kelas,Tahun=@Tahun
                                     WHERE Nis = @Nis";
                var dp = new DynamicParameters();
                dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                dp.Add("@JenisKelamin", siswa.JenisKelamin, System.Data.DbType.String);
                dp.Add("@Persensi", siswa.Persensi, System.Data.DbType.Int16);
                dp.Add("@Kelas", siswa.Kelas, System.Data.DbType.String);
                dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
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
    }
}



