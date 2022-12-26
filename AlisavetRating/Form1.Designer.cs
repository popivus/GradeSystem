namespace AlisavetRating
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.findBtn = new System.Windows.Forms.Button();
            this.findBox = new System.Windows.Forms.TextBox();
            this.showGridView = new System.Windows.Forms.DataGridView();
            this.showRateBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.showGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // findBtn
            // 
            this.findBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findBtn.Location = new System.Drawing.Point(475, 12);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 20);
            this.findBtn.TabIndex = 1;
            this.findBtn.Text = "Найти";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // findBox
            // 
            this.findBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findBox.Location = new System.Drawing.Point(256, 12);
            this.findBox.Name = "findBox";
            this.findBox.Size = new System.Drawing.Size(192, 20);
            this.findBox.TabIndex = 2;
            this.findBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.findBox_KeyUp);
            // 
            // showGridView
            // 
            this.showGridView.AllowUserToAddRows = false;
            this.showGridView.AllowUserToDeleteRows = false;
            this.showGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.showGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.showGridView.Location = new System.Drawing.Point(12, 38);
            this.showGridView.MultiSelect = false;
            this.showGridView.Name = "showGridView";
            this.showGridView.ReadOnly = true;
            this.showGridView.RowHeadersVisible = false;
            this.showGridView.RowHeadersWidth = 51;
            this.showGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.showGridView.Size = new System.Drawing.Size(538, 196);
            this.showGridView.TabIndex = 3;
            // 
            // showRateBtn
            // 
            this.showRateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showRateBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.showRateBtn.Location = new System.Drawing.Point(12, 254);
            this.showRateBtn.Name = "showRateBtn";
            this.showRateBtn.Size = new System.Drawing.Size(538, 38);
            this.showRateBtn.TabIndex = 4;
            this.showRateBtn.Text = "Вывести на оценивающее устройство";
            this.showRateBtn.UseVisualStyleBackColor = false;
            this.showRateBtn.Click += new System.EventHandler(this.showRateBtn_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(449, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "Х";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(562, 313);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showRateBtn);
            this.Controls.Add(this.showGridView);
            this.Controls.Add(this.findBox);
            this.Controls.Add(this.findBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(578, 352);
            this.Name = "Form1";
            this.Text = "   Оценивание врачей";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.showGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.TextBox findBox;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DataGridView showGridView;
        public System.Windows.Forms.Button showRateBtn;
    }
}

