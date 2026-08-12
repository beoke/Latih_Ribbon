using latihribbon.Dal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    /// <summary>
    /// Form detail log perubahan.
    /// Menampilkan before/after data untuk operasi UPDATE.
    /// Untuk INSERT, DELETE, ACTIVATE, DEACTIVATE — menampilkan snapshot data.
    /// </summary>
    public partial class FormDataLogDetail : Form
    {
        private readonly DataLogModel _log;

        public FormDataLogDetail(DataLogModel log)
        {
            InitializeComponent();
            _log = log ?? throw new ArgumentNullException(nameof(log));
            this.Load += FormDataLogDetail_Load;
        }

        private void FormDataLogDetail_Load(object sender, EventArgs e)
        {
            PopulateHeader();
            PopulateContent();
        }

        private void PopulateHeader()
        {
            lblLogId.Text = $"Log ID: {_log.LogId}";
            lblTabel.Text = $"Tabel: {_log.ReferenceTable}";
            lblAction.Text = $"Aksi: {_log.Action}";
            lblPkId.Text = $"ID Data: {_log.PkId}";
            lblUser.Text = $"User: {_log.User ?? "-"}";
            lblWaktu.Text = $"Waktu: {_log.Timestamp}";

            // Warna action label
            Color actionColor;
            switch (_log.Action)
            {
                case "INSERT": actionColor = Color.FromArgb(39, 174, 96); break;
                case "UPDATE": actionColor = Color.FromArgb(230, 126, 34); break;
                case "DELETE": actionColor = Color.FromArgb(192, 57, 43); break;
                case "ACTIVATE": actionColor = Color.FromArgb(41, 128, 185); break;
                case "DEACTIVATE": actionColor = Color.FromArgb(142, 68, 173); break;
                default: actionColor = Color.Gray; break;
            }
            lblAction.ForeColor = actionColor;
            lblAction.Font = new Font(lblAction.Font, FontStyle.Bold);

            if (_log.Action == "DELETE")
            {
                btnRestore.Visible = true;
            }
        }

        private void PopulateContent()
        {
            if (string.IsNullOrWhiteSpace(_log.ContentJson))
            {
                ShowRawContent("(Tidak ada data log)");
                return;
            }

            try
            {
                // ContentJson structure: { "Before": {...}, "After": {...} } untuk UPDATE
                //                        { "Data": {...} } untuk INSERT/DELETE/ACTIVATE/DEACTIVATE
                var obj = JObject.Parse(_log.ContentJson);

                if (_log.Action == "UPDATE")
                {
                    var before = obj["Before"] as JObject;
                    var after = obj["After"] as JObject;
                    ShowBeforeAfterComparison(before, after);
                }
                else
                {
                    // INSERT: tampilkan "After", DELETE: tampilkan "Before"
                    var data = obj["After"] ?? obj["Before"] ?? obj;
                    ShowSingleSnapshot(data as JObject, _log.Action);
                }
            }
            catch
            {
                // Fallback: tampilkan raw JSON
                ShowRawContent(_log.ContentJson);
            }
        }

        private void ShowBeforeAfterComparison(JObject before, JObject after)
        {
            panelBefore.Visible = true;
            panelAfter.Visible = true;
            panelSingle.Visible = false;

            var beforeData = FlattenJson(before);
            var afterData = FlattenJson(after);
            var allKeys = beforeData.Keys.Union(afterData.Keys).Distinct().OrderBy(k => k).ToList();

            // Tabel Before
            gridBefore.Rows.Clear();
            gridBefore.Columns.Clear();
            gridBefore.Columns.Add("Key", "Field");
            gridBefore.Columns.Add("Value", "Nilai Lama");
            gridBefore.Columns["Key"].Width = 160;
            gridBefore.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridBefore.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 100, 80);
            gridBefore.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridBefore.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            gridBefore.EnableHeadersVisualStyles = false;

            // Tabel After
            gridAfter.Rows.Clear();
            gridAfter.Columns.Clear();
            gridAfter.Columns.Add("Key", "Field");
            gridAfter.Columns.Add("Value", "Nilai Baru");
            gridAfter.Columns["Key"].Width = 160;
            gridAfter.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridAfter.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(39, 174, 96);
            gridAfter.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridAfter.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            gridAfter.EnableHeadersVisualStyles = false;

            foreach (var key in allKeys)
            {
                string beforeVal = beforeData.ContainsKey(key) ? beforeData[key] : "(tidak ada)";
                string afterVal = afterData.ContainsKey(key) ? afterData[key] : "(tidak ada)";
                bool changed = beforeVal != afterVal;

                int rowBefore = gridBefore.Rows.Add(key, beforeVal);
                int rowAfter = gridAfter.Rows.Add(key, afterVal);

                if (changed)
                {
                    // Highlight baris yang berubah
                    gridBefore.Rows[rowBefore].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 215);
                    gridBefore.Rows[rowBefore].DefaultCellStyle.ForeColor = Color.FromArgb(180, 0, 0);
                    gridAfter.Rows[rowAfter].DefaultCellStyle.BackColor = Color.FromArgb(210, 255, 210);
                    gridAfter.Rows[rowAfter].DefaultCellStyle.ForeColor = Color.FromArgb(0, 130, 0);

                    // Tandai field yang berubah dengan ★
                    gridBefore.Rows[rowBefore].Cells["Key"].Value = "★ " + key;
                    gridAfter.Rows[rowAfter].Cells["Key"].Value = "★ " + key;
                }
            }
        }

        private void ShowSingleSnapshot(JObject data, string action)
        {
            panelBefore.Visible = false;
            panelAfter.Visible = false;
            panelSingle.Visible = true;

            gridSingle.Rows.Clear();
            gridSingle.Columns.Clear();
            gridSingle.Columns.Add("Key", "Field");
            gridSingle.Columns.Add("Value", "Nilai");
            gridSingle.Columns["Key"].Width = 160;
            gridSingle.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Color headerColor = action == "INSERT" ? Color.FromArgb(39, 174, 96) :
                                action == "DELETE" ? Color.FromArgb(192, 57, 43) :
                                Color.FromArgb(41, 128, 185);
            gridSingle.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            gridSingle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridSingle.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            gridSingle.EnableHeadersVisualStyles = false;

            var flatData = FlattenJson(data);
            foreach (var kv in flatData)
                gridSingle.Rows.Add(kv.Key, kv.Value);
        }

        private void ShowRawContent(string raw)
        {
            panelBefore.Visible = false;
            panelAfter.Visible = false;
            panelSingle.Visible = true;

            gridSingle.Rows.Clear();
            gridSingle.Columns.Clear();
            gridSingle.Columns.Add("Raw", "Raw JSON");
            gridSingle.Columns["Raw"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridSingle.Rows.Add(raw);
        }

        private Dictionary<string, string> FlattenJson(JObject obj)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (obj == null) return result;

            foreach (var prop in obj.Properties())
                result[prop.Name] = prop.Value?.ToString() ?? "(null)";

            return result;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (new MesQuestionYN("Apakah Anda yakin ingin memulihkan (restore) data ini ke tabel aslinya?").ShowDialog(this) == DialogResult.Yes)
            {
                try
                {
                    DataLogDal dal = new DataLogDal();
                    // Untuk mendapatkan nama logTable, kita butuh "Log_" + _log.ReferenceTable
                    string logTable = "Log_" + _log.ReferenceTable;
                    
                    dal.RestoreDeletedData(logTable, _log.LogId);
                    
                    new MesWarningOK("Data berhasil dipulihkan!").ShowDialog(this);
                    
                    // Sembunyikan tombol agar tidak di-restore dua kali
                    btnRestore.Visible = false; 
                }
                catch (Exception ex)
                {
                    new MesError("Gagal me-restore data:\n" + ex.Message).ShowDialog(this);
                }
            }
        }
    }
}
