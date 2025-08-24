using System.Collections.Generic;

namespace HumanizerTrayApp.Services;

public static class TextProcessingService
{
    private static List<Rule> _rules = new();

    public static void Load(string rulePath)
    {
        _rules = RuleEngine.LoadRules(rulePath);
    }

    public static string Humanize(string input)
    {
        return string.Empty;  //RuleEngine.ApplyRules(input, _rules);
    }
}