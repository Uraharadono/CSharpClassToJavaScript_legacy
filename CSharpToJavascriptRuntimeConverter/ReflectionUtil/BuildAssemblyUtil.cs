/*
 * Idea taken from: https://www.codeproject.com/Articles/9019/Compiling-and-Executing-Code-at-Runtime
 * More explanation on: https://www.codeproject.com/Articles/12499/Run-Time-Code-Generation-I-Compile-C-Code-using-Mi
 */

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace CSharpToJavascriptRuntimeConverter.ReflectionUtil
{
    public static class BuildAssemblyUtil
    {
        public static Assembly BuildAssembly(string code)
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

        public static List<TypeInfo> GetDefinedTypeInfo(Assembly asm)
        {
            return asm.DefinedTypes.ToList();
        }

        public static List<Type> GetExportedTypes(Assembly asm)
        {
            return asm.ExportedTypes.ToList();
        }

    }
}
