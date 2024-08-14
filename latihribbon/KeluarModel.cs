using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Model
{
    public class KeluarModel
    {
        public int Id { get; set; }
        public int Nis { get; set; }
        public string Nama { get; set; }   
        public DateTime Tanggal {  get; set; }
        public DateTime JamKeluar { get; set; }
        public DateTime JamMasuk { get; set; }
        public string Keperluan {  get; set; }
    }
}
