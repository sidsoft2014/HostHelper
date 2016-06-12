using ApacheLib.ViewModels;
using HostHelper.Services;
using System.Windows;

namespace HostHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowVM vm = new MainWindowVM(new FileService(), new ApacheSettings());
            this.DataContext = vm;
        }
    }
}
