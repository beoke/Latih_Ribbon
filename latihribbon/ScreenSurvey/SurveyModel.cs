using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon
{
    public class SurveyModel
    {
        public int SurveyId { get; set; }
        public int HasilSurvey { get; set; }
        public DateTime Tanggal { get; set; }
        public TimeSpan Waktu { get; set; }
    }
}
