using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using avio.ViewModels;

namespace avio.Pages;

public partial class AdminPage : UserControl
{
    public AdminPage()
    {
         this.DataContextChanged += (sender, e) =>
        {
            if (this.DataContext?.GetType() == typeof(MainWindowViewModel))
            {
                var t = 10;
            }
        };
        InitializeComponent();
    }
    
     public AdminPage(object? dataContext)
     {
         this.DataContext = dataContext;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}