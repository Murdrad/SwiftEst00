namespace SwiftEst00
{
    partial class Form1
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
            this.browseCodesFileBtn = new System.Windows.Forms.Button();
            this.codesImportFileTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.importCodesBtn = new System.Windows.Forms.Button();
            this.codesListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.costCodesCSVIncludesHeaderCheck = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.speedTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // browseCodesFileBtn
            // 
            this.browseCodesFileBtn.Location = new System.Drawing.Point(118, 53);
            this.browseCodesFileBtn.Name = "browseCodesFileBtn";
            this.browseCodesFileBtn.Size = new System.Drawing.Size(75, 23);
            this.browseCodesFileBtn.TabIndex = 0;
            this.browseCodesFileBtn.Text = "Browse Files";
            this.browseCodesFileBtn.UseVisualStyleBackColor = true;
            this.browseCodesFileBtn.Click += new System.EventHandler(this.browseCodesFileBtn_Click);
            // 
            // codesImportFileTxt
            // 
            this.codesImportFileTxt.Location = new System.Drawing.Point(12, 53);
            this.codesImportFileTxt.Name = "codesImportFileTxt";
            this.codesImportFileTxt.Size = new System.Drawing.Size(100, 20);
            this.codesImportFileTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Import Cost Codes";
            // 
            // importCodesBtn
            // 
            this.importCodesBtn.Location = new System.Drawing.Point(13, 80);
            this.importCodesBtn.Name = "importCodesBtn";
            this.importCodesBtn.Size = new System.Drawing.Size(92, 23);
            this.importCodesBtn.TabIndex = 3;
            this.importCodesBtn.Text = "Import Codes";
            this.importCodesBtn.UseVisualStyleBackColor = true;
            this.importCodesBtn.Click += new System.EventHandler(this.importCodesBtn_Click);
            // 
            // codesListBox
            // 
            this.codesListBox.FormattingEnabled = true;
            this.codesListBox.Location = new System.Drawing.Point(16, 120);
            this.codesListBox.Name = "codesListBox";
            this.codesListBox.Size = new System.Drawing.Size(354, 108);
            this.codesListBox.TabIndex = 4;
            this.codesListBox.SelectedIndexChanged += new System.EventHandler(this.codesListBox_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // costCodesCSVIncludesHeaderCheck
            // 
            this.costCodesCSVIncludesHeaderCheck.AutoSize = true;
            this.costCodesCSVIncludesHeaderCheck.Checked = true;
            this.costCodesCSVIncludesHeaderCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.costCodesCSVIncludesHeaderCheck.Location = new System.Drawing.Point(214, 58);
            this.costCodesCSVIncludesHeaderCheck.Name = "costCodesCSVIncludesHeaderCheck";
            this.costCodesCSVIncludesHeaderCheck.Size = new System.Drawing.Size(129, 17);
            this.costCodesCSVIncludesHeaderCheck.TabIndex = 5;
            this.costCodesCSVIncludesHeaderCheck.Text = "Includes Header Row";
            this.costCodesCSVIncludesHeaderCheck.UseVisualStyleBackColor = true;
            // 
            // speedTxtBox
            // 
            this.speedTxtBox.Location = new System.Drawing.Point(78, 234);
            this.speedTxtBox.Name = "speedTxtBox";
            this.speedTxtBox.Size = new System.Drawing.Size(100, 20);
            this.speedTxtBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Speed Test";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 489);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.speedTxtBox);
            this.Controls.Add(this.costCodesCSVIncludesHeaderCheck);
            this.Controls.Add(this.codesListBox);
            this.Controls.Add(this.importCodesBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codesImportFileTxt);
            this.Controls.Add(this.browseCodesFileBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseCodesFileBtn;
        private System.Windows.Forms.TextBox codesImportFileTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button importCodesBtn;
        private System.Windows.Forms.ListBox codesListBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox costCodesCSVIncludesHeaderCheck;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox speedTxtBox;
        private System.Windows.Forms.Label label2;
    }
}

