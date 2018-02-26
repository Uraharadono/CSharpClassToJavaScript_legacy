namespace CsFilesUploadRuntimeConverterWithOptions
{
    partial class Main
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
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.codeTextEditor = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.includeHeadersCheckBox = new System.Windows.Forms.CheckBox();
            this.allObservableCheckBox = new System.Windows.Forms.CheckBox();
            this.unmapFunctionCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.isLoadingCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.generateTypesDropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(132, 30);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(153, 36);
            this.SelectFileButton.TabIndex = 0;
            this.SelectFileButton.Text = "Select File";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(129, 69);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(0, 17);
            this.fileNameLabel.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGenerate.Location = new System.Drawing.Point(198, 421);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(181, 39);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // codeTextEditor
            // 
            this.codeTextEditor.Dock = System.Windows.Forms.DockStyle.Right;
            this.codeTextEditor.Location = new System.Drawing.Point(385, 0);
            this.codeTextEditor.Name = "codeTextEditor";
            this.codeTextEditor.Size = new System.Drawing.Size(693, 472);
            this.codeTextEditor.TabIndex = 3;
            this.codeTextEditor.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Include headers:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "All observable:";
            // 
            // includeHeadersCheckBox
            // 
            this.includeHeadersCheckBox.AutoSize = true;
            this.includeHeadersCheckBox.Location = new System.Drawing.Point(132, 138);
            this.includeHeadersCheckBox.Name = "includeHeadersCheckBox";
            this.includeHeadersCheckBox.Size = new System.Drawing.Size(55, 21);
            this.includeHeadersCheckBox.TabIndex = 7;
            this.includeHeadersCheckBox.Text = "Yea";
            this.includeHeadersCheckBox.UseVisualStyleBackColor = true;
            // 
            // allObservableCheckBox
            // 
            this.allObservableCheckBox.AutoSize = true;
            this.allObservableCheckBox.Location = new System.Drawing.Point(132, 185);
            this.allObservableCheckBox.Name = "allObservableCheckBox";
            this.allObservableCheckBox.Size = new System.Drawing.Size(55, 21);
            this.allObservableCheckBox.TabIndex = 8;
            this.allObservableCheckBox.Text = "Yea";
            this.allObservableCheckBox.UseVisualStyleBackColor = true;
            // 
            // unmapFunctionCheckBox
            // 
            this.unmapFunctionCheckBox.AutoSize = true;
            this.unmapFunctionCheckBox.Location = new System.Drawing.Point(132, 229);
            this.unmapFunctionCheckBox.Name = "unmapFunctionCheckBox";
            this.unmapFunctionCheckBox.Size = new System.Drawing.Size(92, 21);
            this.unmapFunctionCheckBox.TabIndex = 9;
            this.unmapFunctionCheckBox.Text = "YeeeeaH!";
            this.unmapFunctionCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Unmap funct:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Add IsLoading:";
            // 
            // isLoadingCheckBox
            // 
            this.isLoadingCheckBox.AutoSize = true;
            this.isLoadingCheckBox.Location = new System.Drawing.Point(132, 274);
            this.isLoadingCheckBox.Name = "isLoadingCheckBox";
            this.isLoadingCheckBox.Size = new System.Drawing.Size(55, 21);
            this.isLoadingCheckBox.TabIndex = 12;
            this.isLoadingCheckBox.Text = "Yea";
            this.isLoadingCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Type:";
            // 
            // generateTypesDropdown
            // 
            this.generateTypesDropdown.FormattingEnabled = true;
            this.generateTypesDropdown.Location = new System.Drawing.Point(132, 95);
            this.generateTypesDropdown.Name = "generateTypesDropdown";
            this.generateTypesDropdown.Size = new System.Drawing.Size(153, 24);
            this.generateTypesDropdown.TabIndex = 15;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 472);
            this.Controls.Add(this.generateTypesDropdown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.isLoadingCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.unmapFunctionCheckBox);
            this.Controls.Add(this.allObservableCheckBox);
            this.Controls.Add(this.includeHeadersCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codeTextEditor);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.SelectFileButton);
            this.Name = "Main";
            this.Text = "C# class runtime generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RichTextBox codeTextEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox includeHeadersCheckBox;
        private System.Windows.Forms.CheckBox allObservableCheckBox;
        private System.Windows.Forms.CheckBox unmapFunctionCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox isLoadingCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox generateTypesDropdown;
    }
}

