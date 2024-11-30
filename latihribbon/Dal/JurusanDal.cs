﻿using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class JurusanDal
    {
        public IEnumerable<JurusanModel> ListData()
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = "SELECT * FROM Jurusan ORDER BY Id ASC";
                return Conn.Query<JurusanModel>(sql);
            }
        }

        public void Insert(string namaJurusan)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                    INSERT INTO Jurusan
                        (NamaJurusan)
                    VALUES
                        (@NamaJurusan)";

                var Dp = new DynamicParameters();
                Dp.Add("@NamaJurusan", namaJurusan, System.Data.DbType.String);

                Conn.Execute(sql, Dp);
            }
        }

        public void Update(JurusanModel jurusan)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
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

        public void Delete(int JurusanId)
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                    DELETE FROM Jurusan WHERE Id = @Id";

                var Dp = new DynamicParameters();
                Dp.Add("@Id", JurusanId, System.Data.DbType.Int32);

                Conn.Execute(sql, Dp);
            }
        }

        public int GetIdJurusan(string NamaJurusan)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"SELECT Id FROM Jurusan WHERE NamaJurusan = @NamaJurusan";
                return koneksi.QueryFirstOrDefault<int>(sql, new { NamaJurusan = NamaJurusan });
            }
        }
    }
}
