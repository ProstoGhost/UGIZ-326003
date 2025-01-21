namespace PhotoEnhancer
{
    partial class MainForm
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
            this.filtersComboBox = new System.Windows.Forms.ComboBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // filtersComboBox
            // 
            this.filtersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtersComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filtersComboBox.FormattingEnabled = true;
            this.filtersComboBox.Location = new System.Drawing.Point(16, 16);
            this.filtersComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filtersComboBox.Name = "filtersComboBox";
            this.filtersComboBox.Size = new System.Drawing.Size(342, 28);
            this.filtersComboBox.TabIndex = 0;
            this.filtersComboBox.SelectedIndexChanged += new System.EventHandler(this.filtersComboBox_SelectedIndexChanged);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(13, 606);
            this.applyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(130, 40);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Применить";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Visible = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Location = new System.Drawing.Point(388, 16);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(400, 300);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalPictureBox.TabIndex = 2;
            this.originalPictureBox.TabStop = false;
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.Location = new System.Drawing.Point(388, 340);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(400, 300);
            this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.resultPictureBox.TabIndex = 2;
            this.resultPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 659);
            this.Controls.Add(this.resultPictureBox);
            this.Controls.Add(this.originalPictureBox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.filtersComboBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PhotoEnhancer";
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox filtersComboBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.PictureBox resultPictureBox;
    }
}

