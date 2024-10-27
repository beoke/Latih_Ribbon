using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Model
{
    public class KelasModel
    {
        public int Id {  get; set; }
        public string NamaKelas { get; set; }

        public string Rombel {  get; set; }
        public int IdJurusan {  get; set; }
        public string Tingkat {  get; set; }
        public int status { get; set; }
        public string NamaJurusan { get; set; }
    }
}
