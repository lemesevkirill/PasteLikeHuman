using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace HumanizerTrayApp.Services;

public class Rule
{
    public string Pattern { get; set; } = "";
    public string Replacement { get; set; } = "";
}

public static class RuleEngine
{
    public static List<Rule> LoadRules(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Rule>>(json) ?? new();
    }

    public static string ApplyAllRules(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return input
            
            .Replace("—", "-")   // эм-даш
            .Replace("–", "-")   // эн-даш
            .Replace("−", "-")   // минус
            //  « » → "
            .Replace("«", "\"")
            .Replace("»", "\"")
            // " → "
            .Replace("“", "\"")
            .Replace("”", "\"")
            // ' → '
            .Replace("‘", "'")
            .Replace("’", "'");
    }
}