using ApacheLib.Interfaces;
using ApacheLib.Models;
using ApacheLib.Services;
using System.Collections.ObjectModel;
using static ApacheLib.SysSettings;

namespace ApacheLib.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<HostFileEntry> _hostFileEntries;
        private ObservableCollection<VirtualHost> _vHosts;
        private HostFileEntry _selectedHostFileEntry;
        private VirtualHost _selectedVirtualHost;
        private IViewModel _currentViewModel;

        public MainWindowVM()
        {
#if DEBUG
            CurrentViewModel = new VirtualHostVM();
#endif
        }

        public string Name
        {
            get { return "Some name"; }
        }
        public ObservableCollection<HostFileEntry> HostFileEntries
        {
            get
            {
                if(_hostFileEntries == null)
                {
                    var hosts = HFS.GetAllHosts();
                    _hostFileEntries = new ObservableCollection<HostFileEntry>(hosts);
                }
                return _hostFileEntries;
            }
            private set
            {
                if(value != _hostFileEntries)
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
                    var hosts = VHS.GetVirtualHosts();
                    _vHosts = new ObservableCollection<VirtualHost>(hosts);
                }
                return _vHosts;
            }
            private set
            {
                if (value != _vHosts)
                {
                    _vHosts = value;
                    OnPropertyChanged();
                }
            }
        }
        public HostFileEntry SelectedHostFileEntry
        {
            get
            {
                return _selectedHostFileEntry;
            }
            set
            {
                if(value != _selectedHostFileEntry)
                {
                    _selectedHostFileEntry = value;
                    OnPropertyChanged();
                }
            }
        }
        public VirtualHost SelectedVirtualHost
        {
            get
            {
                return _selectedVirtualHost;
            }
            set
            {
                if(value != _selectedVirtualHost)
                {
                    _selectedVirtualHost = value;
                    OnPropertyChanged();

                    if(CurrentViewModel != null)
                        CurrentViewModel.SetSelectedObject(_selectedVirtualHost);
                }
            }
        }
        public IViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            private set
            {
                if (value != _currentViewModel)
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
