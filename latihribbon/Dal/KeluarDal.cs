﻿using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class KeluarDal
    {
        public IEnumerable<KeluarModel> ListData(string sqlc, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT k.Id,k.Nis,s.Nama,kls.NamaKelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k 
                            INNER JOIN siswa s ON k.Nis=s.Nis
                            INNER JOIN kelas kls ON s.IdKelas = kls.Id 
                            {sqlc} 
                            ORDER BY k.Tanggal DESC, k.JamKeluar DESC LIMIT @Fetch OFFSET @Offset";
                return koneksi.Query<KeluarModel>(sql,dp);
            }
        }

       

        public KeluarModel GetData(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
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
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Keluar(Nis,Tanggal,JamKeluar,JamMasuk,Tujuan)
                                VALUES(@Nis,@Tanggal,@JamKeluar,@JamMasuk,@Tujuan)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", keluar.Nis, System.Data.DbType.Int32);
                dp.Add("@Tanggal", keluar.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamKeluar", keluar.JamKeluar, System.Data.DbType.String);
                dp.Add("@JamMasuk", keluar.JamMasuk, System.Data.DbType.String);
                dp.Add("@Tujuan", keluar.Tujuan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        public void Update(KeluarModel keluar)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Keluar SET Nis=@Nis,Tanggal=@Tanggal,JamKeluar=@JamKeluar,
                                    JamMasuk=@JamMasuk,Tujuan=@Tujuan WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id", keluar.Id, System.Data.DbType.Int32);
                dp.Add("@Nis", keluar.Nis, System.Data.DbType.Int32);
                dp.Add("@Tanggal", keluar.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamKeluar", keluar.JamKeluar, System.Data.DbType.String);
                dp.Add("@JamMasuk", keluar.JamMasuk, System.Data.DbType.String);
                dp.Add("@Tujuan", keluar.Tujuan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        public void Delete(int IdKeluar)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Keluar WHERE Id=@Id";
                koneksi.Execute(sql, new { Id=IdKeluar });
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = @"SELECT COUNT(*) FROM Keluar k
                                INNER JOIN siswa s ON k.Nis=s.Nis
                                INNER JOIN kelas kls ON s.IdKelas = kls.Id";
                if (sqlc != string.Empty) sql += sqlc;
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }

        public IEnumerable<KeluarModel> ListData2(DateTime tgl1, DateTime tgl2)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT s.NIS,s.Nama,kls.NamaKelas,k.Tanggal,k.Tujuan
                                    FROM Siswa s
                                        INNER JOIN Kelas kls ON s.IdKelas = kls.Id
	                                    INNER JOIN Keluar k ON s.Nis = k.Nis
	                                    INNER JOIN Jurusan j ON kls.idJurusan = j.Id
                                    WHERE k.Tanggal BETWEEN @tgl1 AND @tgl2
                                    ORDER BY
		                                    CASE
			                                    WHEN kls.Tingkat = 'X' THEN 1
			                                    WHEN kls.Tingkat = 'XI' THEN 2
			                                    WHEN kls.Tingkat = 'XII' THEN 3
			                                    ELSE 4
		                                    END,j.NamaJurusan ASC, kls.Rombel ASC,
		                                    s.Persensi ASC, k.Tanggal ASC";
                return koneksi.Query<KeluarModel>(sql, new { tgl1 = tgl1, tgl2 = tgl2 });
            }
        }
    }
}
