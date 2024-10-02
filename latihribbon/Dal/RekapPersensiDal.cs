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
        public IEnumerable<RekapPersensiModel> ListData2(string filter, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
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
                            sd.Persensi ASC,
                            sd.Tanggal DESC OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                

                return koneksi.Query<RekapPersensiModel>(sql, dp);
            }
        }

        public int CekRows(string filter1, string filter2, object dp)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = $@"SELECT  
                                      (SELECT COUNT(DISTINCT Tanggal) FROM Persensi {filter2}) *
                                      (SELECT COUNT(*) FROM siswa s INNER JOIN Kelas k ON s.IdKelas = k.Id {filter1})
                               AS TotalRows";
                return koneksi.QuerySingle<int>(sql,dp);
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
