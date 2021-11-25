namespace ufanet_client
{
    partial class FormStart
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagePhoto = new System.Windows.Forms.TabPage();
            this.labelRez = new System.Windows.Forms.Label();
            this.buttonRec = new System.Windows.Forms.Button();
            this.buttonClearPhoto = new System.Windows.Forms.Button();
            this.buttonBrowsePhoto = new System.Windows.Forms.Button();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.openFileDialogPhoto = new System.Windows.Forms.OpenFileDialog();
            this.tabControlMain.SuspendLayout();
            this.tabPagePhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPagePhoto);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(500, 475);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPagePhoto
            // 
            this.tabPagePhoto.Controls.Add(this.labelRez);
            this.tabPagePhoto.Controls.Add(this.buttonRec);
            this.tabPagePhoto.Controls.Add(this.buttonClearPhoto);
            this.tabPagePhoto.Controls.Add(this.buttonBrowsePhoto);
            this.tabPagePhoto.Controls.Add(this.pictureBoxPhoto);
            this.tabPagePhoto.Location = new System.Drawing.Point(4, 22);
            this.tabPagePhoto.Name = "tabPagePhoto";
            this.tabPagePhoto.Size = new System.Drawing.Size(492, 449);
            this.tabPagePhoto.TabIndex = 0;
            this.tabPagePhoto.Text = "Фото";
            // 
            // labelRez
            // 
            this.labelRez.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRez.Location = new System.Drawing.Point(259, 20);
            this.labelRez.Name = "labelRez";
            this.labelRez.Size = new System.Drawing.Size(210, 327);
            this.labelRez.TabIndex = 4;
            this.labelRez.Text = "Результат:";
            // 
            // buttonRec
            // 
            this.buttonRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRec.Location = new System.Drawing.Point(368, 367);
            this.buttonRec.Name = "buttonRec";
            this.buttonRec.Size = new System.Drawing.Size(80, 23);
            this.buttonRec.TabIndex = 3;
            this.buttonRec.Text = "Распознать";
            this.buttonRec.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonClearPhoto
            // 
            this.buttonClearPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClearPhoto.Location = new System.Drawing.Point(272, 367);
            this.buttonClearPhoto.Name = "buttonClearPhoto";
            this.buttonClearPhoto.Size = new System.Drawing.Size(80, 23);
            this.buttonClearPhoto.TabIndex = 1;
            this.buttonClearPhoto.Text = "Очистить";
            this.buttonClearPhoto.Click += new System.EventHandler(this.buttonClearPhoto_Click);
            // 
            // buttonBrowsePhoto
            // 
            this.buttonBrowsePhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowsePhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowsePhoto.Location = new System.Drawing.Point(272, 399);
            this.buttonBrowsePhoto.Name = "buttonBrowsePhoto";
            this.buttonBrowsePhoto.Size = new System.Drawing.Size(80, 23);
            this.buttonBrowsePhoto.TabIndex = 2;
            this.buttonBrowsePhoto.Text = "Прикрепить";
            this.buttonBrowsePhoto.Click += new System.EventHandler(this.buttonBrowsePhoto_Click);
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPhoto.Location = new System.Drawing.Point(18, 20);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(236, 406);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhoto.TabIndex = 0;
            this.pictureBoxPhoto.TabStop = false;
            // 
            // openFileDialogPhoto
            // 
            this.openFileDialogPhoto.Filter = "Изображения (*.jpg; *.jpeg; *.bmp; *.png)|*.jpg;*.jpeg;*.bmp;*.png";
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 475);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ufanet Client";
            this.tabControlMain.ResumeLayout(false);
            this.tabPagePhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPagePhoto;
        private System.Windows.Forms.Label labelRez;
        private System.Windows.Forms.Button buttonRec;
        private System.Windows.Forms.Button buttonClearPhoto;
        private System.Windows.Forms.Button buttonBrowsePhoto;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.OpenFileDialog openFileDialogPhoto;
    }
}

