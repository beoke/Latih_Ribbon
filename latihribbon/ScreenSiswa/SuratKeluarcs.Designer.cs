namespace latihribbon
{
    partial class SuratKeluarcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuratKeluarcs));
            this.printPreviewDialogKeluar = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocumentKeluar = new System.Drawing.Printing.PrintDocument();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTanggal = new System.Windows.Forms.TextBox();
            this.jamKembali = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNIS = new System.Windows.Forms.TextBox();
            this.tx_keluar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtAlasan = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_kembali = new System.Windows.Forms.Button();
            this.btn_PrintKeluar = new System.Windows.Forms.Button();
            this.LabelLenghKeperluan = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewDialogKeluar
            // 
            this.printPreviewDialogKeluar.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialogKeluar.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialogKeluar.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialogKeluar.Enabled = true;
            this.printPreviewDialogKeluar.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialogKeluar.Icon")));
            this.printPreviewDialogKeluar.Name = "printPreviewDialogKeluar";
            this.printPreviewDialogKeluar.Visible = false;
            // 
            // printDocumentKeluar
            // 
            this.printDocumentKeluar.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentKeluar_PrintPage);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1540, 846);
            this.panel2.TabIndex = 74;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtTanggal);
            this.panel3.Controls.Add(this.jamKembali);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtNIS);
            this.panel3.Controls.Add(this.tx_keluar);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtKelas);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtNama);
            this.panel3.Location = new System.Drawing.Point(21, 160);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1118, 537);
            this.panel3.TabIndex = 76;
            // 
            // txtTanggal
            // 
            this.txtTanggal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTanggal.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTanggal.Location = new System.Drawing.Point(493, 354);
            this.txtTanggal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTanggal.Name = "txtTanggal";
            this.txtTanggal.ReadOnly = true;
            this.txtTanggal.Size = new System.Drawing.Size(558, 45);
            this.txtTanggal.TabIndex = 74;
            // 
            // jamKembali
            // 
            this.jamKembali.CalendarTitleForeColor = System.Drawing.Color.AliceBlue;
            this.jamKembali.CustomFormat = "HH:mm";
            this.jamKembali.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jamKembali.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.jamKembali.Location = new System.Drawing.Point(493, 560);
            this.jamKembali.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.jamKembali.Name = "jamKembali";
            this.jamKembali.ShowUpDown = true;
            this.jamKembali.Size = new System.Drawing.Size(559, 45);
            this.jamKembali.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 42);
            this.label1.TabIndex = 24;
            this.label1.Text = "NIS                              :";
            // 
            // txtNIS
            // 
            this.txtNIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNIS.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(493, 46);
            this.txtNIS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.ReadOnly = true;
            this.txtNIS.Size = new System.Drawing.Size(558, 45);
            this.txtNIS.TabIndex = 25;
            // 
            // tx_keluar
            // 
            this.tx_keluar.BackColor = System.Drawing.SystemColors.Control;
            this.tx_keluar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tx_keluar.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_keluar.Location = new System.Drawing.Point(493, 458);
            this.tx_keluar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_keluar.Name = "tx_keluar";
            this.tx_keluar.ReadOnly = true;
            this.tx_keluar.Size = new System.Drawing.Size(558, 45);
            this.tx_keluar.TabIndex = 73;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 42);
            this.label2.TabIndex = 26;
            this.label2.Text = "Kelas                            :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(359, 42);
            this.label3.TabIndex = 27;
            this.label3.Text = "Tanggal                        :";
            // 
            // txtKelas
            // 
            this.txtKelas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKelas.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(493, 250);
            this.txtKelas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.ReadOnly = true;
            this.txtKelas.Size = new System.Drawing.Size(558, 45);
            this.txtKelas.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(59, 559);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(360, 42);
            this.label6.TabIndex = 36;
            this.label6.Text = "Kembali Pada Jam ke  :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(56, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(358, 42);
            this.label4.TabIndex = 29;
            this.label4.Text = "Nama                           :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(60, 459);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(355, 42);
            this.label5.TabIndex = 34;
            this.label5.Text = "Keluar Pada Jam         :";
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(493, 146);
            this.txtNama.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNama.Name = "txtNama";
            this.txtNama.ReadOnly = true;
            this.txtNama.Size = new System.Drawing.Size(558, 45);
            this.txtNama.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.BlueViolet;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Location = new System.Drawing.Point(-20, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1591, 128);
            this.panel1.TabIndex = 71;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(656, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(318, 54);
            this.label11.TabIndex = 68;
            this.label11.Text = "Surat Izin Keluar";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.LabelLenghKeperluan);
            this.panel4.Controls.Add(this.txtAlasan);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(1169, 160);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(345, 537);
            this.panel4.TabIndex = 77;
            // 
            // txtAlasan
            // 
            this.txtAlasan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAlasan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlasan.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlasan.Location = new System.Drawing.Point(243, 25);
            this.txtAlasan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAlasan.MaxLength = 60;
            this.txtAlasan.Multiline = true;
            this.txtAlasan.Name = "txtAlasan";
            this.txtAlasan.Size = new System.Drawing.Size(69, 454);
            this.txtAlasan.TabIndex = 43;
            this.txtAlasan.TextChanged += new System.EventHandler(this.txtAlasan_TextChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(29, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(187, 42);
            this.label9.TabIndex = 42;
            this.label9.Text = "Keperluan :";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btn_kembali);
            this.panel5.Controls.Add(this.btn_PrintKeluar);
            this.panel5.Location = new System.Drawing.Point(21, 720);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1491, 100);
            this.panel5.TabIndex = 78;
            // 
            // btn_kembali
            // 
            this.btn_kembali.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_kembali.BackColor = System.Drawing.Color.Transparent;
            this.btn_kembali.BackgroundImage = global::latihribbon.Properties.Resources.left_arrow_removebg_preview;
            this.btn_kembali.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_kembali.FlatAppearance.BorderSize = 0;
            this.btn_kembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kembali.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_kembali.ForeColor = System.Drawing.Color.White;
            this.btn_kembali.Location = new System.Drawing.Point(16, 9);
            this.btn_kembali.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_kembali.Name = "btn_kembali";
            this.btn_kembali.Size = new System.Drawing.Size(76, 76);
            this.btn_kembali.TabIndex = 52;
            this.btn_kembali.UseVisualStyleBackColor = false;
            this.btn_kembali.Click += new System.EventHandler(this.btn_kembali_Click);
            // 
            // btn_PrintKeluar
            // 
            this.btn_PrintKeluar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PrintKeluar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_PrintKeluar.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PrintKeluar.Image = ((System.Drawing.Image)(resources.GetObject("btn_PrintKeluar.Image")));
            this.btn_PrintKeluar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_PrintKeluar.Location = new System.Drawing.Point(1309, 18);
            this.btn_PrintKeluar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PrintKeluar.Name = "btn_PrintKeluar";
            this.btn_PrintKeluar.Size = new System.Drawing.Size(160, 60);
            this.btn_PrintKeluar.TabIndex = 51;
            this.btn_PrintKeluar.Text = "Print ";
            this.btn_PrintKeluar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PrintKeluar.UseVisualStyleBackColor = true;
            this.btn_PrintKeluar.Click += new System.EventHandler(this.btn_PrintKeluar_Click);
            // 
            // LabelLenghKeperluan
            // 
            this.LabelLenghKeperluan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelLenghKeperluan.AutoSize = true;
            this.LabelLenghKeperluan.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLenghKeperluan.Location = new System.Drawing.Point(249, 481);
            this.LabelLenghKeperluan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLenghKeperluan.Name = "LabelLenghKeperluan";
            this.LabelLenghKeperluan.Size = new System.Drawing.Size(63, 32);
            this.LabelLenghKeperluan.TabIndex = 65;
            this.LabelLenghKeperluan.Text = "0/60";
            // 
            // SuratKeluarcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 846);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SuratKeluarcs";
            this.Text = "SuratKeluarcs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SuratKeluarcs_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtAlasan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKelas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNIS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_PrintKeluar;
        private System.Windows.Forms.Button btn_kembali;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tx_keluar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialogKeluar;
        private System.Drawing.Printing.PrintDocument printDocumentKeluar;
        private System.Windows.Forms.TextBox txtTanggal;
        private System.Windows.Forms.DateTimePicker jamKembali;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label LabelLenghKeperluan;
    }
}