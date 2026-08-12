using System;

namespace latihribbon.Model
{
    public class JurusanModel
    {
        public int Id { get; set; }
        public string NamaJurusan { get; set; }
        public string Kode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int IsActive { get; set; } = 1;
    }
}