using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.Conn
{
    public class conn
    {
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

            return $@"Data Source={databasePath};Version=3;";
        }
    }
}