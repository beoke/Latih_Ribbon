namespace latihribbon
{
    partial class SurveyForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonKirim = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PictureBoxPuas = new System.Windows.Forms.PictureBox();
            this.PictureBoxTidakPuas = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPuas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxTidakPuas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ButtonKirim);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PictureBoxPuas);
            this.panel1.Controls.Add(this.PictureBoxTidakPuas);
            this.panel1.Location = new System.Drawing.Point(36, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1260, 690);
            this.panel1.TabIndex = 1;
            // 
            // ButtonKirim
            // 
            this.ButtonKirim.BackColor = System.Drawing.Color.Gray;
            this.ButtonKirim.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonKirim.ForeColor = System.Drawing.Color.White;
            this.ButtonKirim.Location = new System.Drawing.Point(527, 549);
            this.ButtonKirim.Name = "ButtonKirim";
            this.ButtonKirim.Size = new System.Drawing.Size(209, 59);
            this.ButtonKirim.TabIndex = 27;
            this.ButtonKirim.Text = "Kirim";
            this.ButtonKirim.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(788, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 31);
            this.label3.TabIndex = 26;
            this.label3.Text = "Puas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(354, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 31);
            this.label2.TabIndex = 25;
            this.label2.Text = "Tidak Puas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(340, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(605, 76);
            this.label1.TabIndex = 19;
            this.label1.Text = "BAGAIMANA PENILAIAN ANDA TERHADAP \r\nPELAYANAN  DISEKOLAH INI ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBoxPuas
            // 
            this.PictureBoxPuas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBoxPuas.Location = new System.Drawing.Point(749, 275);
            this.PictureBoxPuas.Name = "PictureBoxPuas";
            this.PictureBoxPuas.Size = new System.Drawing.Size(150, 150);
            this.PictureBoxPuas.TabIndex = 24;
            this.PictureBoxPuas.TabStop = false;
            // 
            // PictureBoxTidakPuas
            // 
            this.PictureBoxTidakPuas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBoxTidakPuas.Location = new System.Drawing.Point(340, 275);
            this.PictureBoxTidakPuas.Name = "PictureBoxTidakPuas";
            this.PictureBoxTidakPuas.Size = new System.Drawing.Size(155, 150);
            this.PictureBoxTidakPuas.TabIndex = 23;
            this.PictureBoxTidakPuas.TabStop = false;
            // 
            // SurveyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 763);
            this.Controls.Add(this.panel1);
            this.Name = "SurveyForm";
            this.Text = "Survey Form";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPuas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxTidakPuas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonKirim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PictureBoxPuas;
        private System.Windows.Forms.PictureBox PictureBoxTidakPuas;
    }
}