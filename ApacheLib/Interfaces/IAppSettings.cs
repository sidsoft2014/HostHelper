using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApacheLib.Interfaces
{
    public interface IAppSettings
    {
        string HostFilePath { get; }
        string VirtualHostsFilePath { get; }
    }
}
