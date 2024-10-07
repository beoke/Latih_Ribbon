using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Conn
{
    public class conn
    {
        public static string connstr()
        {
            return "Server = (local);Database = RekapSiswa;Trusted_Connection = True;TrustServerCertificate = True";
            // return "Server = 192.168.163.107; Database = RekapSiswa; User Id = RESI ; Password = ATM_RekapSiswa";
        }
    }
}
  