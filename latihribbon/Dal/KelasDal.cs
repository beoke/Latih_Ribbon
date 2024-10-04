using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class KelasDal
    {
        public IEnumerable<KelasModel> listKelas(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT k.Id,k.NamaKelas,k.Rombel,k.IdJurusan,k.Tingkat,j.NamaJurusan FROM Kelas k
                                INNER JOIN Jurusan j ON k.IdJurusan=j.Id  {sqlc}
                                ORDER BY CASE 
                                        WHEN k.Tingkat = 'X' THEN 1
                                        WHEN k.Tingkat = 'XI' THEN 2
                                        WHEN k.Tingkat = 'XII' THEN 3
                                        ELSE 4
                                    END, idJurusan, Rombel";
                return koneksi.Query<KelasModel>(sql,dp);
            }
        }

        public void Insert(KelasModel kelas)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Kelas(NamaKelas,Rombel,IdJurusan,Tingkat)
                                    VALUES(@NamaKelas,@Rombel,@IdJurusan,@Tingkat)";
                var dp = new DynamicParameters();
                dp.Add("@NamaKelas",kelas.NamaKelas, System.Data.DbType.String);
                dp.Add("@Rombel",kelas.Rombel, System.Data.DbType.String);
                dp.Add("@idJurusan",kelas.IdJurusan, System.Data.DbType.Int32);
                dp.Add("@Tingkat",kelas.Tingkat, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        public void Update(KelasModel kelas)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Kelas SET NamaKelas=@NamaKelas,Rombel=@Rombel,idJurusan = @idJurusan,Tingkat = @Tingkat WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id",kelas.Id,System.Data.DbType.Int32);
                dp.Add("@NamaKelas",kelas.NamaKelas,System.Data.DbType.String);
                dp.Add("@Rombel",kelas.Rombel,System.Data.DbType.String);
                dp.Add("@idJurusan",kelas.IdJurusan,System.Data.DbType.Int32);
                dp.Add("@Tingkat", kelas.Tingkat, System.Data.DbType.String);

                koneksi.Execute(sql,dp);
            }
        }

        public void UpdateNamaKelas(List<string> listKelas, int idJurusan)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Kelas SET NamaKelas=@NamaKelas WHERE idJurusan=@idJurusan";
                foreach (var x in listKelas)
                    koneksi.Execute(sql, new {NamaKelas = x, idJurusan=idJurusan});
            }
        }

        public KelasModel GetData(int Id)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT k.Id,k.NamaKelas,k.Rombel,k.IdJurusan,
                                        k.Tingkat,j.NamaJurusan 
                                    FROM Kelas k INNER JOIN Jurusan j ON k.IdJurusan = j.Id
                                    WHERE k.Id=@Id";
                return koneksi.QueryFirstOrDefault<KelasModel>(sql, new {Id=Id});
            }
        }

        public void Delete(int Id)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Kelas WHERE Id=@Id";
                koneksi.Execute(sql, new {Id=Id});
            }
        }
        public IEnumerable<KelasModel> GetDataRombel(int idJurusan,string Tingkat)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT Rombel,Id FROM Kelas WHERE idJurusan=@idJurusan AND Tingkat=@Tingkat";
                return koneksi.Query<KelasModel>(sql, new { idJurusan = idJurusan,Tingkat=Tingkat });
            }
        }

        public int GetIdKelas(string NamaKelas)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT Id FROM Kelas WHERE NamaKelas = @NamaKelas";
                return koneksi.QuerySingleOrDefault<int>(sql, new {NamaKelas = NamaKelas});
            }
        }

    }
}
