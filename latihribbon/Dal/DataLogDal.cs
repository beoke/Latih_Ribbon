using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace latihribbon.Dal
{
    /// <summary>
    /// DAL untuk mengakses data log perubahan dari tabel-tabel Log_*.
    /// Query dilakukan secara dinamis berdasarkan nama tabel yang dipilih user.
    /// Nama tabel divalidasi dari sqlite_master sebelum digunakan — tidak ada string injection.
    /// </summary>
    public class DataLogDal
    {
        /// <summary>
        /// Ambil daftar nama tabel log yang tersedia di database.
        /// Hanya tabel yang namanya diawali "Log_" yang dikembalikan.
        /// </summary>
        public List<string> GetLogTableNames()
        {
            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                const string sql = @"
                    SELECT name FROM sqlite_master 
                    WHERE type='table' AND name LIKE 'Log_%' 
                    ORDER BY name";
                return koneksi.Query<string>(sql).ToList();
            }
        }

        /// <summary>
        /// Hitung jumlah baris log untuk pagination.
        /// </summary>
        public int CountLog(string tableName, string userFilter)
        {
            if (!IsValidLogTable(tableName)) return 0;

            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                var dp = new DynamicParameters();
                string where = BuildWhereClause(userFilter, dp);
                string sql = $"SELECT COUNT(*) FROM [{tableName}] {where}";
                return koneksi.QuerySingle<int>(sql, dp);
            }
        }

        /// <summary>
        /// Ambil data log dengan pagination dan filter opsional by user.
        /// </summary>
        public List<DataLogModel> GetLogs(string tableName, string userFilter, int offset, int fetch)
        {
            if (!IsValidLogTable(tableName)) return new List<DataLogModel>();

            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                var dp = new DynamicParameters();
                string where = BuildWhereClause(userFilter, dp);
                dp.Add("@Offset", offset);
                dp.Add("@Fetch", fetch);

                string sql = $@"
                    SELECT LogId, Timestamp, User, Action, PkId, ReferenceTable, ContentJson 
                    FROM [{tableName}]
                    {where}
                    ORDER BY Timestamp DESC
                    LIMIT @Fetch OFFSET @Offset";

                return koneksi.Query<DataLogModel>(sql, dp).ToList();
            }
        }

        /// <summary>
        /// Ambil satu record log berdasarkan nama tabel dan LogId.
        /// </summary>
        public DataLogModel GetLogById(string tableName, int logId)
        {
            if (!IsValidLogTable(tableName)) return null;

            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                string sql = $@"
                    SELECT LogId, Timestamp, User, Action, PkId, ReferenceTable, ContentJson 
                    FROM [{tableName}]
                    WHERE LogId = @LogId";
                return koneksi.QueryFirstOrDefault<DataLogModel>(sql, new { LogId = logId });
            }
        }

        /// <summary>
        /// Validasi nama tabel agar tidak ada injection via nama tabel.
        /// Hanya izinkan tabel yang benar-benar ada di database dengan prefix Log_.
        /// </summary>
        private bool IsValidLogTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) return false;
            if (!tableName.StartsWith("Log_", StringComparison.OrdinalIgnoreCase)) return false;

            using (var koneksi = new SQLiteConnection(conn.connstr()))
            {
                int count = koneksi.QuerySingleOrDefault<int>(
                    "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name = @name",
                    new { name = tableName });
                return count > 0;
            }
        }

        private string BuildWhereClause(string userFilter, DynamicParameters dp)
        {
            if (!string.IsNullOrWhiteSpace(userFilter))
            {
                dp.Add("@User", userFilter);
                return "WHERE User LIKE '%' || @User || '%'";
            }
            return string.Empty;
        }
    }

    public class DataLogModel
    {
        public int LogId { get; set; }
        public string Timestamp { get; set; }
        public string User { get; set; }
        public string Action { get; set; }
        public string PkId { get; set; }
        public string ReferenceTable { get; set; }
        public string ContentJson { get; set; }
    }
}
