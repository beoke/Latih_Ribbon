  var presensi = int.TryParse(workSheet.Cells[row, 1].Value?.ToString(), out int parsedPresensi) ? parsedPresensi : (int?)null;
  var nis = long.TryParse(workSheet.Cells[row, 2].Value?.ToString(), out long parsedNis) ? parsedNis : (long?)null;
  var nama = workSheet.Cells[row, 3].Value?.ToString();
  var kelas = workSheet.Cells[row, 4].Value?.ToString();
  var jenisKelamin = workSheet.Cells[row, 5].Value?.ToString();
  var tahun = int.TryParse(workSheet.Cells[row, 6].Value?.ToString(), out int parsedTahun) ? parsedTahun : (int?)null;