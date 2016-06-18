using System.Text;

namespace ApacheLib.Models
{
    public class VirtualHost
    {
        private string _virtualHostName;
        private int _port;
        private string _serverAdmin;
        private string _documentRoot;
        private string _serverName;
        private string _serverAlias;
        private string _errorLog;
        private string _customLog;
        private bool _isActive;

        public VirtualHost()
        {
            this.ServerName = this.VirtualHostName = "New";
            IsActive = true;
        }
        public VirtualHost(string hostName, string serverName, string documentRoot, bool isActive)
        {
            VirtualHostName = hostName;
            ServerName = serverName;
            DocumentRoot = documentRoot;
            IsActive = isActive;
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
        public string VirtualHostName
        {
            get
            {
                return _virtualHostName;
            }
            set
            {
                _virtualHostName = value;
            }
        }
        public int Port
        {
            get
            {
                if (_port < 1)
                    return 80;
                return _port;
            }
            set
            {
                _port = value;
            }
        }
        public string ServerAdmin
        {
            get
            {
                return _serverAdmin;
            }
            set
            {
                _serverAdmin = value;
            }
        }
        public string DocumentRoot
        {
            get
            {
                return _documentRoot;
            }
            set
            {
                _documentRoot = value;
            }
        }
        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
            }
        }
        public string ServerAlias
        {
            get
            {
                return _serverAlias;
            }
            set
            {
                _serverAlias = value;
            }
        }
        public string ErrorLog
        {
            get
            {
                return _errorLog;
            }
            set
            {
                _errorLog = value;
            }
        }
        public string CustomLog
        {
            get
            {
                return _customLog;
            }
            set
            {
                _customLog = value;
            }
        }

        public string GenerateText(bool addNameVirtualHost = false)
        {
            var sb = new StringBuilder();
            string line = "";

            // Add NameVirtualHost line if requsted.
            if (addNameVirtualHost)
            {
                line = IsActive ?
                    $"NameVirtualHost {VirtualHostName}"
                    : $"##NameVirtualHost {VirtualHostName}";
                sb.AppendLine(line);
            }

            // Add VirtualHost opening tag, VirtualHostName and port if required.
            if (Port < 1)
            {
                line = IsActive ?
                    $"<VirtualHost {VirtualHostName}>"
                    : $"##<VirtualHost {VirtualHostName}>";
                sb.AppendLine(line);
            }
            else
            {
                line = IsActive ?
                    $"<VirtualHost {VirtualHostName}:{Port}>"
                    : $"##<VirtualHost {VirtualHostName}:{Port}>";
                sb.AppendLine(line);
            }

            // Add ServerAdmin if required
            if (!string.IsNullOrEmpty(ServerAdmin))
            {
                line = IsActive ?
                    $"\tServerAdmin {ServerAdmin}"
                    : $"\t##ServerAdmin {ServerAdmin}";
                sb.AppendLine(line);
            }

            // Add DocumentRoot
            line = IsActive ?
                $"\tDocumentRoot \"{DocumentRoot}\""
                : $"\t##DocumentRoot \"{DocumentRoot}\"";
            sb.AppendLine(line);

            // Add ServerName
            line = IsActive ?
                $"\tServerName {ServerName}"
                : $"\t##ServerName {ServerName}";
            sb.AppendLine(line);

            // Add ServerAlias if required
            if (!string.IsNullOrEmpty(ServerAlias))
            {
                line = IsActive ?
                    $"\tServerAlias {ServerAlias}"
                    : $"\t##ServerAlias {ServerAlias}";
                sb.AppendLine(line);
            }

            // Add ErrorLog if required
            if (!string.IsNullOrEmpty(ErrorLog))
            {
                line = IsActive ?
                    $"\tErrorLog \"{ErrorLog}\""
                    : $"\t##ErrorLog \"{ErrorLog}\"";
                sb.AppendLine(line);
            }

            // Add CustomLog if required
            if (!string.IsNullOrEmpty(CustomLog))
            {
                line = IsActive ?
                    $"\tCustomLog \"{CustomLog}\" common"
                    : $"\t##CustomLog \"{CustomLog}\" common";
                sb.AppendLine(line);
            }

            // Add VirtualHost closing tag.
            line = IsActive ?
                "</VirtualHost>"
                : "##</VirtualHost>";
            sb.AppendLine(line);

            return sb.ToString();
        }
    }
}
