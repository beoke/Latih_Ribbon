using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class MasukDal
    {
        public IEnumerable<MasukModel> ListData(string sqlc, object dp)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"SELECT m.id, m.NIS, s.Nama, kls.NamaKelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m 
                                    INNER JOIN siswa s ON m.NIS = s.NIS
                                    INNER JOIN Kelas kls ON s.IdKelas = kls.Id 
                                    {sqlc} 
                                    ORDER BY m.Tanggal DESC, 
                                    m.JamMasuk DESC LIMIT @Fetch OFFSET @Offset";
                return koneksi.Query<MasukModel>(sql, dp);
            }
        }

        public MasukModel GetData(int id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT m.id, m.NIS, s.Nama, kls.NamaKelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m 
                                    INNER JOIN siswa s ON m.NIS = s.NIS
                                    INNER JOIN Kelas kls ON s.IdKelas = kls.Id 
                                    WHERE m.id=@id";
                return koneksi.QueryFirstOrDefault<MasukModel>(sql, new { id = id });
            }
        }

        public void Insert(MasukModel masuk)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"INSERT INTO Masuk(Nis, Tanggal, JamMasuk, Alasan, CreatedAt, CreatedBy)
                                        VALUES(@Nis, @Tanggal, @JamMasuk, @Alasan, @CreatedAt, @CreatedBy);
                                        SELECT last_insert_rowid();";
                        var dp = new DynamicParameters();
                        dp.Add("@Nis", masuk.NIS, System.Data.DbType.Int32);
                        dp.Add("@Tanggal", masuk.Tanggal, System.Data.DbType.Date);
                        dp.Add("@JamMasuk", masuk.JamMasuk, System.Data.DbType.String);
                        dp.Add("@Alasan", masuk.Alasan, System.Data.DbType.String);
                        dp.Add("@CreatedAt", DateTime.Now);
                        dp.Add("@CreatedBy", UserSession.CurrentUser);

                        int newId = koneksi.QuerySingle<int>(sql, dp, trans);
                        masuk.Id = newId;

                        LoggingHelper.WriteLog(koneksi, trans, "Masuk", "INSERT", newId, null, masuk);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "MasukDal.Insert");
                        throw;
                    }
                }
            }
        }

        public void Update(MasukModel masuk)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<MasukModel>("SELECT * FROM Masuk WHERE Id=@Id", new { Id = masuk.Id }, trans);

                        const string sql = @"UPDATE Masuk SET Nis=@Nis, Tanggal=@Tanggal, JamMasuk=@JamMasuk, Alasan=@Alasan,
                                            UpdatedAt=@UpdatedAt, UpdatedBy=@UpdatedBy WHERE Id=@Id";
                        var dp = new DynamicParameters();
                        dp.Add("@Id", masuk.Id, System.Data.DbType.Int32);
                        dp.Add("@Nis", masuk.NIS, System.Data.DbType.Int32);
                        dp.Add("@Tanggal", masuk.Tanggal, System.Data.DbType.Date);
                        dp.Add("@JamMasuk", masuk.JamMasuk, System.Data.DbType.String);
                        dp.Add("@Alasan", masuk.Alasan, System.Data.DbType.String);
                        dp.Add("@UpdatedAt", DateTime.Now);
                        dp.Add("@UpdatedBy", UserSession.CurrentUser);

                        koneksi.Execute(sql, dp, trans);

                        var afterData = koneksi.QueryFirstOrDefault<MasukModel>("SELECT * FROM Masuk WHERE Id=@Id", new { Id = masuk.Id }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Masuk", "UPDATE", masuk.Id, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "MasukDal.Update");
                        throw;
                    }
                }
            }
        }

        public void Delete(int IdMasuk)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<MasukModel>("SELECT * FROM Masuk WHERE Id=@Id", new { Id = IdMasuk }, trans);

                        const string sql = @"DELETE FROM Masuk WHERE Id=@Id";
                        koneksi.Execute(sql, new { Id = IdMasuk }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Masuk", "DELETE", IdMasuk, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "MasukDal.Delete");
                        throw;
                    }
                }
            }
        }

        public int CekRows(string sqlc, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT COUNT(*) FROM Masuk m 
                                INNER JOIN siswa s ON m.Nis=s.Nis
                                INNER JOIN kelas kls ON s.idKelas=kls.Id {sqlc}";
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }

        public IEnumerable<MasukModel> ListMasuk2(DateTime tgl1, DateTime tgl2)
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"SELECT s.NIS, s.Nama, k.NamaKelas, m.Tanggal, m.Alasan
                                        FROM Siswa s
                                        INNER JOIN Kelas k ON s.IdKelas = k.Id
                                        INNER JOIN Masuk m ON s.Nis = m.Nis
                                        INNER JOIN Jurusan j ON k.idJurusan = j.Id
                                        WHERE m.Tanggal BETWEEN @tgl1 AND @tgl2
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
                var dp = new DynamicParameters();
                dp.Add("@tgl1", tgl1, DbType.Date);
                dp.Add("@tgl2", tgl2, DbType.Date);
                return koneksi.Query<MasukModel>(sql, dp);
            }
        }
    }
}
