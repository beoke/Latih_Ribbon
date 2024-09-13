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
    public partial class PopUp : Form
    {
        private readonly SuratKeluarcs suratKeluarcs;
        public PopUp(SuratKeluarcs suratKeluarcs)
        {
            InitializeComponent();
            this.suratKeluarcs = suratKeluarcs;

            InitLabel();
            InitEvent();
        }

        private void InitLabel()
        {
            LabelNama.Text = $": {suratKeluarcs.TextNama}";
            LabelKelas.Text = $": {suratKeluarcs.TextKelas}";
            LabelTanggal.Text = $": {suratKeluarcs.TextTanggal}";
            LabelJamKeluar.Text = $": {suratKeluarcs.TextKeluar}";
            LabelMasuk.Text = $": {suratKeluarcs.PickerMasuk}";
            LabelKeperluan.Text = $": {suratKeluarcs.TextKeperluan}";
        }

        private void InitEvent()
        {
            BtnPrint.Click += BtnPrint_Click;
            BtnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        public void BtnPrint_Click(object sender, EventArgs e)
        {
            suratKeluarcs.Print();
            suratKeluarcs.Insert();

            this.Close();
            suratKeluarcs.Close();

            Pemakai pemakai = new Pemakai();
            pemakai.Show();
        }
    }
}
