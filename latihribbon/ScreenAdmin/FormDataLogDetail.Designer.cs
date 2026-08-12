namespace latihribbon.ScreenAdmin
{
    partial class FormDataLogDetail
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblLogId = new System.Windows.Forms.Label();
            this.lblTabel = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblPkId = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblWaktu = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelBefore = new System.Windows.Forms.Panel();
            this.lblBeforeTitle = new System.Windows.Forms.Label();
            this.gridBefore = new System.Windows.Forms.DataGridView();
            this.panelAfter = new System.Windows.Forms.Panel();
            this.lblAfterTitle = new System.Windows.Forms.Label();
            this.gridAfter = new System.Windows.Forms.DataGridView();
            this.panelSingle = new System.Windows.Forms.Panel();
            this.lblSingleTitle = new System.Windows.Forms.Label();
            this.gridSingle = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelBefore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBefore)).BeginInit();
            this.panelAfter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAfter)).BeginInit();
            this.panelSingle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSingle)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelHeader.Controls.Add(this.lblLogId);
            this.panelHeader.Controls.Add(this.lblTabel);
            this.panelHeader.Controls.Add(this.lblAction);
            this.panelHeader.Controls.Add(this.lblPkId);
            this.panelHeader.Controls.Add(this.lblUser);
            this.panelHeader.Controls.Add(this.lblWaktu);
            this.panelHeader.Controls.Add(this.btnRestore);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 105;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelHeader.TabIndex = 0;
            // 
            // lblLogId
            // 
            this.lblLogId.AutoSize = true;
            this.lblLogId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLogId.ForeColor = System.Drawing.Color.White;
            this.lblLogId.Location = new System.Drawing.Point(15, 12);
            this.lblLogId.Name = "lblLogId";
            this.lblLogId.Text = "Log ID: -";
            this.lblLogId.TabIndex = 0;
            // 
            // lblTabel
            // 
            this.lblTabel.AutoSize = true;
            this.lblTabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTabel.ForeColor = System.Drawing.Color.LightGray;
            this.lblTabel.Location = new System.Drawing.Point(15, 36);
            this.lblTabel.Name = "lblTabel";
            this.lblTabel.Text = "Tabel: -";
            this.lblTabel.TabIndex = 1;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAction.ForeColor = System.Drawing.Color.Orange;
            this.lblAction.Location = new System.Drawing.Point(200, 36);
            this.lblAction.Name = "lblAction";
            this.lblAction.Text = "Aksi: -";
            this.lblAction.TabIndex = 2;
            // 
            // lblPkId
            // 
            this.lblPkId.AutoSize = true;
            this.lblPkId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPkId.ForeColor = System.Drawing.Color.LightGray;
            this.lblPkId.Location = new System.Drawing.Point(380, 36);
            this.lblPkId.Name = "lblPkId";
            this.lblPkId.Text = "ID Data: -";
            this.lblPkId.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUser.ForeColor = System.Drawing.Color.LightGray;
            this.lblUser.Location = new System.Drawing.Point(15, 60);
            this.lblUser.Name = "lblUser";
            this.lblUser.Text = "User: -";
            this.lblUser.TabIndex = 4;
            // 
            // lblWaktu
            // 
            this.lblWaktu.AutoSize = true;
            this.lblWaktu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblWaktu.ForeColor = System.Drawing.Color.LightGray;
            this.lblWaktu.Location = new System.Drawing.Point(15, 80);
            this.lblWaktu.Name = "lblWaktu";
            this.lblWaktu.Text = "Waktu: -";
            this.lblWaktu.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.Location = new System.Drawing.Point(680, 38);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Tutup";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(39, 174, 96); // Hijau Emerald
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnRestore.Location = new System.Drawing.Point(540, 38); // Di sebelah kiri tombol tutup
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(130, 30);
            this.btnRestore.TabIndex = 7;
            this.btnRestore.Text = "Restore Data";
            this.btnRestore.Visible = false; // Hanya terlihat jika Action = DELETE
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelBefore);
            this.panelContent.Controls.Add(this.panelAfter);
            this.panelContent.Controls.Add(this.panelSingle);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(10);
            this.panelContent.TabIndex = 1;
            // 
            // panelBefore
            // 
            this.panelBefore.Controls.Add(this.lblBeforeTitle);
            this.panelBefore.Controls.Add(this.gridBefore);
            this.panelBefore.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBefore.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.panelBefore.Name = "panelBefore";
            this.panelBefore.Width = 400;
            this.panelBefore.TabIndex = 0;
            // 
            // lblBeforeTitle
            // 
            this.lblBeforeTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBeforeTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblBeforeTitle.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblBeforeTitle.Height = 30;
            this.lblBeforeTitle.Name = "lblBeforeTitle";
            this.lblBeforeTitle.Text = "  ◀ Sebelum Perubahan";
            this.lblBeforeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBeforeTitle.TabIndex = 0;
            // 
            // gridBefore
            // 
            this.gridBefore.AllowUserToAddRows = false;
            this.gridBefore.AllowUserToDeleteRows = false;
            this.gridBefore.AllowUserToResizeRows = false;
            this.gridBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBefore.Name = "gridBefore";
            this.gridBefore.ReadOnly = true;
            this.gridBefore.RowHeadersVisible = false;
            this.gridBefore.TabIndex = 1;
            this.gridBefore.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.gridBefore.RowTemplate.Height = 26;
            // 
            // panelAfter
            // 
            this.panelAfter.Controls.Add(this.lblAfterTitle);
            this.panelAfter.Controls.Add(this.gridAfter);
            this.panelAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAfter.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.panelAfter.Name = "panelAfter";
            this.panelAfter.TabIndex = 1;
            // 
            // lblAfterTitle
            // 
            this.lblAfterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAfterTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblAfterTitle.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblAfterTitle.Height = 30;
            this.lblAfterTitle.Name = "lblAfterTitle";
            this.lblAfterTitle.Text = "  ▶ Sesudah Perubahan";
            this.lblAfterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAfterTitle.TabIndex = 0;
            // 
            // gridAfter
            // 
            this.gridAfter.AllowUserToAddRows = false;
            this.gridAfter.AllowUserToDeleteRows = false;
            this.gridAfter.AllowUserToResizeRows = false;
            this.gridAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAfter.Name = "gridAfter";
            this.gridAfter.ReadOnly = true;
            this.gridAfter.RowHeadersVisible = false;
            this.gridAfter.TabIndex = 1;
            this.gridAfter.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.gridAfter.RowTemplate.Height = 26;
            // 
            // panelSingle
            // 
            this.panelSingle.Controls.Add(this.lblSingleTitle);
            this.panelSingle.Controls.Add(this.gridSingle);
            this.panelSingle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSingle.Name = "panelSingle";
            this.panelSingle.TabIndex = 2;
            this.panelSingle.Visible = false;
            // 
            // lblSingleTitle
            // 
            this.lblSingleTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSingleTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSingleTitle.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblSingleTitle.Height = 30;
            this.lblSingleTitle.Name = "lblSingleTitle";
            this.lblSingleTitle.Text = "  Data Snapshot";
            this.lblSingleTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSingleTitle.TabIndex = 0;
            // 
            // gridSingle
            // 
            this.gridSingle.AllowUserToAddRows = false;
            this.gridSingle.AllowUserToDeleteRows = false;
            this.gridSingle.AllowUserToResizeRows = false;
            this.gridSingle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSingle.Name = "gridSingle";
            this.gridSingle.ReadOnly = true;
            this.gridSingle.RowHeadersVisible = false;
            this.gridSingle.TabIndex = 1;
            this.gridSingle.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.gridSingle.RowTemplate.Height = 26;
            // 
            // FormDataLogDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "FormDataLogDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detail Log Perubahan";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelBefore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBefore)).EndInit();
            this.panelAfter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAfter)).EndInit();
            this.panelSingle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSingle)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblLogId;
        private System.Windows.Forms.Label lblTabel;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblPkId;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblWaktu;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelBefore;
        private System.Windows.Forms.Label lblBeforeTitle;
        private System.Windows.Forms.DataGridView gridBefore;
        private System.Windows.Forms.Panel panelAfter;
        private System.Windows.Forms.Label lblAfterTitle;
        private System.Windows.Forms.DataGridView gridAfter;
        private System.Windows.Forms.Panel panelSingle;
        private System.Windows.Forms.Label lblSingleTitle;
        private System.Windows.Forms.DataGridView gridSingle;
    }
}
