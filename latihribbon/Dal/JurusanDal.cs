using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class JurusanDal
    {
        public IEnumerable<JurusanModel> ListData(bool includeInactive = false)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = "SELECT * FROM Jurusan ";
                if (!includeInactive)
                {
                    sql += "WHERE IsActive = 1 ";
                }
                sql += "ORDER BY Id ASC";
                return Conn.Query<JurusanModel>(sql);
            }
        }

        public void Insert(string kode, string namaJurusan)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"
                            INSERT INTO Jurusan
                                (NamaJurusan, Kode, CreatedAt, CreatedBy, IsActive)
                            VALUES
                                (@NamaJurusan, @Kode, @CreatedAt, @CreatedBy, 1);
                            SELECT last_insert_rowid();";

                        var Dp = new DynamicParameters();
                        Dp.Add("@NamaJurusan", namaJurusan, System.Data.DbType.String);
                        Dp.Add("@Kode", kode, System.Data.DbType.String);
                        Dp.Add("@CreatedAt", DateTime.Now);
                        Dp.Add("@CreatedBy", UserSession.CurrentUser);

                        int newId = Conn.QuerySingle<int>(sql, Dp, trans);

                        var createdObj = new JurusanModel
                        {
                            Id = newId,
                            Kode = kode,
                            NamaJurusan = namaJurusan,
                            CreatedAt = DateTime.Now,
                            CreatedBy = UserSession.CurrentUser,
                            IsActive = 1
                        };

                        LoggingHelper.WriteLog(Conn, trans, "Jurusan", "INSERT", newId, null, createdObj);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "JurusanDal.Insert");
                        throw;
                    }
                }
            }
        }

        public void Update(JurusanModel jurusan)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeData = Conn.QueryFirstOrDefault<JurusanModel>("SELECT * FROM Jurusan WHERE Id=@Id", new { Id = jurusan.Id }, trans);

                        const string sql = @"
                            UPDATE Jurusan
                            SET
                                NamaJurusan = @NamaJurusan, 
                                Kode = @Kode,
                                UpdatedAt = @UpdatedAt,
                                UpdatedBy = @UpdatedBy,
                                IsActive = @IsActive
                            WHERE
                                Id = @Id";

                        var Dp = new DynamicParameters();
                        Dp.Add("@Id", jurusan.Id, System.Data.DbType.Int32);
                        Dp.Add("@NamaJurusan", jurusan.NamaJurusan, System.Data.DbType.String);
                        Dp.Add("@Kode", jurusan.Kode, System.Data.DbType.String);
                        Dp.Add("@UpdatedAt", DateTime.Now);
                        Dp.Add("@UpdatedBy", UserSession.CurrentUser);
                        Dp.Add("@IsActive", jurusan.IsActive);

                        Conn.Execute(sql, Dp, trans);

                        var afterData = Conn.QueryFirstOrDefault<JurusanModel>("SELECT * FROM Jurusan WHERE Id=@Id", new { Id = jurusan.Id }, trans);

                        LoggingHelper.WriteLog(Conn, trans, "Jurusan", "UPDATE", jurusan.Id, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "JurusanDal.Update");
                        throw;
                    }
                }
            }
        }

        public void Delete(int JurusanId)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeData = Conn.QueryFirstOrDefault<JurusanModel>("SELECT * FROM Jurusan WHERE Id=@Id", new { Id = JurusanId }, trans);

                        const string sql = @"DELETE FROM Jurusan WHERE Id = @Id";
                        var Dp = new DynamicParameters();
                        Dp.Add("@Id", JurusanId, System.Data.DbType.Int32);

                        Conn.Execute(sql, Dp, trans);

                        LoggingHelper.WriteLog(Conn, trans, "Jurusan", "DELETE", JurusanId, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "JurusanDal.Delete");
                        throw;
                    }
                }
            }
        }

        public void SetIsActive(int JurusanId, int isActive)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeData = Conn.QueryFirstOrDefault<JurusanModel>("SELECT * FROM Jurusan WHERE Id=@Id", new { Id = JurusanId }, trans);

                        const string sql = @"UPDATE Jurusan SET IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy WHERE Id = @Id";
                        Conn.Execute(sql, new { Id = JurusanId, IsActive = isActive, UpdatedAt = DateTime.Now, UpdatedBy = UserSession.CurrentUser }, trans);

                        var afterData = Conn.QueryFirstOrDefault<JurusanModel>("SELECT * FROM Jurusan WHERE Id=@Id", new { Id = JurusanId }, trans);

                        string actionStr = isActive == 1 ? "ACTIVATE" : "DEACTIVATE";
                        LoggingHelper.WriteLog(Conn, trans, "Jurusan", actionStr, JurusanId, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "JurusanDal.SetIsActive");
                        throw;
                    }
                }
            }
        }

        public bool CekDeleteIsValid(int JurusanId)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT j.Id, k.Namakelas
                        FROM Jurusan j
                        LEFT JOIN Kelas k ON k.idJurusan = j.Id
                        WHERE k.idJurusan ISNULL AND j.Id = @Id";
                var Dp = new DynamicParameters();
                Dp.Add("@Id", JurusanId, System.Data.DbType.Int32);

                return Conn.QuerySingleOrDefault<bool>(sql, Dp);
            }
        }

        public bool CekDuplikasi(string Kode, string namaJurusan)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT Id FROM Jurusan WHERE Kode = @Kode OR namaJurusan = @namaJurusan";
                return koneksi.QuerySingleOrDefault<bool>(sql, new { Kode, namaJurusan });
            }
        }

        public bool CekDuplikasiUpdate(int Id, string Kode, string namaJurusan)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT Id FROM Jurusan WHERE (Kode = @Kode OR namaJurusan = @namaJurusan) AND Id <> @Id";
                return koneksi.QuerySingleOrDefault<bool>(sql, new { Kode, namaJurusan, Id });
            }
        }

        public JurusanModel GetData(int Id)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM Jurusan WHERE Id=@Id";
                return koneksi.QueryFirstOrDefault<JurusanModel>(sql, new { Id = Id });
            }
        }
    }
}
