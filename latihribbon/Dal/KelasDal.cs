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
                const string sql = @"SELECT IdKelas,NamaKelas FROM Kelas";
                return koneksi.Query<KelasModel>(sql);
            }
        }

        public void Insert(KelasModel kelas)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Kelas(idKelas,NamaKelas,Rombel,IdJurusan)
                                    VALUES(@idKelas,@NamaKelas,@Rombel,@IdJurusan)";
                var dp = new DynamicParameters();
                dp.Add("@idKelas",kelas.IdKelas, System.Data.DbType.Int32);
                dp.Add("@NamaKelas",kelas.NamaKelas, System.Data.DbType.String);
                dp.Add("@Rombel",kelas.Rombel, System.Data.DbType.String);
                dp.Add("@idJurusan",kelas.IdJurusan, System.Data.DbType.Int32);

                koneksi.Execute(sql, dp);
            }
        }
    }
}
