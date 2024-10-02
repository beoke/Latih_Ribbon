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


        private void ButtonAturPrint_Click(object sender, EventArgs e)
        {
            if (ListBoxKelas.CheckedItems.Count > 0)
            {
                List<string> data = new List<string>();
                foreach (var item in ListBoxKelas.CheckedItems)
                {
                    data.Add(item.ToString());
                }
                ExportToExcel(data);
            }
            else
            {
                MessageBox.Show("Pilih data kelas terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportToExcel(List<string> selectedClasses)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var groupedData = new Dictionary<string, List<RekapPersensiModel>>();

                    foreach (var kelas in selectedClasses)
                    {
                        var studentsData = rekapPersensiDal.GetStudentDataByClass(kelas).ToList();

                        var angkatan = kelas.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries).First();

                        if (!groupedData.ContainsKey(angkatan))
                        {
                            groupedData[angkatan] = new List<RekapPersensiModel>();
                        }

                        groupedData[angkatan].AddRange(studentsData);
                    }

                    foreach (var angkatan in groupedData.Keys)
                    {
                        var studentsInAngkatan = groupedData[angkatan];
                        if (studentsInAngkatan == null || studentsInAngkatan.Count == 0)
                        {
                            MessageBox.Show($"Tidak ada data untuk angkatan {angkatan}.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }

                        var groupedByKelas = studentsInAngkatan.GroupBy(s => s.Kelas).ToList();

                        var worksheet = package.Workbook.Worksheets.Add(angkatan);

                        worksheet.Cells[1, 1].Value = $"Data Siswa Angkatan: {angkatan}";
                        worksheet.Cells[1, 1].Style.Font.Bold = true;

                        int currentRow = 4;

                        foreach (var kelasGroup in groupedByKelas)
                        {
                            var uniqueStudents = kelasGroup.GroupBy(s => s.Nama).Select(g => g.First()).ToList();

                            var rekapPerSiswa = uniqueStudents.Select(student => new
                            {
                                student.Nama,
                                student.Kelas,
                                S = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "S"),
                                I = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "I"),
                                A = studentsInAngkatan.Count(s => s.Nama == student.Nama && s.Keterangan == "A")
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
                                    MessageBox.Show("Jumlah data melebihi batas maksimum sheet Excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                worksheet.Cells[currentRow, 1].Value = currentRow - 3; // No (mulai dari 1)
                                worksheet.Cells[currentRow, 2].Value = rekap.Nama; // Nama siswa
                                worksheet.Cells[currentRow, 3].Value = rekap.Kelas; // Kelas siswa
                                worksheet.Cells[currentRow, 4].Value = rekap.S; // Jumlah Sakit
                                worksheet.Cells[currentRow, 5].Value = rekap.I; // Jumlah Izin
                                worksheet.Cells[currentRow, 6].Value = rekap.A; // Jumlah Alpa

                                currentRow++;
                            }

                            currentRow += 5;
                        }
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
                        MessageBox.Show("Data berhasil dieksport ke Excel", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat eksport data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }























    }
}