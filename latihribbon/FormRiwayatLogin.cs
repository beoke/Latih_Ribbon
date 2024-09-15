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
    public partial class FormRiwayatLogin : Form
    {

        private readonly RiwayatLoginDal _riwayatLoginDal;
        public FormRiwayatLogin()
        {
            _riwayatLoginDal = new RiwayatLoginDal();
            InitializeComponent();
            InitialGrid();
        }

        private void InitialGrid()
        {
            GridListRiwayatLogin.DataSource = _riwayatLoginDal.ListData();


            if (GridListRiwayatLogin.Rows.Count > 0)
            {
                GridListRiwayatLogin.ReadOnly = true;
                GridListRiwayatLogin.EnableHeadersVisualStyles = false;
                GridListRiwayatLogin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                GridListRiwayatLogin.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                GridListRiwayatLogin.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                GridListRiwayatLogin.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                GridListRiwayatLogin.RowTemplate.Height = 30;
                GridListRiwayatLogin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                GridListRiwayatLogin.ColumnHeadersHeight = 35;
            }
        }




    }
}
