using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace latihribbon
{
    public class RiwayatLogin_UserDal
    {
        public void Insert(RiwayatLoginModel riwayat)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                        INSERT INTO RiwayatLogin 
                            (UserLogin, Tanggal, Waktu)
                        VALUES 
                            (@UserLogin, @Tanggal, @Waktu)";

                var Dp = new DynamicParameters();
                Dp.Add("@UserLogin", riwayat.UserLogin, DbType.String);
                Dp.Add("@Tanggal", riwayat.Tanggal, DbType.DateTime);
                Dp.Add("@Waktu", riwayat.Waktu, DbType.String);

                Conn.Execute(sql, Dp);
            }
        }

        public IEnumerable<RiwayatLoginModel> GetSiswaFilter(string sqlc, object dp)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"SELECT IdLogin, UserLogin , Tanggal, Waktu FROM RiwayatLogin {sqlc} 
                                ORDER BY Tanggal DESC, Waktu DESC LIMIT @Fetch OFFSET @Offset";
                return Conn.Query<RiwayatLoginModel>(sql, dp);
            }
        }

        public void DeleteAfter30Days()
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"DELETE FROM RiwayatLogin
                                    WHERE julianday('now') - julianday(Tanggal) > 29";
                koneksi.Execute(sql);
            }
        }

        // USER CRUD WITH AUDIT LOGGING & TRANSACTIONS
        public void Insert(UserModel userModel)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"
                        INSERT INTO Users
                            (username, password, role, CreatedAt, CreatedBy, IsActive)
                        VALUES 
                            (@username, @password, @role, @CreatedAt, @CreatedBy, 1);
                        SELECT last_insert_rowid();";

                        var Dp = new DynamicParameters();
                        Dp.Add("@username", userModel.username, DbType.String);
                        Dp.Add("@password", userModel.password, DbType.String);
                        Dp.Add("@role", userModel.Role, DbType.String);
                        Dp.Add("@CreatedAt", DateTime.Now);
                        Dp.Add("@CreatedBy", UserSession.CurrentUser);

                        int newId = Conn.QuerySingle<int>(sql, Dp, trans);
                        userModel.Id = newId;

                        // Create sanitised copy for audit log (hide password)
                        var auditCopy = new UserModel
                        {
                            Id = newId,
                            username = userModel.username,
                            password = "***",
                            Role = userModel.Role,
                            CreatedAt = DateTime.Now,
                            CreatedBy = UserSession.CurrentUser,
                            IsActive = 1
                        };

                        LoggingHelper.WriteLog(Conn, trans, "Users", "INSERT", newId, null, auditCopy);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "RiwayatLogin_UserDal.InsertUser");
                        throw;
                    }
                }
            }
        }

        public void Update(UserModel user)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeUser = Conn.QueryFirstOrDefault<UserModel>("SELECT id, username, role, IsActive FROM Users WHERE id = @id", new { id = user.Id }, trans);

                        const string sql = @"
                            UPDATE Users SET 
                                username = @username,
                                password = @password,
                                role = @role,
                                UpdatedAt = @UpdatedAt,
                                UpdatedBy = @UpdatedBy,
                                IsActive = @IsActive
                            WHERE 
                                id = @id";

                        var Dp = new DynamicParameters();
                        Dp.Add("@id", user.Id, DbType.Int32);
                        Dp.Add("@username", user.username, DbType.String);
                        Dp.Add("@password", user.password, DbType.String);
                        Dp.Add("@role", user.Role, DbType.String);
                        Dp.Add("@UpdatedAt", DateTime.Now);
                        Dp.Add("@UpdatedBy", UserSession.CurrentUser);
                        Dp.Add("@IsActive", user.IsActive);

                        Conn.Execute(sql, Dp, trans);

                        var afterUser = new UserModel { Id = user.Id, username = user.username, password = "***", Role = user.Role, IsActive = user.IsActive };

                        LoggingHelper.WriteLog(Conn, trans, "Users", "UPDATE", user.Id, beforeUser, afterUser);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "RiwayatLogin_UserDal.UpdateUser");
                        throw;
                    }
                }
            }
        }

        public void DeleteUser(int idUser)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeUser = Conn.QueryFirstOrDefault<UserModel>("SELECT id, username, role, IsActive, IsSystem FROM Users WHERE id = @id", new { id = idUser }, trans);

                        // Proteksi level DAL: system account tidak dapat dihapus
                        if (beforeUser != null && beforeUser.IsSystem == 1)
                            throw new InvalidOperationException($"User '{beforeUser.username}' adalah system account dan tidak dapat dihapus.");

                        const string sql = @"DELETE FROM Users WHERE id = @id";
                        Conn.Execute(sql, new { id = idUser }, trans);

                        LoggingHelper.WriteLog(Conn, trans, "Users", "DELETE", idUser, beforeUser, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "RiwayatLogin_UserDal.DeleteUser");
                        throw;
                    }
                }
            }
        }

        public void SetIsActiveUser(int idUser, int isActive)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                Conn.Open();
                using (var trans = Conn.BeginTransaction())
                {
                    try
                    {
                        var beforeUser = Conn.QueryFirstOrDefault<UserModel>("SELECT id, username, role, IsActive, IsSystem FROM Users WHERE id = @id", new { id = idUser }, trans);

                        // Proteksi level DAL: system account tidak dapat dinonaktifkan
                        if (beforeUser != null && beforeUser.IsSystem == 1)
                            throw new InvalidOperationException($"User '{beforeUser.username}' adalah system account dan tidak dapat dinonaktifkan.");

                        const string sql = @"UPDATE Users SET IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy WHERE id = @id";
                        Conn.Execute(sql, new { id = idUser, IsActive = isActive, UpdatedAt = DateTime.Now, UpdatedBy = UserSession.CurrentUser }, trans);

                        var afterUser = Conn.QueryFirstOrDefault<UserModel>("SELECT id, username, role, IsActive FROM Users WHERE id = @id", new { id = idUser }, trans);

                        string actionStr = isActive == 1 ? "ACTIVATE" : "DEACTIVATE";
                        LoggingHelper.WriteLog(Conn, trans, "Users", actionStr, idUser, beforeUser, afterUser);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "RiwayatLogin_UserDal.SetIsActiveUser");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<UserModel> ListUser(bool includeInactive = false)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                string sql = @"SELECT id, username, Role, IsActive, COALESCE(IsSystem, 0) AS IsSystem FROM Users ";
                if (!includeInactive)
                {
                    sql += "WHERE (IsActive = 1 OR IsActive IS NULL) ";
                }
                sql += "ORDER BY id ASC";

                return Conn.Query<UserModel>(sql);
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"SELECT COUNT(*) FROM RiwayatLogin {sqlc}";
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }

        public void DeleteOtomatis(DateTime tanggal)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string sql = $"DELETE FROM RiwayatLogin WHERE Tanggal <= @Tanggal";
                koneksi.Execute(sql, new { Tanggal = tanggal });
            }
        }

        public void UpdateUserRiwayat(string UserLogin, string userLama)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"UPDATE RiwayatLogin SET UserLogin = @UserLogin WHERE UserLogin = @userLama";
                koneksi.Execute(sql, new { UserLogin = UserLogin, userLama = userLama });
            }
        }

        public bool ExistUsername(string username)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT 1 FROM Users WHERE username = @username";
                return koneksi.QuerySingleOrDefault<bool>(sql, new { username });
            }
        }
    }
}
