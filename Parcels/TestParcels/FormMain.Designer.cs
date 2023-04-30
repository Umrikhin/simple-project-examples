namespace TestParcels
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonBrowse = new Button();
            labelFile = new Label();
            textBoxUrl = new TextBox();
            labelUrl = new Label();
            buttonGetData = new Button();
            listBoxIDs = new ListBox();
            SuspendLayout();
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(31, 50);
            buttonBrowse.Margin = new Padding(3, 4, 3, 4);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(455, 31);
            buttonBrowse.TabIndex = 0;
            buttonBrowse.Text = "Выбрать файл для загрузки на сервер";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // labelFile
            // 
            labelFile.BorderStyle = BorderStyle.Fixed3D;
            labelFile.Location = new Point(34, 104);
            labelFile.Name = "labelFile";
            labelFile.Size = new Size(451, 80);
            labelFile.TabIndex = 1;
            labelFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(77, 17);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(409, 27);
            textBoxUrl.TabIndex = 2;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.Location = new Point(34, 23);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(38, 20);
            labelUrl.TabIndex = 3;
            labelUrl.Text = "URL:";
            // 
            // buttonGetData
            // 
            buttonGetData.Location = new Point(31, 212);
            buttonGetData.Margin = new Padding(3, 4, 3, 4);
            buttonGetData.Name = "buttonGetData";
            buttonGetData.Size = new Size(455, 31);
            buttonGetData.TabIndex = 4;
            buttonGetData.Text = "Получить ответы с сервера";
            buttonGetData.UseVisualStyleBackColor = true;
            buttonGetData.Click += buttonGetData_Click;
            // 
            // listBoxIDs
            // 
            listBoxIDs.FormattingEnabled = true;
            listBoxIDs.ItemHeight = 20;
            listBoxIDs.Location = new Point(34, 265);
            listBoxIDs.Name = "listBoxIDs";
            listBoxIDs.Size = new Size(451, 104);
            listBoxIDs.TabIndex = 5;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 447);
            Controls.Add(listBoxIDs);
            Controls.Add(buttonGetData);
            Controls.Add(labelUrl);
            Controls.Add(textBoxUrl);
            Controls.Add(labelFile);
            Controls.Add(buttonBrowse);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Тестирование сервиса для обмена посылками";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonBrowse;
        private Label labelFile;
        private TextBox textBoxUrl;
        private Label labelUrl;
        private Button buttonGetData;
        private ListBox listBoxIDs;
    }
}