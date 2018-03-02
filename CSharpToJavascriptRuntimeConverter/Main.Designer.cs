namespace CSharpToJavascriptRuntimeConverter
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
            this.camelCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.includeMergeFunctionCheckBox = new System.Windows.Forms.CheckBox();
            this.respectDataMemberAttributeCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.asdasd = new System.Windows.Forms.Label();
            this.respectDefaultValueAttributeCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.generateTypesDropdown = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.treatEnumsAsStringsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.FlatAppearance.BorderSize = 2;
            this.SelectFileButton.Location = new System.Drawing.Point(271, 30);
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
            this.btnGenerate.Location = new System.Drawing.Point(243, 418);
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
            this.codeTextEditor.Location = new System.Drawing.Point(460, 0);
            this.codeTextEditor.Name = "codeTextEditor";
            this.codeTextEditor.Size = new System.Drawing.Size(822, 527);
            this.codeTextEditor.TabIndex = 3;
            this.codeTextEditor.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Camel case:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Include merge function:";
            // 
            // camelCaseCheckBox
            // 
            this.camelCaseCheckBox.AutoSize = true;
            this.camelCaseCheckBox.Location = new System.Drawing.Point(271, 138);
            this.camelCaseCheckBox.Name = "camelCaseCheckBox";
            this.camelCaseCheckBox.Size = new System.Drawing.Size(55, 21);
            this.camelCaseCheckBox.TabIndex = 7;
            this.camelCaseCheckBox.Text = "Yea";
            this.camelCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeMergeFunctionCheckBox
            // 
            this.includeMergeFunctionCheckBox.AutoSize = true;
            this.includeMergeFunctionCheckBox.Location = new System.Drawing.Point(271, 174);
            this.includeMergeFunctionCheckBox.Name = "includeMergeFunctionCheckBox";
            this.includeMergeFunctionCheckBox.Size = new System.Drawing.Size(55, 21);
            this.includeMergeFunctionCheckBox.TabIndex = 8;
            this.includeMergeFunctionCheckBox.Text = "Yea";
            this.includeMergeFunctionCheckBox.UseVisualStyleBackColor = true;
            // 
            // respectDataMemberAttributeCheckBox
            // 
            this.respectDataMemberAttributeCheckBox.AutoSize = true;
            this.respectDataMemberAttributeCheckBox.Location = new System.Drawing.Point(271, 210);
            this.respectDataMemberAttributeCheckBox.Name = "respectDataMemberAttributeCheckBox";
            this.respectDataMemberAttributeCheckBox.Size = new System.Drawing.Size(55, 21);
            this.respectDataMemberAttributeCheckBox.TabIndex = 9;
            this.respectDataMemberAttributeCheckBox.Text = "Yea";
            this.respectDataMemberAttributeCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Respect data member attribute";
            // 
            // asdasd
            // 
            this.asdasd.AutoSize = true;
            this.asdasd.Location = new System.Drawing.Point(15, 243);
            this.asdasd.Name = "asdasd";
            this.asdasd.Size = new System.Drawing.Size(205, 17);
            this.asdasd.TabIndex = 11;
            this.asdasd.Text = "Respect default value attribute:";
            // 
            // respectDefaultValueAttributeCheckBox
            // 
            this.respectDefaultValueAttributeCheckBox.AutoSize = true;
            this.respectDefaultValueAttributeCheckBox.Location = new System.Drawing.Point(271, 243);
            this.respectDefaultValueAttributeCheckBox.Name = "respectDefaultValueAttributeCheckBox";
            this.respectDefaultValueAttributeCheckBox.Size = new System.Drawing.Size(55, 21);
            this.respectDefaultValueAttributeCheckBox.TabIndex = 12;
            this.respectDefaultValueAttributeCheckBox.Text = "Yea";
            this.respectDefaultValueAttributeCheckBox.UseVisualStyleBackColor = true;
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
            this.generateTypesDropdown.Location = new System.Drawing.Point(271, 95);
            this.generateTypesDropdown.Name = "generateTypesDropdown";
            this.generateTypesDropdown.Size = new System.Drawing.Size(153, 24);
            this.generateTypesDropdown.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Treat enums as strings:";
            // 
            // treatEnumsAsStringsCheckBox
            // 
            this.treatEnumsAsStringsCheckBox.AutoSize = true;
            this.treatEnumsAsStringsCheckBox.Location = new System.Drawing.Point(271, 279);
            this.treatEnumsAsStringsCheckBox.Name = "treatEnumsAsStringsCheckBox";
            this.treatEnumsAsStringsCheckBox.Size = new System.Drawing.Size(55, 21);
            this.treatEnumsAsStringsCheckBox.TabIndex = 17;
            this.treatEnumsAsStringsCheckBox.Text = "Yea";
            this.treatEnumsAsStringsCheckBox.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1282, 527);
            this.Controls.Add(this.treatEnumsAsStringsCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.generateTypesDropdown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.respectDefaultValueAttributeCheckBox);
            this.Controls.Add(this.asdasd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.respectDataMemberAttributeCheckBox);
            this.Controls.Add(this.includeMergeFunctionCheckBox);
            this.Controls.Add(this.camelCaseCheckBox);
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
        private System.Windows.Forms.CheckBox camelCaseCheckBox;
        private System.Windows.Forms.CheckBox includeMergeFunctionCheckBox;
        private System.Windows.Forms.CheckBox respectDataMemberAttributeCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label asdasd;
        private System.Windows.Forms.CheckBox respectDefaultValueAttributeCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox generateTypesDropdown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox treatEnumsAsStringsCheckBox;
    }
}

