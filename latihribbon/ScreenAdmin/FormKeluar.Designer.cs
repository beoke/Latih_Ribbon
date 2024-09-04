namespace latihribbon
{
    partial class FormKeluar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKeluar));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tglsatu = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnSave_FormSiswa = new System.Windows.Forms.Button();
            this.btnDelete_FormSiswa = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtTahun_FormSiswa = new System.Windows.Forms.TextBox();
            this.txtNIS_FormSiswa = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNama_FormSiswa = new System.Windows.Forms.TextBox();
            this.txtKelas_FormSiswa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 7);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(980, 462);
            this.dataGridView1.TabIndex = 0;
            // 
            // tglsatu
            // 
            this.tglsatu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tglsatu.Location = new System.Drawing.Point(528, 32);
            this.tglsatu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tglsatu.Name = "tglsatu";
            this.tglsatu.Size = new System.Drawing.Size(175, 24);
            this.tglsatu.TabIndex = 19;
            this.tglsatu.ValueChanged += new System.EventHandler(this.tglsatu_ValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(1436, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 46);
            this.button1.TabIndex = 18;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtTahun
            // 
            this.txtTahun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTahun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTahun.Location = new System.Drawing.Point(1213, 30);
            this.txtTahun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTahun.Name = "txtTahun";
            this.txtTahun.Size = new System.Drawing.Size(100, 24);
            this.txtTahun.TabIndex = 17;
            this.txtTahun.TextChanged += new System.EventHandler(this.txtTahun_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1141, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tahun :";
            // 
            // txtKelas
            // 
            this.txtKelas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(1036, 30);
            this.txtKelas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(100, 24);
            this.txtKelas.TabIndex = 15;
            this.txtKelas.TextChanged += new System.EventHandler(this.txtKelas_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(977, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Kelas :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(453, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tanggal :";
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(277, 34);
            this.txtNama.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(151, 24);
            this.txtNama.TabIndex = 12;
            this.txtNama.TextChanged += new System.EventHandler(this.txtNama_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(215, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nama :";
            // 
            // tgldua
            // 
            this.tgldua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgldua.Location = new System.Drawing.Point(779, 31);
            this.tgldua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tgldua.Name = "tgldua";
            this.tgldua.Size = new System.Drawing.Size(175, 24);
            this.tgldua.TabIndex = 20;
            this.tgldua.ValueChanged += new System.EventHandler(this.tgldua_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(711, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "Sampai ";
            // 
            // txtNIS
            // 
            this.txtNIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(80, 34);
            this.txtNIS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(113, 24);
            this.txtNIS.TabIndex = 23;
            this.txtNIS.TextChanged += new System.EventHandler(this.txtNIS_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(35, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "NIS :";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1511, 505);
            this.panel1.TabIndex = 29;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(9, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1000, 478);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(1027, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(469, 478);
            this.panel4.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Location = new System.Drawing.Point(7, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(455, 462);
            this.panel2.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel6.BackColor = System.Drawing.Color.Gainsboro;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btnSave_FormSiswa);
            this.panel6.Controls.Add(this.btnDelete_FormSiswa);
            this.panel6.Location = new System.Drawing.Point(6, 361);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(442, 89);
            this.panel6.TabIndex = 6;
            // 
            // btnSave_FormSiswa
            // 
            this.btnSave_FormSiswa.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSave_FormSiswa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave_FormSiswa.ForeColor = System.Drawing.Color.White;
            this.btnSave_FormSiswa.Location = new System.Drawing.Point(44, 27);
            this.btnSave_FormSiswa.Name = "btnSave_FormSiswa";
            this.btnSave_FormSiswa.Size = new System.Drawing.Size(99, 33);
            this.btnSave_FormSiswa.TabIndex = 1;
            this.btnSave_FormSiswa.Text = "Save";
            this.btnSave_FormSiswa.UseVisualStyleBackColor = false;
            // 
            // btnDelete_FormSiswa
            // 
            this.btnDelete_FormSiswa.BackColor = System.Drawing.Color.Red;
            this.btnDelete_FormSiswa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete_FormSiswa.ForeColor = System.Drawing.Color.White;
            this.btnDelete_FormSiswa.Location = new System.Drawing.Point(298, 27);
            this.btnDelete_FormSiswa.Name = "btnDelete_FormSiswa";
            this.btnDelete_FormSiswa.Size = new System.Drawing.Size(99, 33);
            this.btnDelete_FormSiswa.TabIndex = 0;
            this.btnDelete_FormSiswa.Text = "Delete";
            this.btnDelete_FormSiswa.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtTahun_FormSiswa);
            this.panel5.Controls.Add(this.txtNIS_FormSiswa);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.txtNama_FormSiswa);
            this.panel5.Controls.Add(this.txtKelas_FormSiswa);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Location = new System.Drawing.Point(6, 7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(442, 313);
            this.panel5.TabIndex = 5;
            // 
            // txtTahun_FormSiswa
            // 
            this.txtTahun_FormSiswa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTahun_FormSiswa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTahun_FormSiswa.Location = new System.Drawing.Point(24, 242);
            this.txtTahun_FormSiswa.Name = "txtTahun_FormSiswa";
            this.txtTahun_FormSiswa.Size = new System.Drawing.Size(399, 27);
            this.txtTahun_FormSiswa.TabIndex = 22;
            // 
            // txtNIS_FormSiswa
            // 
            this.txtNIS_FormSiswa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNIS_FormSiswa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS_FormSiswa.Location = new System.Drawing.Point(24, 67);
            this.txtNIS_FormSiswa.Name = "txtNIS_FormSiswa";
            this.txtNIS_FormSiswa.Size = new System.Drawing.Size(399, 27);
            this.txtNIS_FormSiswa.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Tahun";
            // 
            // txtNama_FormSiswa
            // 
            this.txtNama_FormSiswa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama_FormSiswa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama_FormSiswa.Location = new System.Drawing.Point(24, 131);
            this.txtNama_FormSiswa.Name = "txtNama_FormSiswa";
            this.txtNama_FormSiswa.Size = new System.Drawing.Size(399, 27);
            this.txtNama_FormSiswa.TabIndex = 18;
            // 
            // txtKelas_FormSiswa
            // 
            this.txtKelas_FormSiswa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKelas_FormSiswa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas_FormSiswa.Location = new System.Drawing.Point(24, 186);
            this.txtKelas_FormSiswa.Name = "txtKelas_FormSiswa";
            this.txtKelas_FormSiswa.Size = new System.Drawing.Size(399, 27);
            this.txtKelas_FormSiswa.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Nama";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "NIS";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Kelas";
            // 
            // FormKeluar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1512, 588);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtNIS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tgldua);
            this.Controls.Add(this.tglsatu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTahun);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKelas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormKeluar";
            this.Text = "FormKeluar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker tglsatu;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnSave_FormSiswa;
        private System.Windows.Forms.Button btnDelete_FormSiswa;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtTahun_FormSiswa;
        private System.Windows.Forms.TextBox txtNIS_FormSiswa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNama_FormSiswa;
        private System.Windows.Forms.TextBox txtKelas_FormSiswa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}