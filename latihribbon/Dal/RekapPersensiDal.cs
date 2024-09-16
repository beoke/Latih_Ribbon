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
    public class RekapPersensiDal
    {
        public IEnumerable<AbsensiModel> ListData()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"WITH UniqueDates AS (
                            SELECT DISTINCT Tanggal
                            FROM Persensi)

                        ,SiswaDates AS (
                            SELECT s.NIS, s.Nama,s.Persensi,s.Kelas, d.Tanggal
                            FROM Siswa s
                            CROSS JOIN UniqueDates d)

                        SELECT sd.NIS, sd.Nama,sd.Persensi,sd.Kelas,
                               sd.Tanggal,
                               COALESCE(a.Keterangan, '*') AS Keterangan
                        FROM SiswaDates sd
                        LEFT JOIN Persensi a ON sd.NIS = a.NIS AND sd.Tanggal = a.Tanggal
                        ORDER BY 
                            sd.Persensi ASC,
                            CASE
                                WHEN sd.Kelas LIKE 'X %' THEN 1
                                WHEN sd.Kelas LIKE 'XI %' THEN 2
                                WHEN sd.Kelas LIKE 'XII %' THEN 3
                                ELSE 4
                            END,
                            SUBSTRING(sd.Kelas, CHARINDEX(' ', sd.Kelas) + 1, LEN(sd.Kelas)) ASC,
                            sd.Tanggal DESC";

                return koneksi.Query<AbsensiModel>(sql);
            }
        }

        public IEnumerable<AbsensiModel> ListData2(int Offset, int Fetch)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"WITH UniqueDates AS (
                            SELECT DISTINCT Tanggal
                            FROM Persensi)

                        ,SiswaDates AS (
                            SELECT s.NIS, s.Nama,s.Persensi,s.Kelas, d.Tanggal
                            FROM Siswa s
                            CROSS JOIN UniqueDates d)

                        SELECT sd.NIS, sd.Nama,sd.Persensi,sd.Kelas,
                               sd.Tanggal,
                               COALESCE(a.Keterangan, '*') AS Keterangan
                        FROM SiswaDates sd
                        LEFT JOIN Persensi a ON sd.NIS = a.NIS AND sd.Tanggal = a.Tanggal
                        ORDER BY 
                            sd.Persensi ASC,
                            CASE
                                WHEN sd.Kelas LIKE 'X %' THEN 1
                                WHEN sd.Kelas LIKE 'XI %' THEN 2
                                WHEN sd.Kelas LIKE 'XII %' THEN 3
                                ELSE 4
                            END,
                            SUBSTRING(sd.Kelas, CHARINDEX(' ', sd.Kelas) + 1, LEN(sd.Kelas)) ASC,
                            sd.Tanggal DESC OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                

                return koneksi.Query<AbsensiModel>(sql, new { Offset = Offset , Fetch = Fetch});
            }
        }

        public int CekRows()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT
                                        (SELECT COUNT(DISTINCT Tanggal) FROM Persensi) *
                                        (SELECT COUNT(*) FROM siswa)
                                     AS TotalRows";
                return koneksi.QuerySingle<int>(sql);
            }
        }
    }
}
