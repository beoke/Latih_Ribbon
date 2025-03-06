using Dapper;
using latihribbon.Conn;
using latihribbon.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latihribbon
{
    public class SetPrintDal
    {
        public IEnumerable<KelasModel> ListKelas()
        {
            using (var Conn = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                    SELECT k.Namakelas FROM Kelas k
                    INNER JOIN Jurusan j ON k.IdJurusan = j.Id
                    ORDER BY
                         CASE
                            WHEN k.Tingkat = 'X' THEN 1
                            WHEN k.Tingkat = 'XI' THEN 2
                            WHEN k.Tingkat = 'XII' THEN 3
                            ELSE 4
                        END,j.Kode ASC,k.Rombel ASC";

                return Conn.Query<KelasModel>(sql);
            }
        }
    }
}
