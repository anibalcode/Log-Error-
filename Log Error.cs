using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class LogAnalyzer
{
	static void Main()
	{
		Console.Write("Enter the path to the log file: ");
		string filePath = Console.ReadLine();

		if (!File.Exists(filePath))
		{
			Console.WriteLine("File not found. Exiting...");
			return;
		}

		List<string> logLines = File.ReadAllLines(filePath).ToList();
		AnalyzeLogs(logLines);
	}

	static void AnalyzeLogs(List<string> logLines)
	{
		int errorCount = 0;
		int warningCount = 0;
		Dictionary<string, int> issueFrequency = new Dictionary<string, int>();

		foreach (var line in logLines)
		{
			if (line.Contains("ERROR"))
			{
				errorCount++;
				IncrementIssueFrequency(issueFrequency, line);
			}
			else if (line.Contains("WARNING"))
			{
				warningCount++;
				IncrementIssueFrequency(issueFrequency, line);
			}
		}

		Console.WriteLine("\n=== Log Analysis Results ===");
		Console.WriteLine($"Total Errors: {errorCount}");
		Console.WriteLine($"Total Warnings: {warningCount}");

		Console.WriteLine("\nMost Common Issues:");
		foreach (var issue in issueFrequency.OrderByDescending(i => i.Value).Take(5))
		{
			Console.WriteLine($"{issue.Key}: {issue.Value} occurrences");
		}

		Console.WriteLine("\nPress Enter to exit.");
		Console.ReadLine();
	}

	static void IncrementIssueFrequency(Dictionary<string, int> issueFrequency, string line)
	{
		string issue = ExtractIssue(line);
		if (issueFrequency.ContainsKey(issue))
		{
			issueFrequency[issue]++;
		}
		else
		{
			issueFrequency[issue] = 1;
		}
	}

	static string ExtractIssue(string logLine)
	{
		int startIndex = logLine.IndexOf(':');
		return startIndex >= 0 ? logLine.Substring(startIndex + 1).Trim() : logLine.Trim();
	}
}
