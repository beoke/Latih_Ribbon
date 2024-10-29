namespace latihribbon
{
    partial class FormIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIndex));
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonAdmin = new System.Windows.Forms.Button();
            this.ButtonSimResi = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonSurvey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(141, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 33);
            this.label2.TabIndex = 59;
            this.label2.Text = "Rekap Siswa";
            // 
            // ButtonAdmin
            // 
            this.ButtonAdmin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonAdmin.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonAdmin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAdmin.ForeColor = System.Drawing.Color.White;
            this.ButtonAdmin.Image = global::latihribbon.Properties.Resources.software_engineer;
            this.ButtonAdmin.Location = new System.Drawing.Point(713, 745);
            this.ButtonAdmin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonAdmin.Name = "ButtonAdmin";
            this.ButtonAdmin.Size = new System.Drawing.Size(196, 86);
            this.ButtonAdmin.TabIndex = 60;
            this.ButtonAdmin.Text = "ADMIN";
            this.ButtonAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonAdmin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonAdmin.UseVisualStyleBackColor = false;
            // 
            // ButtonSimResi
            // 
            this.ButtonSimResi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonSimResi.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ButtonSimResi.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSimResi.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonSimResi.Image = global::latihribbon.Properties.Resources.ATM_TINGGI;
            this.ButtonSimResi.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ButtonSimResi.Location = new System.Drawing.Point(13, 276);
            this.ButtonSimResi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonSimResi.Name = "ButtonSimResi";
            this.ButtonSimResi.Size = new System.Drawing.Size(557, 304);
            this.ButtonSimResi.TabIndex = 56;
            this.ButtonSimResi.Text = "SIM RESI";
            this.ButtonSimResi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ButtonSimResi.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(13, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 126);
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // ButtonSurvey
            // 
            this.ButtonSurvey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonSurvey.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonSurvey.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSurvey.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonSurvey.Image = global::latihribbon.Properties.Resources.SURVEY_TINGGI;
            this.ButtonSurvey.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ButtonSurvey.Location = new System.Drawing.Point(1053, 276);
            this.ButtonSurvey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonSurvey.Name = "ButtonSurvey";
            this.ButtonSurvey.Size = new System.Drawing.Size(557, 304);
            this.ButtonSurvey.TabIndex = 57;
            this.ButtonSurvey.Text = "SURVEY";
            this.ButtonSurvey.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ButtonSurvey.UseVisualStyleBackColor = false;
            // 
            // FormIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1623, 842);
            this.Controls.Add(this.ButtonAdmin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ButtonSimResi);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ButtonSurvey);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormIndex";
            this.ShowInTaskbar = false;
            this.Text = "FormIndex";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonSimResi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ButtonSurvey;
        private System.Windows.Forms.Button ButtonAdmin;
    }
}