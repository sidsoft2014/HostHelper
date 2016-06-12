using ApacheLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Services
{
    public class ApacheSettings : IAppSettings
    {
        public string HostFilePath
        {
            get
            {
                return @"C:\Windows\System32\drivers\etc\hosts";
            }
        }
        public string VirtualHostsFilePath
        {
            get
            {
                return @"C:\xampp\apache\conf\extra\httpd-vhosts.conf";
            }
        }
    }
}
