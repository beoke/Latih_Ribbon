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
    public class AbsensiDal
    {
        public void Insert(AbsensiModel absensi)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"";
                var dp = new DynamicParameters();
                dp.Add("");
            }
        }

        public void Update(AbsensiModel absensi)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE Persensi SET Nis=@Nis,Tanggal=@Tanggal,Keterangan=@Keterangan WHERE Id=@Id";
                var dp = new DynamicParameters();
                dp.Add("@Id",absensi.Id,System.Data.DbType.Int32);
                dp.Add("@Nis",absensi.Nis,System.Data.DbType.Int32);
                dp.Add("@Tanggal",absensi.Tanggal,System.Data.DbType.Date);
                dp.Add("@Keterangan",absensi.Keterangan,System.Data.DbType.String);

                 koneksi.Execute(sql,dp);
            }
        }

        public IEnumerable<AbsensiModel> ListData()
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT p.ID,p.NIS,s.Nama,s.Persensi,s.Kelas,p.Tanggal,p.Keterangan
                                     FROM Persensi p INNER JOIN siswa s ON p.NIS=s.NIS";
                return koneksi.Query<AbsensiModel>(sql);
            }
        }

        public AbsensiModel GetData(int ID)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT p.ID,p.NIS,s.Nama,s.Persensi,s.Kelas,p.Tanggal,p.Keterangan
                                     FROM Persensi p INNER JOIN siswa s ON p.NIS=s.NIS
                                     WHERE p.ID = @ID";
                return koneksi.QueryFirstOrDefault<AbsensiModel>(sql, new {ID=ID});
            }
        }

        public void Delete(int Nis)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"DELETE FROM Absensi WHERE Nis=@Nis";
                koneksi.Execute(sql, new {Nis=Nis});
            }
        }

        public IEnumerable<AbsensiModel> Filter(string sql, object param)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                return koneksi.Query<AbsensiModel>(sql,param);
            }
        }
    }
}
