namespace HostHelper.Models
{
    public class HostFileEntry
    {
        private string _url;
        private string _ip;
        private bool _isActive;

        public HostFileEntry(string url, string ip, bool active)
        {
            Url = url;
            IP = ip;
            IsActive = active;
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
            private set
            {
                _url = value;
            }
        }
        public string IP
        {
            get
            {
                return _ip;
            }
            private set
            {
                _ip = value;
            }
        }

        public override string ToString()
        {
            return IsActive ? $"{IP}\t\t{Url}" : $"#{IP}\t\t{Url}";
        }
    }
}

