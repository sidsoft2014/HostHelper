using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApacheLib.Interfaces
{
    public interface IObjectViewModel
    {
        void SetSelectedObject(object obj);
        void SetAssociatedObject(object obj);
        void Save();

        event EventHandler OnSaved;
    }
}
