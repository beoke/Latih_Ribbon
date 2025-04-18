﻿using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace latihribbon.Dal
{
    public class RekapPersensiDal
    {
        public IEnumerable<RekapPersensiModel> ListData2(string filter, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"WITH UniqueDates AS (
                            SELECT DISTINCT Tanggal
                            FROM Persensi)

                        ,SiswaDates AS (
                            SELECT s.NIS, s.Nama,s.Persensi,k.NamaKelas, d.Tanggal
                            FROM Siswa s
                            INNER JOIN Kelas k ON s.IdKelas = k.Id
                            CROSS JOIN UniqueDates d)

                        SELECT sd.NIS, sd.Nama,sd.Persensi,sd.NamaKelas,
                               sd.Tanggal,
                               COALESCE(a.Keterangan, '*') AS Keterangan
                        FROM SiswaDates sd
                        LEFT JOIN Persensi a ON sd.NIS = a.NIS AND sd.Tanggal = a.Tanggal 
                        {filter} 
                        ORDER BY 
                            sd.Tanggal DESC,
                            sd.Persensi ASC
                        LIMIT @Fetch OFFSET @Offset";
                

                return koneksi.Query<RekapPersensiModel>(sql, dp);
            }
        }

        public int CekRows(string filter1, string filter2, object dp)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT  
                                      (SELECT COUNT(DISTINCT Tanggal) FROM Persensi {filter2}) *
                                      (SELECT COUNT(*) FROM siswa s INNER JOIN Kelas k ON s.IdKelas = k.Id {filter1})
                               AS TotalRows";
                return koneksi.QuerySingle<int>(sql,dp);
            }
        }

        public IEnumerable<RekapPersensiModel> GetStudentDataByClass(string kelas,DateTime tgl1, DateTime tgl2)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = @"
                        SELECT s.NIS, s.Persensi, s.Nama, k.NamaKelas, p.Keterangan, p.Tanggal
                        FROM Siswa s
                        INNER JOIN Kelas k ON s.IdKelas = k.Id
                        LEFT JOIN Persensi p ON s.NIS = p.NIS AND p.Tanggal BETWEEN @tgl1 AND @tgl2
                        WHERE k.NamaKelas = @Kelas
                        ORDER BY s.Nama";
                var dp = new DynamicParameters();
                dp.Add("@tgl1",tgl1,DbType.Date);
                dp.Add("@tgl2",tgl2,DbType.Date);
                dp.Add("@Kelas",kelas,DbType.String);

                return koneksi.Query<RekapPersensiModel>(sql,dp);
            }
        }

        public IEnumerable<RekapPersensiModel> GetPresensiByNis(int nis, DateTime tgl1, DateTime tgl2)
        {
            using (var koneksi = new SQLiteConnection(Conn.conn.connstr()))
            {
                string sql = @"
                        WITH RECURSIVE DateRange(Tanggal) AS (
                        SELECT date(@tgl1)
                        UNION ALL
                        SELECT date(Tanggal, '+1 day')
                        FROM DateRange
                            WHERE Tanggal < date(@tgl2)
                        )
                        SELECT 
                            dr.Tanggal,
                            p.Keterangan
                        FROM 
                            Siswa s
                        INNER JOIN Kelas k ON s.IdKelas = k.Id
                        CROSS JOIN DateRange dr
                        LEFT JOIN Persensi p ON s.NIS = p.NIS AND date(p.Tanggal) = dr.Tanggal
                        WHERE 
                            s.Nis = @nis
                        ORDER BY 
                            dr.Tanggal ASC";
                var dp = new DynamicParameters();
                dp.Add("@tgl1", tgl1, DbType.Date);
                dp.Add("@tgl2", tgl2, DbType.Date);
                dp.Add("@nis", nis, DbType.String);

                return koneksi.Query<RekapPersensiModel>(sql, dp);
            }
        }
    }
}
 