using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMilih));
            this.btn_Keluar = new System.Windows.Forms.Button();
            this.btn_masuk = new System.Windows.Forms.Button();
            this.txtNIS = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.Label();
            this.btn_kembali = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Keluar
            // 
            this.btn_Keluar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Keluar.BackColor = System.Drawing.Color.Red;
            this.btn_Keluar.Font = new System.Drawing.Font("Calibri", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Keluar.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Keluar.Location = new System.Drawing.Point(914, 454);
            this.btn_Keluar.Name = "btn_Keluar";
            this.btn_Keluar.Size = new System.Drawing.Size(557, 302);
            this.btn_Keluar.TabIndex = 7;
            this.btn_Keluar.Text = "Keluar";
            this.btn_Keluar.UseVisualStyleBackColor = false;
            this.btn_Keluar.Click += new System.EventHandler(this.btn_Keluar_Click);
            // 
            // btn_masuk
            // 
            this.btn_masuk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_masuk.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_masuk.Font = new System.Drawing.Font("Calibri", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_masuk.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_masuk.Location = new System.Drawing.Point(217, 454);
            this.btn_masuk.Name = "btn_masuk";
            this.btn_masuk.Size = new System.Drawing.Size(557, 302);
            this.btn_masuk.TabIndex = 6;
            this.btn_masuk.Text = "Masuk";
            this.btn_masuk.UseVisualStyleBackColor = false;
            this.btn_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // txtNIS
            // 
            this.txtNIS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNIS.AutoSize = true;
            this.txtNIS.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(623, 126);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(120, 45);
            this.txtNIS.TabIndex = 8;
            this.txtNIS.Text = "NIS : ";
            // 
            // txtNama
            // 
            this.txtNama.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNama.AutoSize = true;
            this.txtNama.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(587, 189);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(156, 45);
            this.txtNama.TabIndex = 9;
            this.txtNama.Text = "Nama : ";
            // 
            // txtKelas
            // 
            this.txtKelas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtKelas.AutoSize = true;
            this.txtKelas.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(594, 256);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(149, 45);
            this.txtKelas.TabIndex = 11;
            this.txtKelas.Text = "Kelas : ";
            // 
            // btn_kembali
            // 
            this.btn_kembali.BackColor = System.Drawing.Color.Transparent;
            this.btn_kembali.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_kembali.BackgroundImage")));
            this.btn_kembali.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_kembali.FlatAppearance.BorderSize = 0;
            this.btn_kembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kembali.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_kembali.ForeColor = System.Drawing.Color.White;
            this.btn_kembali.Location = new System.Drawing.Point(12, 12);
            this.btn_kembali.Name = "btn_kembali";
            this.btn_kembali.Size = new System.Drawing.Size(48, 39);
            this.btn_kembali.TabIndex = 53;
            this.btn_kembali.UseVisualStyleBackColor = false;
            this.btn_kembali.Click += new System.EventHandler(this.btn_kembali_Click);
            // 
            // FormMilih
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1623, 768);
            this.Controls.Add(this.btn_kembali);
            this.Controls.Add(this.txtKelas);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNIS);
            this.Controls.Add(this.btn_Keluar);
            this.Controls.Add(this.btn_masuk);
            this.Name = "FormMilih";
            this.Text = "FormMilih";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private Button btn_kembali;
    }
}