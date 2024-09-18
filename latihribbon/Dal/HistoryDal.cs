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
    public class HistoryDal
    {
        public HistoryModel GetData(string Nama)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"SELECT History FROM History WHERE Nama=@Nama";
                return koneksi.QueryFirstOrDefault<HistoryModel>(sql, new {Nama=Nama});
            }
        }

        public void Update(string Nama, string History)
        {
            using (var koneksi = new SqlConnection(Conn.conn.connstr()))
            {
                const string sql = @"UPDATE History SET History=@History WHERE Nama=@Nama";
                koneksi.Execute(sql, new {History=History,Nama=Nama});
            }
        }
    }
}
