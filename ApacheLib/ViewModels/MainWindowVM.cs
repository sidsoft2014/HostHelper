﻿using ApacheLib.Interfaces;
using ApacheLib.Models;
using ApacheLib.Services;
using System.Collections.ObjectModel;
using System.Linq;
using static ApacheLib.SysSettings;

namespace ApacheLib.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<HostFileEntry> _hostFileEntries;
        private ObservableCollection<VirtualHost> _vHosts;
        private HostFileEntry _selectedHostFileEntry;
        private VirtualHost _selectedVirtualHost;
        private IObjectViewModel _currentViewModel;

        public MainWindowVM()
            : base()
        {
#if DEBUG
            CurrentViewModel = new VirtualHostVM();
#endif
        }

        public ObservableCollection<HostFileEntry> HostFileEntries
        {
            get
            {
                if (_hostFileEntries == null)
                {
                    var hosts = HFS.GetAllHosts();
                    _hostFileEntries = new ObservableCollection<HostFileEntry>(hosts);
                    _hostFileEntries.CollectionChanged += _collectionChanged;
                }
                return _hostFileEntries;
            }
            private set
            {
                if (value != _hostFileEntries)
                {
                    if(_hostFileEntries != null)
                        _hostFileEntries.CollectionChanged -= _collectionChanged;

                    _hostFileEntries = value;
                    OnPropertyChanged();

                    if (_hostFileEntries != null)
                        _hostFileEntries.CollectionChanged += _collectionChanged;
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
                    _vHosts.CollectionChanged += _collectionChanged;
                }
                return _vHosts;
            }
            private set
            {
                if (value != _vHosts)
                {
                    if(_vHosts != null)
                        _vHosts.CollectionChanged -= _collectionChanged;

                    _vHosts = value;
                    OnPropertyChanged();

                    if (_vHosts != null)
                        _vHosts.CollectionChanged += _collectionChanged;
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
                if (value != _selectedHostFileEntry)
                {
                    _selectedHostFileEntry = value;

                    if (value != null)
                    {
                        if (CurrentViewModel.GetType() != typeof(HostFileEntryVM))
                        {
                            _selectedVirtualHost = null;
                            CurrentViewModel = new HostFileEntryVM();
                        }
                        CurrentViewModel.SetSelectedObject(_selectedHostFileEntry);
                    }

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
                if (value != _selectedVirtualHost)
                {
                    _selectedVirtualHost = value;

                    if (value != null)
                    {
                        if (CurrentViewModel.GetType() != typeof(VirtualHostVM))
                        {
                            _selectedHostFileEntry = null;
                            CurrentViewModel = new VirtualHostVM();
                        }
                        CurrentViewModel.SetSelectedObject(_selectedVirtualHost);
                    }

                    OnPropertyChanged();
                }
            }
        }
        public IObjectViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            private set
            {
                if (value != _currentViewModel)
                {
                    if (_currentViewModel != null)
                        _currentViewModel.OnSaved -= _currentViewModel_OnSaved;

                    _currentViewModel = value;
                    OnPropertyChanged();

                    if (_currentViewModel != null)
                        _currentViewModel.OnSaved += _currentViewModel_OnSaved;
                }
            }
        }

        private void _collectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var list = sender as ObservableCollection<dynamic>;
            var obj = list?.FirstOrDefault();
            if (obj == null)
                return;

            string name = "";
            if (obj is VirtualHost)
                name = "VirtualHosts";
            else if (obj is HostFileEntry)
                name = "HostFileEntries";

            if (name != "")
                OnPropertyChanged(name);
        }

        private void _currentViewModel_OnSaved(object sender, System.EventArgs e)
        {
            if (CurrentViewModel is VirtualHostVM)
            {
                var vHost = ((VirtualHostVM)CurrentViewModel).SelectedVirtualHost;
                if (vHost == null)
                    return;

                var old = VirtualHosts.FirstOrDefault(p => p.Id == vHost.Id);

                if (old == null)
                    VirtualHosts.Add(vHost);
                else
                {
                    var idx = VirtualHosts.IndexOf(old);
                    VirtualHosts.Remove(old);
                    VirtualHosts.Insert(idx, vHost);
                }

                // This bypasses the INotify event.
                // Currently there doesn't seem much point firing the event, but this may change later.
                _selectedVirtualHost = vHost;
            }
            else if (CurrentViewModel is HostFileEntryVM)
            {
                var host = ((HostFileEntryVM)CurrentViewModel).CurrentHostFileEntry;
                if (host == null)
                    return;

                var old = HostFileEntries.FirstOrDefault(p => p.Id == host.Id);

                if (old == null)
                    HostFileEntries.Add(host);
                else
                {
                    var idx = HostFileEntries.IndexOf(old);
                    HostFileEntries.Remove(old);
                    HostFileEntries.Insert(idx, host);
                }

                // This bypasses the INotify event.
                // Currently there doesn't seem much point firing the event, but this may change later.
                _selectedHostFileEntry = host;
            }
        }
    }
}
