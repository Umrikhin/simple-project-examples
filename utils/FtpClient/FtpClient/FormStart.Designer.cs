namespace FtpClient
{
    partial class FormStart
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
            this.buttonCreateDir = new System.Windows.Forms.Button();
            this.buttonCopyDir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCreateDir
            // 
            this.buttonCreateDir.Location = new System.Drawing.Point(78, 46);
            this.buttonCreateDir.Name = "buttonCreateDir";
            this.buttonCreateDir.Size = new System.Drawing.Size(112, 23);
            this.buttonCreateDir.TabIndex = 0;
            this.buttonCreateDir.Text = "Создать папку";
            this.buttonCreateDir.UseVisualStyleBackColor = true;
            this.buttonCreateDir.Click += new System.EventHandler(this.buttonCreateDir_Click);
            // 
            // buttonCopyDir
            // 
            this.buttonCopyDir.Location = new System.Drawing.Point(78, 93);
            this.buttonCopyDir.Name = "buttonCopyDir";
            this.buttonCopyDir.Size = new System.Drawing.Size(112, 23);
            this.buttonCopyDir.TabIndex = 1;
            this.buttonCopyDir.Text = "Копировать папку";
            this.buttonCopyDir.UseVisualStyleBackColor = true;
            this.buttonCopyDir.Click += new System.EventHandler(this.buttonUploadDir_Click);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 227);
            this.Controls.Add(this.buttonCopyDir);
            this.Controls.Add(this.buttonCreateDir);
            this.Name = "FormStart";
            this.Text = "Start";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateDir;
        private System.Windows.Forms.Button buttonCopyDir;
    }
}

