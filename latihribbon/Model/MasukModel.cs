using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Model
{
    public class MasukModel
    {
        public int Id { get; set; }
        public int NIS { get; set; }
        public string Nama{ get; set; }
        public string NamaKelas {  get; set; }
        public DateTime Tanggal {  get; set; }
        public string JamMasuk { get; set; }
        public string Alasan {  get; set; }
    }
}
