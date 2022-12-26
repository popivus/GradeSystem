
namespace AlisavetAdmin
{
    partial class FormShowRating
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowRating));
            this.averageRatingLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ratesGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filialBox = new System.Windows.Forms.ComboBox();
            this.doctorBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toDatePicker = new System.Windows.Forms.DateTimePicker();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.allFilialsBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ratesGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // averageRatingLabel
            // 
            this.averageRatingLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.averageRatingLabel.AutoSize = true;
            this.averageRatingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.averageRatingLabel.Location = new System.Drawing.Point(247, 468);
            this.averageRatingLabel.Name = "averageRatingLabel";
            this.averageRatingLabel.Size = new System.Drawing.Size(13, 16);
            this.averageRatingLabel.TabIndex = 17;
            this.averageRatingLabel.Text = "-";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(125, 468);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Средняя оценка:";
            // 
            // ratesGridView
            // 
            this.ratesGridView.AllowUserToAddRows = false;
            this.ratesGridView.AllowUserToDeleteRows = false;
            this.ratesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ratesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ratesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ratesGridView.Location = new System.Drawing.Point(12, 244);
            this.ratesGridView.MultiSelect = false;
            this.ratesGridView.Name = "ratesGridView";
            this.ratesGridView.ReadOnly = true;
            this.ratesGridView.RowHeadersVisible = false;
            this.ratesGridView.RowHeadersWidth = 51;
            this.ratesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ratesGridView.Size = new System.Drawing.Size(377, 196);
            this.ratesGridView.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.allFilialsBox);
            this.groupBox1.Controls.Add(this.filialBox);
            this.groupBox1.Controls.Add(this.doctorBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fromDatePicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.toDatePicker);
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 166);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // filialBox
            // 
            this.filialBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filialBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filialBox.FormattingEnabled = true;
            this.filialBox.Location = new System.Drawing.Point(116, 122);
            this.filialBox.Name = "filialBox";
            this.filialBox.Size = new System.Drawing.Size(178, 21);
            this.filialBox.TabIndex = 7;
            // 
            // doctorBox
            // 
            this.doctorBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.doctorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doctorBox.FormattingEnabled = true;
            this.doctorBox.Location = new System.Drawing.Point(58, 31);
            this.doctorBox.Name = "doctorBox";
            this.doctorBox.Size = new System.Drawing.Size(289, 21);
            this.doctorBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Врач";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Филиал";
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fromDatePicker.Location = new System.Drawing.Point(58, 75);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(118, 20);
            this.fromDatePicker.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "До";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "С";
            // 
            // toDatePicker
            // 
            this.toDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toDatePicker.Location = new System.Drawing.Point(229, 75);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(118, 20);
            this.toDatePicker.TabIndex = 4;
            // 
            // acceptBtn
            // 
            this.acceptBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.acceptBtn.Location = new System.Drawing.Point(70, 203);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(267, 23);
            this.acceptBtn.TabIndex = 13;
            this.acceptBtn.Text = "Применить";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // allFilialsBox
            // 
            this.allFilialsBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.allFilialsBox.AutoSize = true;
            this.allFilialsBox.Location = new System.Drawing.Point(301, 125);
            this.allFilialsBox.Name = "allFilialsBox";
            this.allFilialsBox.Size = new System.Drawing.Size(45, 17);
            this.allFilialsBox.TabIndex = 8;
            this.allFilialsBox.Text = "Все";
            this.allFilialsBox.UseVisualStyleBackColor = true;
            this.allFilialsBox.CheckedChanged += new System.EventHandler(this.allFilialsBox_CheckedChanged);
            // 
            // FormShowRating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 504);
            this.Controls.Add(this.averageRatingLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ratesGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.acceptBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(417, 543);
            this.Name = "FormShowRating";
            this.Text = "Просмотр оценок";
            this.Load += new System.EventHandler(this.FormShowRating_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ratesGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label averageRatingLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView ratesGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox filialBox;
        private System.Windows.Forms.ComboBox doctorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker fromDatePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker toDatePicker;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.CheckBox allFilialsBox;
    }
}