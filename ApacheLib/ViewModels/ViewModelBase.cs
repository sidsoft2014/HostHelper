using ApacheLib.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static ApacheLib.SysSettings;

namespace ApacheLib.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public ViewModelBase()
        {
#if DEBUG
            /// This gets rid of anoying error in UI.WPF app.xaml
            return;
#endif
            if (FileService == null || AppSettings == null)
            {
                throw new Exception("Apache lib not configured correctly. Ensure SysSettings.Init has run successfuly.");
            }
            
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #region IDisposable Support
        private bool isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    PropertyChanged = null;
                }

                isDisposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
