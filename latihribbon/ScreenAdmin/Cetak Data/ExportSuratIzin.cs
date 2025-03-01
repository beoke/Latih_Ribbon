using latihribbon.Dal;
using latihribbon.Model;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace latihribbon
{
    public partial class ExportSuratIzin : Form
    {
        private readonly MasukDal masukDal;
        private readonly KeluarDal keluarDal;
        private CultureInfo _culture;
        private int _infoExport;

        public ExportSuratIzin(int infoExport)
        {
            InitializeComponent();
            masukDal = new MasukDal();
            keluarDal = new KeluarDal();
            _culture = new CultureInfo("id-ID");
            this._infoExport = infoExport;
            RegisterEvent();
        }
        private void RegisterEvent() 
        {
            ButtonAturPrint.Click += ButtonAturPrint_Click;
        }

        private void ButtonAturPrint_Click(object sender, EventArgs e)
        {
            DateTime tgl1 = new DateTime(tgl1DT.Value.Year, tgl1DT.Value.Month, tgl1DT.Value.Day);
            DateTime tgl2 = new DateTime(tgl2DT.Value.Year, tgl2DT.Value.Month, tgl2DT.Value.Day);

            if (_infoExport == 0)
                ExportToExcel_Terlambat(tgl1, tgl2);
            else
                ExportToExcel_Keluar(tgl1, tgl2);
            
        }

        #region EXPORT TERLAMBAT
        private async void ExportToExcel_Terlambat(DateTime tgl1, DateTime tgl2)
        {
            Loading loading = new Loading();
            loading.WindowState = FormWindowState.Maximized;
           
            try
            {
                using (var package = new ExcelPackage())
                {
                    var groupedData = new List<MasukModel>();
                    var KelasCek = new List<string>();

                    var listSiswaTerlambat = masukDal.ListMasuk2(tgl1, tgl2).ToList();
                    groupedData.AddRange(listSiswaTerlambat);

                    if (!listSiswaTerlambat.Any())
                    {
                        new MesError("Belum Ada Siswa Yang Terlambat!").ShowDialog(this);
                        return;
                    }

                    loading.Show();
                    await Task.Delay(500);

                    var worksheet = package.Workbook.Worksheets.Add("Siswa Terlambat");

                    // Pengaturan awal worksheet
                    worksheet.Cells.Style.Font.Size = 12;

                    int currentRow = 1;

                    // Header tabel
                    worksheet.Cells[currentRow, 1].Value = "DAFTAR SISWA TERLAMBAT SMK N 1 BANTUL";
                    worksheet.Cells[currentRow + 1, 1].Value = $"TANGGAL : {tgl1DT.Value.ToString(@"d MMMM yyyy", _culture)} - {tgl2DT.Value.ToString(@"d MMMM yyyy", _culture)}";

                    worksheet.Cells[currentRow, 1, currentRow + 1, 7].Style.Font.Bold = true;
                    worksheet.Cells[currentRow, 1, currentRow + 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[currentRow, 1, currentRow, 7].Merge = true;
                    worksheet.Cells[currentRow + 1, 1, currentRow + 1, 7].Merge = true;

                    currentRow += 3;

                    // Kolom header
                    worksheet.Cells[currentRow, 1].Value = "NO";
                    worksheet.Cells[currentRow, 2].Value = "NIS";
                    worksheet.Cells[currentRow, 3].Value = "NAMA";
                    worksheet.Cells[currentRow, 4].Value = "KELAS";
                    worksheet.Cells[currentRow, 5].Value = "TERLAMBAT KE-";
                    worksheet.Cells[currentRow, 6].Value = "TANGGAL";
                    worksheet.Cells[currentRow, 7].Value = "ALASAN";

                    //Gaya untuk Header
                    using (var headerRange = worksheet.Cells[currentRow, 1, currentRow, 7])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Row(currentRow).Height = 36;
                        worksheet.Cells[currentRow, 5].Style.WrapText = true;
                    }

                    currentRow++;


                    int currentRowTopFirst = currentRow;
                    var listMergeCell = new List<CellMergeDto>();
                    // Data siswa Terlambat
                    int no = 1;
                    foreach (var student in groupedData.GroupBy(x => x.Nama))
                    {
                        if (currentRow > ExcelPackage.MaxRows)
                        {
                            new MesError("Jumlah data melebihi batas maksimum sheet Excel.").ShowDialog();
                            return;
                        }
                        bool firstData = true;
                        int terlambatKe = 1;
                        int currentRowTop = currentRow;
                        foreach (var item in student)
                        {
                            if (firstData)
                            {
                                worksheet.Cells[currentRow, 1].Value = no++;
                                worksheet.Cells[currentRow, 2].Value = item.NIS;
                                worksheet.Cells[currentRow, 3].Value = item.Nama;
                                worksheet.Cells[currentRow, 4].Value = item.NamaKelas;
                                worksheet.Cells[currentRow, 5].Value = terlambatKe++;
                                worksheet.Cells[currentRow, 6].Value = item.Tanggal.ToString("d MMMM yyyy", _culture);
                                worksheet.Cells[currentRow, 7].Value = item.Alasan;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, 1].Value = null;
                                worksheet.Cells[currentRow, 2].Value = null;
                                worksheet.Cells[currentRow, 3].Value = null;
                                worksheet.Cells[currentRow, 4].Value = null;
                                worksheet.Cells[currentRow, 5].Value = terlambatKe++;
                                worksheet.Cells[currentRow, 6].Value = item.Tanggal.ToString("d MMMM yyyy", _culture);
                                worksheet.Cells[currentRow, 7].Value = item.Alasan;
                            }
                            firstData = false;
                            currentRow++;
                        }

                        listMergeCell.Add(new CellMergeDto { CurrentRowTop = currentRowTop, CurrentRow = currentRow });
                    }

                    // Lebar kolom
                    worksheet.Column(1).Width = 6;
                    worksheet.Column(2).Width = 10;
                    worksheet.Column(3).AutoFit();
                    worksheet.Column(4).AutoFit();
                    worksheet.Column(4).Width += 1;
                    worksheet.Column(5).Width = 14;
                    worksheet.Column(6).Width = 25;
                    worksheet.Column(7).AutoFit();

                    //Merge Cell
                    foreach (var item in listMergeCell)
                    {
                        worksheet.Cells[item.CurrentRowTop, 1, item.CurrentRow - 1, 1].Merge = true; // No
                        worksheet.Cells[item.CurrentRowTop, 2, item.CurrentRow - 1, 2].Merge = true; // NIS
                        worksheet.Cells[item.CurrentRowTop, 3, item.CurrentRow - 1, 3].Merge = true; // NAMA
                        worksheet.Cells[item.CurrentRowTop, 4, item.CurrentRow - 1, 4].Merge = true; // KELAS
                    }

                    // Gaya border & Aligment Awal
                    using (var dataRange = worksheet.Cells[5, 1, currentRow - 1, 7])
                    {
                        dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
                    }
                    //Gaya Border All
                    using (var dataRange = worksheet.Cells[4, 1, currentRow - 1, 7])
                    {
                        dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }


                    //Gaya Alignment Khusus - No,NIS,Nama,Kelas
                    using (var dataRange = worksheet.Cells[currentRowTopFirst, 1, currentRow - 1, 4])
                    {
                        dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    }
                    using (var dataRange = worksheet.Cells[currentRowTopFirst, 7, currentRow - 1, 7]) // Alasan
                    {
                        dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }

                    // Gaya Lanjutan No,NIS, serta High Row All
                    for (int row = 5; row < currentRow; row++)
                    {
                        worksheet.Row(row).Height = 21;
                        worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    // Ambil default name dan tanggal
                    string SuratizinKeluar = "Rekap Siswa Izin Keluar ";
                    string tanggalAwal = tgl1DT.Value.ToString("yyyy-MM-dd");
                    string tanggalAkhir = tgl2DT.Value.ToString("yyyy-MM-dd");

                    // Menyimpan file
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx",
                        Title = "Save Excel File",
                        FileName = $"{SuratizinKeluar} {tanggalAwal} sampai {tanggalAkhir}.xlsx"

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
                loading.Close();
                await Task.Delay(300);
                new MesError($"Terjadi kesalahan saat eksport data: {ex.Message}").ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region EXPORT KELUAR
        private async void ExportToExcel_Keluar(DateTime tgl1, DateTime tgl2)
        {
            Loading loading = new Loading();
            loading.WindowState = FormWindowState.Maximized;

            try
            {
                using (var package = new ExcelPackage())
                {
                    var groupedData = new List<KeluarModel>();
                    var KelasCek = new List<string>();  

                    var listSiswaKeluar = keluarDal.ListData2(tgl1, tgl2).ToList();
                    groupedData.AddRange(listSiswaKeluar);

                    if (!listSiswaKeluar.Any())
                    {
                        new MesError("Belum Ada Siswa Yang Terlambat!").ShowDialog(this);
                        return;
                    }
                    loading.Show();
                    await Task.Delay(500);
                    var worksheet = package.Workbook.Worksheets.Add("Siswa Izin Keluar");

                    // Pengaturan awal worksheet
                    worksheet.Cells.Style.Font.Size = 12;

                    int currentRow = 1;

                    // Header tabel
                    worksheet.Cells[currentRow, 1].Value = "DAFTAR SISWA IZIN KELUAR SMK N 1 BANTUL";
                    worksheet.Cells[currentRow + 1, 1].Value = $"TANGGAL : {tgl1DT.Value.ToString(@"d MMMM yyyy", _culture)} - {tgl2DT.Value.ToString(@"d MMMM yyyy", _culture)}";

                    worksheet.Cells[currentRow, 1, currentRow + 1, 7].Style.Font.Bold = true;
                    worksheet.Cells[currentRow, 1, currentRow + 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[currentRow, 1, currentRow, 7].Merge = true;
                    worksheet.Cells[currentRow + 1, 1, currentRow + 1, 7].Merge = true;

                    currentRow += 3;

                    // Kolom header
                    worksheet.Cells[currentRow, 1].Value = "NO";
                    worksheet.Cells[currentRow, 2].Value = "NIS";
                    worksheet.Cells[currentRow, 3].Value = "NAMA";
                    worksheet.Cells[currentRow, 4].Value = "KELAS";
                    worksheet.Cells[currentRow, 5].Value = "IZIN KELUAR KE-";
                    worksheet.Cells[currentRow, 6].Value = "TANGGAL";
                    worksheet.Cells[currentRow, 7].Value = "TUJUAN";

                    //Gaya untuk Header
                    using (var headerRange = worksheet.Cells[currentRow, 1, currentRow, 7])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Row(currentRow).Height = 36;
                        worksheet.Cells[currentRow, 5].Style.WrapText = true;
                    }

                    currentRow++;


                    int currentRowTopFirst = currentRow;
                    var listMergeCell = new List<CellMergeDto>();
                    // Data siswa Izin Keluar
                    int no = 1;
                    foreach (var student in groupedData.GroupBy(x => x.Nama))
                    {
                        if (currentRow > ExcelPackage.MaxRows)
                        {
                            new MesError("Jumlah data melebihi batas maksimum sheet Excel.").ShowDialog();
                            return;
                        }
                        bool firstData = true;
                        int terlambatKe = 1;
                        int currentRowTop = currentRow;
                        foreach (var item in student)
                        {
                            if (firstData)
                            {
                                worksheet.Cells[currentRow, 1].Value = no++;
                                worksheet.Cells[currentRow, 2].Value = item.Nis;
                                worksheet.Cells[currentRow, 3].Value = item.Nama;
                                worksheet.Cells[currentRow, 4].Value = item.NamaKelas;
                                worksheet.Cells[currentRow, 5].Value = terlambatKe++;
                                worksheet.Cells[currentRow, 6].Value = item.Tanggal.ToString("d MMMM yyyy", _culture);
                                worksheet.Cells[currentRow, 7].Value = item.Tujuan;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, 1].Value = null;
                                worksheet.Cells[currentRow, 2].Value = null;
                                worksheet.Cells[currentRow, 3].Value = null;
                                worksheet.Cells[currentRow, 4].Value = null;
                                worksheet.Cells[currentRow, 5].Value = terlambatKe++;
                                worksheet.Cells[currentRow, 6].Value = item.Tanggal.ToString("d MMMM yyyy", _culture);
                                worksheet.Cells[currentRow, 7].Value = item.Tujuan;
                            }
                            firstData = false;
                            currentRow++;
                        }

                        listMergeCell.Add(new CellMergeDto { CurrentRowTop = currentRowTop, CurrentRow = currentRow });
                    }

                    // Lebar kolom
                    worksheet.Column(1).Width = 6;
                    worksheet.Column(2).Width = 10;
                    worksheet.Column(3).AutoFit();
                    worksheet.Column(4).AutoFit();
                    worksheet.Column(4).Width += 1;
                    worksheet.Column(5).Width = 14;
                    worksheet.Column(6).Width = 25;
                    worksheet.Column(7).AutoFit();

                    //Merge Cell
                    foreach (var item in listMergeCell)
                    {
                        worksheet.Cells[item.CurrentRowTop, 1, item.CurrentRow - 1, 1].Merge = true; // No
                        worksheet.Cells[item.CurrentRowTop, 2, item.CurrentRow - 1, 2].Merge = true; // NIS
                        worksheet.Cells[item.CurrentRowTop, 3, item.CurrentRow - 1, 3].Merge = true; // NAMA
                        worksheet.Cells[item.CurrentRowTop, 4, item.CurrentRow - 1, 4].Merge = true; // KELAS
                    }

                    // Gaya border & Aligment Awal
                    using (var dataRange = worksheet.Cells[5, 1, currentRow - 1, 7])
                    {
                        dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
                    }
                    //Gaya Border All
                    using (var dataRange = worksheet.Cells[4, 1, currentRow - 1, 7])
                    {
                        dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }


                    //Gaya Alignment Khusus - No,NIS,Nama,Kelas
                    using (var dataRange = worksheet.Cells[currentRowTopFirst, 1, currentRow - 1, 4])
                    {
                        dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    }
                    using (var dataRange = worksheet.Cells[currentRowTopFirst, 7, currentRow - 1, 7]) // Alasan
                    {
                        dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }

                    // Gaya Lanjutan No,NIS, serta High Row All
                    for (int row = 5; row < currentRow; row++)
                    {
                        worksheet.Row(row).Height = 21;
                        worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                       
                    // Ambil default name dan tanggal
                    string SuratTerlambat = "Rekap Terlambat Siswa ";
                    string tanggalAwal = tgl1DT.Value.ToString("yyyy-MM-dd");
                    string tanggalAkhir = tgl2DT.Value.ToString("yyyy-MM-dd");

                    // Menyimpan file
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx",
                        Title = "Save Excel File",
                        FileName = $"{SuratTerlambat} {tanggalAwal} sampai {tanggalAkhir}.xlsx"
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
                loading.Close();
                await Task.Delay(300);
                new MesError($"Terjadi kesalahan saat eksport data: {ex.Message}").ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}
