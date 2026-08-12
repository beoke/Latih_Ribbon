using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class AbsensiDal
    {
        public void Insert(AbsensiModel absensi)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"INSERT INTO Persensi(Nis, Tanggal, Keterangan, CreatedAt, CreatedBy) 
                                            VALUES(@Nis, @Tanggal, @Keterangan, @CreatedAt, @CreatedBy);
                                            SELECT last_insert_rowid();";
                        var dp = new DynamicParameters();
                        dp.Add("@Nis", absensi.Nis, System.Data.DbType.Int32);
                        dp.Add("@Tanggal", absensi.Tanggal, System.Data.DbType.Date);
                        dp.Add("@Keterangan", absensi.Keterangan, System.Data.DbType.String);
                        dp.Add("@CreatedAt", DateTime.Now);
                        dp.Add("@CreatedBy", UserSession.CurrentUser);

                        int newId = koneksi.QuerySingle<int>(sql, dp, trans);
                        absensi.Id = newId;

                        LoggingHelper.WriteLog(koneksi, trans, "Persensi", "INSERT", newId, null, absensi);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "AbsensiDal.Insert");
                        throw;
                    }
                }
            }
        }

        public void Update(AbsensiModel absensi)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<AbsensiModel>("SELECT * FROM Persensi WHERE Id=@Id", new { Id = absensi.Id }, trans);

                        const string sql = @"UPDATE Persensi SET 
                                            Nis=@Nis, Tanggal=@Tanggal, Keterangan=@Keterangan, 
                                            UpdatedAt=@UpdatedAt, UpdatedBy=@UpdatedBy 
                                            WHERE Id=@Id";
                        var dp = new DynamicParameters();
                        dp.Add("@Id", absensi.Id, System.Data.DbType.Int32);
                        dp.Add("@Nis", absensi.Nis, System.Data.DbType.Int32);
                        dp.Add("@Tanggal", absensi.Tanggal, System.Data.DbType.Date);
                        dp.Add("@Keterangan", absensi.Keterangan, System.Data.DbType.String);
                        dp.Add("@UpdatedAt", DateTime.Now);
                        dp.Add("@UpdatedBy", UserSession.CurrentUser);

                        koneksi.Execute(sql, dp, trans);

                        var afterData = koneksi.QueryFirstOrDefault<AbsensiModel>("SELECT * FROM Persensi WHERE Id=@Id", new { Id = absensi.Id }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Persensi", "UPDATE", absensi.Id, beforeData, afterData);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "AbsensiDal.Update");
                        throw;
                    }
                }
            }
        }

        public void Delete(int Id)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                koneksi.Open();
                using (var trans = koneksi.BeginTransaction())
                {
                    try
                    {
                        var beforeData = koneksi.QueryFirstOrDefault<AbsensiModel>("SELECT * FROM Persensi WHERE Id=@Id", new { Id = Id }, trans);

                        const string sql = @"DELETE FROM Persensi WHERE Id=@Id";
                        koneksi.Execute(sql, new { Id = Id }, trans);

                        LoggingHelper.WriteLog(koneksi, trans, "Persensi", "DELETE", Id, beforeData, null);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        AppLogger.LogError(ex, "AbsensiDal.Delete");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<AbsensiModel> ListData(string sqlc, string sqlcSorting, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT p.ID, p.NIS, s.Nama, s.Persensi, k.NamaKelas, p.Tanggal, p.Keterangan
                                     FROM Persensi p 
                                     INNER JOIN siswa s ON p.NIS=s.NIS
                                     INNER JOIN Kelas k ON s.IdKelas = k.Id 
                                     {sqlc} 
                                     ORDER BY {sqlcSorting} LIMIT @Fetch OFFSET @Offset";
                return koneksi.Query<AbsensiModel>(sql, dp);
            }
        }

        public AbsensiModel GetData(int ID)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT p.ID, p.NIS, s.Nama, s.Persensi, k.NamaKelas, p.Tanggal, p.Keterangan
                                     FROM Persensi p 
                                     INNER JOIN siswa s ON p.NIS=s.NIS
                                     INNER JOIN Kelas k ON s.IdKelas = k.Id
                                     WHERE p.ID = @ID";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, new { ID = ID });
            }
        }

        public IEnumerable<AbsensiModel> Filter(string sql, object param)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                return koneksi.Query<AbsensiModel>(sql, param);
            }
        }

        public int CekRows(string sqlcRow, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT COUNT(*)
                                    FROM Persensi p
                                    INNER JOIN Siswa s ON p.NIS = s.NIS
                                    INNER JOIN Kelas k ON s.IdKelas = k.Id {sqlcRow}";
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }

        public AbsensiModel GetByCondition(string condition, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT p.ID, p.NIS, s.Nama, s.Persensi, p.Tanggal, p.Keterangan
                                     FROM siswa s 
                                     INNER JOIN Persensi p ON s.Nis = p.NIS {condition}";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, dp);
            }
        }

        public AbsensiModel GetByAbsensiKelas(string NamaKelas, int Persensi)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT s.NIS, s.Nama FROM siswa s INNER JOIN Kelas k ON k.Id = s.IdKelas WHERE s.Persensi = @Persensi AND k.NamaKelas = @NamaKelas";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, new { Persensi = Persensi, NamaKelas = NamaKelas });
            }
        }
    }
}
