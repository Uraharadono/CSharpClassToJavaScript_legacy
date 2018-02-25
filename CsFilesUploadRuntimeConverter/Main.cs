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

            // Convert.to
            List<string> listOfClassNames = new List<string>();
            List<FileLinesOverviewModel> listOfProperties = new List<FileLinesOverviewModel>();
            List<string> listOfVarTypes = new List<string>
            {
                "Int",
                "Int16",
                "Int32",
                "Int64",
                "UInt",
                "Short",
                "Bool",
                "Boolean",
                "Byte",
                "SByte",
                "Char",
                "Date",
                "DateTime",
                "Decimal",
                "Double",
                "Float",
                "String",
                "Object"
            };
            List<string> listOfLowerVarTypes = listOfVarTypes.Select(d => d.ToLower()).ToList();

            string line;
            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                // Add all class names (Classes are imidiately stripped)
                if (ClassNamesUtility.IsClass(line))
                {
                    listOfClassNames.Add(ClassNamesUtility.StripClassName(line));
                }
                // And their properties (Properties are stripped down below)
                else if (PropertyNamesUtility.IsProperty(line))
                {
                    listOfProperties.Add(new FileLinesOverviewModel
                    {
                        ClassName = listOfClassNames.Last(),
                        OriginalPropertyLine = line,
                        LineType = new LineType()
                    });
                }
            }
            file.Close();

            // First determine property type
            foreach (var pair in listOfProperties)
            {
                pair.LineType = PropertyNamesUtility.GetPropertyType(pair.OriginalPropertyLine, listOfClassNames, listOfLowerVarTypes);
            }

            // Then get property name
            foreach (var pair in listOfProperties)
            {
                pair.PropertyName = PropertyNamesUtility.StripPropertyName(pair.OriginalPropertyLine.Trim(), pair.LineType);
            }

            // Map it into proper model, so data is purposely there
            JSBuilderModel model = ModelsMapper.GetBuilderModel(listOfProperties);

            // Finally generate
            string result = FileUploadEcma6JsKnockoutGenerator.GenerateJs(model);

            Debug.WriteLine(result);

            codeTextEditor.Text = result;

            //foreach (var pair in listOfProperties)
            //{
            //    Debug.WriteLine($"{pair.ClassName}  -  {pair.OriginalPropertyLine}  ( {pair.LineType.PropertyType}/{pair.LineType.IsArray} ) /// {pair.PropertyName}  {pair.LineType.PropertyTypeName}");
            //}
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
