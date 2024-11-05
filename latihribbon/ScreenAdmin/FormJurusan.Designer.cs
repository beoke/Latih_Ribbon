namespace latihribbon.ScreenAdmin
{
    partial class FormJurusan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel3 = new System.Windows.Forms.Panel();
            this.GridListJurusan = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelJurusan = new System.Windows.Forms.Label();
            this.btnSaveJurusan = new System.Windows.Forms.Button();
            this.txtNamaJurusan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridListJurusan)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.GridListJurusan);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(518, 462);
            this.panel3.TabIndex = 0;
            // 
            // GridListJurusan
            // 
            this.GridListJurusan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridListJurusan.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.GridListJurusan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridListJurusan.Location = new System.Drawing.Point(5, 5);
            this.GridListJurusan.Name = "GridListJurusan";
            this.GridListJurusan.RowHeadersWidth = 51;
            this.GridListJurusan.Size = new System.Drawing.Size(508, 452);
            this.GridListJurusan.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(538, 12);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(341, 462);
            this.panel4.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LabelJurusan);
            this.panel2.Controls.Add(this.btnSaveJurusan);
            this.panel2.Controls.Add(this.txtNamaJurusan);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 452);
            this.panel2.TabIndex = 4;
            // 
            // LabelJurusan
            // 
            this.LabelJurusan.AutoSize = true;
            this.LabelJurusan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelJurusan.Location = new System.Drawing.Point(127, 20);
            this.LabelJurusan.Name = "LabelJurusan";
            this.LabelJurusan.Size = new System.Drawing.Size(74, 20);
            this.LabelJurusan.TabIndex = 62;
            this.LabelJurusan.Text = "INSERT";
            // 
            // btnSaveJurusan
            // 
            this.btnSaveJurusan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveJurusan.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSaveJurusan.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveJurusan.ForeColor = System.Drawing.Color.White;
            this.btnSaveJurusan.Location = new System.Drawing.Point(218, 396);
            this.btnSaveJurusan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveJurusan.Name = "btnSaveJurusan";
            this.btnSaveJurusan.Size = new System.Drawing.Size(90, 32);
            this.btnSaveJurusan.TabIndex = 2;
            this.btnSaveJurusan.Text = "Save";
            this.btnSaveJurusan.UseVisualStyleBackColor = false;
            // 
            // txtNamaJurusan
            // 
            this.txtNamaJurusan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaJurusan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaJurusan.Location = new System.Drawing.Point(20, 80);
            this.txtNamaJurusan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNamaJurusan.MaxLength = 10;
            this.txtNamaJurusan.Name = "txtNamaJurusan";
            this.txtNamaJurusan.Size = new System.Drawing.Size(290, 23);
            this.txtNamaJurusan.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 18);
            this.label7.TabIndex = 59;
            this.label7.Text = "Nama Jurusan";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditMenuStrip,
            this.DeleteMenuStrip});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 56);
            // 
            // EditMenuStrip
            // 
            this.EditMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditMenuStrip.Image = global::latihribbon.Properties.Resources.pencil;
            this.EditMenuStrip.Name = "EditMenuStrip";
            this.EditMenuStrip.Size = new System.Drawing.Size(126, 26);
            this.EditMenuStrip.Text = "Edit";
            // 
            // DeleteMenuStrip
            // 
            this.DeleteMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteMenuStrip.Image = global::latihribbon.Properties.Resources.bin;
            this.DeleteMenuStrip.Name = "DeleteMenuStrip";
            this.DeleteMenuStrip.Size = new System.Drawing.Size(126, 26);
            this.DeleteMenuStrip.Text = "Delete";
            // 
            // FormJurusan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 484);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormJurusan";
            this.Text = "FormJurusan";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridListJurusan)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView GridListJurusan;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EditMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuStrip;
        private System.Windows.Forms.Label LabelJurusan;
        private System.Windows.Forms.Button btnSaveJurusan;
        private System.Windows.Forms.TextBox txtNamaJurusan;
        private System.Windows.Forms.Label label7;
    }
}