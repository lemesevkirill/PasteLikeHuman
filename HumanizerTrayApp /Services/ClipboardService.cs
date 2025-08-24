using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.VisualTree;

namespace HumanizerTrayApp.Services;

public static class ClipboardService
{
    public static async Task<string?> GetTextAsync(Visual visual)
    {
        var topLevel = TopLevel.GetTopLevel(visual);
        if (topLevel?.Clipboard is { } clipboard)
        {
            return await clipboard.GetTextAsync();
        }
        return null;
    }

    public static async Task SetTextAsync(Visual visual, string text)
    {
        var topLevel = TopLevel.GetTopLevel(visual);
        if (topLevel?.Clipboard is { } clipboard)
        {
            await clipboard.SetTextAsync(text);
        }
    }
}