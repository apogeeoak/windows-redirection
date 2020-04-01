namespace Apogee.Redirection
{
    using System.IO;

    public class CommandParameters
    {
        public static CommandParameters? TryCreate(string[] args)
        {
            if (args == null || args.Length == 0)
                return null;

            var source = args[0];
            var filename = (args.Length > 1) ? args[1] : Path.GetFileName(source);
            var arguments = (args.Length > 2) ? Flatten(args, 2, args.Length - 2) : string.Empty;

            return new CommandParameters(source, filename, arguments);
        }

        private CommandParameters(string source, string filename, string arguments)
        {
            Source = source;
            Filename = filename;
            Arguments = arguments;
        }

        public string Source { get; }
        public string Filename { get; }
        public string Arguments { get; }

        private static string Flatten(string[] args, int start, int count)
        {
            // Wrap in double double quotes if argument contains any whitespace.
            for (int i = start; i < start + count; ++i)
                if (ContainsWhiteSpace(args[i]))
                    args[i] = @"""""" + args[i] + @"""""";

            return string.Join(" ", args, start, count);
        }

        private static bool ContainsWhiteSpace(string value)
        {
            foreach (var c in value)
                if (char.IsWhiteSpace(c))
                    return true;
            return false;
        }
    }
}
