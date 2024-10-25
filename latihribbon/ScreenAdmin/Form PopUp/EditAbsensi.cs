using DocumentFormat.OpenXml.Wordprocessing;
using latihribbon.Model;
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
    public partial class EditAbsensi : Form
    {
        private readonly Dal.AbsensiDal absensiDal;
        private readonly Dal.SiswaDal siswaDal;
        private int globalId = 0;
        private DateTime globalTgl;
        private bool InternalChanged = true;
        public EditAbsensi(int Id)
        {
            InitializeComponent();
            absensiDal = new Dal.AbsensiDal();
            siswaDal = new Dal.SiswaDal();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            globalId = Id;
            GetData(Id);
            RegisterEvent();
            txtNIS.MaxLength = 9;
            txtPersensi.MaxLength = 3;
        }
        private void GetData(int Id)
        {
            var absensi = absensiDal.GetData(Id);
            if (absensi is null) return;
            txtNIS.Text = absensi.Nis.ToString();
            txtNama.Text = absensi.Nama;
            txtPersensi.Text = absensi.Persensi.ToString();
            txtKelas.Text = absensi.NamaKelas;
            tglDT.Value = absensi.Tanggal;
            Izinradio.Checked = absensi.Keterangan == "I" ? true : false;
            sakitRadio.Checked = absensi.Keterangan == "S" ? true : false;
            alphaRadio.Checked = absensi.Keterangan == "A" ? true : false;
            globalTgl = absensi.Tanggal;
        }
        private void RegisterEvent()
        {
            txtNIS.TextChanged += TxtNIS_TextChanged;
            txtPersensi.TextChanged += TxtPK_TextChanged;
            txtKelas.TextChanged += TxtPK_TextChanged;
            btnSave.Click += BtnSave_Click;
            txtNIS.KeyPress += Input_KeyPress;
            txtPersensi.KeyPress += Input_KeyPress;
            btnKelas.Click += BtnKelas_Click;
        }

        private void BtnKelas_Click(object sender, EventArgs e)
        {
           // new PopUpKelas().ShowDialog();
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(txtNama.Text == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog();
                return;
            }
            var absensi = new AbsensiModel()
            {
                Id = globalId,
                Nis = Convert.ToInt32(txtNIS.Text),
                Tanggal = tglDT.Value,
                Keterangan = Izinradio.Checked ? "I" : sakitRadio.Checked ? "S" : alphaRadio.Checked ? "A" : ""
            };
            var cekData = absensiDal.GetByCondition("WHERE p.Nis=@Nis AND p.Tanggal=@Tanggal", new {Nis=Convert.ToInt32(txtNIS.Text),Tanggal=tglDT.Value});
            if(cekData != null && tglDT.Value != globalTgl)
            {
                new MesWarningOK($"{txtNama.Text} Sudah Absensi Pada " + tglDT.Value.ToString("dd/MM/yyyy")).ShowDialog();
                return;
            }
            if (new MesQuestionYN("Update Data?").ShowDialog() != DialogResult.Yes) return;
            absensiDal.Update(absensi);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void TxtPK_TextChanged(object sender, EventArgs e)
        {
            if (!InternalChanged) return;
            InternalChanged = false;
            int persensi = txtPersensi.Text == "" ? 0 : Convert.ToInt16(txtPersensi.Text);
            string kelas = txtKelas.Text;
            if (persensi != 0 || kelas != "")
            {
                var data = absensiDal.GetByAbsensiKelas(kelas,persensi);
                txtNIS.Text = data?.Nis.ToString() ?? string.Empty;
                txtNama.Text = data?.Nama ?? string.Empty;
            }
            else
            {
                txtNama.Text = "";
                txtPersensi.Text = "";
                txtKelas.Text = "";
            }
            InternalChanged = true;
        }

        private void TxtNIS_TextChanged(object sender, EventArgs e)
        {
            if (!InternalChanged) return;
            InternalChanged = false;
            if(txtNIS.Text.Length >= 5)
            {
                var data = siswaDal.GetData(Convert.ToInt32(txtNIS.Text));
                if(data != null)
                {
                    txtNama.Text = data.Nama;
                    txtPersensi.Text = data.Persensi.ToString();
                    txtKelas.Text = data.NamaKelas;
                    lblNisTidakDitemukan.Visible = false;
                }
                else
                {
                    txtNama.Text = "";
                    txtPersensi.Text = "";
                    txtKelas.Text = "";
                    lblNisTidakDitemukan.Visible = true;
                }
            }
            else
            {
                txtNama.Text = "";
                txtPersensi.Text = "";
                txtKelas.Text = "";
                lblNisTidakDitemukan.Visible = false;
            }
            InternalChanged = true;
        }
    }
}
