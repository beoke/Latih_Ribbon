using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class MasukDal
    {
        public IEnumerable<MasukModel> ListData(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(conn.connstr()))
            {
                string sql = $@"SELECT m.id, m.NIS, s.Nama, kls.NamaKelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m 
                                    INNER JOIN siswa s ON m.NIS = s.NIS
                                    INNER JOIN Kelas kls ON s.IdKelas = kls.Id 
                                    {sqlc} 
                                    ORDER BY m.Tanggal DESC, 
                                    m.JamMasuk DESC OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                return koneksi.Query<MasukModel>(sql,dp);
            }
        }
        
        public MasukModel GetData(int id)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT m.id, m.NIS, s.Nama, kls.NamaKelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m 
                                    INNER JOIN siswa s ON m.NIS = s.NIS
                                    INNER JOIN Kelas kls ON s.IdKelas = kls.Id 
                                    WHERE m.id=@id";
                return koneksi.QueryFirstOrDefault<MasukModel>(sql, new {id=id});
            }
        }

        public void Insert(MasukModel masuk)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Masuk(Nis,Tanggal,JamMasuk,Alasan)
                                VALUES(@Nis,@Tanggal,@JamMasuk,@Alasan)";
                var dp = new DynamicParameters();
                dp.Add("@Nis", masuk.NIS, System.Data.DbType.Int32);
                dp.Add("@Tanggal", masuk.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamMasuk", masuk.JamMasuk, System.Data.DbType.Time);
                dp.Add("@Alasan", masuk.Alasan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }

        public void Update(MasukModel masuk)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Masuk SET Nis=@Nis,Tanggal=@Tanggal,
                                    JamMasuk=@JamMasuk,Alasan=@Alasan WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id", masuk.Id, System.Data.DbType.Int32);
                dp.Add("@Nis", masuk.NIS, System.Data.DbType.Int32);
                dp.Add("@Tanggal", masuk.Tanggal, System.Data.DbType.Date);
                dp.Add("@JamMasuk", masuk.JamMasuk, System.Data.DbType.Time);
                dp.Add("@Alasan", masuk.Alasan, System.Data.DbType.String);

                koneksi.Execute(sql, dp);
            }
        }
        public void Delete(int IdMasuk)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Masuk WHERE Id=@Id";
                koneksi.Execute(sql, new { Id=IdMasuk });
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT COUNT(*) FROM Masuk m 
                                INNER JOIN siswa s ON m.Nis=s.Nis
                                INNER JOIN kelas kls ON s.idKelas=kls.Id {sqlc}";
                return koneksi.QuerySingle<int>(sql,dp);
            }
        }

        public IEnumerable<MasukModel> ListMasuk2()
        {
            using (var koneksi = new SqlConnection(conn.connstr()))
            {
                string sql = $@"SELECT s.NIS, s.Nama, k.NamaKelas, m.Tanggal, m.Alasan
                                        FROM Siswa s
                                        INNER JOIN Kelas k ON s.IdKelas = k.Id
                                        INNER JOIN Masuk m ON s.Nis = m.Nis
                                        INNER JOIN Jurusan j ON k.idJurusan = j.Id 
                                        ORDER BY  
                                            CASE
                                                WHEN k.Tingkat='X' THEN 1
                                                WHEN k.Tingkat='XI' THEN 2
                                                WHEN k.Tingkat='XII' THEN 3
                                                ELSE 4
                                            END,
	                                        j.NamaJurusan ASC,
	                                        k.Rombel ASC,
	                                        s.Persensi ASC,
	                                        m.Tanggal ASC";
                return koneksi.Query<MasukModel>(sql);
            }
        }
    }
}
