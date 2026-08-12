using Dapper;
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
                string sql = $@"SELECT k.Id, k.Nis, s.Nama, kls.NamaKelas, k.Tanggal, k.JamKeluar, k.JamMasuk, k.Tujuan 
                            FROM Keluar k 
                            INNER JOIN siswa s ON k.Nis=s.Nis
                            INNER JOIN kelas kls ON s.IdKelas = kls.Id 
                            {sqlc} 
                            ORDER BY k.Tanggal DESC, k.JamKeluar DESC LIMIT @Fetch OFFSET @Offset";
                return koneksi.Query<KeluarModel>(sql, dp);
            }
        }

        public KeluarModel GetData(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT k.Id, k.Nis, s.Nama, kls.NamaKelas, k.Tanggal, k.JamKeluar, k.JamMasuk, k.Tujuan 
                            FROM Keluar k 
                            INNER JOIN siswa s ON k.Nis=s.Nis
                            INNER JOIN kelas kls ON s.IdKelas = kls.Id 
                            WHERE k.Id = @Id";
                return koneksi.QueryFirstOrDefault<KeluarModel>(sql, new { Id = Id });
            }
        }

        public void Insert(KeluarModel keluar)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"INSERT INTO Keluar(Nis, Tanggal, JamKeluar, JamMasuk, Tujuan, CreatedAt, CreatedBy)
                                        VALUES(@Nis, @Tanggal, @JamKeluar, @JamMasuk, @Tujuan, @CreatedAt, @CreatedBy);
                                        SELECT last_insert_rowid();";
                        var dp = new DynamicParameters();
                        dp.Add("@Nis", keluar.Nis, System.Data.DbType.Int32);
                        dp.Add("@Tanggal", keluar.Tanggal, System.Data.DbType.Date);
                        dp.Add("@JamKeluar", keluar.JamKeluar, System.Data.DbType.String);
                        dp.Add("@JamMasuk", keluar.JamMasuk, System.Data.DbType.String);
                        dp.Add("@Tujuan", keluar.Tujuan, System.Data.DbType.String);
                        dp.Add("@CreatedAt", DateTime.Now);
                        dp.Add("@CreatedBy", UserSession.CurrentUser);

                        int newId = koneksi.QuerySingle<int>(sql, dp, trans);
                        keluar.Id = newId;

                        LoggingHelper.WriteLog(koneksi, trans, "Keluar", "INSERT", newId, null, keluar);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KeluarDal.Insert");
                        throw;
                    }
                }
            }
        }

        public void Update(KeluarModel keluar)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<KeluarModel>("SELECT * FROM Keluar WHERE Id=@Id", new { Id = keluar.Id }, trans);

                        const string sql = @"UPDATE Keluar SET Nis=@Nis, Tanggal=@Tanggal, JamKeluar=@JamKeluar,
                                            JamMasuk=@JamMasuk, Tujuan=@Tujuan, UpdatedAt=@UpdatedAt, UpdatedBy=@UpdatedBy WHERE Id=@Id";
                        var dp = new DynamicParameters();
                        dp.Add("@Id", keluar.Id, System.Data.DbType.Int32);
                        dp.Add("@Nis", keluar.Nis, System.Data.DbType.Int32);
                        dp.Add("@Tanggal", keluar.Tanggal, System.Data.DbType.Date);
                        dp.Add("@JamKeluar", keluar.JamKeluar, System.Data.DbType.String);
                        dp.Add("@JamMasuk", keluar.JamMasuk, System.Data.DbType.String);
                        dp.Add("@Tujuan", keluar.Tujuan, System.Data.DbType.String);
                        dp.Add("@UpdatedAt", DateTime.Now);
                        dp.Add("@UpdatedBy", UserSession.CurrentUser);

                        koneksi.Execute(sql, dp, trans);

                        var afterData = koneksi.QueryFirstOrDefault<KeluarModel>("SELECT * FROM Keluar WHERE Id=@Id", new { Id = keluar.Id }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Keluar", "UPDATE", keluar.Id, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KeluarDal.Update");
                        throw;
                    }
                }
            }
        }

        public void Delete(int IdKeluar)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<KeluarModel>("SELECT * FROM Keluar WHERE Id=@Id", new { Id = IdKeluar }, trans);

                        const string sql = @"DELETE FROM Keluar WHERE Id=@Id";
                        koneksi.Execute(sql, new { Id = IdKeluar }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Keluar", "DELETE", IdKeluar, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KeluarDal.Delete");
                        throw;
                    }
                }
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
                const string sql = @"SELECT s.NIS, s.Nama, kls.NamaKelas, k.Tanggal, k.Tujuan
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
		                                    END, j.NamaJurusan ASC, kls.Rombel ASC,
		                                    s.Persensi ASC, k.Tanggal ASC";
                return koneksi.Query<KeluarModel>(sql, new { tgl1 = tgl1, tgl2 = tgl2 });
            }
        }
    }
}
