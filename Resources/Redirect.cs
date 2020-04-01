namespace Apogee.Redirection.Embedded
{
    using System;
    using System.Diagnostics;

    public class Redirect
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration();
            new Redirect().Start(args, configuration);
        }

        private int Start(string[] args, Configuration configuration)
        {
            var arguments = string.Format("{0} {1}", configuration.Arguments, Flatten(args));
            var startInfo = new ProcessStartInfo()
            {
                FileName = configuration.Source,
                Arguments = arguments,
                CreateNoWindow = false,
                UseShellExecute = false
            };

            Console.CancelKeyPress += (object s, ConsoleCancelEventArgs e) => e.Cancel = true;
            var process = Process.Start(startInfo);
            process.WaitForExit();

            return process.ExitCode;
        }

        private string Flatten(string[] args)
        {
            // Wrap in double quotes if argument contains any whitespace.
            for (int i = 0; i < args.Length; ++i)
                if (ContainsWhiteSpace(args[i]))
                    args[i] = "\"" + args[i] + "\"";

            return string.Join(" ", args);
        }

        private bool ContainsWhiteSpace(string value)
        {
            foreach (var c in value)
                if (char.IsWhiteSpace(c))
                    return true;
            return false;
        }
    }
}
