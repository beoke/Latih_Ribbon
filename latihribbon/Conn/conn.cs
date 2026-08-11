using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.Conn
{
    public class conn
    {
        private static bool _isSchemaSynced = false;
        private static readonly object _syncLock = new object();

        /// <summary>
        /// Definisi skema tabel dan kolom yang diharapkan oleh sistem.
        /// Jika ada penambahan kolom/tabel di versi aplikasi terbaru, cukup tambahkan di kamus ini.
        /// </summary>
        public static readonly Dictionary<string, Dictionary<string, string>> ExpectedSchema =
            new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "Users", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "username", "TEXT" },
                    { "password", "TEXT" },
                    { "Role", "TEXT" }
                }
            },
            {
                "siswa", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Nis", "INTEGER PRIMARY KEY" },
                    { "Nama", "TEXT" },
                    { "JenisKelamin", "TEXT" },
                    { "Persensi", "INTEGER" },
                    { "IdKelas", "INTEGER" },
                    { "Tahun", "TEXT" }
                }
            },
            {
                "Kelas", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "NamaKelas", "TEXT" },
                    { "Rombel", "TEXT" },
                    { "IdJurusan", "INTEGER" },
                    { "Tingkat", "TEXT" },
                    { "status", "INTEGER" }
                }
            },
            {
                "Jurusan", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "NamaJurusan", "TEXT" },
                    { "Kode", "TEXT" }
                }
            },
            {
                "Persensi", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "Nis", "INTEGER" },
                    { "Tanggal", "DATE" },
                    { "Keterangan", "TEXT" }
                }
            },
            {
                "History", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "Nama", "TEXT" },
                    { "History", "TEXT" }
                }
            },
            {
                "Keluar", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "Nis", "INTEGER" },
                    { "Tanggal", "DATE" },
                    { "JamKeluar", "TEXT" },
                    { "JamMasuk", "TEXT" },
                    { "Tujuan", "TEXT" }
                }
            },
            {
                "Masuk", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "NIS", "INTEGER" },
                    { "Tanggal", "DATE" },
                    { "JamMasuk", "TEXT" },
                    { "Alasan", "TEXT" }
                }
            },
            {
                "RiwayatLogin", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "IdLogin", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "UserLogin", "TEXT" },
                    { "Tanggal", "DATETIME" },
                    { "Waktu", "TEXT" }
                }
            },
            {
                "Survey", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "SurveyId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
                    { "HasilSurvey", "INTEGER" },
                    { "Tanggal", "DATETIME" },
                    { "Waktu", "TEXT" }
                }
            }
        };

        public static string connstr()
        {
            //return "Server = (local);Database = RekapSiswa;Trusted_Connection = True;TrustServerCertificate = True";
            //return "Server=192.168.100.122;Database=RekapSiswa;User ID=RESI;Password=ATM_RekapSiswa;TrustServerCertificate=True";
            //return @"Data source=D:\SQLite Browser\Database\RekapSiswaCek.db;Version = 3";

            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SIM RESI");
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            string databasePath = System.IO.Path.Combine(folderPath, "RekapSiswaCek.db");
            if (!System.IO.File.Exists(databasePath))
            {
                System.IO.File.Copy(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RekapSiswaCek.db"), databasePath);
            }

            string connectionString = $@"Data Source={databasePath};Version=3;";

            EnsureTableColumns(connectionString);

            return connectionString;
        }

        /// <summary>
        /// Fungsi utama untuk mengecek isi kolom dari setiap tabel di database dan menyelaraskan dengan kodingan sistem.
        /// Apabila ada kolom yang kurang di database, fungsi ini akan menjalankan perintah ALTER TABLE ... ADD COLUMN ...
        /// </summary>
        public static void EnsureTableColumns(string connectionString = null)
        {
            lock (_syncLock)
            {
                if (_isSchemaSynced) return;

                if (string.IsNullOrEmpty(connectionString))
                {
                    string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SIM RESI");
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }
                    string databasePath = System.IO.Path.Combine(folderPath, "RekapSiswaCek.db");
                    if (!System.IO.File.Exists(databasePath))
                    {
                        System.IO.File.Copy(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RekapSiswaCek.db"), databasePath);
                    }
                    connectionString = $@"Data Source={databasePath};Version=3;";
                }

                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        foreach (var tableKvp in ExpectedSchema)
                        {
                            string tableName = tableKvp.Key;
                            var columns = tableKvp.Value;

                            // 1. Cek apakah tabel ada di database
                            bool tableExists = false;
                            using (var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND LOWER(name)=LOWER(@tableName)", connection))
                            {
                                checkCmd.Parameters.AddWithValue("@tableName", tableName);
                                long count = Convert.ToInt64(checkCmd.ExecuteScalar());
                                tableExists = (count > 0);
                            }

                            if (!tableExists)
                            {
                                // Buat tabel baru jika belum ada
                                var colDefinitions = columns.Select(c => $"[{c.Key}] {c.Value}");
                                string createSql = $"CREATE TABLE [{tableName}] ({string.Join(", ", colDefinitions)});";
                                using (var createCmd = new SQLiteCommand(createSql, connection))
                                {
                                    createCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // 2. Ambil daftar kolom yang saat ini ada di database SQLite
                                HashSet<string> existingColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                                using (var pragmaCmd = new SQLiteCommand($"PRAGMA table_info([{tableName}])", connection))
                                {
                                    using (var reader = pragmaCmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            string colName = reader["name"].ToString();
                                            existingColumns.Add(colName);
                                        }
                                    }
                                }

                                // 3. Tambahkan kolom baru yang ada di kodingan tapi belum ada di database
                                foreach (var colKvp in columns)
                                {
                                    string colName = colKvp.Key;
                                    string rawType = colKvp.Value;

                                    if (!existingColumns.Contains(colName))
                                    {
                                        string alterType = GetDataTypeForAlter(rawType);
                                        string alterSql = $"ALTER TABLE [{tableName}] ADD COLUMN [{colName}] {alterType};";
                                        using (var alterCmd = new SQLiteCommand(alterSql, connection))
                                        {
                                            alterCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    _isSchemaSynced = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error syncing database schema: " + ex.Message);
                }
            }
        }

        private static string GetDataTypeForAlter(string rawType)
        {
            if (string.IsNullOrWhiteSpace(rawType)) return "TEXT";

            string clean = rawType;
            int pkIndex = clean.IndexOf("PRIMARY KEY", StringComparison.OrdinalIgnoreCase);
            if (pkIndex >= 0) clean = clean.Substring(0, pkIndex);

            clean = clean.Trim();
            return string.IsNullOrEmpty(clean) ? "TEXT" : clean;
        }
    }
}