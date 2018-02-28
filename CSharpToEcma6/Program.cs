using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Utility;

namespace CSharpToEcma6
{
    [ExcludeFromCodeCoverage]
    public class AddressInformation
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public OwnerInformation Owner { get; set; }
        public List<Feature> Features { get; set; }
        public List<string> Tags { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class OwnerInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Feature
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var options = new JsGeneratorOptions()
            {
                CamelCase = true,
                OutputNamespace = "class",
                IncludeMergeFunction = false,
                ClassNameConstantsToRemove = new List<string>() { "Dto" },
                RespectDataMemberAttribute = true,
                RespectDefaultValueAttribute = true,
                TreatEnumsAsStrings = false,
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
            };

            // Plain Javascript generator
            var str = JsGenerator.Generate(new[] { typeof(AddressInformation) }, options);

            // ECMA6 Javascript class generator
            // var str = Ecma6Generator.Generate(new[] { typeof(AddressInformation) }, options);

            // Knockout + Ecma6 generator
            // var str = Ecma6KnockoutGenerator.Generate(new[] { typeof(AddressInformation) }, options);

            Console.WriteLine(str);
        }
    }
}
