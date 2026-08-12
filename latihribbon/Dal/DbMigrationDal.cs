using Dapper;
using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace latihribbon.Dal
{
    public class DbMigrationDal
    {
        public static void EnsureDatabaseUpToDate()
        {
            try
            {
                using (var koneksi = new SQLiteConnection(conn.connstr()))
                {
                    koneksi.Open();

                    // 1. Audit tables list
                    string[] auditTables = new string[]
                    {
                        "Users", "Jurusan", "Kelas", "siswa", "Persensi", "Masuk", "Keluar", "Survey"
                    };

                    foreach (var tbl in auditTables)
                    {
                        string logTableSql = $@"
                            CREATE TABLE IF NOT EXISTS Log_{tbl} (
                                LogId INTEGER PRIMARY KEY AUTOINCREMENT,
                                Timestamp DATETIME NOT NULL DEFAULT (datetime('now', 'localtime')),
                                User TEXT,
                                Action TEXT NOT NULL,
                                PkId TEXT NOT NULL,
                                ReferenceTable TEXT NOT NULL,
                                ContentJson TEXT NOT NULL
                            );
                            CREATE INDEX IF NOT EXISTS IX_Log_{tbl}_Timestamp ON Log_{tbl}(Timestamp);
                            CREATE INDEX IF NOT EXISTS IX_Log_{tbl}_PkId ON Log_{tbl}(PkId);";
                        koneksi.Execute(logTableSql);
                    }

                    // 2. Metadata columns to add
                    EnsureColumn(koneksi, "Users", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Users", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Users", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Users", "UpdatedBy", "TEXT");
                    EnsureColumn(koneksi, "Users", "IsActive", "INTEGER DEFAULT 1");

                    EnsureColumn(koneksi, "Jurusan", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Jurusan", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Jurusan", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Jurusan", "UpdatedBy", "TEXT");
                    EnsureColumn(koneksi, "Jurusan", "IsActive", "INTEGER DEFAULT 1");

                    EnsureColumn(koneksi, "Kelas", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Kelas", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Kelas", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Kelas", "UpdatedBy", "TEXT");
                    EnsureColumn(koneksi, "Kelas", "IsActive", "INTEGER DEFAULT 1");

                    EnsureColumn(koneksi, "siswa", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "siswa", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "siswa", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "siswa", "UpdatedBy", "TEXT");
                    EnsureColumn(koneksi, "siswa", "IsActive", "INTEGER DEFAULT 1");

                    EnsureColumn(koneksi, "Persensi", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Persensi", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Persensi", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Persensi", "UpdatedBy", "TEXT");

                    EnsureColumn(koneksi, "Masuk", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Masuk", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Masuk", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Masuk", "UpdatedBy", "TEXT");

                    EnsureColumn(koneksi, "Keluar", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Keluar", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Keluar", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Keluar", "UpdatedBy", "TEXT");

                    EnsureColumn(koneksi, "Survey", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Survey", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Survey", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Survey", "UpdatedBy", "TEXT");

                    // Synch Kelas status -> IsActive for legacy data
                    koneksi.Execute("UPDATE Kelas SET IsActive = status WHERE IsActive IS NULL OR IsActive = 1 AND status IS NOT NULL;");
                    koneksi.Execute("UPDATE Users SET IsActive = 1 WHERE IsActive IS NULL;");
                    koneksi.Execute("UPDATE Jurusan SET IsActive = 1 WHERE IsActive IS NULL;");
                    koneksi.Execute("UPDATE siswa SET IsActive = 1 WHERE IsActive IS NULL;");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "DbMigrationDal", "Error during automatic database migration");
            }
        }

        private static void EnsureColumn(SQLiteConnection conn, string tableName, string columnName, string columnType)
        {
            var columns = conn.Query<TableInfo>($"PRAGMA table_info({tableName});").ToList();
            if (!columns.Any(c => c.name.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
            {
                conn.Execute($"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType};");
            }
        }

        private class TableInfo
        {
            public string name { get; set; }
        }
    }
}
