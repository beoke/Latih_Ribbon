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

namespace latihribbon
{
    public partial class FormAbsensi : Form
    {
        private readonly AbsensiDal absensiDal;
        public FormAbsensi()
        {
            InitializeComponent();
            absensiDal = new AbsensiDal();
        }

        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            if (txtNIS.Text == "") return;
            var a = absensiDal.GetData(int.Parse(txtNIS.Text));
            var b = a == null ? "NULL" : "NOT NULL";
            MessageBox.Show(b);
        }
    }
}
