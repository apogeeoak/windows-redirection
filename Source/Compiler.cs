namespace Apogee.Redirection
{
    using System;
    using System.CodeDom.Compiler;
    using Apogee.Redirection.Properties;

    public class Compiler
    {
        public bool Compile(CommandParameters parameters, Configuration configuration, params string[] sources)
        {
            var compilerParameters = CompilerParameters(parameters, configuration);
            var compilerResults = CompileAssembly(compilerParameters, sources);
            return ReportResults(compilerResults, parameters);
        }

        private CompilerParameters CompilerParameters(CommandParameters parameters, Configuration configuration)
        {
            var compilerParameters = new CompilerParameters
            {
                OutputAssembly = parameters.Filename,
                CompilerOptions = configuration.CompilerOptions,
                GenerateExecutable = true,
                GenerateInMemory = false,
            };

            compilerParameters.ReferencedAssemblies.AddRange(configuration.ReferencedAssemblies);

            return compilerParameters;
        }

        private CompilerResults CompileAssembly(CompilerParameters compilerParameters, string[] sources)
        {
            using var provider = CodeDomProvider.CreateProvider("CSharp");
            return provider.CompileAssemblyFromSource(compilerParameters, sources);
        }

        private bool ReportResults(CompilerResults compilerResults, CommandParameters parameters)
        {
            if (compilerResults.Errors.Count > 0)
            {
                Console.WriteLine("Build failed.");
                Console.WriteLine("  {0} Error(s)\n", compilerResults.Errors.Count);
                foreach (var error in compilerResults.Errors)
                    Console.WriteLine("  {0}", error);
            }
            else
            {
                Console.WriteLine("Build succeeded.");
                Console.WriteLine("  {0} Error(s)\n", compilerResults.Errors.Count);
                Console.WriteLine("  {0} -> {1}", compilerResults.PathToAssembly, parameters.Source);
            }

            return compilerResults.Errors.Count > 0;
        }
    }
}
