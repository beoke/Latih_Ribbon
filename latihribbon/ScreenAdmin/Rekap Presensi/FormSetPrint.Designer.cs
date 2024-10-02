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
            this.ListBoxKelas = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonAturPrint = new System.Windows.Forms.Button();
            this.CheckBoxAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ListBoxKelas
            // 
            this.ListBoxKelas.FormattingEnabled = true;
            this.ListBoxKelas.Location = new System.Drawing.Point(12, 98);
            this.ListBoxKelas.Name = "ListBoxKelas";
            this.ListBoxKelas.Size = new System.Drawing.Size(368, 326);
            this.ListBoxKelas.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pilih data yang ingin anda print";
            // 
            // ButtonAturPrint
            // 
            this.ButtonAturPrint.BackColor = System.Drawing.Color.Gray;
            this.ButtonAturPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAturPrint.ForeColor = System.Drawing.Color.White;
            this.ButtonAturPrint.Location = new System.Drawing.Point(130, 433);
            this.ButtonAturPrint.Name = "ButtonAturPrint";
            this.ButtonAturPrint.Size = new System.Drawing.Size(135, 49);
            this.ButtonAturPrint.TabIndex = 2;
            this.ButtonAturPrint.Text = "Print";
            this.ButtonAturPrint.UseVisualStyleBackColor = false;
            // 
            // CheckBoxAll
            // 
            this.CheckBoxAll.AutoSize = true;
            this.CheckBoxAll.Location = new System.Drawing.Point(15, 68);
            this.CheckBoxAll.Name = "CheckBoxAll";
            this.CheckBoxAll.Size = new System.Drawing.Size(118, 24);
            this.CheckBoxAll.TabIndex = 3;
            this.CheckBoxAll.Text = "Pilih Semua";
            this.CheckBoxAll.UseVisualStyleBackColor = true;
            // 
            // FormSetPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 492);
            this.Controls.Add(this.CheckBoxAll);
            this.Controls.Add(this.ButtonAturPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBoxKelas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSetPrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ListBoxKelas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonAturPrint;
        private System.Windows.Forms.CheckBox CheckBoxAll;
    }
}