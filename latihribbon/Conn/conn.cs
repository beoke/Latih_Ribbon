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
            return "Server = (local);Database = RekapSiswaBanyak;Trusted_Connection = True;TrustServerCertificate = True";
            //return "Server=192.168.100.122;Database=RekapSiswa;User ID=RESI;Password=ATM_RekapSiswa;TrustServerCertificate=True";
        }
    }
}
  