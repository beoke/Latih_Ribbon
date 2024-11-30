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
                                ORDER BY Tanggal DESC,Waktu DESC LIMIT @Fetch OFFSET @Offset";
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


        //USER
        public void Insert(UserModel userModel)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                INSERT INTO Users
                    (username, password, role)
                VALUES 
                    (@username, @password, @role)";

                var Dp = new DynamicParameters();
                Dp.Add("@username", userModel.username, DbType.String);
                Dp.Add("@password", userModel.password, DbType.String);
                Dp.Add("@role", userModel.Role, DbType.String);

                Conn.Execute(sql, Dp);
            }
        }

        public void Update(UserModel user)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                    UPDATE Users SET 
                        username = @username,
                        password = @password,
                        role = @role
                    WHERE 
                        id = @id";

                var Dp = new DynamicParameters();
                Dp.Add("@id", user.Id, DbType.Int32);
                Dp.Add("@username", user.username, DbType.String);
                Dp.Add("@password", user.password, DbType.String);
                Dp.Add("@role", user.Role, DbType.String);

                Conn.Execute(sql, Dp);
            }
        }

        public void DeleteUser(int idUser)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                    DELETE FROM Users WHERE id = @id";

                var Dp = new DynamicParameters();
                Dp.Add("@id", idUser, DbType.Int32);

                Conn.Execute(sql, Dp);
            }
        }
        public IEnumerable<UserModel> ListUser()
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT id, username, password FROM Users WHERE Role = 'admin' ORDER BY id ASC";

                return Conn.Query<UserModel>(sql);
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using(var koneksi = new SQLiteConnection(conn.connstr()))
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

                koneksi.Execute(sql, new { Tanggal = tanggal});
            }
        }

        public void UpdateUserRiwayat(string UserLogin,string userLama)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"UPDATE RiwayatLogin SET UserLogin = @UserLogin WHERE UserLogin = @userLama";
                koneksi.Execute(sql, new {UserLogin=UserLogin, userLama=userLama});
            }
        }
    }
}
