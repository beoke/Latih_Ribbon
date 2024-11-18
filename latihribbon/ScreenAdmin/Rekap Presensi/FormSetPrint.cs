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
        private readonly SetPrintDal _setPrintDal;

        public FormSetPrint()
        {
            _setPrintDal = new SetPrintDal();
            rekapPersensiDal = new RekapPersensiDal();
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
                }).ToList();

            foreach (var item in data)
            {
                ListBoxKelas.Items.Add(item.NamaKelas);
            }
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
            if(tgl1DT.Value == tgl2DT.Value)
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
            ExportToExcel(data,tgl1DT.Value,tgl2DT.Value);
        }

        private void ExportToExcel(List<string> selectedClasses,DateTime tgl1, DateTime tgl2)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var groupedData = new Dictionary<string, List<RekapPersensiModel>>();
                    var KelasCek = new List<string>();

                    foreach (var kelas in selectedClasses)
                    {
                        var studentsData = rekapPersensiDal.GetStudentDataByClass(kelas,tgl1,tgl2).ToList();

                        if(!studentsData.Any()) KelasCek.Add(kelas);
                        var angkatan = kelas.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries).First();

                        if (!groupedData.ContainsKey(angkatan))
                        {
                            groupedData[angkatan] = new List<RekapPersensiModel>();
                        }

                        groupedData[angkatan].AddRange(studentsData);
                    }
                    if(KelasCek.Count != 0)
                    {
                        new MesError($"Tidak Ada Data Untuk Kelas :  { string.Join(", ", KelasCek)} \nJika data tersebut memang tidak diperlukan, \nBatalkan centang pada kelas tersebut",3).ShowDialog(this);
                        return;
                    }
                    foreach (var angkatan in groupedData.Keys)
                    {
                        var studentsInAngkatan = groupedData[angkatan];
                        var groupedByKelas = studentsInAngkatan.GroupBy(s => s.NamaKelas).ToList();
                        var worksheet = package.Workbook.Worksheets.Add(angkatan);

                        worksheet.Cells[1, 1].Value = $"Data Siswa Kelas: {angkatan}";
                        worksheet.Cells[2, 1].Value = $"Tanggal: {tgl1DT.Value.ToString("dd MMMM yyyy")} - {tgl2DT.Value.ToString("dd MMMM yyyy")}";
                        worksheet.Cells[1, 1].Style.Font.Bold = true;

                        int currentRow = 4;

                        foreach (var kelasGroup in groupedByKelas)
                        {
                            var uniqueStudents = kelasGroup.GroupBy(s => s.Nama).Select(g => g.First()).ToList();

                            var rekapPerSiswa = uniqueStudents.Select(student => new
                            {
                                student.Persensi,
                                student.Nama,
                                student.NamaKelas,
                                S = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "S") == 0 ? (int?)null : studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "S"),
                                I = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "I") == 0 ? (int?)null : studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "I"),
                                A = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "A") == 0 ? (int?)null : studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "A")
                            }).ToList();

                            worksheet.Cells[currentRow, 1].Value = $"Tabel Kelas: {kelasGroup.Key}";
                            worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                            currentRow++;

                            worksheet.Cells[currentRow, 1].Value = "No";
                            worksheet.Cells[currentRow, 2].Value = "Nama";
                            worksheet.Cells[currentRow, 3].Value = "Kelas";
                            worksheet.Cells[currentRow, 4].Value = "S"; // Sakit
                            worksheet.Cells[currentRow, 5].Value = "I"; // Izin
                            worksheet.Cells[currentRow, 6].Value = "A"; // Alpa

                            using (var headerRange = worksheet.Cells[currentRow, 1, currentRow, 6])
                            {
                                headerRange.Style.Font.Bold = true;
                                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            
                            currentRow++;

                            foreach (var rekap in rekapPerSiswa)
                            {
                                if (currentRow > ExcelPackage.MaxRows) // Cek apakah currentRow melebihi batas
                                {
                                    new MesError("Jumlah data melebihi batas maksimum sheet Excel.").ShowDialog();
                                    return;
                                }

                                worksheet.Cells[currentRow, 1].Value = rekap.Persensi; // No (mulai dari 1)
                                worksheet.Cells[currentRow, 2].Value = rekap.Nama; // Nama siswa
                                worksheet.Cells[currentRow, 3].Value = rekap.NamaKelas; // Kelas siswa
                                worksheet.Cells[currentRow, 4].Value = rekap.S; // Jumlah Sakit
                                worksheet.Cells[currentRow, 5].Value = rekap.I; // Jumlah Izin
                                worksheet.Cells[currentRow, 6].Value = rekap.A; // Jumlah Alpa

                                currentRow++;
                            }
                            currentRow += 5;
                        }
                        worksheet.Column(2).AutoFit();
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx",
                        Title = "Save Excel File"
                    };

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
                this.DialogResult= DialogResult.OK;
                this.Close();
            }
        }
    }
}