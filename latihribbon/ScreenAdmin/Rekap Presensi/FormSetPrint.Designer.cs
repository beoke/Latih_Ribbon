namespace latihribbon
{
    partial class FormSetPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetPrint));
            this.ListBoxKelas = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonAturPrint = new System.Windows.Forms.Button();
            this.CheckBoxAll = new System.Windows.Forms.CheckBox();
            this.tgl1DT = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tgl2DT = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // ListBoxKelas
            // 
            this.ListBoxKelas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ListBoxKelas.FormattingEnabled = true;
            this.ListBoxKelas.Location = new System.Drawing.Point(11, 181);
            this.ListBoxKelas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ListBoxKelas.Name = "ListBoxKelas";
            this.ListBoxKelas.Size = new System.Drawing.Size(328, 191);
            this.ListBoxKelas.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pilih data yang ingin Anda print";
            // 
            // ButtonAturPrint
            // 
            this.ButtonAturPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonAturPrint.BackColor = System.Drawing.Color.Gray;
            this.ButtonAturPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAturPrint.ForeColor = System.Drawing.Color.White;
            this.ButtonAturPrint.Location = new System.Drawing.Point(116, 399);
            this.ButtonAturPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonAturPrint.Name = "ButtonAturPrint";
            this.ButtonAturPrint.Size = new System.Drawing.Size(120, 39);
            this.ButtonAturPrint.TabIndex = 2;
            this.ButtonAturPrint.Text = "Print";
            this.ButtonAturPrint.UseVisualStyleBackColor = false;
            // 
            // CheckBoxAll
            // 
            this.CheckBoxAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBoxAll.AutoSize = true;
            this.CheckBoxAll.Location = new System.Drawing.Point(13, 157);
            this.CheckBoxAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckBoxAll.Name = "CheckBoxAll";
            this.CheckBoxAll.Size = new System.Drawing.Size(100, 20);
            this.CheckBoxAll.TabIndex = 3;
            this.CheckBoxAll.Text = "Pilih Semua";
            this.CheckBoxAll.UseVisualStyleBackColor = true;
            // 
            // tgl1DT
            // 
            this.tgl1DT.Location = new System.Drawing.Point(13, 81);
            this.tgl1DT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tgl1DT.Name = "tgl1DT";
            this.tgl1DT.Size = new System.Drawing.Size(221, 22);
            this.tgl1DT.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tanggal :";
            // 
            // tgl2DT
            // 
            this.tgl2DT.Location = new System.Drawing.Point(13, 112);
            this.tgl2DT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tgl2DT.Name = "tgl2DT";
            this.tgl2DT.Size = new System.Drawing.Size(221, 22);
            this.tgl2DT.TabIndex = 5;
            // 
            // FormSetPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(348, 447);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tgl2DT);
            this.Controls.Add(this.tgl1DT);
            this.Controls.Add(this.CheckBoxAll);
            this.Controls.Add(this.ButtonAturPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBoxKelas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormSetPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ListBoxKelas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonAturPrint;
        private System.Windows.Forms.CheckBox CheckBoxAll;
        private System.Windows.Forms.DateTimePicker tgl1DT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker tgl2DT;
    }
}