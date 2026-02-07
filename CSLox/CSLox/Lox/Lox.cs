using System.Text;

namespace CSLox.Lox;

public static class Lox
{
	private static bool hadError = false;

    public static void RunFile(string path)
    {
		try
		{
            string text = File.ReadAllText(Path.GetFullPath(path));
			Run(text);

			if (hadError)
				Environment.Exit(65);
        }
		catch (IOException ex)
		{
            Console.WriteLine(ex);
			throw;
		}
    }

    public static void RunPrompt()
    {
		try
		{
			for (;;)
			{
                Console.WriteLine("> ");
                string? line = Console.ReadLine();

				if (line == null) 
					break;

				Run(line);
				hadError = false;
            }

		}
		catch (IOException ex)
		{
            Console.WriteLine(ex);
            throw;
		}
    }

    public static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    private static void Run(string source)
	{
		Scanner scanner = new(source);
		List<Token> tokens = scanner.ScanTokens();

		foreach (var token in tokens)
		{
            Console.WriteLine(token);
		}
    }

    private static void Report(int line, string where, string message)
	{
		Console.Error.WriteLine($"[{line}] Error {where}: {message}");

		hadError = true;
	}
}
