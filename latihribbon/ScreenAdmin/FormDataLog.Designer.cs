namespace latihribbon.ScreenAdmin
{
    partial class FormDataLog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblTable = new System.Windows.Forms.Label();
            this.comboTable = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUserFilter = new System.Windows.Forms.TextBox();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblPerPage = new System.Windows.Forms.Label();
            this.comboPerPage = new System.Windows.Forms.ComboBox();
            this.GridLog = new System.Windows.Forms.DataGridView();
            this.panelPagination = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblHalaman = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.contextMenuLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lihatDetailMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridLog)).BeginInit();
            this.panelPagination.SuspendLayout();
            this.contextMenuLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 50;
            this.panelTop.Name = "panelTop";
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "📋  Data Log Perubahan";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblTitle.TabIndex = 0;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(240, 243, 247);
            this.panelFilter.Controls.Add(this.lblTable);
            this.panelFilter.Controls.Add(this.comboTable);
            this.panelFilter.Controls.Add(this.lblUser);
            this.panelFilter.Controls.Add(this.txtUserFilter);
            this.panelFilter.Controls.Add(this.btnResetFilter);
            this.panelFilter.Controls.Add(this.btnRefresh);
            this.panelFilter.Controls.Add(this.lblPerPage);
            this.panelFilter.Controls.Add(this.comboPerPage);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Height = 60;
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 8);
            this.panelFilter.TabIndex = 1;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTable.Location = new System.Drawing.Point(12, 18);
            this.lblTable.Name = "lblTable";
            this.lblTable.Text = "Tabel:";
            this.lblTable.TabIndex = 0;
            // 
            // comboTable
            // 
            this.comboTable.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.comboTable.Location = new System.Drawing.Point(55, 14);
            this.comboTable.Name = "comboTable";
            this.comboTable.Size = new System.Drawing.Size(160, 25);
            this.comboTable.TabIndex = 1;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(228, 18);
            this.lblUser.Name = "lblUser";
            this.lblUser.Text = "Filter User:";
            this.lblUser.TabIndex = 2;
            // 
            // txtUserFilter
            // 
            this.txtUserFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtUserFilter.Location = new System.Drawing.Point(310, 14);
            this.txtUserFilter.Name = "txtUserFilter";
            this.txtUserFilter.Size = new System.Drawing.Size(160, 25);
            this.txtUserFilter.TabIndex = 3;
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnResetFilter.Location = new System.Drawing.Point(478, 14);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(80, 25);
            this.btnResetFilter.TabIndex = 4;
            this.btnResetFilter.Text = "Reset";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(564, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 25);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblPerPage
            // 
            this.lblPerPage.AutoSize = true;
            this.lblPerPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPerPage.Location = new System.Drawing.Point(655, 18);
            this.lblPerPage.Name = "lblPerPage";
            this.lblPerPage.Text = "Per Halaman:";
            this.lblPerPage.TabIndex = 6;
            // 
            // comboPerPage
            // 
            this.comboPerPage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.comboPerPage.Location = new System.Drawing.Point(748, 14);
            this.comboPerPage.Name = "comboPerPage";
            this.comboPerPage.Size = new System.Drawing.Size(80, 25);
            this.comboPerPage.TabIndex = 7;
            // 
            // GridLog
            // 
            this.GridLog.AllowUserToAddRows = false;
            this.GridLog.AllowUserToDeleteRows = false;
            this.GridLog.AllowUserToResizeRows = false;
            this.GridLog.BackgroundColor = System.Drawing.Color.White;
            this.GridLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridLog.Name = "GridLog";
            this.GridLog.ReadOnly = true;
            this.GridLog.RowHeadersVisible = false;
            this.GridLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLog.TabIndex = 2;
            this.GridLog.MultiSelect = false;
            // 
            // panelPagination
            // 
            this.panelPagination.Controls.Add(this.btnPrevious);
            this.panelPagination.Controls.Add(this.lblHalaman);
            this.panelPagination.Controls.Add(this.btnNext);
            this.panelPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPagination.Height = 40;
            this.panelPagination.Name = "panelPagination";
            this.panelPagination.TabIndex = 3;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrevious.Location = new System.Drawing.Point(10, 8);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(80, 26);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Text = "◀ Prev";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // lblHalaman
            // 
            this.lblHalaman.AutoSize = true;
            this.lblHalaman.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHalaman.Location = new System.Drawing.Point(100, 13);
            this.lblHalaman.Name = "lblHalaman";
            this.lblHalaman.Text = "Halaman 1/1";
            this.lblHalaman.TabIndex = 1;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.Location = new System.Drawing.Point(200, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 26);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next ▶";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // contextMenuLog
            // 
            this.contextMenuLog.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.contextMenuLog.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lihatDetailMenuStrip});
            this.contextMenuLog.Name = "contextMenuLog";
            this.contextMenuLog.Size = new System.Drawing.Size(160, 30);
            // 
            // lihatDetailMenuStrip
            // 
            this.lihatDetailMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lihatDetailMenuStrip.Name = "lihatDetailMenuStrip";
            this.lihatDetailMenuStrip.Size = new System.Drawing.Size(159, 26);
            this.lihatDetailMenuStrip.Text = "Lihat Detail";
            // 
            // FormDataLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.GridLog);
            this.Controls.Add(this.panelPagination);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Name = "FormDataLog";
            this.Text = "Data Log";
            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridLog)).EndInit();
            this.panelPagination.ResumeLayout(false);
            this.panelPagination.PerformLayout();
            this.contextMenuLog.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox comboTable;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUserFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblPerPage;
        private System.Windows.Forms.ComboBox comboPerPage;
        private System.Windows.Forms.DataGridView GridLog;
        private System.Windows.Forms.Panel panelPagination;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblHalaman;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ContextMenuStrip contextMenuLog;
        private System.Windows.Forms.ToolStripMenuItem lihatDetailMenuStrip;
    }
}
