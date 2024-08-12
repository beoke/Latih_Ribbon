namespace latihribbon
{
    partial class FormTerlambat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTerlambat));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tglsatu = new System.Windows.Forms.DateTimePicker();
            this.btn_terlambat = new System.Windows.Forms.Button();
            this.txtTahun = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tgldua = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNIS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1497, 470);
            this.dataGridView1.TabIndex = 0;
            // 
            // tglsatu
            // 
            this.tglsatu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tglsatu.Location = new System.Drawing.Point(590, 29);
            this.tglsatu.Name = "tglsatu";
            this.tglsatu.Size = new System.Drawing.Size(182, 24);
            this.tglsatu.TabIndex = 19;
            // 
            // btn_terlambat
            // 
            this.btn_terlambat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_terlambat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_terlambat.BackgroundImage")));
            this.btn_terlambat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_terlambat.Location = new System.Drawing.Point(1447, 18);
            this.btn_terlambat.Name = "btn_terlambat";
            this.btn_terlambat.Size = new System.Drawing.Size(54, 46);
            this.btn_terlambat.TabIndex = 18;
            this.btn_terlambat.UseVisualStyleBackColor = true;
            this.btn_terlambat.Click += new System.EventHandler(this.btn_terlambat_Click);
            // 
            // txtTahun
            // 
            this.txtTahun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTahun.Location = new System.Drawing.Point(1295, 27);
            this.txtTahun.Name = "txtTahun";
            this.txtTahun.Size = new System.Drawing.Size(100, 24);
            this.txtTahun.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1232, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tahun :";
            // 
            // txtKelas
            // 
            this.txtKelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(1117, 28);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(100, 24);
            this.txtKelas.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1058, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Kelas :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(516, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tanggal :";
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(284, 29);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(216, 24);
            this.txtNama.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nama :";
            // 
            // tgldua
            // 
            this.tgldua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgldua.Location = new System.Drawing.Point(844, 30);
            this.tgldua.Name = "tgldua";
            this.tgldua.Size = new System.Drawing.Size(182, 24);
            this.tgldua.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(780, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "Sampai";
            // 
            // txtNIS
            // 
            this.txtNIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(87, 29);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(113, 24);
            this.txtNIS.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "NIS :";
            // 
            // FormTerlambat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1513, 553);
            this.Controls.Add(this.txtNIS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tgldua);
            this.Controls.Add(this.tglsatu);
            this.Controls.Add(this.btn_terlambat);
            this.Controls.Add(this.txtTahun);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKelas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormTerlambat";
            this.Text = "FormTerlambat";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker tglsatu;
        private System.Windows.Forms.Button btn_terlambat;
        private System.Windows.Forms.TextBox txtTahun;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKelas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker tgldua;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNIS;
        private System.Windows.Forms.Label label6;
    }
}