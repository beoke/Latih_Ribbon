namespace latihribbon
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.label1 = new System.Windows.Forms.Label();
            this.tx_Username = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tx_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // tx_Username
            // 
            this.tx_Username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tx_Username.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_Username.Location = new System.Drawing.Point(99, 260);
            this.tx_Username.Name = "tx_Username";
            this.tx_Username.Size = new System.Drawing.Size(248, 34);
            this.tx_Username.TabIndex = 1;
            this.tx_Username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tx_Username_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(131, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tx_Password
            // 
            this.tx_Password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tx_Password.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_Password.Location = new System.Drawing.Point(99, 344);
            this.tx_Password.Name = "tx_Password";
            this.tx_Password.PasswordChar = '*';
            this.tx_Password.Size = new System.Drawing.Size(248, 34);
            this.tx_Password.TabIndex = 4;
            this.tx_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tx_Password_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(96, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // btn_Login
            // 
            this.btn_Login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Login.BackgroundImage")));
            this.btn_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Login.FlatAppearance.BorderSize = 0;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Location = new System.Drawing.Point(109, 384);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(219, 53);
            this.btn_Login.TabIndex = 5;
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btn_Login);
            this.panel1.Controls.Add(this.tx_Password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.tx_Username);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(89, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 530);
            this.panel1.TabIndex = 6;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 556);
            this.Controls.Add(this.panel1);
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tx_Username;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tx_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Panel panel1;
    }
}