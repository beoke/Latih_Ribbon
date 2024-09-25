using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Dal
{
    public class RekapPersensiDal
    {
        public IEnumerable<RekapPersensiModel> ListData()
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

                return koneksi.Query<RekapPersensiModel>(sql);
            }
        }

        public IEnumerable<RekapPersensiModel> ListData2(int Offset, int Fetch, string Kelas)
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
                        WHERE sd.Kelas LIKE  @Kelas+'%'
                        ORDER BY 
                            sd.Persensi ASC,
                            sd.Tanggal DESC OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                

                return koneksi.Query<RekapPersensiModel>(sql, new { Offset = Offset , Fetch = Fetch, Kelas=Kelas});
            }
        }

        public int CekRows(string Kelas, string filter1, string filter2)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT  
                                      (SELECT COUNT(DISTINCT Tanggal) FROM Persensi {filter1}) *
                                      (SELECT COUNT(*) FROM siswa {filter2})
                               AS TotalRows";
                return koneksi.QuerySingle<int>(sql, new {kelas=Kelas});
            }
        }


        public void InsertDataToDatabase(DataTable dataTable)
        {
            using (var connection = new SqlConnection(Conn.conn.connstr()))
            {
                connection.Open();

                foreach (DataRow row in dataTable.Rows)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Kelas", row["Kelas"]);
                    parameters.Add("@Persensi", row["Persensi"]);
                    parameters.Add("@NIS", row["NIS"]);
                    parameters.Add("@Nama", row["Nama"]);
                    parameters.Add("@JenisKelamin", row["JenisKelamin"]);

                    string query = @"
                    INSERT INTO Siswa (Kelas, Persensi, NIS, Nama, JenisKelamin) 
                    VALUES (@Kelas, @Persensi, @NIS, @Nama, @JenisKelamin)";

                    connection.Execute(query, parameters);
                }
            }
        }
    }
}
