using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CSharpToJavascriptRuntimeConverter.NamesUtility;
using CSharpToJavascriptRuntimeConverter.ReflectionUtil;
using Utility;
using Utility.Extensions;
using Utility.Models;

namespace CSharpToJavascriptRuntimeConverter
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
            string line;
            string fileContent = null;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (ClassNamesUtility.IsClass(line))
                {
                    listOfClassNames.Add(ClassNamesUtility.StripClassName(line));
                }
                fileContent += line;
            }
            file.Close();

            var options = new JsGeneratorOptions()
            {
                CamelCase = camelCaseCheckBox.Checked,
                OutputNamespace = "class",
                IncludeMergeFunction = includeMergeFunctionCheckBox.Checked,
                ClassNameConstantsToRemove = new List<string>() { "Dto" },
                RespectDataMemberAttribute = respectDataMemberAttributeCheckBox.Checked,
                RespectDefaultValueAttribute = respectDefaultValueAttributeCheckBox.Checked,
                TreatEnumsAsStrings = treatEnumsAsStringsCheckBox.Checked,
                //  CustomFunctionProcessors =
                //new List<Action<StringBuilder, IEnumerable<PropertyBag>, JsGeneratorOptions>>()
                //{
                //    (builder, bags, arg3) =>
                //    {
                //        builder.AppendLine("\tthis.helloWorld = function () {");
                //        builder.AppendLine("\t\tconsole.log('hello');");
                //        builder.AppendLine("\t}");
                //    }
                //}
                ConversionType = (EGenerateOptions)((SelectViewModel)generateTypesDropdown.SelectedItem).Value
            };


            Assembly asm = BuildAssemblyUtil.BuildAssembly(fileContent);
            object instance = null;
            Type type = null;
            // instance = asm.CreateInstance(namespacename + "." + classname);
            instance = asm.CreateInstance("AddressInformation");
            type = instance.GetType();

            // Finally generate
            string result = null;
            switch (options.ConversionType)
            {
                case EGenerateOptions.Javascript:
                    result = JsGenerator.Generate(new[] { type }, options);
                    break;
                case EGenerateOptions.Ecma6:
                    // result = Ecma6Generator.GenerateJs(model, options);
                    break;
                case EGenerateOptions.KnockoutEcma6:
                    // result = Ecma6WithKnockoutGenerator.GenerateJs(model, options);
                    break;
            }

            codeTextEditor.Text = result;
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
