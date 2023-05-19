namespace unisender
{
    partial class FormStart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            buttonOk = new Button();
            buttonGetResult = new Button();
            textBoxMsg = new TextBox();
            labelMsg = new Label();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(110, 324);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(212, 23);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "Отправить";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonGetResult
            // 
            buttonGetResult.Location = new Point(436, 324);
            buttonGetResult.Name = "buttonGetResult";
            buttonGetResult.Size = new Size(212, 23);
            buttonGetResult.TabIndex = 1;
            buttonGetResult.Text = "Результат";
            buttonGetResult.UseVisualStyleBackColor = true;
            buttonGetResult.Click += buttonGetResult_Click;
            // 
            // textBoxMsg
            // 
            textBoxMsg.Location = new Point(21, 45);
            textBoxMsg.Multiline = true;
            textBoxMsg.Name = "textBoxMsg";
            textBoxMsg.ScrollBars = ScrollBars.Vertical;
            textBoxMsg.Size = new Size(707, 253);
            textBoxMsg.TabIndex = 2;
            textBoxMsg.Text = resources.GetString("textBoxMsg.Text");
            // 
            // labelMsg
            // 
            labelMsg.AutoSize = true;
            labelMsg.Location = new Point(16, 17);
            labelMsg.Name = "labelMsg";
            labelMsg.Size = new Size(97, 15);
            labelMsg.TabIndex = 3;
            labelMsg.Text = "Тест сообщения";
            // 
            // FormStart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(759, 379);
            Controls.Add(labelMsg);
            Controls.Add(textBoxMsg);
            Controls.Add(buttonGetResult);
            Controls.Add(buttonOk);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormStart";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "unisender";
            Load += FormStart_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOk;
        private Button buttonGetResult;
        private TextBox textBoxMsg;
        private Label labelMsg;
    }
}