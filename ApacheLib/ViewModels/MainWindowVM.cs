using ApacheLib.Interfaces;
using ApacheLib.Models;
using ApacheLib.Services;
using System.Collections.ObjectModel;

namespace ApacheLib.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private VirtualHostService _vhs;
        private ObservableCollection<HostFileEntry> _hostFileEntries;
        private ObservableCollection<VirtualHost> _vHosts;

        public MainWindowVM(IFileService fileService)
        {
            _vhs = new VirtualHostService(fileService);
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
                    var hosts = _vhs.GetVirtualHosts();
                    _vHosts = new ObservableCollection<VirtualHost>(hosts);
                }
                return _vHosts;
            }
        }
    }
}
