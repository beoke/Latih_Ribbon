namespace latihribbon
{
    partial class FormDataSurvey
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ComboFilter = new System.Windows.Forms.ComboBox();
            this.ButtonResetFilter = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PickerRentan_1 = new System.Windows.Forms.DateTimePicker();
            this.PickerRentan_2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.comboPerPage = new System.Windows.Forms.ComboBox();
            this.TextTotalTidakPuas = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextTotalPuas = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonPrevious = new System.Windows.Forms.Button();
            this.LabelHalaman = new System.Windows.Forms.Label();
            this.GridListSurvey = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridListSurvey)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1205, 757);
            this.panel1.TabIndex = 37;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.ComboFilter);
            this.panel2.Controls.Add(this.ButtonResetFilter);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.PickerRentan_1);
            this.panel2.Controls.Add(this.PickerRentan_2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1205, 76);
            this.panel2.TabIndex = 0;
            // 
            // ComboFilter
            // 
            this.ComboFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboFilter.FormattingEnabled = true;
            this.ComboFilter.Location = new System.Drawing.Point(33, 25);
            this.ComboFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboFilter.Name = "ComboFilter";
            this.ComboFilter.Size = new System.Drawing.Size(121, 26);
            this.ComboFilter.TabIndex = 0;
            // 
            // ButtonResetFilter
            // 
            this.ButtonResetFilter.Location = new System.Drawing.Point(688, 26);
            this.ButtonResetFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ButtonResetFilter.Name = "ButtonResetFilter";
            this.ButtonResetFilter.Size = new System.Drawing.Size(100, 28);
            this.ButtonResetFilter.TabIndex = 32;
            this.ButtonResetFilter.TabStop = false;
            this.ButtonResetFilter.Text = "Reset";
            this.ButtonResetFilter.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(464, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 23);
            this.label5.TabIndex = 31;
            this.label5.Text = "_";
            // 
            // PickerRentan_1
            // 
            this.PickerRentan_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PickerRentan_1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerRentan_1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerRentan_1.Location = new System.Drawing.Point(275, 25);
            this.PickerRentan_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PickerRentan_1.Name = "PickerRentan_1";
            this.PickerRentan_1.Size = new System.Drawing.Size(183, 27);
            this.PickerRentan_1.TabIndex = 29;
            this.PickerRentan_1.TabStop = false;
            // 
            // PickerRentan_2
            // 
            this.PickerRentan_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PickerRentan_2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerRentan_2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerRentan_2.Location = new System.Drawing.Point(488, 25);
            this.PickerRentan_2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PickerRentan_2.Name = "PickerRentan_2";
            this.PickerRentan_2.Size = new System.Drawing.Size(183, 27);
            this.PickerRentan_2.TabIndex = 30;
            this.PickerRentan_2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(181, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 26;
            this.label2.Text = "Tanggal :";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.comboPerPage);
            this.panel3.Controls.Add(this.TextTotalTidakPuas);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.TextTotalPuas);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.ButtonNext);
            this.panel3.Controls.Add(this.ButtonPrevious);
            this.panel3.Controls.Add(this.LabelHalaman);
            this.panel3.Controls.Add(this.GridListSurvey);
            this.panel3.Location = new System.Drawing.Point(16, 90);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1173, 652);
            this.panel3.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(113, 588);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 23);
            this.label14.TabIndex = 91;
            this.label14.Text = "/Page";
            // 
            // comboPerPage
            // 
            this.comboPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboPerPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboPerPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPerPage.FormattingEnabled = true;
            this.comboPerPage.ItemHeight = 17;
            this.comboPerPage.Location = new System.Drawing.Point(28, 588);
            this.comboPerPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboPerPage.Name = "comboPerPage";
            this.comboPerPage.Size = new System.Drawing.Size(80, 25);
            this.comboPerPage.TabIndex = 1;
            // 
            // TextTotalTidakPuas
            // 
            this.TextTotalTidakPuas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTotalTidakPuas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextTotalTidakPuas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextTotalTidakPuas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TextTotalTidakPuas.Location = new System.Drawing.Point(1049, 613);
            this.TextTotalTidakPuas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextTotalTidakPuas.Name = "TextTotalTidakPuas";
            this.TextTotalTidakPuas.ReadOnly = true;
            this.TextTotalTidakPuas.Size = new System.Drawing.Size(91, 27);
            this.TextTotalTidakPuas.TabIndex = 89;
            this.TextTotalTidakPuas.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(924, 617);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 24);
            this.label6.TabIndex = 88;
            this.label6.Text = "Tidak Puas : ";
            // 
            // TextTotalPuas
            // 
            this.TextTotalPuas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTotalPuas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextTotalPuas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextTotalPuas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TextTotalPuas.Location = new System.Drawing.Point(1049, 565);
            this.TextTotalPuas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextTotalPuas.Name = "TextTotalPuas";
            this.TextTotalPuas.ReadOnly = true;
            this.TextTotalPuas.Size = new System.Drawing.Size(91, 27);
            this.TextTotalPuas.TabIndex = 87;
            this.TextTotalPuas.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(925, 567);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 24);
            this.label1.TabIndex = 86;
            this.label1.Text = "Puas           :";
            // 
            // ButtonNext
            // 
            this.ButtonNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonNext.Location = new System.Drawing.Point(643, 587);
            this.ButtonNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(100, 28);
            this.ButtonNext.TabIndex = 40;
            this.ButtonNext.TabStop = false;
            this.ButtonNext.Text = ">";
            this.ButtonNext.UseVisualStyleBackColor = true;
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonPrevious.Location = new System.Drawing.Point(379, 587);
            this.ButtonPrevious.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(100, 28);
            this.ButtonPrevious.TabIndex = 39;
            this.ButtonPrevious.TabStop = false;
            this.ButtonPrevious.Text = "<";
            this.ButtonPrevious.UseVisualStyleBackColor = true;
            // 
            // LabelHalaman
            // 
            this.LabelHalaman.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelHalaman.AutoSize = true;
            this.LabelHalaman.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHalaman.Location = new System.Drawing.Point(505, 594);
            this.LabelHalaman.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelHalaman.Name = "LabelHalaman";
            this.LabelHalaman.Size = new System.Drawing.Size(113, 20);
            this.LabelHalaman.TabIndex = 38;
            this.LabelHalaman.Text = "Halaman 1/10";
            // 
            // GridListSurvey
            // 
            this.GridListSurvey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridListSurvey.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.GridListSurvey.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridListSurvey.Location = new System.Drawing.Point(9, 9);
            this.GridListSurvey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GridListSurvey.Name = "GridListSurvey";
            this.GridListSurvey.RowHeadersWidth = 51;
            this.GridListSurvey.Size = new System.Drawing.Size(1155, 545);
            this.GridListSurvey.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenuStrip});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 34);
            // 
            // DeleteMenuStrip
            // 
            this.DeleteMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteMenuStrip.Image = global::latihribbon.Properties.Resources.bin;
            this.DeleteMenuStrip.Name = "DeleteMenuStrip";
            this.DeleteMenuStrip.Size = new System.Drawing.Size(144, 30);
            this.DeleteMenuStrip.Text = "Delete";
            // 
            // FormDataSurvey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 757);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormDataSurvey";
            this.Text = "FormDataSurvey";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridListSurvey)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView GridListSurvey;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker PickerRentan_1;
        private System.Windows.Forms.DateTimePicker PickerRentan_2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonResetFilter;
        private System.Windows.Forms.Label LabelHalaman;
        private System.Windows.Forms.ComboBox ComboFilter;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.Button ButtonPrevious;
        private System.Windows.Forms.TextBox TextTotalTidakPuas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextTotalPuas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuStrip;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboPerPage;
    }
}