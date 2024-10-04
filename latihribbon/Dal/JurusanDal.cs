using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class JurusanDal
    {
        public IEnumerable<JurusanModel> ListData()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = "SELECT * FROM Jurusan";
                return Conn.Query<JurusanModel>(sql);
            }
        }

        public void Insert(JurusanModel jurusan)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                    INSERT INTO Jurusan
                        (NamaJurusan)
                    VALUES
                        (@NamaJurusan)";

                var Dp = new DynamicParameters();
                Dp.Add("@NamaJurusan", jurusan.NamaJurusan, System.Data.DbType.String);

                Conn.Execute(sql, Dp);
            }
        }

        public void Update(JurusanModel jurusan)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
            UPDATE Jurusan
            SET
                NamaJurusan = @NamaJurusan
            WHERE
                Id = @Id";

                var Dp = new DynamicParameters();
                Dp.Add("@Id", jurusan.Id, System.Data.DbType.Int32);
                Dp.Add("@NamaJurusan", jurusan.NamaJurusan, System.Data.DbType.String);
                Conn.Execute(sql, Dp);
            }
        }

        private void UpdateKelas()
        {
            using (var koneksi = new SqlConnection(conn.connstr()))
            {
                const string sql = @"UPDATE Kelas SET";
            }
        }


        public void Delete(int JurusanId)
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sql = @"
                    DELETE FROM Jurusan WHERE Id = @Id";

                var Dp = new DynamicParameters();
                Dp.Add("@Id", JurusanId, System.Data.DbType.Int32);

                Conn.Execute(sql, Dp);
            }
        }
    }
}
