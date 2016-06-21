using ApacheLib.Interfaces;
using ApacheLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApacheLib.ViewModels
{
    public class VirtualHostVM : ViewModelBase, IObjectViewModel
    {
        private VirtualHost _selectedVirtualHost;
        private bool _isActive;
        private string _virtualHostName;
        private int _port;
        private string _serverAdmin;
        private string _documentRoot;
        private string _serverName;
        private string _serverAlias;
        private string _errorLog;
        private string _customLog;

        public event EventHandler OnSaved;

        public VirtualHostVM()
        {

        }

        public VirtualHost SelectedVirtualHost
        {
            get
            {
                return _selectedVirtualHost;
            }
            private set
            {
                if (value != _selectedVirtualHost)
                {
                    _selectedVirtualHost = value;
                    ModelToView();
                    OnPropertyChanged();
                }
            }
        }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    OnPropertyChanged();
                }
            }
        }
        public string VirtualHostName
        {
            get
            {
                return _virtualHostName;
            }
            set
            {
                if (value != _virtualHostName)
                {
                    _virtualHostName = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Port
        {
            get
            {
                if (_port < 1)
                    return 80;
                return _port;
            }
            set
            {
                if (value != _port)
                {
                    _port = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ServerAdmin
        {
            get
            {
                return _serverAdmin;
            }
            set
            {
                if (value != _serverAdmin)
                {
                    _serverAdmin = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DocumentRoot
        {
            get
            {
                return _documentRoot;
            }
            set
            {
                if (value != _documentRoot)
                {
                    _documentRoot = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                if (value != _serverName)
                {
                    _serverName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ServerAlias
        {
            get
            {
                return _serverAlias;
            }
            set
            {
                if (value != _serverAlias)
                {
                    _serverAlias = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ErrorLog
        {
            get
            {
                return _errorLog;
            }
            set
            {
                if (value != _errorLog)
                {
                    _errorLog = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CustomLog
        {
            get
            {
                return _customLog;
            }
            set
            {
                if (value != _customLog)
                {
                    _customLog = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ModelToView()
        {
            if (SelectedVirtualHost == null)
                return;

            IsActive = SelectedVirtualHost.IsActive;
            VirtualHostName = SelectedVirtualHost.VirtualHostName;
            Port = SelectedVirtualHost.Port;
            ServerAdmin = SelectedVirtualHost.ServerAdmin;
            DocumentRoot = SelectedVirtualHost.DocumentRoot;
            ServerName = SelectedVirtualHost.ServerName;
            ServerAlias = SelectedVirtualHost.ServerAlias;
            ErrorLog = SelectedVirtualHost.ErrorLog;
            CustomLog = SelectedVirtualHost.CustomLog;
        }
        private void ViewToModel()
        {
            if (SelectedVirtualHost == null)
                return;

            SelectedVirtualHost.IsActive = IsActive;
            SelectedVirtualHost.VirtualHostName = VirtualHostName;
            SelectedVirtualHost.Port = Port;
            SelectedVirtualHost.ServerAdmin = ServerAdmin;
            SelectedVirtualHost.DocumentRoot = DocumentRoot;
            SelectedVirtualHost.ServerName = ServerName;
            SelectedVirtualHost.ServerAlias = ServerAlias;
            SelectedVirtualHost.ErrorLog = ErrorLog;
            SelectedVirtualHost.CustomLog = CustomLog;
        }

        public void SetSelectedObject(object obj)
        {
            var vHost = obj as VirtualHost;
            if (obj == null)
                return;

            SelectedVirtualHost = vHost;
        }
        public void SetAssociatedObject(object obj)
        {
            //var vhost = obj as VirtualHost;
            //if (vhost == null)
            //    return;
            //AssociatedVirtualHost = vhost;
        }
        public void Save()
        {
            ViewToModel();
            OnSaved?.Invoke(this, null);
        }
    }
}
