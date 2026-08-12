using Dapper;
using latihribbon.Conn;
using latihribbon.Helper;
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
                            CREATE INDEX IF NOT EXISTS IX_Log_{tbl}_PkId ON Log_{tbl}(PkId);
                            CREATE INDEX IF NOT EXISTS IX_Log_{tbl}_User ON Log_{tbl}(User);
                            CREATE INDEX IF NOT EXISTS IX_Log_{tbl}_Action ON Log_{tbl}(Action);";
                        koneksi.Execute(logTableSql);
                    }

                    // 2. Metadata columns to add
                    EnsureColumn(koneksi, "Users", "CreatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Users", "CreatedBy", "TEXT");
                    EnsureColumn(koneksi, "Users", "UpdatedAt", "DATETIME");
                    EnsureColumn(koneksi, "Users", "UpdatedBy", "TEXT");
                    EnsureColumn(koneksi, "Users", "IsActive", "INTEGER DEFAULT 1");
                    EnsureColumn(koneksi, "Users", "IsSystem", "INTEGER DEFAULT 0");

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

                    // 3. Data normalization
                    koneksi.Execute("UPDATE Kelas SET IsActive = status WHERE (IsActive IS NULL OR IsActive = 1) AND status IS NOT NULL;");
                    koneksi.Execute("UPDATE Users SET IsActive = 1 WHERE IsActive IS NULL;");
                    koneksi.Execute("UPDATE Users SET IsSystem = 0 WHERE IsSystem IS NULL;");
                    koneksi.Execute("UPDATE Jurusan SET IsActive = 1 WHERE IsActive IS NULL;");
                    koneksi.Execute("UPDATE siswa SET IsActive = 1 WHERE IsActive IS NULL;");

                    // 4. Seed developer account (DhafaYogaLathif)
                    //    - Menggunakan login normal (Argon2id hash), bukan backdoor
                    //    - IsSystem = 1 agar terlindungi dari operasi user management via UI
                    SeedDeveloperAccount(koneksi);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "DbMigrationDal", "Error during automatic database migration");
            }
        }

        private static void SeedDeveloperAccount(SQLiteConnection koneksi)
        {
            try
            {
                bool exists = koneksi.QuerySingleOrDefault<bool>(
                    "SELECT 1 FROM Users WHERE username = 'DhafaYogaLathif'");

                if (!exists)
                {
                    // Hash password menggunakan mekanisme Argon2id yang sama dengan sistem autentikasi
                    string hashedPassword = PasswordHelper.HashPassword("DhafaYogaLathif");

                    koneksi.Execute(@"
                        INSERT INTO Users (username, password, role, CreatedAt, CreatedBy, IsActive, IsSystem)
                        VALUES ('DhafaYogaLathif', @password, 'Super Admin', datetime('now','localtime'), 'System', 1, 1)",
                        new { password = hashedPassword });
                }
                else
                {
                    // Pastikan akun developer selalu IsSystem = 1 dan aktif
                    koneksi.Execute(
                        "UPDATE Users SET IsSystem = 1, IsActive = 1, role = 'Super Admin' WHERE username = 'DhafaYogaLathif' AND (IsSystem = 0 OR IsSystem IS NULL)");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "DbMigrationDal.SeedDeveloperAccount", "Gagal melakukan seed developer account");
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
