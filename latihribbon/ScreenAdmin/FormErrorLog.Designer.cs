namespace latihribbon.ScreenAdmin
{
    partial class FormErrorLog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView GridLogFiles;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RichTextBox txtLogContent;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnClearFilter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GridLogFiles = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtLogContent = new System.Windows.Forms.RichTextBox();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridLogFiles)).BeginInit();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 50);
            this.panelHeader.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(16, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(320, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SYSTEM APPLICATION ERROR LOG";

            // splitContainer1
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            
            // Panel1
            this.splitContainer1.Panel1.Controls.Add(this.GridLogFiles);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            
            // Panel2
            this.splitContainer1.Panel2.Controls.Add(this.txtLogContent);
            this.splitContainer1.Panel2.Controls.Add(this.panelSearch);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 550);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 1;

            // btnRefresh
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(320, 32);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Refresh Daftar Log";
            this.btnRefresh.UseVisualStyleBackColor = true;

            // GridLogFiles
            this.GridLogFiles.AllowUserToAddRows = false;
            this.GridLogFiles.AllowUserToDeleteRows = false;
            this.GridLogFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridLogFiles.BackgroundColor = System.Drawing.Color.White;
            this.GridLogFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridLogFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridLogFiles.Location = new System.Drawing.Point(0, 32);
            this.GridLogFiles.MultiSelect = false;
            this.GridLogFiles.Name = "GridLogFiles";
            this.GridLogFiles.ReadOnly = true;
            this.GridLogFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLogFiles.Size = new System.Drawing.Size(320, 518);
            this.GridLogFiles.TabIndex = 1;

            // panelSearch
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.btnClearFilter);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(676, 36);
            this.panelSearch.TabIndex = 0;

            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(10, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(74, 16);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Cari Text:";

            // txtSearch
            this.txtSearch.Font = new System.Drawing.Font("Sans Serif", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(90, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 22);
            this.txtSearch.TabIndex = 1;

            // btnClearFilter
            this.btnClearFilter.Font = new System.Drawing.Font("Sans Serif", 9F);
            this.btnClearFilter.Location = new System.Drawing.Point(398, 6);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(75, 25);
            this.btnClearFilter.TabIndex = 2;
            this.btnClearFilter.Text = "Clear";
            this.btnClearFilter.UseVisualStyleBackColor = true;

            // txtLogContent
            this.txtLogContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogContent.Location = new System.Drawing.Point(0, 36);
            this.txtLogContent.Name = "txtLogContent";
            this.txtLogContent.Size = new System.Drawing.Size(676, 514);
            this.txtLogContent.TabIndex = 1;
            this.txtLogContent.Text = "";

            // FormErrorLog
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelHeader);
            this.Name = "FormErrorLog";
            this.Text = "System Error Log Viewer";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridLogFiles)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
