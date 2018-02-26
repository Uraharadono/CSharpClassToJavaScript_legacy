using System.Collections.Generic;
using System.Text;
using Utility;

namespace CsFilesUploadRuntimeConverterWithOptions
{
    public static class JavascriptClassGenerator
    {
        public static string GenerateJs(JSBuilderModel model, ClassGeneratorOptions options)
        {
            var sbOut = new StringBuilder();

            foreach (var cModel in model.FileClasses)
            {
                var sb = new StringBuilder();

                if (options.IncludeHeaders) BuildClassHeaders(sb);

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
                
                if (options.IncludeIsLoadingVar) BuildLoadingVar(sb, options.ConversionType);
                sb.AppendLine("    }"); // first close constructor parenthesis
                if (options.IncludeUnmapFunctions) BuildUnmapFunction(sb, cModel.FileProperties, options.ConversionType);

                BuildClassClosure(cModel.ClassName, sb);

                sbOut.AppendLine(sb.ToString());
                sbOut.AppendLine();
            }
            return sbOut.ToString();
        }


        // ======================= Special Utility Functions =======================
        private static void BuildClassHeaders(StringBuilder sb)
        {
            sb.AppendLine("import $ from 'jquery';");
            sb.AppendLine("import ko from 'knockout';");
            sb.AppendLine("import ajax from '../../services/ajax-service';");
            sb.AppendLine("import validation from '../../common/validator';");
            sb.AppendLine("import toastr from '../../common/toastr';");
            sb.AppendLine("import { isNullOrWs, formatDateTime } from '../../common/util';");
            sb.AppendLine("");
        }

        private static void BuildLoadingVar(StringBuilder sb, EGenerateOptions gOption)
        {
            switch (gOption)
            {
                case EGenerateOptions.Ecma6:
                    sb.AppendLine("\tthis.isLoading = false;");
                    break;
                case EGenerateOptions.KnockoutEcma6:
                    sb.AppendLine("\tthis.isLoading = ko.observable(false); ");
                    break;
                case EGenerateOptions.Javascript:
                    // TODO: Implement
                    break;
            }
        }

        private static void BuildUnmapFunction(StringBuilder sb, List<FilePropertyModel> properties, EGenerateOptions gOption)
        {
            sb.AppendLine($" unmap() {{");
            sb.AppendLine($"\t return {{");
            foreach (var fileProperty in properties)
            {
                // Note: I don't see point in creating unmap for arrays and class-es, so in this version it will be left out
                if (!fileProperty.IsArray)
                {
                    if (fileProperty.PropertyType == PropertyType.PrimitiveType)
                    {
                        string nameOfMapVar = Helpers.ToCamelCase(fileProperty.PropertyName, true);
                        switch (gOption)
                        {
                            case EGenerateOptions.Ecma6:
                                sb.AppendLine($"\t\t {nameOfMapVar}: this.{nameOfMapVar}");
                                break;
                            case EGenerateOptions.KnockoutEcma6:
                                sb.AppendLine($"\t\t {nameOfMapVar}: this.{nameOfMapVar}()");
                                break;
                            case EGenerateOptions.Javascript:
                                // TODO: Implement
                                break;
                        }
                    }
                }
            }
        }

        // ======================= Specific Build Functions =======================

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
            // sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine($"export default {cName};");
        }

    }
}
