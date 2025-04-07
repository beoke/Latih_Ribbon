using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon.Model
{
    public class RekapPersensiModel
    {
        public int Nis { get; set; }
        public int Persensi { get; set; }
        public string Nama { get; set; }
        public string NamaKelas { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }
    }

    public class Angkatan
    {
        public string Tahun { get; set; }  // Misalnya: "2024/2025"
        public List<Kelas> KelasList { get; set; } = new List<Kelas>();
    }

    public class Kelas
    {
        public string NamaKelas { get; set; }  // Misalnya: "XII RPL 1"
        public List<Siswa> SiswaList { get; set; } = new List<Siswa>();
    }

    public class Siswa
    {
        public string NIS { get; set; }
        public string Nama { get; set; }
        public int Persensi { get; set; }
        public List<Absensi> RiwayatAbsensi { get; set; } = new List<Absensi>();
    }

    public class Absensi
    {
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; } // Misalnya: "Hadir", "Izin", "Sakit"
    }
}
