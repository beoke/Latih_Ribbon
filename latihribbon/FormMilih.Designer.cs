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
            this.btn_Keluar = new System.Windows.Forms.Button();
            this.btn_masuk = new System.Windows.Forms.Button();
            this.txtNIS = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Keluar
            // 
            this.btn_Keluar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Keluar.BackColor = System.Drawing.Color.Red;
            this.btn_Keluar.Font = new System.Drawing.Font("Swis721 BlkCn BT", 12F);
            this.btn_Keluar.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Keluar.Location = new System.Drawing.Point(474, 163);
            this.btn_Keluar.Name = "btn_Keluar";
            this.btn_Keluar.Size = new System.Drawing.Size(173, 129);
            this.btn_Keluar.TabIndex = 7;
            this.btn_Keluar.Text = "Keluar";
            this.btn_Keluar.UseVisualStyleBackColor = false;
            this.btn_Keluar.Click += new System.EventHandler(this.btn_Keluar_Click);
            // 
            // btn_masuk
            // 
            this.btn_masuk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_masuk.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_masuk.Font = new System.Drawing.Font("Swis721 BlkCn BT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_masuk.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_masuk.Location = new System.Drawing.Point(182, 163);
            this.btn_masuk.Name = "btn_masuk";
            this.btn_masuk.Size = new System.Drawing.Size(173, 129);
            this.btn_masuk.TabIndex = 6;
            this.btn_masuk.Text = "Masuk";
            this.btn_masuk.UseVisualStyleBackColor = false;
            this.btn_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // txtNIS
            // 
            this.txtNIS.AutoSize = true;
            this.txtNIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(21, 21);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(57, 20);
            this.txtNIS.TabIndex = 8;
            this.txtNIS.Text = "NIS : ";
            // 
            // txtNama
            // 
            this.txtNama.AutoSize = true;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(21, 60);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(75, 20);
            this.txtNama.TabIndex = 9;
            this.txtNama.Text = "Nama : ";
            // 
            // txtKelas
            // 
            this.txtKelas.AutoSize = true;
            this.txtKelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(21, 99);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(74, 20);
            this.txtKelas.TabIndex = 11;
            this.txtKelas.Text = "Kelas : ";
            // 
            // FormMilih
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtKelas);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNIS);
            this.Controls.Add(this.btn_Keluar);
            this.Controls.Add(this.btn_masuk);
            this.Name = "FormMilih";
            this.Text = "FormMilih";
            this.Load += new System.EventHandler(this.FormMilih_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Keluar;
        private System.Windows.Forms.Button btn_masuk;
        private Label txtNIS;
        private Label txtNama;
        private Label txtKelas;
    }
}