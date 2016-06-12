using ApacheLib.ViewModels;
using UI.WPF.Services;
using System.Windows;

namespace UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ApacheLib.SysSettings.Init(new FileService(), new ApacheSettings());
            InitializeComponent();
            MainWindowVM vm = new MainWindowVM();
            this.DataContext = vm;
        }
    }
}
