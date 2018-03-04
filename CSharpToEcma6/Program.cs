using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using Utility;
using Utility.Generators;

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
        //static void Main(string[] args)
        //{
        //    var options = new JsGeneratorOptions()
        //    {
        //        CamelCase = true,
        //        OutputNamespace = "class",
        //        IncludeMergeFunction = false,
        //        ClassNameConstantsToRemove = new List<string>() { "Dto" },
        //        RespectDataMemberAttribute = true,
        //        RespectDefaultValueAttribute = true,
        //        TreatEnumsAsStrings = false,
        //        //  CustomFunctionProcessors =
        //            //new List<Action<StringBuilder, IEnumerable<PropertyBag>, JsGeneratorOptions>>()
        //            //{
        //            //    (builder, bags, arg3) =>
        //            //    {
        //            //        builder.AppendLine("\tthis.helloWorld = function () {");
        //            //        builder.AppendLine("\t\tconsole.log('hello');");
        //            //        builder.AppendLine("\t}");
        //            //    }
        //            //}
        //    };

        //    // Plain Javascript generator
        //    var str = JsGenerator.Generate(new[] { typeof(AddressInformation) }, options);

        //    // ECMA6 Javascript class generator
        //    // var str = Ecma6Generator.Generate(new[] { typeof(AddressInformation) }, options);

        //    // Knockout + Ecma6 generator
        //    // var str = Ecma6KnockoutGenerator.Generate(new[] { typeof(AddressInformation) }, options);

        //    Console.WriteLine(str);
        //}


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

            string filePath = "C:\\Users\\Uraharadono\\Desktop\\new 59.cs";
            string fileContent = null, line;
            List<string> listOfClassNames = new List<string>();
            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                fileContent += line;
            }
            file.Close();

            object returnval = null;
            Assembly asm = BuildAssembly(fileContent);
            object instance = null;
            Type type = null;
            // instance = asm.CreateInstance(namespacename + "." + classname);
            instance = asm.CreateInstance("AddressInformation");
            type = instance.GetType();


            // Plain Javascript generator
            var str = JsGenerator.Generate(new[] { type }, options);
            Console.WriteLine(str);
        }

        private static Assembly BuildAssembly(string code)
        {
            Microsoft.CSharp.CSharpCodeProvider provider = new CSharpCodeProvider();
            ICodeCompiler compiler = provider.CreateCompiler();
            CompilerParameters compilerparams = new CompilerParameters();
            compilerparams.GenerateExecutable = false;
            compilerparams.GenerateInMemory = true;
            CompilerResults results =
                compiler.CompileAssemblyFromSource(compilerparams, code);
            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors)
                {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n",
                        error.Line, error.Column, error.ErrorText);
                }
                throw new Exception(errors.ToString());
            }
            return results.CompiledAssembly;
        }

    }
}