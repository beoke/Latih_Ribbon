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
            this.txtNIS = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.Label();
            this.txtKelas = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_masuk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Keluar = new System.Windows.Forms.Button();
            this.btn_kembali = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNIS
            // 
            this.txtNIS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNIS.AutoSize = true;
            this.txtNIS.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNIS.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIS.Location = new System.Drawing.Point(400, 141);
            this.txtNIS.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNIS.Name = "txtNIS";
            this.txtNIS.Size = new System.Drawing.Size(32, 36);
            this.txtNIS.TabIndex = 8;
            this.txtNIS.Text = ": ";
            // 
            // txtNama
            // 
            this.txtNama.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNama.AutoSize = true;
            this.txtNama.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNama.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(391, 180);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(32, 36);
            this.txtNama.TabIndex = 9;
            this.txtNama.Text = ": ";
            // 
            // txtKelas
            // 
            this.txtKelas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtKelas.AutoSize = true;
            this.txtKelas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtKelas.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKelas.Location = new System.Drawing.Point(391, 231);
            this.txtKelas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtKelas.Name = "txtKelas";
            this.txtKelas.Size = new System.Drawing.Size(32, 36);
            this.txtKelas.TabIndex = 11;
            this.txtKelas.Text = ": ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(103, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 25);
            this.label2.TabIndex = 55;
            this.label2.Text = "Rekap Siswa";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNama);
            this.panel1.Controls.Add(this.txtKelas);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_masuk);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_Keluar);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 700);
            this.panel1.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.btn_kembali);
            this.panel2.Location = new System.Drawing.Point(9, 722);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1199, 98);
            this.panel2.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(283, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 36);
            this.label1.TabIndex = 58;
            this.label1.Text = "NIS";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(283, 180);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 36);
            this.label3.TabIndex = 59;
            this.label3.Text = "Nama";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(283, 231);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 36);
            this.label4.TabIndex = 60;
            this.label4.Text = "Kelas";
            // 
            // btn_masuk
            // 
            this.btn_masuk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_masuk.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_masuk.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_masuk.ForeColor = System.Drawing.Color.White;
            this.btn_masuk.Image = global::latihribbon.Properties.Resources.Group_13_1;
            this.btn_masuk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_masuk.Location = new System.Drawing.Point(129, 332);
            this.btn_masuk.Margin = new System.Windows.Forms.Padding(2);
            this.btn_masuk.Name = "btn_masuk";
            this.btn_masuk.Size = new System.Drawing.Size(450, 293);
            this.btn_masuk.TabIndex = 0;
            this.btn_masuk.Text = "SURAT IZIN \r\nMASUK";
            this.btn_masuk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_masuk.UseVisualStyleBackColor = false;
            this.btn_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(7, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 102);
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Keluar
            // 
            this.btn_Keluar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Keluar.BackColor = System.Drawing.Color.Silver;
            this.btn_Keluar.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Keluar.ForeColor = System.Drawing.Color.White;
            this.btn_Keluar.Image = global::latihribbon.Properties.Resources.Group_14_1;
            this.btn_Keluar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Keluar.Location = new System.Drawing.Point(626, 332);
            this.btn_Keluar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Keluar.Name = "btn_Keluar";
            this.btn_Keluar.Size = new System.Drawing.Size(450, 293);
            this.btn_Keluar.TabIndex = 1;
            this.btn_Keluar.Text = "SURAT IZIN\r\nKELUAR";
            this.btn_Keluar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Keluar.UseVisualStyleBackColor = false;
            this.btn_Keluar.Click += new System.EventHandler(this.btn_Keluar_Click);
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
            this.btn_kembali.Location = new System.Drawing.Point(15, 21);
            this.btn_kembali.Margin = new System.Windows.Forms.Padding(2);
            this.btn_kembali.Name = "btn_kembali";
            this.btn_kembali.Size = new System.Drawing.Size(52, 57);
            this.btn_kembali.TabIndex = 53;
            this.btn_kembali.UseVisualStyleBackColor = false;
            this.btn_kembali.Click += new System.EventHandler(this.btn_kembali_Click);
            // 
            // FormMilih
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1217, 830);
            this.Controls.Add(this.txtNIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormMilih";
            this.Text = "FormMilih";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private Label label4;
        private Label label3;
        private Label label1;
    }
}