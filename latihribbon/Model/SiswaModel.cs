using System;

namespace latihribbon
{
    public class SiswaModel
    {
        public int Nis { get; set; }
        public int Persensi { get; set; }
        public string Nama { get; set; }
        public string JenisKelamin { get; set; }
        public int IdKelas { get; set; }
        public string NamaKelas { get; set; }
        public string Tahun { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int IsActive { get; set; } = 1;
    }
}
