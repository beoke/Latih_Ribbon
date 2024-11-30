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
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(904, 615);
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
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(904, 62);
            this.panel2.TabIndex = 0;
            // 
            // ComboFilter
            // 
            this.ComboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ComboFilter.FormattingEnabled = true;
            this.ComboFilter.Location = new System.Drawing.Point(32, 21);
            this.ComboFilter.Margin = new System.Windows.Forms.Padding(2);
            this.ComboFilter.Name = "ComboFilter";
            this.ComboFilter.Size = new System.Drawing.Size(85, 21);
            this.ComboFilter.TabIndex = 0;
            // 
            // ButtonResetFilter
            // 
            this.ButtonResetFilter.Location = new System.Drawing.Point(516, 20);
            this.ButtonResetFilter.Name = "ButtonResetFilter";
            this.ButtonResetFilter.Size = new System.Drawing.Size(75, 24);
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
            this.label5.Location = new System.Drawing.Point(348, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 19);
            this.label5.TabIndex = 31;
            this.label5.Text = "_";
            // 
            // PickerRentan_1
            // 
            this.PickerRentan_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PickerRentan_1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.PickerRentan_1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerRentan_1.Location = new System.Drawing.Point(206, 20);
            this.PickerRentan_1.Margin = new System.Windows.Forms.Padding(2);
            this.PickerRentan_1.Name = "PickerRentan_1";
            this.PickerRentan_1.Size = new System.Drawing.Size(138, 23);
            this.PickerRentan_1.TabIndex = 29;
            this.PickerRentan_1.TabStop = false;
            // 
            // PickerRentan_2
            // 
            this.PickerRentan_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PickerRentan_2.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.PickerRentan_2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerRentan_2.Location = new System.Drawing.Point(366, 20);
            this.PickerRentan_2.Margin = new System.Windows.Forms.Padding(2);
            this.PickerRentan_2.Name = "PickerRentan_2";
            this.PickerRentan_2.Size = new System.Drawing.Size(138, 23);
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
            this.label2.Location = new System.Drawing.Point(136, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
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
            this.panel3.Location = new System.Drawing.Point(12, 73);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(880, 530);
            this.panel3.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(85, 478);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 19);
            this.label14.TabIndex = 91;
            this.label14.Text = "/Page";
            // 
            // comboPerPage
            // 
            this.comboPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboPerPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPerPage.FormattingEnabled = true;
            this.comboPerPage.ItemHeight = 13;
            this.comboPerPage.Location = new System.Drawing.Point(21, 478);
            this.comboPerPage.Name = "comboPerPage";
            this.comboPerPage.Size = new System.Drawing.Size(61, 21);
            this.comboPerPage.TabIndex = 1;
            // 
            // TextTotalTidakPuas
            // 
            this.TextTotalTidakPuas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTotalTidakPuas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextTotalTidakPuas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextTotalTidakPuas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TextTotalTidakPuas.Location = new System.Drawing.Point(787, 498);
            this.TextTotalTidakPuas.Margin = new System.Windows.Forms.Padding(2);
            this.TextTotalTidakPuas.Name = "TextTotalTidakPuas";
            this.TextTotalTidakPuas.ReadOnly = true;
            this.TextTotalTidakPuas.Size = new System.Drawing.Size(69, 23);
            this.TextTotalTidakPuas.TabIndex = 89;
            this.TextTotalTidakPuas.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(693, 501);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 18);
            this.label6.TabIndex = 88;
            this.label6.Text = "Tidak Puas : ";
            // 
            // TextTotalPuas
            // 
            this.TextTotalPuas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTotalPuas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextTotalPuas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextTotalPuas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TextTotalPuas.Location = new System.Drawing.Point(787, 459);
            this.TextTotalPuas.Margin = new System.Windows.Forms.Padding(2);
            this.TextTotalPuas.Name = "TextTotalPuas";
            this.TextTotalPuas.ReadOnly = true;
            this.TextTotalPuas.Size = new System.Drawing.Size(69, 23);
            this.TextTotalPuas.TabIndex = 87;
            this.TextTotalPuas.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(694, 461);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 86;
            this.label1.Text = "Puas           :";
            // 
            // ButtonNext
            // 
            this.ButtonNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonNext.Location = new System.Drawing.Point(482, 477);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(75, 23);
            this.ButtonNext.TabIndex = 40;
            this.ButtonNext.TabStop = false;
            this.ButtonNext.Text = ">";
            this.ButtonNext.UseVisualStyleBackColor = true;
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonPrevious.Location = new System.Drawing.Point(284, 477);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(75, 23);
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
            this.LabelHalaman.Location = new System.Drawing.Point(379, 483);
            this.LabelHalaman.Name = "LabelHalaman";
            this.LabelHalaman.Size = new System.Drawing.Size(90, 16);
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
            this.GridListSurvey.Location = new System.Drawing.Point(7, 7);
            this.GridListSurvey.Name = "GridListSurvey";
            this.GridListSurvey.RowHeadersWidth = 51;
            this.GridListSurvey.Size = new System.Drawing.Size(866, 443);
            this.GridListSurvey.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenuStrip});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 30);
            // 
            // DeleteMenuStrip
            // 
            this.DeleteMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteMenuStrip.Image = global::latihribbon.Properties.Resources.bin;
            this.DeleteMenuStrip.Name = "DeleteMenuStrip";
            this.DeleteMenuStrip.Size = new System.Drawing.Size(126, 26);
            this.DeleteMenuStrip.Text = "Delete";
            // 
            // FormDataSurvey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 615);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDataSurvey";
            this.Text = " ";
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