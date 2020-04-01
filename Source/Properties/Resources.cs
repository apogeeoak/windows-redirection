namespace Apogee.Redirection.Properties
{
    using System.IO;
    using System.Reflection;

    public class Resource
    {
        internal Resource(Configuration configuration)
        {
            Redirect = Read(configuration.RedirectPath);
            Configuration = Read(configuration.ConfigurationPath);
        }

        private readonly Assembly assembly = typeof(Resource).Assembly;

        public string Redirect { get; }
        public string Configuration { get; }

        public string Read(string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
