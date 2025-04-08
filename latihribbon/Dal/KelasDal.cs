using Dapper;
using latihribbon.Model;
using System.Collections.Generic;
using System.Data.SQLite;


namespace latihribbon.Dal
{
    public class KelasDal
    {
        public IEnumerable<KelasModel> listKelas(string sqlc, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT k.Id,k.NamaKelas,k.Rombel,k.IdJurusan,k.Tingkat,j.Kode FROM Kelas k
                                INNER JOIN Jurusan j ON k.IdJurusan=j.Id  {sqlc}
                                ORDER BY CASE 
                                        WHEN k.Tingkat = '' THEN 1
                                        WHEN k.Tingkat = 'X' THEN 2
                                        WHEN k.Tingkat = 'XI' THEN 3
                                        WHEN k.Tingkat = 'XII' THEN 4
                                        WHEN k.Tingkat = 'LULUS' THEN 5
                                        ELSE 6
                                    END, idJurusan, Rombel";
                return koneksi.Query<KelasModel>(sql,dp);
            }
        }

        public void Insert(KelasModel kelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Kelas(NamaKelas,Rombel,IdJurusan,Tingkat, status)
                                    VALUES(@NamaKelas,@Rombel,@idJurusan,@Tingkat,@status)";
                var dp = new DynamicParameters();
                dp.Add("@NamaKelas",kelas.NamaKelas, System.Data.DbType.String);
                dp.Add("@Rombel",kelas.Rombel, System.Data.DbType.String);
                dp.Add("@idJurusan",kelas.IdJurusan, System.Data.DbType.Int32);
                dp.Add("@Tingkat",kelas.Tingkat, System.Data.DbType.String);
                dp.Add("@status", kelas.status, System.Data.DbType.Int16);

                koneksi.Execute(sql, dp);
            }
        }

        public void Update(KelasModel kelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Kelas SET NamaKelas=@NamaKelas,Rombel=@Rombel,idJurusan = @idJurusan,Tingkat = @Tingkat, status=@status WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id",kelas.Id,System.Data.DbType.Int32);
                dp.Add("@NamaKelas",kelas.NamaKelas,System.Data.DbType.String);
                dp.Add("@Rombel",kelas.Rombel,System.Data.DbType.String);
                dp.Add("@idJurusan",kelas.IdJurusan,System.Data.DbType.Int32);
                dp.Add("@Tingkat", kelas.Tingkat, System.Data.DbType.String);
                dp.Add("@status", kelas.status, System.Data.DbType.Int16);

                koneksi.Execute(sql,dp);
            }
        }

        public void UpdateNamaKelas(int Id, string NamaKelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Kelas SET NamaKelas=@NamaKelas WHERE Id=@Id";
                koneksi.Execute(sql, new {Id=Id,NamaKelas=NamaKelas});
            }
        }

        public KelasModel GetData(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT k.Id,k.NamaKelas,k.Rombel,k.IdJurusan,
                                        k.Tingkat,j.Kode 
                                    FROM Kelas k INNER JOIN Jurusan j ON k.IdJurusan = j.Id
                                    WHERE k.Id=@Id";
                return koneksi.QueryFirstOrDefault<KelasModel>(sql, new {Id=Id});
            }
        }

        public void Delete(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Kelas WHERE Id=@Id";
                koneksi.Execute(sql, new {Id=Id});
            }
        }
        public IEnumerable<KelasModel> GetDataRombel(int idJurusan,string Tingkat)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT Rombel,Id FROM Kelas WHERE idJurusan=@idJurusan AND Tingkat=@Tingkat";
                return koneksi.Query<KelasModel>(sql, new { idJurusan = idJurusan,Tingkat=Tingkat });
            }
        }

        public int GetIdKelas(string NamaKelas)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT Id FROM Kelas WHERE NamaKelas = @NamaKelas";
                return koneksi.QueryFirstOrDefault<int>(sql, new {NamaKelas = NamaKelas});
            }
        }

        public void DuplikatKelas(string Tingkat)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"INSERT INTO Kelas(NamaKelas,Rombel,IdJurusan,Tingkat, status)
                                    SELECT NamaKelas, Rombel, IdJurusan, Tingkat, status FROM Kelas
                                    WHERE Tingkat = @Tingkat";
                koneksi.Execute(sql, new { Tingkat = Tingkat});
            }
        }

        public void DeleteDataLulus()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM kelas
                                    WHERE status = 0 
                                    AND Id NOT IN (SELECT DISTINCT IdKelas FROM siswa)";
                koneksi.Execute(sql);
            }
        }
        public bool TurunkanKelas()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sqlTurun = @"DELETE FROM kelas
                                    WHERE Tingkat = 'X'";
                const string sqlCek = @"SELECT COUNT(*) FROM Siswa s INNER JOIN Kelas k ON s.idKelas = k.Id WHERE k.Tingkat = 'X'";

                if (koneksi.QuerySingleOrDefault<int>(sqlCek) == 0)
                {
                    koneksi.Execute(sqlTurun);
                    return true;
                }
                return false;
            }
        }

        public int DeleteSiswaLulus()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Kelas WHERE status = 0";
                return koneksi.Execute(sql);
            }
        }

        public bool cekLulus()
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT COUNT(1) FROM Kelas WHERE status = 0";
                int count = koneksi.ExecuteScalar<int>(sql);

                return count > 0;
            }
        }

        public bool CekDuplikasi(KelasModel kelas, bool update = false)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = @"SELECT 1 FROM Kelas WHERE 
                    Tingkat = @Tingkat AND idJurusan = @idJurusan ";
                if (kelas.Rombel != string.Empty)
                    sql += "AND Rombel = @Rombel";
                if(update)
                    sql += " AND Id <> @Id";

                var dp = new DynamicParameters();
                dp.Add("@Tingkat", kelas.Tingkat);
                dp.Add("@idJurusan", kelas.IdJurusan);
                dp.Add("@Rombel", kelas.Rombel);
                dp.Add("@Id", kelas.Id);

                return koneksi.QuerySingleOrDefault<bool>(sql, dp);
            }
        }
    } 
}
