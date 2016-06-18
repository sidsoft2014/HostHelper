using ApacheLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.WPF.Commands
{
    internal sealed class LaunchSubUnit : ICommand
    {
        public LaunchSubUnit(IViewModel vm)
        {
            this._vm = vm;
        }

        private IViewModel _vm;

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _vm.SaveSelected();
        }
    }
}
