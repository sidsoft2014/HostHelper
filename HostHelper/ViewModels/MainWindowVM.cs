using HostHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostHelper.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        private string[] _hostFileText = new[] { "Here is hosts text", "Here is hosts text again", "And again here is hosts text again" };
        private string[] _vHosts = new[] { "V Host 1", "V host 2", "www.hdsjdhaskjd.ddhsajdak.sadjhaskdhaskdj.uk.net.com" };

        public MainWindowVM()
        {
            var vhosts = VirtualHostService.GetVirtualHosts();
            var x = 0;
        }

        public string Name
        {
            get { return "Some name"; }
        }
        public string[] HostFileText
        {
            get
            {
                return _hostFileText;
            }
            private set
            {
                if (_hostFileText != value)
                {
                    _hostFileText = value;
                    OnPropertyChanged();
                }
            }
        }
        public string[] VirtualHosts
        {
            get
            {
                return _vHosts;
            }
            private set
            {
                if (_vHosts != value)
                {
                    _vHosts = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
