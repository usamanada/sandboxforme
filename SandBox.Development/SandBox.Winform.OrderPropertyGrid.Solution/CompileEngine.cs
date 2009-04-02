//
// (C) Paul Tingey 2004 
//
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace OrderedPropertyGrid
{
    public class CompileEngine
    {
        //
        // Thanks to jconwell for a great sample on how to do this!
        // see http://www.codeproject.com/dotnet/DotNetScript.asp
        // This code is based on that sample.
        //
        static internal Assembly CreateAssembly(string sourceCode)
        {
            CodeDomProvider codeDomProvider = new CSharpCodeProvider();
            ICodeCompiler codeCompiler = codeDomProvider.CreateCompiler();
            //
            // Compiler parameters
            //
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.CompilerOptions = "/target:library";
            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;			
            compilerParams.IncludeDebugInformation = false;
            compilerParams.ReferencedAssemblies.Add( "mscorlib.dll");
            compilerParams.ReferencedAssemblies.Add( "System.dll");			
            compilerParams.ReferencedAssemblies.Add( "OrderedPropertyGridDemo.exe" );
            //
            // Compile the code
            //
            CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParams, sourceCode);
            //
            // Show a message box if any errors
            //
            if (compilerResults.Errors.Count > 0)
            {
                StringBuilder sb = new StringBuilder("Compile failed. ");
                foreach (CompilerError error in compilerResults.Errors)
                {
                    sb.AppendFormat("Error: {0}\n",error.ErrorText);                    
                }
                MessageBox.Show(sb.ToString(),"Compile failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            return compilerResults.CompiledAssembly;
        }
    }
}
