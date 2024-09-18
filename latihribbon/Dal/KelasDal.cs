using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class KelasDal
    {
        public IEnumerable<KelasModel> listKelas()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT Id,NamaKelas FROM Kelas";
                return koneksi.Query<KelasModel>(sql);
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

        public KelasModel GetData(int Id)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT * FROM Kelas WHERE Id=@Id";
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
                const string sql = @"SELECT Rombel FROM Kelas WHERE idJurusan=@idJurusan AND Tingkat=@Tingkat";
                return koneksi.Query<KelasModel>(sql, new { idJurusan = idJurusan,Tingkat=Tingkat });
            }
        }

    }
}
