using latihribbon.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    public partial class FormRekapPersensi : Form
    {
        private readonly RekapPersensiDal rekapPersensiDal;
        public FormRekapPersensi()
        {
            InitializeComponent();
            InitKeterangan();
            rekapPersensiDal = new RekapPersensiDal();
            dataGridView1.DataSource = rekapPersensiDal.ListData();
        }

        public void InitKeterangan()
        {
            List<string> Keterangan = new List<string>() { "Semua", "A", "I", "S" };
            KeteranganCombo.DataSource = Keterangan;
        }
    }
}
