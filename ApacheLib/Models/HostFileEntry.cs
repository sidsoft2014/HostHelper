using System;

namespace ApacheLib.Models
{
    public class HostFileEntry
    {
        private string _url;
        private string _ip;
        private bool _isActive;

        public HostFileEntry()
        {
            Id = Guid.NewGuid();
        }
        public HostFileEntry(string url, string ip, bool active)
            : this()
        {
            Url = url;
            IP = ip;
            IsActive = active;
        }

        public Guid Id { get; private set; }
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
            set
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
            set
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

