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
   
    public partial class FormKeluar : Form
    {
        private DbDal db;
        public FormKeluar()
        {
            InitializeComponent();
            db=new DbDal();
        }

        public void Filter()
        {
            string nama, kelas, tahun;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;


        }

        

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
        


        }
    }
}
