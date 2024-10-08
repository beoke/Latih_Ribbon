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
            //string conn = "Server = (local);Database = RekapSiswa;Trusted_Connection = True;TrustServerCertificate = True";
            string conn = "Server = 192.168.195.107; Database = RekapSiswa; User Id = RESI ; Password = ATM_RekapSiswa";
            try
            {
                using (var connection = new SqlConnection(conn))
                {
                    connection.Open();
                    return conn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi ke database terputus: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
    }
}
  