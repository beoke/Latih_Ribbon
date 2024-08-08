namespace latihribbon
{
    partial class Pemakai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pemakai));
            this.tx_NIS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_enter = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tx_NIS
            // 
            this.tx_NIS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tx_NIS.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tx_NIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tx_NIS.Font = new System.Drawing.Font("News706 BT", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_NIS.Location = new System.Drawing.Point(35, 240);
            this.tx_NIS.Multiline = true;
            this.tx_NIS.Name = "tx_NIS";
            this.tx_NIS.Size = new System.Drawing.Size(303, 38);
            this.tx_NIS.TabIndex = 0;
            this.tx_NIS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tx_NIS_KeyPress_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scan atau Masukkan NIS";
            // 
            // btn_enter
            // 
            this.btn_enter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_enter.BackColor = System.Drawing.Color.Gray;
            this.btn_enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enter.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_enter.Location = new System.Drawing.Point(131, 295);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(107, 35);
            this.btn_enter.TabIndex = 2;
            this.btn_enter.Text = "Enter";
            this.btn_enter.UseVisualStyleBackColor = false;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(81, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 150);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_enter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tx_NIS);
            this.panel1.Location = new System.Drawing.Point(201, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 404);
            this.panel1.TabIndex = 4;
            // 
            // Pemakai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Pemakai";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tx_NIS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_enter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}