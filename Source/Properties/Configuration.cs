namespace Apogee.Redirection.Properties
{
    public class Configuration
    {
        public string RedirectPath { get; } = "Redirect.Resources.Redirect.cs";
        public string ConfigurationPath { get; } = "Redirect.Resources.Configuration.cs";
        public string CompilerOptions { get; } = "-optimize";

        public string[] ReferencedAssemblies { get; } = new[] {
            "System.dll",
            "System.Diagnostics.Process.dll"
        };
    }
}
