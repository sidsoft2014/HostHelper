using ApacheLib.Interfaces;
using ApacheLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for VirtualHostView.xaml
    /// </summary>
    public partial class VirtualHostView : UserControl
    {
        public VirtualHostView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as IObjectViewModel;
            if (vm != null)
                vm.Save();
        }
    }
}
