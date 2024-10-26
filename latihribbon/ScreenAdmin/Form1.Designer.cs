namespace latihribbon
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonOrbMenuItem1 = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbon_Siswa = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribbon_terlambat = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbon_keluar = new System.Windows.Forms.RibbonButton();
            this.AbsensiSiswa = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.ribbonSiswaAbsensi = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel9 = new System.Windows.Forms.RibbonPanel();
            this.ribbonAbsensi = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel10 = new System.Windows.Forms.RibbonPanel();
            this.ribbonRekapPersensi = new System.Windows.Forms.RibbonButton();
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel12 = new System.Windows.Forms.RibbonPanel();
            this.ribbonJurusan = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel13 = new System.Windows.Forms.RibbonPanel();
            this.ribbonKelas = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel14 = new System.Windows.Forms.RibbonPanel();
            this.ribbonUserLogin = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonSurvey = new System.Windows.Forms.RibbonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ButtonLogOut = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Margin = new System.Windows.Forms.Padding(2);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItem1);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 116);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(882, 162);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.AbsensiSiswa);
            this.ribbon1.Tabs.Add(this.ribbonTab3);
            this.ribbon1.Text = "ribbon1";
            // 
            // ribbonOrbMenuItem1
            // 
            this.ribbonOrbMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem1.Image")));
            this.ribbonOrbMenuItem1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem1.LargeImage")));
            this.ribbonOrbMenuItem1.Name = "ribbonOrbMenuItem1";
            this.ribbonOrbMenuItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem1.SmallImage")));
            this.ribbonOrbMenuItem1.Text = "ATM RESI";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Panels.Add(this.ribbonPanel2);
            this.ribbonTab1.Panels.Add(this.ribbonPanel3);
            this.ribbonTab1.Text = "ATM Siswa";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbon_Siswa);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "";
            // 
            // ribbon_Siswa
            // 
            this.ribbon_Siswa.Image = ((System.Drawing.Image)(resources.GetObject("ribbon_Siswa.Image")));
            this.ribbon_Siswa.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbon_Siswa.LargeImage")));
            this.ribbon_Siswa.Name = "ribbon_Siswa";
            this.ribbon_Siswa.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbon_Siswa.SmallImage")));
            this.ribbon_Siswa.Text = "Siswa";
            this.ribbon_Siswa.Click += new System.EventHandler(this.ribbon_Siswa_Click);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.ribbon_terlambat);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "";
            // 
            // ribbon_terlambat
            // 
            this.ribbon_terlambat.Image = ((System.Drawing.Image)(resources.GetObject("ribbon_terlambat.Image")));
            this.ribbon_terlambat.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbon_terlambat.LargeImage")));
            this.ribbon_terlambat.Name = "ribbon_terlambat";
            this.ribbon_terlambat.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbon_terlambat.SmallImage")));
            this.ribbon_terlambat.Text = "Terlambat";
            this.ribbon_terlambat.Click += new System.EventHandler(this.ribbon_terlambat_Click);
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Items.Add(this.ribbon_keluar);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "";
            // 
            // ribbon_keluar
            // 
            this.ribbon_keluar.Image = ((System.Drawing.Image)(resources.GetObject("ribbon_keluar.Image")));
            this.ribbon_keluar.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbon_keluar.LargeImage")));
            this.ribbon_keluar.Name = "ribbon_keluar";
            this.ribbon_keluar.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbon_keluar.SmallImage")));
            this.ribbon_keluar.Text = "Izin Keluar";
            this.ribbon_keluar.Click += new System.EventHandler(this.ribbon_keluar_Click);
            // 
            // AbsensiSiswa
            // 
            this.AbsensiSiswa.Name = "AbsensiSiswa";
            this.AbsensiSiswa.Panels.Add(this.ribbonPanel5);
            this.AbsensiSiswa.Panels.Add(this.ribbonPanel9);
            this.AbsensiSiswa.Panels.Add(this.ribbonPanel10);
            this.AbsensiSiswa.Text = "Absensi Siswa";
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.Items.Add(this.ribbonSiswaAbsensi);
            this.ribbonPanel5.Name = "ribbonPanel5";
            this.ribbonPanel5.Text = "";
            // 
            // ribbonSiswaAbsensi
            // 
            this.ribbonSiswaAbsensi.Image = ((System.Drawing.Image)(resources.GetObject("ribbonSiswaAbsensi.Image")));
            this.ribbonSiswaAbsensi.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonSiswaAbsensi.LargeImage")));
            this.ribbonSiswaAbsensi.Name = "ribbonSiswaAbsensi";
            this.ribbonSiswaAbsensi.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonSiswaAbsensi.SmallImage")));
            this.ribbonSiswaAbsensi.Text = "Siswa";
            this.ribbonSiswaAbsensi.Click += new System.EventHandler(this.ribbonSiswaAbsensi_Click);
            // 
            // ribbonPanel9
            // 
            this.ribbonPanel9.Items.Add(this.ribbonAbsensi);
            this.ribbonPanel9.Name = "ribbonPanel9";
            this.ribbonPanel9.Text = "";
            // 
            // ribbonAbsensi
            // 
            this.ribbonAbsensi.Image = global::latihribbon.Properties.Resources.absence__1_;
            this.ribbonAbsensi.LargeImage = global::latihribbon.Properties.Resources.absence__1_;
            this.ribbonAbsensi.Name = "ribbonAbsensi";
            this.ribbonAbsensi.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonAbsensi.SmallImage")));
            this.ribbonAbsensi.Text = "Absensi";
            this.ribbonAbsensi.Click += new System.EventHandler(this.ribbonAbsensi_Click);
            // 
            // ribbonPanel10
            // 
            this.ribbonPanel10.Items.Add(this.ribbonRekapPersensi);
            this.ribbonPanel10.Name = "ribbonPanel10";
            this.ribbonPanel10.Text = "";
            // 
            // ribbonRekapPersensi
            // 
            this.ribbonRekapPersensi.Image = ((System.Drawing.Image)(resources.GetObject("ribbonRekapPersensi.Image")));
            this.ribbonRekapPersensi.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonRekapPersensi.LargeImage")));
            this.ribbonRekapPersensi.Name = "ribbonRekapPersensi";
            this.ribbonRekapPersensi.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonRekapPersensi.SmallImage")));
            this.ribbonRekapPersensi.Text = "Rekap";
            this.ribbonRekapPersensi.Click += new System.EventHandler(this.ribbonRekapPersensi_Click);
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Panels.Add(this.ribbonPanel12);
            this.ribbonTab3.Panels.Add(this.ribbonPanel13);
            this.ribbonTab3.Panels.Add(this.ribbonPanel14);
            this.ribbonTab3.Panels.Add(this.ribbonPanel4);
            this.ribbonTab3.Text = "Data Lainnya";
            // 
            // ribbonPanel12
            // 
            this.ribbonPanel12.Items.Add(this.ribbonJurusan);
            this.ribbonPanel12.Name = "ribbonPanel12";
            this.ribbonPanel12.Text = "";
            // 
            // ribbonJurusan
            // 
            this.ribbonJurusan.Image = ((System.Drawing.Image)(resources.GetObject("ribbonJurusan.Image")));
            this.ribbonJurusan.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonJurusan.LargeImage")));
            this.ribbonJurusan.Name = "ribbonJurusan";
            this.ribbonJurusan.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonJurusan.SmallImage")));
            this.ribbonJurusan.Text = "Jurusan";
            this.ribbonJurusan.Click += new System.EventHandler(this.ribbonJurusan_Click);
            // 
            // ribbonPanel13
            // 
            this.ribbonPanel13.Items.Add(this.ribbonKelas);
            this.ribbonPanel13.Name = "ribbonPanel13";
            this.ribbonPanel13.Text = "";
            // 
            // ribbonKelas
            // 
            this.ribbonKelas.Image = ((System.Drawing.Image)(resources.GetObject("ribbonKelas.Image")));
            this.ribbonKelas.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonKelas.LargeImage")));
            this.ribbonKelas.Name = "ribbonKelas";
            this.ribbonKelas.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonKelas.SmallImage")));
            this.ribbonKelas.Text = "Kelas";
            this.ribbonKelas.Click += new System.EventHandler(this.ribbonKelas_Click);
            // 
            // ribbonPanel14
            // 
            this.ribbonPanel14.Items.Add(this.ribbonUserLogin);
            this.ribbonPanel14.Name = "ribbonPanel14";
            this.ribbonPanel14.Text = "";
            // 
            // ribbonUserLogin
            // 
            this.ribbonUserLogin.Image = ((System.Drawing.Image)(resources.GetObject("ribbonUserLogin.Image")));
            this.ribbonUserLogin.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonUserLogin.LargeImage")));
            this.ribbonUserLogin.Name = "ribbonUserLogin";
            this.ribbonUserLogin.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonUserLogin.SmallImage")));
            this.ribbonUserLogin.Text = "User Login";
            this.ribbonUserLogin.Click += new System.EventHandler(this.ribbonUserLogin_Click);
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.Items.Add(this.ribbonButtonSurvey);
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Text = "";
            // 
            // ribbonButtonSurvey
            // 
            this.ribbonButtonSurvey.Image = global::latihribbon.Properties.Resources.satisfaction__1_;
            this.ribbonButtonSurvey.LargeImage = global::latihribbon.Properties.Resources.satisfaction__1_;
            this.ribbonButtonSurvey.Name = "ribbonButtonSurvey";
            this.ribbonButtonSurvey.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSurvey.SmallImage")));
            this.ribbonButtonSurvey.Text = "Survey";
            this.ribbonButtonSurvey.Click += new System.EventHandler(this.ribbonButtonSurvey_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 162);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(882, 453);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(882, 453);
            this.dataGridView1.TabIndex = 0;
            // 
            // ButtonLogOut
            // 
            this.ButtonLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLogOut.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ButtonLogOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.ButtonLogOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.ButtonLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLogOut.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLogOut.ForeColor = System.Drawing.Color.Black;
            this.ButtonLogOut.Image = global::latihribbon.Properties.Resources.LogOutIcon;
            this.ButtonLogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonLogOut.Location = new System.Drawing.Point(750, 89);
            this.ButtonLogOut.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonLogOut.Name = "ButtonLogOut";
            this.ButtonLogOut.Size = new System.Drawing.Size(104, 36);
            this.ButtonLogOut.TabIndex = 1;
            this.ButtonLogOut.Text = "LogOut";
            this.ButtonLogOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonLogOut.UseVisualStyleBackColor = false;
            this.ButtonLogOut.Click += new System.EventHandler(this.ButtonLogOut_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 615);
            this.Controls.Add(this.ButtonLogOut);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ribbon1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "ATM RESI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbon_Siswa;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton ribbon_terlambat;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton ribbon_keluar;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RibbonPanel ribbonPanel8;
        private System.Windows.Forms.RibbonTab AbsensiSiswa;
        private System.Windows.Forms.RibbonPanel ribbonPanel5;
        private System.Windows.Forms.RibbonButton ribbonSiswaAbsensi;
        private System.Windows.Forms.RibbonPanel ribbonPanel9;
        private System.Windows.Forms.RibbonButton ribbonAbsensi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RibbonPanel ribbonPanel10;
        private System.Windows.Forms.RibbonButton ribbonRekapPersensi;
        private System.Windows.Forms.RibbonTab ribbonTab3;
        private System.Windows.Forms.RibbonPanel ribbonPanel12;
        private System.Windows.Forms.RibbonButton ribbonJurusan;
        private System.Windows.Forms.RibbonPanel ribbonPanel13;
        private System.Windows.Forms.RibbonButton ribbonKelas;
        private System.Windows.Forms.RibbonPanel ribbonPanel14;
        private System.Windows.Forms.RibbonButton ribbonUserLogin;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton ribbonButtonSurvey;
        private System.Windows.Forms.RibbonPanel ribbonPanel6;
        private System.Windows.Forms.Button ButtonLogOut;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}

