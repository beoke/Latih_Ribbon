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
            rekapPersensiDal = new RekapPersensiDal();
            dataGridView1.DataSource = rekapPersensiDal.ListData();
        }
    }
}
