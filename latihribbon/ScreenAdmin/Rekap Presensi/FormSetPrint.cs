using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace latihribbon
{
    public partial class FormSetPrint : Form
    {
        private readonly SetPrintDal _setPrintDal;
        public FormSetPrint()
        {
            _setPrintDal = new SetPrintDal();
            InitializeComponent();
            ControlEvent();
            InitialListBox();
        }

        private void InitialListBox()
        {
            var data = _setPrintDal.ListKelas()
                .Select(x => new KelasModel
                {
                    NamaKelas = x.NamaKelas,
                }).ToList();

            foreach ( var item in data )
            {
                ListBoxKelas.Items.Add( item.NamaKelas );
            }
        }

        private void ControlEvent()
        {
            CheckBoxAll.CheckedChanged += CheckBoxAll_CheckedChanged;
            ButtonAturPrint.Click += ButtonAturPrint_Click;
            
        }

        private void ButtonAturPrint_Click(object sender, EventArgs e)
        {
            if (ListBoxKelas.CheckedItems.Count > 0)
            {
                string dt = string.Empty;

                foreach (var item in ListBoxKelas.CheckedItems)
                {
                    dt += item.ToString();
                }

                MessageBox.Show("Data berhasil dieksport", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pilih data kelas terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAll.Checked)
            {
                for (int i = 0; i < ListBoxKelas.Items.Count; i++)
                {
                    ListBoxKelas.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < ListBoxKelas.Items.Count; i++)
                {
                    ListBoxKelas.SetItemChecked(i, false);
                }
            }
        }


        private void Print()
        {
            
        }
        
    }
}




