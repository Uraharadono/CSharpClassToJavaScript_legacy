using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CsFilesUploadRuntimeConverter
{
    public partial class Main : Form
    {
        private static string filePath;
        public Main()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.cs";
            filePath = op1.FileName;
            fileNameLabel.Text = op1.SafeFileName;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                ShowErrorMessage();
                return;
            }

            List<string> listOfClassNames = new List<string>();
            //  KEY = ClassName - Value = Property
            List<ClassPropertyPair> listOfProperties = new List<ClassPropertyPair>();

            string line;
            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (IsClass(line))
                {
                    listOfClassNames.Add(StripClassName(line));
                }

                else if (IsProperty(line))
                {
                    listOfProperties.Add(new ClassPropertyPair
                    {
                        ClassName = listOfClassNames.Last(),
                        PropertyValue = line
                    });
                }
            }

            file.Close();

            foreach (var pair in listOfProperties)
            {
                Debug.WriteLine(pair.ClassName + "  -  " + pair.PropertyValue);
            }
        }


        private bool IsClass(string line)
        {
            return line.Contains("class");
        }

        private string StripClassName(string line)
        {
            var startIndex = line.IndexOf("class ", StringComparison.Ordinal) + 6;
            var endIndex = 0;
            var length = line.Length;

            var hasCurly = line.Contains("{");
            if (!hasCurly)
            {
                // var endIndex = line.Length - line.LastIndexOf(" ", StringComparison.Ordinal);
                endIndex = line.LastIndexOf(" ", StringComparison.Ordinal);
            }
            else
            {
                endIndex = line.LastIndexOf("{", StringComparison.Ordinal);
            }

            endIndex -= startIndex - 1;

            if (endIndex <= startIndex)
                return line.Substring(startIndex);

            return line.Substring(startIndex, endIndex);
        }


        private bool IsProperty(string line)
        {
            // even though I am avare that this is error prone I can't find better way to check if
            // given line is really variable
            return (line.Contains("private") ||
                    line.Contains("public"));
            // || (line.Contains("{ get"));
        }


        private void ShowErrorMessage()
        {
            string message = "You have to select file.";
            string caption = "Error !";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            // Displays the MessageBox.
            MessageBox.Show(message, caption, buttons);
        }

    }
}
