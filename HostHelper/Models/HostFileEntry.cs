namespace HostHelper.Models
{
    public class HostFileEntry
    {
        private string _url;
        private string _ip;

        public HostFileEntry(string url, string ip)
        {
            Url = url;
            IP = ip;
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
    }
}

