﻿namespace latihribbon.ScreenAdmin
{
    partial class FormRekapPersensi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRekapPersensi));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.KeteranganCombo = new System.Windows.Forms.ComboBox();
            this.tgldua = new System.Windows.Forms.DateTimePicker();
            this.tglsatu = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.comboPerPage = new System.Windows.Forms.ComboBox();
            this.lblHalaman = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnPrintRekap = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnKelas = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 9);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1349, 311);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(1499, 23);
            this.btnResetFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(100, 28);
            this.btnResetFilter.TabIndex = 39;
            this.btnResetFilter.TabStop = false;
            this.btnResetFilter.Text = "Reset";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(1253, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 23);
            this.label2.TabIndex = 38;
            this.label2.Text = "Keterangan :";
            // 
            // KeteranganCombo
            // 
            this.KeteranganCombo.FormattingEnabled = true;
            this.KeteranganCombo.Location = new System.Drawing.Point(1376, 26);
            this.KeteranganCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.KeteranganCombo.Name = "KeteranganCombo";
            this.KeteranganCombo.Size = new System.Drawing.Size(105, 24);
            this.KeteranganCombo.TabIndex = 37;
            this.KeteranganCombo.TabStop = false;
            // 
            // tgldua
            // 
            this.tgldua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgldua.Location = new System.Drawing.Point(1063, 26);
            this.tgldua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tgldua.Name = "tgldua";
            this.tgldua.Size = new System.Drawing.Size(175, 24);
            this.tgldua.TabIndex = 36;
            this.tgldua.TabStop = false;
            // 
            // tglsatu
            // 
            this.tglsatu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tglsatu.Location = new System.Drawing.Point(859, 27);
            this.tglsatu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tglsatu.Name = "tglsatu";
            this.tglsatu.Size = new System.Drawing.Size(175, 24);
            this.tglsatu.TabIndex = 35;
            this.tglsatu.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(767, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 23);
            this.label4.TabIndex = 34;
            this.label4.Text = "Tanggal :";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(104, 27);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(409, 24);
            this.txtSearch.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(21, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 23);
            this.label5.TabIndex = 32;
            this.label5.Text = "Search :";
            // 
            // txtKelas
            // 
            this.txtKelas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(601, 27);
            this.txtKelas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.ReadOnly = true;
            this.txtKelas.Size = new System.Drawing.Size(88, 24);
            this.txtKelas.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(529, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 23);
            this.label3.TabIndex = 30;
            this.label3.Text = "Kelas :";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.comboPerPage);
            this.panel1.Controls.Add(this.lblHalaman);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.btnPrintRekap);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(1, 76);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1367, 393);
            this.panel1.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(115, 343);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 23);
            this.label14.TabIndex = 58;
            this.label14.Text = "/Page";
            // 
            // comboPerPage
            // 
            this.comboPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboPerPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboPerPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPerPage.FormattingEnabled = true;
            this.comboPerPage.ItemHeight = 17;
            this.comboPerPage.Location = new System.Drawing.Point(29, 343);
            this.comboPerPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboPerPage.Name = "comboPerPage";
            this.comboPerPage.Size = new System.Drawing.Size(80, 25);
            this.comboPerPage.TabIndex = 57;
            this.comboPerPage.TabStop = false;
            // 
            // lblHalaman
            // 
            this.lblHalaman.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblHalaman.AutoSize = true;
            this.lblHalaman.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHalaman.Location = new System.Drawing.Point(588, 346);
            this.lblHalaman.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHalaman.Name = "lblHalaman";
            this.lblHalaman.Size = new System.Drawing.Size(113, 20);
            this.lblHalaman.TabIndex = 22;
            this.lblHalaman.Text = "Halaman 1/10";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(727, 342);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 28);
            this.btnNext.TabIndex = 21;
            this.btnNext.TabStop = false;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrevious.Location = new System.Drawing.Point(462, 342);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(100, 28);
            this.btnPrevious.TabIndex = 20;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnPrintRekap
            // 
            this.btnPrintRekap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintRekap.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintRekap.BackgroundImage")));
            this.btnPrintRekap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintRekap.FlatAppearance.BorderSize = 2;
            this.btnPrintRekap.Location = new System.Drawing.Point(1269, 334);
            this.btnPrintRekap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintRekap.Name = "btnPrintRekap";
            this.btnPrintRekap.Size = new System.Drawing.Size(61, 46);
            this.btnPrintRekap.TabIndex = 2;
            this.btnPrintRekap.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(1039, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 23);
            this.label6.TabIndex = 41;
            this.label6.Text = "_";
            // 
            // btnKelas
            // 
            this.btnKelas.Location = new System.Drawing.Point(697, 26);
            this.btnKelas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKelas.Name = "btnKelas";
            this.btnKelas.Size = new System.Drawing.Size(53, 28);
            this.btnKelas.TabIndex = 2;
            this.btnKelas.Text = "...";
            this.btnKelas.UseVisualStyleBackColor = true;
            this.btnKelas.Click += new System.EventHandler(this.btnKelas_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.BackColor = System.Drawing.Color.White;
            this.lblFilter.ForeColor = System.Drawing.Color.Gray;
            this.lblFilter.Location = new System.Drawing.Point(109, 32);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(211, 16);
            this.lblFilter.TabIndex = 49;
            this.lblFilter.Text = "Masukkan Kata Kunci Pencarian ⌕ ";
            // 
            // FormRekapPersensi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1370, 469);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.btnKelas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnResetFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KeteranganCombo);
            this.Controls.Add(this.tgldua);
            this.Controls.Add(this.tglsatu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKelas);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormRekapPersensi";
            this.Text = "FormRekapPersensi";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox KeteranganCombo;
        private System.Windows.Forms.DateTimePicker tgldua;
        private System.Windows.Forms.DateTimePicker tglsatu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKelas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrintRekap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblHalaman;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnKelas;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboPerPage;
    }
}