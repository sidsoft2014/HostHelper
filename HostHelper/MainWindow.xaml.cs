using ApacheLib.ViewModels;
using UI.WPF.Services;
using System.Windows;
using System.Windows.Controls;

namespace UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ApacheLib.SysSettings.Init(new FileService(), new ApacheSettings());
            MainWindowVM vm = new MainWindowVM();
            this.DataContext = vm;
        }

        // This is not best practice for MVVM (This file should only initialise the components).
        // Considering the deviation needed to make the apache library portable, however, this seems acceptable.
        private void DataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid == null)
                return;
            grid.SelectedIndex = -1;
        }
    }
}
