using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon
{
    public class RiwayatLogin_UserDal
    {
        public IEnumerable<RiwayatLoginModel> ListData()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = "SELECT * FROM RiwayatLogin";

                return Conn.Query<RiwayatLoginModel>(sql);
            };
        }

        public void Insert(RiwayatLoginModel riwayat)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                        INSERT INTO RiwayatLogin 
                            (UserLogin, Tanggal, Waktu)
                        VALUES 
                            (@UserLogin, @Tanggal, @Waktu)";

                var Dp = new DynamicParameters();
                Dp.Add("@UserLogin", riwayat.UserLogin, DbType.String);
                Dp.Add("@Tanggal", riwayat.Tanggal, DbType.DateTime);
                Dp.Add("@Waktu", riwayat.Waktu, DbType.Time);

                Conn.Execute(sql, Dp);
            }
        }

        public IEnumerable<RiwayatLoginModel> GetSiswaFilter(string sql, object query)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                var filterLogin = Conn.Query<RiwayatLoginModel>(sql, query);
                return filterLogin;
            }
        }



        //USER
        public void Insert(UserModel userModel)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                INSERT INTO Users
                    (username, password, role)
                VALUES 
                    (@username, @password, @role)";

                var Dp = new DynamicParameters();
                Dp.Add("@username", userModel.Username, DbType.String);
                Dp.Add("@password", userModel.Password, DbType.String);
                Dp.Add("@role", userModel.Role, DbType.String);

                Conn.Execute(sql, Dp);
            }
        }

        public void Update(UserModel user)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
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
                Dp.Add("@username", user.Username, DbType.String);
                Dp.Add("@password", user.Username, DbType.String);
                Dp.Add("@role", user.Role, DbType.String);

                Conn.Execute(sql, Dp);
            }
        }


        public void DeleteUser(int idUser)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
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
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT id, username, role FROM Users";

                return Conn.Query<UserModel>(sql);
            }
        }


        public UserModel GetUser(int idUser)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"SELECT * FROM Users WHERE id = @id";

                var Dp = new DynamicParameters();
                Dp.Add("@id", idUser, DbType.Int32);

                var result = Conn.QuerySingleOrDefault<UserModel>(sql, Dp);
                return result;
            }
        }
    }
}
