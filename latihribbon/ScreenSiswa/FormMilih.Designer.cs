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
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Keluar
            // 
            this.btn_Keluar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Keluar.BackColor = System.Drawing.Color.DarkGray;
            this.btn_Keluar.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Keluar.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Keluar.Location = new System.Drawing.Point(833, 478);
            this.btn_Keluar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Keluar.Name = "btn_Keluar";
            this.btn_Keluar.Size = new System.Drawing.Size(557, 302);
            this.btn_Keluar.TabIndex = 1;
            this.btn_Keluar.Text = "SURAT KELUAR";
            this.btn_Keluar.UseVisualStyleBackColor = false;
            this.btn_Keluar.Click += new System.EventHandler(this.btn_Keluar_Click);
            // 
            // btn_masuk
            // 
            this.btn_masuk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_masuk.BackColor = System.Drawing.Color.BlueViolet;
            this.btn_masuk.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_masuk.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_masuk.Location = new System.Drawing.Point(205, 478);
            this.btn_masuk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_masuk.Name = "btn_masuk";
            this.btn_masuk.Size = new System.Drawing.Size(557, 302);
            this.btn_masuk.TabIndex = 0;
            this.btn_masuk.Text = "SURAT MASUK";
            this.btn_masuk.UseVisualStyleBackColor = false;
            this.btn_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // txtNIS
            // 
            this.txtNIS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNIS.AutoSize = true;
            this.txtNIS.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNIS.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(533, 194);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(165, 46);
            this.txtNIS.TabIndex = 8;
            this.txtNIS.Text = "NIS      : ";
            // 
            // txtNama
            // 
            this.txtNama.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNama.AutoSize = true;
            this.txtNama.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNama.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(536, 256);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(157, 46);
            this.txtNama.TabIndex = 9;
            this.txtNama.Text = "Nama  : ";
            // 
            // txtKelas
            // 
            this.txtKelas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtKelas.AutoSize = true;
            this.txtKelas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtKelas.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(533, 318);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(161, 46);
            this.txtKelas.TabIndex = 11;
            this.txtKelas.Text = "Kelas   : ";
            // 
            // btn_kembali
            // 
            this.btn_kembali.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_kembali.BackColor = System.Drawing.Color.Transparent;
            this.btn_kembali.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_kembali.BackgroundImage")));
            this.btn_kembali.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_kembali.FlatAppearance.BorderSize = 0;
            this.btn_kembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_kembali.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_kembali.ForeColor = System.Drawing.Color.White;
            this.btn_kembali.Location = new System.Drawing.Point(15, 12);
            this.btn_kembali.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_kembali.Name = "btn_kembali";
            this.btn_kembali.Size = new System.Drawing.Size(95, 95);
            this.btn_kembali.TabIndex = 53;
            this.btn_kembali.UseVisualStyleBackColor = false;
            this.btn_kembali.Click += new System.EventHandler(this.btn_kembali_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(131, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 33);
            this.label2.TabIndex = 55;
            this.label2.Text = "Rekap Siswa";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 126);
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_masuk);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_Keluar);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1599, 862);
            this.panel1.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.btn_kembali);
            this.panel2.Location = new System.Drawing.Point(12, 889);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1599, 121);
            this.panel2.TabIndex = 57;
            // 
            // FormMilih
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1623, 1022);
            this.Controls.Add(this.txtKelas);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMilih";
            this.Text = "FormMilih";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMilih_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
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
        private Label label2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel panel2;
    }
}