using ApacheLib.Interfaces;
using ApacheLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApacheLib.ViewModels
{
    public class HostFileEntryVM : ViewModelBase, IObjectViewModel
    {
        private HostFileEntry _currentHostFileEntry;
        private string _url;
        private List<string> _ipParts = new List<string>(4) { "", "", "", "" };
        private string _ipDelimiter = ".";
        private VirtualHost _associatedVirtualHost;

        public event EventHandler OnSaved;
        
        public HostFileEntry CurrentHostFileEntry
        {
            get
            {
                return _currentHostFileEntry;
            }
            private set
            {
                if(value != _currentHostFileEntry)
                {
                    _currentHostFileEntry = value;
                    OnPropertyChanged();
                    HostFileToView();
                }
            }
        }
        public VirtualHost AssociatedVirtualHost
        {
            get
            {
                return _associatedVirtualHost;
            }
            set
            {
                if(value != _associatedVirtualHost)
                {
                    _associatedVirtualHost = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> IPParts
        {
            get
            {
                if (_ipParts == null)
                    _ipParts = new List<string>(4);
                return _ipParts;
            }
            set
            {
                if(value != _ipParts)
                {
                    _ipParts = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if(value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }
        public string IPDelimiter
        {
            get
            {
                return _ipDelimiter;
            }
            private set
            {
                if(value != _ipDelimiter)
                {
                    _ipDelimiter = value;
                    OnPropertyChanged();
                }
            }
        }

        private void HostFileToView()
        {
            if (CurrentHostFileEntry == null)
                return;

            string[] parts;
            if (CurrentHostFileEntry.IP.Contains(":"))
            {
                IPDelimiter = ":";
                parts = CurrentHostFileEntry.IP.Split(':');
            }
            else if (CurrentHostFileEntry.IP.Contains("."))
            {
                IPDelimiter = ".";
                parts = CurrentHostFileEntry.IP.Split('.');
            }
            else return;

            var segments = new List<string>(4) { "", "", "", "" };
            for (int ii = 0; ii < parts.Length; ii++)
            {
                segments[ii] = parts[ii];
            }
            IPParts = segments;

            Url = CurrentHostFileEntry.Url;
        }
        private void ViewToHostFile()
        {
            if (CurrentHostFileEntry == null)
                return;
            
            CurrentHostFileEntry.Url = Url;
        }

        public void Save()
        {
            if (CurrentHostFileEntry == null)
                return;

            ViewToHostFile();
            OnSaved?.Invoke(this, null);
        }
        public void SetSelectedObject(object obj)
        {
            var host = obj as HostFileEntry;
            if (host == null)
                return;
            CurrentHostFileEntry = host;
        }
        public void SetAssociatedObject(object obj)
        {
            if (obj == null)
            {
                AssociatedVirtualHost = null;
                return;
            }

            var vhost = obj as VirtualHost;
            if (vhost == null)
                return;
            AssociatedVirtualHost = vhost;
        }
    }
}
