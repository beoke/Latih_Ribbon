using latihribbon.Conn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    public partial class FormErrorLog : Form
    {
        public FormErrorLog()
        {
            InitializeComponent();
            InitStyle();
            RegisterEvents();
            this.Load += FormErrorLog_Load;
        }

        private void FormErrorLog_Load(object sender, EventArgs e)
        {
            LoadLogFiles();
        }

        private void InitStyle()
        {
            GridLogFiles.EnableHeadersVisualStyles = false;
            GridLogFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            GridLogFiles.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            GridLogFiles.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            GridLogFiles.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            GridLogFiles.RowTemplate.Height = 28;

            txtLogContent.Font = new Font("Consolas", 9.5f);
            txtLogContent.ReadOnly = true;
            txtLogContent.BackColor = Color.FromArgb(245, 247, 250);
        }

        private void RegisterEvents()
        {
            GridLogFiles.SelectionChanged += GridLogFiles_SelectionChanged;
            btnRefresh.Click += (s, e) => LoadLogFiles();
            btnClearFilter.Click += (s, e) => txtSearch.Clear();
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void LoadLogFiles()
        {
            try
            {
                string logDir = AppLogger.GetLogsDirectory();
                if (!Directory.Exists(logDir))
                {
                    txtLogContent.Text = "Belum ada file log error.";
                    return;
                }

                var files = new DirectoryInfo(logDir).GetFiles("*.log")
                    .OrderByDescending(f => f.LastWriteTime)
                    .Select((f, index) => new LogFileItem
                    {
                        No = index + 1,
                        Tanggal = f.Name.Replace(".log", ""),
                        NamaFile = f.Name,
                        Ukuran = $"{f.Length / 1024.0:F1} KB",
                        FilePath = f.FullName
                    }).ToList();

                GridLogFiles.DataSource = files;
                if (GridLogFiles.Columns["FilePath"] != null)
                    GridLogFiles.Columns["FilePath"].Visible = false;

                if (files.Count == 0)
                {
                    txtLogContent.Text = "Tidak ditemukan file log error.";
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "FormErrorLog.LoadLogFiles");
            }
        }

        private void GridLogFiles_SelectionChanged(object sender, EventArgs e)
        {
            if (GridLogFiles.CurrentRow == null) return;
            var item = GridLogFiles.CurrentRow.DataBoundItem as LogFileItem;
            if (item != null && File.Exists(item.FilePath))
            {
                try
                {
                    string content = File.ReadAllText(item.FilePath);
                    txtLogContent.Text = string.IsNullOrWhiteSpace(content) ? "(File log kosong)" : content;
                }
                catch (Exception ex)
                {
                    txtLogContent.Text = $"Gagal membaca file log: {ex.Message}";
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(query)) return;

            int index = txtLogContent.Find(query, RichTextBoxFinds.None);
            if (index != -1)
            {
                txtLogContent.SelectionStart = index;
                txtLogContent.SelectionLength = query.Length;
                txtLogContent.SelectionColor = Color.Red;
                txtLogContent.SelectionBackColor = Color.Yellow;
            }
        }

        private class LogFileItem
        {
            public int No { get; set; }
            public string Tanggal { get; set; }
            public string NamaFile { get; set; }
            public string Ukuran { get; set; }
            public string FilePath { get; set; }
        }
    }
}
