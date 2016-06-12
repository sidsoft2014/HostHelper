using HostHelper.Models;
using HostHelper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostHelper.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<HostFileEntry> _hostFileEntries;
        private ObservableCollection<VirtualHost> _vHosts;

        public MainWindowVM()
        {

        }

        public string Name
        {
            get { return "Some name"; }
        }
        public ObservableCollection<HostFileEntry> HostFileEntries
        {
            get
            {
                return _hostFileEntries;
            }
            private set
            {
                if (_hostFileEntries != value)
                {
                    _hostFileEntries = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<VirtualHost> VirtualHosts
        {
            get
            {
                if (_vHosts == null)
                {
                    var hosts = VirtualHostService.GetVirtualHosts();
                    _vHosts = new ObservableCollection<VirtualHost>(hosts);
                }
                return _vHosts;
            }
        }
    }
}
