namespace Apogee.Redirection
{
    using System;

    public class Code
    {
        public void Main(string[] args)
        {
            var configuration = new Properties.Configuration();
            var resources = new Properties.Resource(configuration);
            var compiler = new Compiler();
            var parameters = CommandParameters.TryCreate(args);

            if (parameters == null)
            {
                Console.WriteLine("Get help from the 'Readme.md' on GitHub.");
                return;
            }

            var redirectSource = resources.Redirect;
            var configurationSource = ResolveConfiguration(resources.Configuration, parameters);

            compiler.Compile(parameters, configuration, redirectSource, configurationSource);
        }

        private string ResolveConfiguration(string config, CommandParameters parameters)
        {
            config = config.Replace($"{{{nameof(parameters.Source)}}}", parameters.Source);
            config = config.Replace($"{{{nameof(parameters.Arguments)}}}", parameters.Arguments);
            return config;
        }
    }
}
