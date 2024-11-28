using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                            sd.Tanggal DESC,
                            sd.Persensi ASC
                        OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";
                

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

        public IEnumerable<RekapPersensiModel> GetStudentDataByClass(string kelas,DateTime tgl1, DateTime tgl2)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                string sql = @"
                        SELECT s.NIS, s.Persensi, s.Nama, k.NamaKelas, p.Keterangan
                        FROM Siswa s
                        INNER JOIN Kelas k ON s.IdKelas = k.Id
                        LEFT JOIN Persensi p ON s.NIS = p.NIS AND p.Tanggal BETWEEN @tgl1 AND @tgl2
                        WHERE k.NamaKelas = @Kelas
                        ORDER BY s.Nama";

                var parameters = new { Kelas = kelas, tgl1=tgl1,tgl2=tgl2};

                try
                {
                    return koneksi.Query<RekapPersensiModel>(sql, parameters);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Enumerable.Empty<RekapPersensiModel>();
                }
            }
        }
    }
}
 