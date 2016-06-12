using ApacheLib.Interfaces;
using ApacheLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ApacheLib.Services
{
    internal class VirtualHostService
    {
        private IFileService FileService;
        private IAppSettings AppSettings;
        private const string RegexHost = @"(#?#?)<VirtualHost ([\S]*)>";
        private const string RegexServerAdmin = @"(#?#?)ServerAdmin ([\S]*)";
        private const string RegexDocumentRoot = "(#?#?)DocumentRoot \"?([\\S]+)\"?";
        private const string RegexServerName = @"(#?#?)ServerName ([\S]*)";
        private const string RegexServerAlias = @"(#?#?)ServerAlias ([\S]*)";
        private const string RegexErrorLog = "(#?#?)ErrorLog \"([\\S]*)\"";
        private const string RegexCustomLog = "(#?#?)CustomLog \"([\\S]*)\"";

        public VirtualHostService(IFileService fileService, IAppSettings appSettings)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");
            else if (appSettings == null)
                throw new ArgumentNullException("appSettings");

            this.FileService = fileService;
            this.AppSettings = appSettings;
        }

        public List<string> GetVirtualHostTextBlocks()
        {
            if (!FileService.FileExists(AppSettings.VirtualHostsFilePath))
                throw new FileNotFoundException("Could not find virtual host file at " + AppSettings.VirtualHostsFilePath);

            var text = FileService.ReadAllLines(AppSettings.VirtualHostsFilePath);
            var blocks = new List<string>();
            for (int ii = 0; ii < text.Length; ii++)
            {
                if (text[ii].Contains("<VirtualHost "))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!text[ii].Contains("</VirtualHost>"))
                    {
                        sb.AppendLine(StandardiseLine(text[ii]));
                        ii++;
                    }
                    sb.AppendLine(StandardiseLine(text[ii]));
                    blocks.Add(sb.ToString());
                }
            }
            return blocks;
        }

        public List<VirtualHost> GetVirtualHosts()
        {
            var blocks = new List<VirtualHost>();
            foreach (var block in GetVirtualHostTextBlocks())
            {
                blocks.Add(ConvertToVirtualHost(block));
            }
            return blocks;
        }

        public VirtualHost ConvertToVirtualHost(string input)
        {
            // Use regex to get try and get essential details. Massive use of Elvis opperator here.
            var active = Regex.Match(input, RegexHost)?.Groups[1]?.Value?.Trim() != "##";
            var host = Regex.Match(input, RegexHost)?.Groups[2]?.Value?.Trim().Split(':'); // Split into name and port.
            var serverName = Regex.Match(input, RegexServerName)?.Groups[2]?.Value?.Trim();
            var docRoot = Regex.Match(input, RegexDocumentRoot)?.Groups[2]?.Value?.Trim().Replace("\"",string.Empty);

            // Check we have required fields.
            if (string.IsNullOrEmpty(host[0]) || string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(docRoot))
                throw new ArgumentNullException("Required parameters not set");

            var hostName = host[0];
            // Create new VirtualHost now we have minimum fields.
            var vHost = new VirtualHost(hostName, serverName, docRoot, active);

            // Add any port we may have removed.
            int port;
            if (host.Length == 2 && int.TryParse(host[1], out port))
                vHost.Port = port;

            // Check for extra fields and add them if present.
            var serAdmin = Regex.Match(input, RegexServerAdmin)?.Groups[2]?.Value?.Trim();
            if (!string.IsNullOrEmpty(serAdmin))
                vHost.ServerAdmin = serAdmin;
            var serAlias = Regex.Match(input, RegexServerAlias)?.Groups[2]?.Value?.Trim();
            if (!string.IsNullOrEmpty(serAlias))
                vHost.ServerAlias = serAlias;
            var eLog = Regex.Match(input, RegexErrorLog)?.Groups[2]?.Value?.Trim();
            if (!string.IsNullOrEmpty(eLog))
                vHost.ErrorLog = eLog;
            var cLog = Regex.Match(input, RegexCustomLog)?.Groups[2]?.Value?.Trim();
            if (!string.IsNullOrEmpty(cLog))
                vHost.CustomLog = cLog;

            return vHost;
        }

        private string StandardiseLine(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var line = input
                .Replace("    ", "\t")
                .Replace("   ", "\t")
                .Replace("  ", " ")
                .Trim(new[] { ' ' });

            return line;
        }
    }
}
