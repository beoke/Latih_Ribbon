using DocumentFormat.OpenXml.Presentation;
using latihribbon.Dal;
using latihribbon.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;


namespace latihribbon
{
    public partial class FormSetPrint : Form
    {
        private readonly RekapPersensiDal rekapPersensiDal;
        private readonly MasukDal masukDal;
        private readonly SetPrintDal _setPrintDal;
        private CultureInfo _culture = new CultureInfo("id-ID");

        public FormSetPrint()
        {
            _setPrintDal = new SetPrintDal();
            rekapPersensiDal = new RekapPersensiDal();
            masukDal = new MasukDal();
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ControlEvent();
            InitialListBox();
        }

        private void InitialListBox()
        {
            var data = _setPrintDal.ListKelas()
                .Select(x => new KelasModel
                {
                    NamaKelas = x.NamaKelas,
                }).ToArray();
            foreach (var item in data)
                ListBoxKelas.Items.Add(item.NamaKelas);
        }

        private void ControlEvent()
        {
            CheckBoxAll.CheckedChanged += CheckBoxAll_CheckedChanged;
            ButtonAturPrint.Click += ButtonAturPrint_Click;
        }

        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAll.Checked)
                for (int i = 0; i < ListBoxKelas.Items.Count; i++)
                    ListBoxKelas.SetItemChecked(i, true);
            else
                for (int i = 0; i < ListBoxKelas.Items.Count; i++)
                    ListBoxKelas.SetItemChecked(i, false);
        }
        private void ButtonAturPrint_Click(object sender, EventArgs e)
        {
            if (tgl1DT.Value == tgl2DT.Value)
            {
                new MesWarningOK("Atur Rentang Tanggal Terlebih Dahulu!").ShowDialog();
                return;
            }
            if (ListBoxKelas.CheckedItems.Count < 1)
            {
                new MesWarningOK("Pilih data kelas terlebih dahulu!").ShowDialog();
                return;
            }
            List<string> data = new List<string>();
            foreach (var item in ListBoxKelas.CheckedItems)
            {
                data.Add(item.ToString());
            }
            DateTime tgl1 = new DateTime(tgl1DT.Value.Year,tgl1DT.Value.Month, tgl1DT.Value.Day);
            DateTime tgl2 = new DateTime(tgl2DT.Value.Year,tgl2DT.Value.Month, tgl2DT.Value.Day);
            
            ExportToExcel_Rekap(data, tgl1, tgl2);
        }

        #region EXPORT REKAP
        private async void ExportToExcel_Rekap(List<string> selectedClasses, DateTime tgl1, DateTime tgl2)
        {
            Loading loading = new Loading();
            loading.WindowState = FormWindowState.Maximized;
            
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var groupedData = new Dictionary<string, List<RekapPersensiModel>>();
                    var KelasCek = new List<string>();

                    foreach (var kelas in selectedClasses)
                    {
                        var studentsData = rekapPersensiDal.GetStudentDataByClass(kelas, tgl1, tgl2).ToList();

                        if (!studentsData.Any()) KelasCek.Add(kelas);
                        var angkatan = kelas.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries).First();

                        if (!groupedData.ContainsKey(angkatan))
                        {
                            groupedData[angkatan] = new List<RekapPersensiModel>();
                        }

                        groupedData[angkatan].AddRange(studentsData);
                    }
                    if (KelasCek.Count != 0)
                    {
                        new MesError($"Tidak Ada Data Untuk Kelas :  {string.Join(", ", KelasCek)} \nJika data tersebut memang tidak diperlukan, \nBatalkan centang pada kelas tersebut", 3).ShowDialog(this);
                        return;
                    }
                    loading.Show();
                    await Task.Delay(500);
                    foreach (var angkatan in groupedData.Keys)
                    {
                        var studentsInAngkatan = groupedData[angkatan];
                        var groupedByKelas = studentsInAngkatan.GroupBy(s => s.NamaKelas).ToList();
                        var worksheet = package.Workbook.Worksheets.Add(angkatan);

                        // Pengaturan awal worksheet
                        worksheet.Cells.Style.Font.Size = 12;

                        int currentRow = 1;
                        int currentRowTop = 5;

                        foreach (var kelasGroup in groupedByKelas)
                        {
                            var uniqueStudents = kelasGroup.GroupBy(s => s.Nama).Select(g => g.First()).ToList();

                            // Rekap per siswa
                            var rekapPerSiswa = uniqueStudents.Select(student => new
                            {
                                student.Persensi,
                                student.Nama,
                                student.NamaKelas,
                                S = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "S") == 0 ? (int?)null
                                    : studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "S"),
                                I = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "I") == 0 ? (int?)null
                                    : studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "I"),
                                A = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "A") == 0 ? (int?)null
                                    : studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "A")
                            }).ToList();

                            // Header tabel
                            worksheet.Cells[currentRow, 1].Value = "DAFTAR ABSENSI SISWA";
                            worksheet.Cells[currentRow + 1, 1].Value = $"TANGGAL : {tgl1DT.Value.ToString(@"d MMMM yyyy",_culture)} - {tgl2DT.Value.ToString(@"d MMMM yyyy",_culture)}";
                            worksheet.Cells[currentRow + 2, 1].Value = $"KELAS : {kelasGroup.Key}";

                            worksheet.Cells[currentRow, 1, currentRow + 2, 6].Style.Font.Bold = true;
                            worksheet.Cells[currentRow, 1, currentRow + 2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            worksheet.Cells[currentRow, 1, currentRow, 6].Merge = true;
                            worksheet.Cells[currentRow + 1, 1, currentRow + 1, 6].Merge = true;
                            worksheet.Cells[currentRow + 2, 1, currentRow + 2, 6].Merge = true;

                            currentRow += 4;

                            // Kolom header
                            worksheet.Cells[currentRow, 1].Value = "NO";
                            worksheet.Cells[currentRow, 2].Value = "NAMA";
                            worksheet.Cells[currentRow, 3].Value = "KELAS";
                            worksheet.Cells[currentRow, 4].Value = "S";
                            worksheet.Cells[currentRow, 5].Value = "I";
                            worksheet.Cells[currentRow, 6].Value = "A";

                            using (var headerRange = worksheet.Cells[currentRow, 1, currentRow, 6])
                            {
                                headerRange.Style.Font.Bold = true;
                                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }

                            currentRow++;

                            // Data siswa
                            foreach (var rekap in rekapPerSiswa)
                            {
                                if (currentRow > ExcelPackage.MaxRows)
                                {
                                    loading.Close();
                                    new MesError("Jumlah data melebihi batas maksimum sheet Excel.").ShowDialog();
                                    return;
                                }

                                worksheet.Cells[currentRow, 1].Value = rekap.Persensi;
                                worksheet.Cells[currentRow, 2].Value = rekap.Nama;
                                worksheet.Cells[currentRow, 3].Value = rekap.NamaKelas;
                                worksheet.Cells[currentRow, 4].Value = rekap.S;
                                worksheet.Cells[currentRow, 5].Value = rekap.I;
                                worksheet.Cells[currentRow, 6].Value = rekap.A;

                                currentRow++;
                            }

                            // Gaya border
                            using (var dataRange = worksheet.Cells[currentRowTop, 1, currentRow - 1, 6])
                            {
                                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            }

                            //Gaya Keterangan Absen
                            using (var dataRange = worksheet.Cells[currentRowTop-1, 4, currentRow -1, 6])
                            {
                                dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                dataRange.Style.Font.Bold = true;
                            }

                            // Tinggi baris
                            for (int row = currentRowTop; row < currentRow; row++)
                            {
                                worksheet.Row(row).Height = 21;
                                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            worksheet.Row(currentRowTop).Height = 23;

                            // Lebar kolom
                            worksheet.Column(3).AutoFit();
                            worksheet.Column(3).Width += 1;
                            worksheet.Column(1).Width = 6;
                            worksheet.Column(2).AutoFit();
                            worksheet.Column(4).Width = 5;
                            worksheet.Column(5).Width = 5;
                            worksheet.Column(6).Width = 5;

                            currentRow += 3;
                            currentRowTop = currentRow + 4;
                        }
                    }
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx",
                        Title = "Save Excel File"
                    };
                    loading.Close();
                    await Task.Delay(300);
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        new MesInformasi("Data berhasil dieksport ke Excel").ShowDialog();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new MesError($"Terjadi kesalahan saat eksport data: {ex.Message}").ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}

public class CellMergeDto
{
    public int CurrentRowTop { get; set; }
    public int CurrentRow {  get; set; }
}