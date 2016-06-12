using ApacheLib.Interfaces;
using ApacheLib.Models;
using ApacheLib.Services;
using System.Collections.ObjectModel;

namespace ApacheLib.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private VirtualHostService _vhs;
        private HostFileService _hfs;
        private ObservableCollection<HostFileEntry> _hostFileEntries;
        private ObservableCollection<VirtualHost> _vHosts;

        public MainWindowVM(IFileService fileService, IAppSettings appSettings)
        {
            _vhs = new VirtualHostService(fileService, appSettings);
            _hfs = new HostFileService(fileService, appSettings);
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
                    var hosts = _hfs.GetAllHosts();
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
                    var hosts = _vhs.GetVirtualHosts();
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
    }
}
