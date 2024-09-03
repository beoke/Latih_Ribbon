using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Model
{
    public class AbsensiModel
    {
        public int ID { get; set; }
        public int Nis { get; set; }
        public string Nama { get; set; }
        public int Persensi { get; set; }
        public string Kelas { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }
    }
}
