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
            return "Server = (local);Database = RekapSiswa;Trusted_Connection = True;TrustServerCertificate = True";
        }
    }
}
  