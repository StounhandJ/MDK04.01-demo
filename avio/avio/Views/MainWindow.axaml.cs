using Avalonia.Controls;
using avio.ViewModels;

namespace avio.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.DataContextChanged += (sender, e) =>
        {
            if (this.DataContext?.GetType() == typeof(MainWindowViewModel))
            {
                MainWindowViewModel.parent = this;
            }
        };


        InitializeComponent();
    }
}