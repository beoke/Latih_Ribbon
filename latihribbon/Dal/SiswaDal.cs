using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class SiswaDal
    {
        public IEnumerable<SiswaModel> ListData(string sqlc, object dp, bool includeInactive = false)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string activeFilter = includeInactive ? "" : " (s.IsActive = 1 OR s.IsActive IS NULL) ";
                string whereClause = "";
                if (!string.IsNullOrWhiteSpace(sqlc))
                {
                    whereClause = sqlc;
                    if (!includeInactive)
                    {
                        whereClause += " AND " + activeFilter;
                    }
                }
                else if (!includeInactive)
                {
                    whereClause = " WHERE " + activeFilter;
                }

                string sql = $@"SELECT s.Nis, s.Nama, s.JenisKelamin, s.Persensi, s.IdKelas, k.NamaKelas, s.Tahun, s.IsActive 
                                FROM siswa s 
                                INNER JOIN Kelas k ON s.IdKelas = k.Id
                                INNER JOIN Jurusan j ON k.IdJurusan = j.Id
                                {whereClause} 
                                ORDER BY  
                                    CASE
                                        WHEN k.NamaKelas LIKE 'X %' THEN 1
                                        WHEN k.NamaKelas LIKE 'XI %' THEN 2
                                        WHEN k.NamaKelas LIKE 'XII %' THEN 3
                                        ELSE 4
                                    END,
                                    j.Kode ASC,
                                    s.IdKelas ASC, s.Persensi ASC
                                    LIMIT @Fetch OFFSET @Offset";
                return koneksi.Query<SiswaModel>(sql, dp);
            }
        }

        public SiswaModel GetData(int Nis)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT s.Nis, s.Nama, s.JenisKelamin, s.Persensi, s.IdKelas, k.NamaKelas, s.Tahun, s.IsActive 
                                    FROM siswa s 
                                    INNER JOIN Kelas k ON s.IdKelas = k.Id
                                    WHERE s.Nis=@Nis";
                return koneksi.QueryFirstOrDefault<SiswaModel>(sql, new { Nis = Nis });
            }
        }

        public void Insert(SiswaModel siswa)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"INSERT INTO siswa(Nis, Nama, JenisKelamin, Persensi, IdKelas, Tahun, CreatedAt, CreatedBy, IsActive)
                                            VALUES(@Nis, @Nama, @JenisKelamin, @Persensi, @IdKelas, @Tahun, @CreatedAt, @CreatedBy, 1)";
                        var dp = new DynamicParameters();
                        dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                        dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                        dp.Add("@JenisKelamin", siswa.JenisKelamin, System.Data.DbType.String);
                        dp.Add("@Persensi", siswa.Persensi, System.Data.DbType.Int16);
                        dp.Add("@IdKelas", siswa.IdKelas, System.Data.DbType.Int32);
                        dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);
                        dp.Add("@CreatedAt", DateTime.Now);
                        dp.Add("@CreatedBy", UserSession.CurrentUser);

                        koneksi.Execute(sql, dp, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "siswa", "INSERT", siswa.Nis, null, siswa);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "SiswaDal.Insert");
                        throw;
                    }
                }
            }
        }

        public void Update(SiswaModel siswa, int oldNis)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<SiswaModel>("SELECT * FROM siswa WHERE Nis=@oldNis", new { oldNis = oldNis }, trans);

                        string sql = @"UPDATE siswa SET 
                                        Nama = @Nama, JenisKelamin = @JenisKelamin, Persensi = @Persensi, 
                                        IdKelas = @IdKelas, Tahun = @Tahun, Nis = @Nis,
                                        UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy, IsActive = @IsActive
                                       WHERE Nis = @oldNis";
                        var dp = new DynamicParameters();
                        dp.Add("@oldNis", oldNis, System.Data.DbType.Int32);
                        dp.Add("@Nis", siswa.Nis, System.Data.DbType.Int32);
                        dp.Add("@Nama", siswa.Nama, System.Data.DbType.String);
                        dp.Add("@JenisKelamin", siswa.JenisKelamin, System.Data.DbType.String);
                        dp.Add("@Persensi", siswa.Persensi, System.Data.DbType.Int16);
                        dp.Add("@IdKelas", siswa.IdKelas, System.Data.DbType.Int32);
                        dp.Add("@Tahun", siswa.Tahun, System.Data.DbType.String);
                        dp.Add("@UpdatedAt", DateTime.Now);
                        dp.Add("@UpdatedBy", UserSession.CurrentUser);
                        dp.Add("@IsActive", siswa.IsActive);

                        koneksi.Execute(sql, dp, trans);

                        var afterData = koneksi.QueryFirstOrDefault<SiswaModel>("SELECT * FROM siswa WHERE Nis=@Nis", new { Nis = siswa.Nis }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "siswa", "UPDATE", siswa.Nis, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "SiswaDal.Update");
                        throw;
                    }
                }
            }
        }

        public void Delete(int siswaNis)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<SiswaModel>("SELECT * FROM siswa WHERE Nis=@Nis", new { Nis = siswaNis }, trans);

                        const string sql = @"DELETE FROM siswa WHERE Nis=@Nis";
                        koneksi.Execute(sql, new { Nis = siswaNis }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "siswa", "DELETE", siswaNis, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "SiswaDal.Delete");
                        throw;
                    }
                }
            }
        }

        public void SetIsActive(int nis, int isActive)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<SiswaModel>("SELECT * FROM siswa WHERE Nis=@Nis", new { Nis = nis }, trans);

                        const string sql = @"UPDATE siswa SET IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy WHERE Nis=@Nis";
                        koneksi.Execute(sql, new { Nis = nis, IsActive = isActive, UpdatedAt = DateTime.Now, UpdatedBy = UserSession.CurrentUser }, trans);

                        var afterData = koneksi.QueryFirstOrDefault<SiswaModel>("SELECT * FROM siswa WHERE Nis=@Nis", new { Nis = nis }, trans);

                        string actionStr = isActive == 1 ? "ACTIVATE" : "DEACTIVATE";
                        LoggingHelper.WriteLog(koneksi, trans, "siswa", actionStr, nis, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "SiswaDal.SetIsActive");
                        throw;
                    }
                }
            }
        }

        public int CekDataSiswa()
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT COUNT(*) FROM siswa WHERE IsActive = 1 OR IsActive IS NULL";
                return koneksi.QuerySingleOrDefault<int>(sql);
            }
        }

        public int CekRows(string sqlc, object dp, bool includeInactive = false)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string activeFilter = includeInactive ? "" : " (s.IsActive = 1 OR s.IsActive IS NULL) ";
                string whereClause = "";
                if (!string.IsNullOrWhiteSpace(sqlc))
                {
                    whereClause = sqlc;
                    if (!includeInactive)
                    {
                        whereClause += " AND " + activeFilter;
                    }
                }
                else if (!includeInactive)
                {
                    whereClause = " WHERE " + activeFilter;
                }

                string sql = $@"SELECT COUNT(*) FROM siswa s INNER JOIN Kelas k ON s.IdKelas = k.Id {whereClause}";
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }
    }
}
