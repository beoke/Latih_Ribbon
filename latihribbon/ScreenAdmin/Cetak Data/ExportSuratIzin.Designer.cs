namespace latihribbon
{
    partial class ExportSuratIzin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportSuratIzin));
            this.label2 = new System.Windows.Forms.Label();
            this.tgl2DT = new System.Windows.Forms.DateTimePicker();
            this.tgl1DT = new System.Windows.Forms.DateTimePicker();
            this.ButtonAturPrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tanggal :";
            // 
            // tgl2DT
            // 
            this.tgl2DT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgl2DT.Location = new System.Drawing.Point(42, 127);
            this.tgl2DT.Name = "tgl2DT";
            this.tgl2DT.Size = new System.Drawing.Size(194, 21);
            this.tgl2DT.TabIndex = 12;
            // 
            // tgl1DT
            // 
            this.tgl1DT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgl1DT.Location = new System.Drawing.Point(42, 101);
            this.tgl1DT.Name = "tgl1DT";
            this.tgl1DT.Size = new System.Drawing.Size(194, 21);
            this.tgl1DT.TabIndex = 11;
            // 
            // ButtonAturPrint
            // 
            this.ButtonAturPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonAturPrint.BackColor = System.Drawing.Color.Gray;
            this.ButtonAturPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAturPrint.ForeColor = System.Drawing.Color.White;
            this.ButtonAturPrint.Location = new System.Drawing.Point(98, 227);
            this.ButtonAturPrint.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonAturPrint.Name = "ButtonAturPrint";
            this.ButtonAturPrint.Size = new System.Drawing.Size(90, 32);
            this.ButtonAturPrint.TabIndex = 9;
            this.ButtonAturPrint.Text = "Export";
            this.ButtonAturPrint.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 259);
            this.panel1.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(83, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Export Data";
            // 
            // ExportSuratIzin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 283);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tgl2DT);
            this.Controls.Add(this.tgl1DT);
            this.Controls.Add(this.ButtonAturPrint);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportSuratIzin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker tgl2DT;
        private System.Windows.Forms.DateTimePicker tgl1DT;
        private System.Windows.Forms.Button ButtonAturPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}