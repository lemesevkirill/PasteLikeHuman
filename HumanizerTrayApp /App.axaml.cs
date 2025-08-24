using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HumanizerTrayApp.Services;

namespace HumanizerTrayApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void MainWindow_OnActivated(object? sender, EventArgs e)
    {
        if (sender is Window window)
        {
            window.Activated -= MainWindow_OnActivated;
            window.IsVisible = false;
        }
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow();
            desktop.MainWindow = mainWindow;
        
            mainWindow.Activated += MainWindow_OnActivated;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void NativeMenuItem_SettingsOnClick(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void NativeMenuItem_ExitOnClick(object? sender, EventArgs e)
    {
        if (Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }

    private async void NativeMenuItem_DoTheMagicOnClick(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = desktop.MainWindow;
            
            try
            {
                string? input = await ClipboardService.GetTextAsync(mainWindow);
                Console.WriteLine($"NativeMenuItem_DoTheMagicOnClick with text {input}");

                string output = RuleEngine.ApplyAllRules(input);
                await ClipboardService.SetTextAsync(mainWindow, output);

                Console.WriteLine($"the text was copied to clipboard: {output}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке текста из буфера обмена: {ex.Message}");
            }
            
        }


       
    }
}