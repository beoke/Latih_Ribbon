using latihribbon.Dal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    /// <summary>
    /// Form untuk melihat log perubahan data.
    /// Hanya dapat diakses oleh Super Admin.
    /// </summary>
    public partial class FormDataLog : Form
    {
        private readonly DataLogDal _dataLogDal;
        private System.Threading.Timer _debounceTimer;
        private int _page = 1;
        private int _totalPage = 1;

        public FormDataLog()
        {
            InitializeComponent();
            buf();

            // Guard: hanya Super Admin yang dapat mengakses
            if (UserSession.CurrentRole != "Super Admin")
            {
                this.Load += (s, e) =>
                {
                    new MesError("Akses ditolak! Halaman ini hanya untuk Super Admin.").ShowDialog(this);
                    this.Close();
                };
                return;
            }

            _dataLogDal = new DataLogDal();

            this.Load += FormDataLog_Load;
        }

        private void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, GridLog, new object[] { true });
        }

        private void FormDataLog_Load(object sender, EventArgs e)
        {
            InitStyle();
            InitPerPageCombo();
            LoadLogTables();
            RegisterEvents();
            txtUserFilter.Focus();
        }

        private void InitStyle()
        {
            StyleComponent.StyleGrid(GridLog);
            GridLog.Columns.Clear();
        }

        private void InitPerPageCombo()
        {
            comboPerPage.DataSource = new List<int> { 20, 50, 100, 200 };
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadLogTables()
        {
            var tables = _dataLogDal.GetLogTableNames();
            comboTable.DataSource = tables;
            comboTable.DropDownStyle = ComboBoxStyle.DropDownList;

            if (tables.Count > 0)
                LoadData();
        }

        private void RegisterEvents()
        {
            comboTable.SelectedIndexChanged += (s, e) => { _page = 1; LoadData(); };
            comboPerPage.SelectedIndexChanged += (s, e) => { _page = 1; LoadData(); };
            txtUserFilter.TextChanged += TxtUserFilter_TextChanged;
            btnResetFilter.Click += (s, e) => { txtUserFilter.Clear(); };
            btnRefresh.Click += (s, e) => LoadData();
            btnNext.Click += BtnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;
            GridLog.CellDoubleClick += GridLog_CellDoubleClick;
            GridLog.CellMouseClick += GridLog_CellMouseClick;
            lihatDetailMenuStrip.Click += (s, e) => OpenDetail();
        }

        private void TxtUserFilter_TextChanged(object sender, EventArgs e)
        {
            _page = 1;
            _debounceTimer?.Dispose();
            _debounceTimer = new System.Threading.Timer(x =>
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                try { this.Invoke(new Action(LoadData)); }
                catch { }
            }, null, 400, Timeout.Infinite);
        }

        private void LoadData()
        {
            if (comboTable.SelectedItem == null) return;

            string tableName = comboTable.SelectedItem.ToString();
            string userFilter = txtUserFilter.Text.Trim();
            int rowPerPage = (int)comboPerPage.SelectedItem;
            int offset = (_page - 1) * rowPerPage;

            int total = _dataLogDal.CountLog(tableName, userFilter);
            _totalPage = (int)Math.Ceiling((double)total / rowPerPage);
            if (_totalPage < 1) _totalPage = 1;
            lblHalaman.Text = $"Halaman {_page}/{_totalPage}";

            var logs = _dataLogDal.GetLogs(tableName, userFilter, offset, rowPerPage);

            GridLog.DataSource = logs.Select((x, i) => new
            {
                No = offset + i + 1,
                x.LogId,
                Tabel = x.ReferenceTable,
                x.Action,
                x.PkId,
                x.User,
                Waktu = x.Timestamp
            }).ToList();

            ApplyGridStyle();
        }

        private void ApplyGridStyle()
        {
            if (GridLog.Columns.Count == 0) return;

            if (GridLog.Columns["LogId"] != null) GridLog.Columns["LogId"].Visible = false;
            if (GridLog.Columns["No"] != null) GridLog.Columns["No"].Width = 50;
            if (GridLog.Columns["Tabel"] != null) GridLog.Columns["Tabel"].Width = 120;
            if (GridLog.Columns["Action"] != null) GridLog.Columns["Action"].Width = 100;
            if (GridLog.Columns["PkId"] != null) GridLog.Columns["PkId"].Width = 80;
            if (GridLog.Columns["User"] != null) GridLog.Columns["User"].Width = 150;
            if (GridLog.Columns["Waktu"] != null)
            {
                GridLog.Columns["Waktu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                GridLog.Columns["Waktu"].MinimumWidth = 150;
            }

            // Warnai baris berdasarkan Action
            foreach (DataGridViewRow row in GridLog.Rows)
            {
                string action = row.Cells["Action"]?.Value?.ToString() ?? "";
                switch (action)
                {
                    case "INSERT":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                        break;
                    case "UPDATE":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 220);
                        break;
                    case "DELETE":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                        break;
                    case "ACTIVATE":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(220, 240, 255);
                        break;
                    case "DEACTIVATE":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(245, 225, 255);
                        break;
                }
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (_page < _totalPage) { _page++; LoadData(); }
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (_page > 1) { _page--; LoadData(); }
        }

        private void GridLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) OpenDetail();
        }

        private void GridLog_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                GridLog.ClearSelection();
                GridLog.CurrentCell = GridLog[e.ColumnIndex, e.RowIndex];
                contextMenuLog.Show(Cursor.Position);
            }
        }

        private void OpenDetail()
        {
            if (GridLog.CurrentRow == null) return;
            int logId = Convert.ToInt32(GridLog.CurrentRow.Cells["LogId"]?.Value ?? 0);
            if (logId == 0) return;

            string tableName = comboTable.SelectedItem?.ToString() ?? string.Empty;
            var logRecord = _dataLogDal.GetLogById(tableName, logId);
            if (logRecord == null) return;

            new FormDataLogDetail(logRecord).ShowDialog(this);
        }
    }
}
