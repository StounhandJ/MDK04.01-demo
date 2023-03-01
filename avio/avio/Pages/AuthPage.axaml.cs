using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace avio.Pages;

public partial class AuthPage : UserControl
{
    public AuthPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}