namespace DuplicatePhotoFinder
{
    partial class DuplicatePhotoFinder
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
            folderPathLabel = new Label();
            browseButton = new Button();
            scanButton = new Button();
            SuspendLayout();
            // 
            // folderPathLabel
            // 
            folderPathLabel.AutoSize = true;
            folderPathLabel.Location = new Point(12, 29);
            folderPathLabel.Name = "folderPathLabel";
            folderPathLabel.Size = new Size(70, 15);
            folderPathLabel.TabIndex = 0;
            folderPathLabel.Text = "Folder Path:";
            // 
            // browseButton
            // 
            browseButton.Location = new Point(12, 94);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(109, 44);
            browseButton.TabIndex = 1;
            browseButton.Text = "browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // scanButton
            // 
            scanButton.Location = new Point(332, 94);
            scanButton.Name = "scanButton";
            scanButton.Size = new Size(109, 44);
            scanButton.TabIndex = 2;
            scanButton.Text = "scan";
            scanButton.UseVisualStyleBackColor = true;
            scanButton.Click += scanButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 150);
            Controls.Add(scanButton);
            Controls.Add(browseButton);
            Controls.Add(folderPathLabel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Find Duplicate Photos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label folderPathLabel;
        private Button browseButton;
        private Button scanButton;
    }
}
