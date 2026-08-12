using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class KelasDal
    {
        public IEnumerable<KelasModel> listKelas(string sqlc, object dp, bool includeInactive = false)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string activeWhere = includeInactive ? "" : " (k.IsActive = 1 OR k.IsActive IS NULL) ";
                string whereClause = "";
                if (!string.IsNullOrWhiteSpace(sqlc))
                {
                    whereClause = sqlc;
                    if (!includeInactive)
                    {
                        whereClause += " AND " + activeWhere;
                    }
                }
                else if (!includeInactive)
                {
                    whereClause = " WHERE " + activeWhere;
                }

                string sql = $@"SELECT k.Id, k.NamaKelas, k.Rombel, k.IdJurusan, k.Tingkat, k.status, k.IsActive, j.Kode FROM Kelas k
                                INNER JOIN Jurusan j ON k.IdJurusan=j.Id {whereClause}
                                ORDER BY CASE 
                                        WHEN k.Tingkat = '' THEN 1
                                        WHEN k.Tingkat = 'X' THEN 2
                                        WHEN k.Tingkat = 'XI' THEN 3
                                        WHEN k.Tingkat = 'XII' THEN 4
                                        WHEN k.Tingkat = 'LULUS' THEN 5
                                        ELSE 6
                                    END, idJurusan, Rombel";
                return koneksi.Query<KelasModel>(sql, dp);
            }
        }

        public void Insert(KelasModel kelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"INSERT INTO Kelas(NamaKelas, Rombel, IdJurusan, Tingkat, status, CreatedAt, CreatedBy, IsActive)
                                            VALUES(@NamaKelas, @Rombel, @idJurusan, @Tingkat, @status, @CreatedAt, @CreatedBy, 1);
                                            SELECT last_insert_rowid();";
                        var dp = new DynamicParameters();
                        dp.Add("@NamaKelas", kelas.NamaKelas, System.Data.DbType.String);
                        dp.Add("@Rombel", kelas.Rombel, System.Data.DbType.String);
                        dp.Add("@idJurusan", kelas.IdJurusan, System.Data.DbType.Int32);
                        dp.Add("@Tingkat", kelas.Tingkat, System.Data.DbType.String);
                        dp.Add("@status", kelas.status, System.Data.DbType.Int16);
                        dp.Add("@CreatedAt", DateTime.Now);
                        dp.Add("@CreatedBy", UserSession.CurrentUser);

                        int newId = koneksi.QuerySingle<int>(sql, dp, trans);
                        kelas.Id = newId;

                        LoggingHelper.WriteLog(koneksi, trans, "Kelas", "INSERT", newId, null, kelas);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KelasDal.Insert");
                        throw;
                    }
                }
            }
        }

        public void Update(KelasModel kelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<KelasModel>("SELECT * FROM Kelas WHERE Id=@Id", new { Id = kelas.Id }, trans);

                        const string sql = @"UPDATE Kelas SET 
                                            NamaKelas=@NamaKelas, Rombel=@Rombel, idJurusan=@idJurusan, Tingkat=@Tingkat, status=@status,
                                            UpdatedAt=@UpdatedAt, UpdatedBy=@UpdatedBy, IsActive=@IsActive 
                                            WHERE Id=@Id";
                        var dp = new DynamicParameters();
                        dp.Add("@Id", kelas.Id, System.Data.DbType.Int32);
                        dp.Add("@NamaKelas", kelas.NamaKelas, System.Data.DbType.String);
                        dp.Add("@Rombel", kelas.Rombel, System.Data.DbType.String);
                        dp.Add("@idJurusan", kelas.IdJurusan, System.Data.DbType.Int32);
                        dp.Add("@Tingkat", kelas.Tingkat, System.Data.DbType.String);
                        dp.Add("@status", kelas.status, System.Data.DbType.Int16);
                        dp.Add("@UpdatedAt", DateTime.Now);
                        dp.Add("@UpdatedBy", UserSession.CurrentUser);
                        dp.Add("@IsActive", kelas.IsActive);

                        koneksi.Execute(sql, dp, trans);

                        var afterData = koneksi.QueryFirstOrDefault<KelasModel>("SELECT * FROM Kelas WHERE Id=@Id", new { Id = kelas.Id }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Kelas", "UPDATE", kelas.Id, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KelasDal.Update");
                        throw;
                    }
                }
            }
        }

        public void Delete(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<KelasModel>("SELECT * FROM Kelas WHERE Id=@Id", new { Id = Id }, trans);

                        const string sql = @"DELETE FROM Kelas WHERE Id=@Id";
                        koneksi.Execute(sql, new { Id = Id }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Kelas", "DELETE", Id, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KelasDal.Delete");
                        throw;
                    }
                }
            }
        }

        public void SetIsActive(int Id, int isActive)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<KelasModel>("SELECT * FROM Kelas WHERE Id=@Id", new { Id = Id }, trans);

                        const string sql = @"UPDATE Kelas SET IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy WHERE Id=@Id";
                        koneksi.Execute(sql, new { Id = Id, IsActive = isActive, UpdatedAt = DateTime.Now, UpdatedBy = UserSession.CurrentUser }, trans);

                        var afterData = koneksi.QueryFirstOrDefault<KelasModel>("SELECT * FROM Kelas WHERE Id=@Id", new { Id = Id }, trans);

                        string actionStr = isActive == 1 ? "ACTIVATE" : "DEACTIVATE";
                        LoggingHelper.WriteLog(koneksi, trans, "Kelas", actionStr, Id, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "KelasDal.SetIsActive");
                        throw;
                    }
                }
            }
        }

        public KelasModel GetData(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT k.Id, k.NamaKelas, k.Rombel, k.IdJurusan, k.Tingkat, k.status, k.IsActive, j.Kode 
                                    FROM Kelas k INNER JOIN Jurusan j ON k.IdJurusan = j.Id
                                    WHERE k.Id=@Id";
                return koneksi.QueryFirstOrDefault<KelasModel>(sql, new { Id = Id });
            }
        }

        public bool CekDeleteIsValid(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sqlCek = @"SELECT k.Id, k.Namakelas, s.Nis
                            FROM Kelas k
                            LEFT JOIN Siswa s ON s.idKelas = k.Id
                            WHERE s.idKelas ISNULL AND k.Id = @Id";
                return koneksi.QuerySingleOrDefault<bool>(sqlCek, new { Id = Id });
            }
        }

        public IEnumerable<KelasModel> GetDataRombel(int idJurusan, string Tingkat)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT Rombel, Id FROM Kelas WHERE idJurusan=@idJurusan AND Tingkat=@Tingkat AND (IsActive = 1 OR IsActive IS NULL)";
                return koneksi.Query<KelasModel>(sql, new { idJurusan = idJurusan, Tingkat = Tingkat });
            }
        }

        public int GetIdKelas(string NamaKelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = @"SELECT Id FROM Kelas WHERE NamaKelas = @NamaKelas";
                return koneksi.QueryFirstOrDefault<int>(sql, new { NamaKelas = NamaKelas });
            }
        }

        public void DeleteDataLulus()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM kelas
                                    WHERE status = 0 
                                    AND Id NOT IN (SELECT DISTINCT IdKelas FROM siswa)";
                koneksi.Execute(sql);
            }
        }

        public bool CekDuplikasi(KelasModel kelas, bool update = false)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = @"SELECT 1 FROM Kelas WHERE 
                    Tingkat = @Tingkat AND idJurusan = @idJurusan ";
                if (!string.IsNullOrEmpty(kelas.Rombel))
                    sql += "AND Rombel = @Rombel ";
                if (update)
                    sql += "AND Id <> @Id ";

                var dp = new DynamicParameters();
                dp.Add("@Tingkat", kelas.Tingkat);
                dp.Add("@idJurusan", kelas.IdJurusan);
                dp.Add("@Rombel", kelas.Rombel);
                dp.Add("@Id", kelas.Id);

                return koneksi.QuerySingleOrDefault<bool>(sql, dp);
            }
        }

        public void UpdateNamaKelas(int Id, string NamaKelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Kelas SET NamaKelas=@NamaKelas WHERE Id=@Id";
                koneksi.Execute(sql, new {Id=Id, NamaKelas=NamaKelas});
            }
        }

        public void DuplikatKelas(string Tingkat)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Kelas(NamaKelas,Rombel,IdJurusan,Tingkat, status)
                                    SELECT NamaKelas, Rombel, IdJurusan, Tingkat, status FROM Kelas
                                    WHERE Tingkat = @Tingkat";
                koneksi.Execute(sql, new { Tingkat = Tingkat });
            }
        }

        public bool TurunkanKelas()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sqlTurun = @"DELETE FROM kelas WHERE Tingkat = 'X'";
                const string sqlCek = @"SELECT COUNT(*) FROM Siswa s 
                                        INNER JOIN Kelas k ON s.idKelas = k.Id 
                                        WHERE k.Tingkat = 'X'";
                if (koneksi.QuerySingleOrDefault<int>(sqlCek) == 0)
                {
                    koneksi.Execute(sqlTurun);
                    return true;
                }
                return false;
            }
        }

        public int DeleteSiswaLulus()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Kelas WHERE status = 0";
                return koneksi.Execute(sql);
            }
        }

        public bool cekLulus()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT COUNT(1) FROM Kelas WHERE status = 0";
                int count = koneksi.ExecuteScalar<int>(sql);
                return count > 0;
            }
        }
    }
}
