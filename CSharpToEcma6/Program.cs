using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToEcma6
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Castle.Sharp2Js.JsGeneratorOptions()
            {
                CamelCase = true,
                OutputNamespace = "models",
                IncludeMergeFunction = true,
                ClassNameConstantsToRemove = new List<string>() { "Dto" },
                RespectDataMemberAttribute = true,
                RespectDefaultValueAttribute = true,
                TreatEnumsAsStrings = false,
                CustomFunctionProcessors =
                    new List<Action<StringBuilder, IEnumerable<Castle.Sharp2Js.PropertyBag>, Castle.Sharp2Js.JsGeneratorOptions>>()
                    {
                        (builder, bags, arg3) =>
                        {
                            builder.AppendLine("\tthis.helloWorld = function () {");
                            builder.AppendLine("\t\tconsole.log('hello');");
                            builder.AppendLine("\t}");
                        }
                    }
            };
            var str = Castle.Sharp2Js.JsGenerator.Generate(new[] { typeof(Castle.Sharp2Js.SampleData.AddressInformation) }, options);

            Console.WriteLine(str);
        }
    }
}
