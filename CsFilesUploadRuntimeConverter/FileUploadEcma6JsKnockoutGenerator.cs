using System.Text;
using Utility;

namespace CsFilesUploadRuntimeConverter
{
    public static class FileUploadEcma6JsKnockoutGenerator
    {
        public static string GenerateJs(JSBuilderModel model)
        {
            var sbOut = new StringBuilder();

            foreach (var cModel in model.FileClasses)
            {
                var sb = new StringBuilder();
                BuildClassConstructor(cModel.ClassName, sb);

                foreach (var fileProperty in cModel.FileProperties)
                {
                    if (fileProperty.IsArray)
                    {
                        BuildArrayProperty(sb, fileProperty, cModel.ClassName);
                    }
                    else
                    {
                        BuildPrimitiveProperty(sb, fileProperty);
                    }
                }
                BuildClassClosure(cModel.ClassName, sb);

                sbOut.AppendLine(sb.ToString());
                sbOut.AppendLine();
            }
            return sbOut.ToString();
        }

        private static void BuildClassConstructor(string cName, StringBuilder sb)
        {
            sb.AppendLine(
                $"class {cName} {{ \n constructor(data) {{ ");
        }

        private static void BuildArrayProperty(StringBuilder sb, FilePropertyModel fileProperty, string className)
        {
            string nameOfMapVar = Helpers.ToCamelCase(fileProperty.PropertyName, true);

            if (fileProperty.PropertyType == PropertyType.ClassType)
            {
                sb.AppendLine(
                    $"\tconst mapped{fileProperty.PropertyName} = data.{nameOfMapVar}.map(s => new {fileProperty.PropertyTypeName}(s));");

                sb.AppendLine(
                    $"\tthis.{nameOfMapVar} = ko.observableArray(mapped{fileProperty.PropertyName});");
            }
            else
            {
                sb.AppendLine(
                    $"\tthis.{nameOfMapVar} = ko.observableArray(data.{nameOfMapVar});");
            }
        }

        private static void BuildPrimitiveProperty(StringBuilder sb, FilePropertyModel fileProperty)
        {
            string nameOfMapVar = Helpers.ToCamelCase(fileProperty.PropertyName, true);

            if (fileProperty.PropertyType == PropertyType.PrimitiveType)
            {
                sb.AppendLine($"\tthis.{nameOfMapVar} = data.{nameOfMapVar};");
            }
            if (fileProperty.PropertyType == PropertyType.ClassType)
            {
                sb.AppendLine($"\tthis.{nameOfMapVar} = new {fileProperty.PropertyTypeName}(data.{nameOfMapVar});");
            }

            if (fileProperty.PropertyType == PropertyType.Undefined)
            {
                sb.AppendLine($"\t // there is property here called: {nameOfMapVar}. We are unable to parse it for some reason.");
            }
        }

        private static void BuildClassClosure(string cName, StringBuilder sb)
        {
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine($"export default {cName};");
        }

    }
}
