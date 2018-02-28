using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsFilesUploadRuntimeConverterWithOptions.Generators;
using CsFilesUploadRuntimeConverterWithOptions.Mappers;
using CsFilesUploadRuntimeConverterWithOptions.Models;
using CsFilesUploadRuntimeConverterWithOptions.NamesUtility;
using Utility.Extensions;

namespace CsFilesUploadRuntimeConverterWithOptions
{
    public partial class Main : Form
    {
        private static string filePath;
        public Main()
        {
            InitializeComponent();
            List<SelectViewModel> listOfGenerateOptions = EnumExtensions.Enumerate<EGenerateOptions>()
                .Select(t => new SelectViewModel(t.ToInt(), t.GetDisplayNameOrDescription()))
                .ToList();

            generateTypesDropdown.DataSource = listOfGenerateOptions;
            generateTypesDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectFileButton_Click(object sender, EventArgs e) 
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            selectFileDialog.Multiselect = true;
            selectFileDialog.ShowDialog();
            selectFileDialog.Filter = "allfiles|*.cs";
            filePath = selectFileDialog.FileName;
            fileNameLabel.Text = selectFileDialog.SafeFileName;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                ShowErrorMessage();
                return;
            }

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
                // "Date", // TODO: Check what to do with Date type vars, cause it is interfeering with DateTime vars
                "DateTime",
                "Decimal",
                "Double",
                "Float",
                "String",
                "Object",
                "Long"
            };
            List<string> listOfLowerVarTypes = listOfVarTypes.Select(d => d.ToLower()).ToList();
            listOfLowerVarTypes.AddRange(listOfVarTypes.Select(d => d.ToLower() + "?").ToList());

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

            // Get options model 
            var options = GetOptionsModel();

            // Finally generate
            string result = null;
            switch (options.ConversionType)
            {
                case EGenerateOptions.Javascript:
                    result = JavascriptGenerator.GenerateJs(model, options);
                    break;
                case EGenerateOptions.Ecma6:
                    result = Ecma6Generator.GenerateJs(model, options);
                    break;
                case EGenerateOptions.KnockoutEcma6:
                    result = Ecma6WithKnockoutGenerator.GenerateJs(model, options);
                    break;
            }
            // result = JavascriptClassGenerator.GenerateJs(model, options);

            codeTextEditor.Text = result;
        }

        private ClassGeneratorOptions GetOptionsModel()
        {
            return new ClassGeneratorOptions
            {
                IncludeHeaders = includeHeadersCheckBox.Checked,
                IncludeUnmapFunctions = unmapFunctionCheckBox.Checked,
                IncludeIsLoadingVar = isLoadingCheckBox.Checked,
                ConversionType = (EGenerateOptions)((SelectViewModel)generateTypesDropdown.SelectedItem).Value
            };
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
