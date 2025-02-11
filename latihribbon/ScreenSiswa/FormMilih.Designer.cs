﻿using System.Windows.Forms;

namespace latihribbon
{
    partial class FormMilih
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
            this.txtNIS = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_kembali = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Keluar = new System.Windows.Forms.Button();
            this.btn_masuk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNIS
            // 
            this.txtNIS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNIS.AutoSize = true;
            this.txtNIS.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNIS.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(147, 21);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(41, 46);
            this.txtNIS.TabIndex = 8;
            this.txtNIS.Text = ": ";
            // 
            // txtNama
            // 
            this.txtNama.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNama.AutoSize = true;
            this.txtNama.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNama.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(147, 81);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(41, 46);
            this.txtNama.TabIndex = 9;
            this.txtNama.Text = ": ";
            // 
            // txtKelas
            // 
            this.txtKelas.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtKelas.AutoSize = true;
            this.txtKelas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtKelas.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(147, 144);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(41, 46);
            this.txtKelas.TabIndex = 11;
            this.txtKelas.Text = ": ";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_kembali);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btn_masuk);
            this.panel1.Controls.Add(this.btn_Keluar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 570);
            this.panel1.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(114, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 33);
            this.label2.TabIndex = 65;
            this.label2.Text = "Rekap Siswa";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::latihribbon.Properties.Resources.Resi_Logo_Round_putih_hitam;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(30, 30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.TabIndex = 64;
            this.pictureBox1.TabStop = false;
            // 
            // btn_kembali
            // 
            this.btn_kembali.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_kembali.BackColor = System.Drawing.Color.Transparent;
            this.btn_kembali.BackgroundImage = global::latihribbon.Properties.Resources.arrow_square_left;
            this.btn_kembali.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_kembali.FlatAppearance.BorderSize = 0;
            this.btn_kembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kembali.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_kembali.ForeColor = System.Drawing.Color.White;
            this.btn_kembali.Location = new System.Drawing.Point(9, 489);
            this.btn_kembali.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_kembali.Name = "btn_kembali";
            this.btn_kembali.Size = new System.Drawing.Size(69, 70);
            this.btn_kembali.TabIndex = 63;
            this.btn_kembali.UseVisualStyleBackColor = false;
            this.btn_kembali.Click += new System.EventHandler(this.btn_kembali_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.txtNIS);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtKelas);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtNama);
            this.panel3.Location = new System.Drawing.Point(316, -8);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 201);
            this.panel3.TabIndex = 61;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 46);
            this.label4.TabIndex = 60;
            this.label4.Text = "Kelas";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 46);
            this.label3.TabIndex = 59;
            this.label3.Text = "Nama";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 46);
            this.label1.TabIndex = 58;
            this.label1.Text = "NIS";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Location = new System.Drawing.Point(12, 596);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(929, 121);
            this.panel2.TabIndex = 57;
            // 
            // btn_Keluar
            // 
            this.btn_Keluar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Keluar.BackColor = System.Drawing.Color.Silver;
            this.btn_Keluar.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Keluar.ForeColor = System.Drawing.Color.White;
            this.btn_Keluar.Image = global::latihribbon.Properties.Resources.Group_14_1;
            this.btn_Keluar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Keluar.Location = new System.Drawing.Point(159, 264);
            this.btn_Keluar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Keluar.Name = "btn_Keluar";
            this.btn_Keluar.Size = new System.Drawing.Size(600, 361);
            this.btn_Keluar.TabIndex = 1;
            this.btn_Keluar.Text = "SURAT IZIN\r\nKELUAR";
            this.btn_Keluar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Keluar.UseVisualStyleBackColor = false;
            this.btn_Keluar.Click += new System.EventHandler(this.btn_Keluar_Click);
            // 
            // btn_masuk
            // 
            this.btn_masuk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_masuk.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_masuk.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_masuk.ForeColor = System.Drawing.Color.White;
            this.btn_masuk.Image = global::latihribbon.Properties.Resources.Group_13_1;
            this.btn_masuk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_masuk.Location = new System.Drawing.Point(189, 264);
            this.btn_masuk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_masuk.Name = "btn_masuk";
            this.btn_masuk.Size = new System.Drawing.Size(600, 361);
            this.btn_masuk.TabIndex = 0;
            this.btn_masuk.Text = "SURAT IZIN\r\nMASUK";
            this.btn_masuk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_masuk.UseVisualStyleBackColor = false;
            this.btn_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // FormMilih
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(953, 570);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMilih";
            this.Text = "FormMilih";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label txtNIS;
        private Label txtNama;
        private Label txtKelas;
        private Panel panel1;
        private Panel panel2;
        private Label label4;
        private Label label3;
        private Label label1;
        private Panel panel3;
        private Button btn_kembali;
        private Label label2;
        private PictureBox pictureBox1;
        private Button btn_masuk;
        private Button btn_Keluar;
    }
}