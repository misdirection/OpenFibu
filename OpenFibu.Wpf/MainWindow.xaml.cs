using System.Windows;
namespace OpenFibu.Wpf;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        DataContext = mainWindowViewModel;
        InitializeComponent();
    }
}