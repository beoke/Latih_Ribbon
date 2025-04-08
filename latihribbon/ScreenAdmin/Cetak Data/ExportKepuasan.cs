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
    public partial class ExportKepuasan : Form
    {
        private readonly SurveyDal _surveyDal;
        public ExportKepuasan()
        {
            InitializeComponent();
            _surveyDal = new SurveyDal();
            RegisterEvent();
        }
        private void RegisterEvent()
        {
            btnExport.Click += BtnExport_Click;
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            Loading loading = new Loading();
            loading.WindowState = FormWindowState.Maximized;

            var culture = new CultureInfo("id-ID");
            DateTime tgl1 = tgl1DT.Value.Date;
            DateTime tgl2 = tgl2DT.Value.Date;

            try
            {
                using (var package = new ExcelPackage())
                {
                    var groupedData = new List<MasukModel>();
                    var KelasCek = new List<string>();

                    var listSurvey = _surveyDal.GetDataExport(tgl1, tgl2).ToList();

                    if (!listSurvey.Any())
                    {
                        new MesError("Tidak ada data pada rentang tanggal tersebut!").ShowDialog(this);
                        return;
                    }

                    loading.Show();
                    await Task.Delay(500);

                    var worksheet = package.Workbook.Worksheets.Add("Survey Kepuasan");

                    // Pengaturan awal worksheet
                    worksheet.Cells.Style.Font.Size = 12;

                    int currentRow = 1;

                    // JUDUL
                    worksheet.Cells[currentRow, 1].Value = "SURVEY KEPUASAN SMK N 1 BANTUL";
                    worksheet.Cells[currentRow + 1, 1].Value = $"TANGGAL : {tgl1DT.Value.ToString(@"d MMMM yyyy", culture)} - {tgl2DT.Value.ToString(@"d MMMM yyyy", culture)}";

                    worksheet.Cells[currentRow, 1, currentRow, 8].Merge = true; //HEADER
                    worksheet.Cells[currentRow + 1, 1, currentRow + 1, 8].Merge = true; //TANGGAL

                    currentRow += 3;

                    worksheet.Cells[currentRow, 1].Value = "DATA RESPONDEN";
                    worksheet.Cells[currentRow, 6].Value = "REKAPITULASI";
                    worksheet.Cells[currentRow, 1, currentRow, 4].Merge = true; //RESPONDEN
                    worksheet.Cells[currentRow, 6, currentRow, 8].Merge = true; //REKAPITULASI

                    currentRow ++;

                    //TABEL
                    worksheet.Cells[currentRow, 1].Value = "NO";
                    worksheet.Cells[currentRow, 2].Value = "TANGGAL";
                    worksheet.Cells[currentRow, 3].Value = "JAM";
                    worksheet.Cells[currentRow, 4].Value = "JAWABAN SURVEY";

                    worksheet.Cells[currentRow, 6].Value = "JAWABAN SURVEY";
                    worksheet.Cells[currentRow, 7].Value = "TOTAL";
                    worksheet.Cells[currentRow, 8].Value = "PERSENTASE";

                    //JUDUL & HEADER STYLE
                    worksheet.Cells[1, 1, 4, 8].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Row(4).Height = 22;
                    worksheet.Row(5).Height = 22;



                    //Gaya untuk Header Responden
                    using (var headerRange = worksheet.Cells[currentRow, 1, currentRow, 4])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[currentRow, 5].Style.WrapText = true;
                    }

                    //Gaya untuk Header Rekapitulasi
                    using (var headerRange = worksheet.Cells[currentRow, 6, currentRow, 8])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[currentRow, 5].Style.WrapText = true;
                    }

                    currentRow++;

                    //Value Data Rekapitulasi
                    int total_data = listSurvey.Count();
                    int total_puas = listSurvey.Count(x => x.HasilSurvey == 1); 
                    int total_tidak_puas = listSurvey.Count(x => x.HasilSurvey == 0);

                    decimal persen_puas = Math.Round((decimal)total_puas / total_data * 100, 1); // Dibulatkan 1 digit
                    decimal persen_tidak_puas = 100 - persen_puas;

                    worksheet.Cells[currentRow, 7].Value = total_puas; // TOTAL PUAS
                    worksheet.Cells[currentRow + 1, 7].Value = total_tidak_puas; // TOTAL TIDAK PUAS

                    worksheet.Cells[currentRow, 8].Value = persen_puas / 100;
                    worksheet.Cells[currentRow + 1, 8].Value = persen_tidak_puas / 100;

                    worksheet.Cells[currentRow, 8].Style.Numberformat.Format = "0.0%";
                    worksheet.Cells[currentRow + 1, 8].Style.Numberformat.Format = "0.0%";

                    worksheet.Cells[currentRow, 6].Value = "PUAS";
                    worksheet.Cells[currentRow + 1, 6].Value = "TIDAK PUAS";

                    //Style Rekapitulasi
                    worksheet.Cells[currentRow, 7, currentRow + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    int currentRowTopFirst = currentRow;
                    var listMergeCell = new List<CellMergeDto>();
                    // Data siswa Terlambat
                    int no = 1;
                    foreach (var survey in listSurvey)
                    {
                        if (currentRow > ExcelPackage.MaxRows)
                        {
                            new MesError("Jumlah data melebihi batas maksimum sheet Excel.").ShowDialog();
                            return;
                        }

                        worksheet.Cells[currentRow, 1].Value = no++;
                        worksheet.Cells[currentRow, 2].Value = survey.Tanggal.ToString("d MMMM yyyy", culture);
                        worksheet.Cells[currentRow, 3].Value = survey.Waktu;
                        worksheet.Cells[currentRow, 4].Value = survey.HasilSurvey == 0 ? "Tidak Puas" : "Puas";

                        currentRow ++;
                    }

                    // Lebar kolom
                    worksheet.Column(1).Width = 6;
                    worksheet.Column(2).Width = 24;
                    worksheet.Column(3).Width = 11;
                    worksheet.Column(4).Width = 22;

                    worksheet.Column(6).Width = 21;
                    worksheet.Column(7).Width = 13.5;
                    worksheet.Column(8).Width = 15.5;

                    //Gaya Border Data Responden
                    using (var dataRange = worksheet.Cells[5, 1, currentRow - 1, 4])
                    {
                        dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                    
                    //Gaya Border Rekapitulasi
                    using (var dataRange = worksheet.Cells[5, 6, 7, 8]) // row, col, row, col
                    {
                        dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                    //Aligment & Row Height
                    for (int row = 6; row < currentRow; row++)
                    {
                        worksheet.Row(row).Height = 21;
                        worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 3, row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    // Ambil default name dan tanggal
                    string SuratizinKeluar = "SURVEY KEPUASAN SMK N 1 BANTUL ";
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
    }
}
