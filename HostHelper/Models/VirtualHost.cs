using System.Text;

namespace HostHelper.Models
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

        public VirtualHost(string hostName, string serverName, string documentRoot)
        {
            VirtualHostName = hostName;
            ServerName = serverName;
            DocumentRoot = documentRoot;
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

        public const string RegexHost = @"<VirtualHost ([\S]*)>";
        public const string RegexServerAdmin = @"ServerAdmin ([\S]*)";
        public const string RegexDocumentRoot = "DocumentRoot \"([\\S]*)\"";
        public const string RegexServerName = @"ServerName ([\S]*)";
        public const string RegexServerAlias = @"ServerAlias ([\S]*)";
        public const string RegexErrorLog = "ErrorLog \"([\\S]*)\"";
        public const string RegexCustomLog = "CustomLog \"([\\S]*)\"";

        public string GenerateText()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"NameVirtualHost {VirtualHostName}");
            if (Port < 1)
                sb.AppendLine($"<VirtualHost {VirtualHostName}>");
            else
                sb.AppendLine($"<VirtualHost {VirtualHostName}:{Port}>");

            if (!string.IsNullOrEmpty(ServerAdmin))
                sb.AppendLine($"ServerAdmin {ServerAdmin}");

            sb.AppendLine($"DocumentRoot \"{DocumentRoot}\"");
            sb.AppendLine($"ServerName {ServerName}");

            if (!string.IsNullOrEmpty(ServerAlias))
                sb.AppendLine($"ServerAlias {ServerAlias}");
            if (!string.IsNullOrEmpty(ErrorLog))
                sb.AppendLine($"ErrorLog \"{ErrorLog}\"");
            if (!string.IsNullOrEmpty(CustomLog))
                sb.AppendLine($"CustomLog \"{CustomLog}\" common");

            sb.AppendLine($"</VirtualHost>");
            return sb.ToString();
        }



    }
}
