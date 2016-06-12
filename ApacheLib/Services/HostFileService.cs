using ApacheLib.Interfaces;
using ApacheLib.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ApacheLib.Services
{
    public class HostFileService
    {
        private IFileService FileService;
        private const string _regexHost = @"(([#\s]+)([\d]+\.[\d]+\.[\d]+\.[\d]+)[\W]+([\S]+))|(([#\s]+)(::[\d])[\W]+([\S]+))";


        public HostFileService(IFileService fileService)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");
            this.FileService = fileService;
        }

        public List<HostFileEntry> GetAllHosts()
        {
            if (!FileService.FileExists(AppSettings.HostFilePath))
                return null;

            var text = FileService.ReadAllText(AppSettings.HostFilePath);
            var matches = Regex.Matches(text, _regexHost);
            if (matches.Count == 0)
                return null;

            var results = new List<HostFileEntry>();
            foreach (Match match in matches)
            {
                string url;
                string ip;
                bool active;
                if (!string.IsNullOrEmpty(match.Groups[3].Value))
                {
                    active = !match.Groups[2].Value.Contains("#");
                    ip = match?.Groups[3]?.Value?.Trim();
                    url = match?.Groups[4]?.Value?.Trim();
                }
                else if (!string.IsNullOrEmpty(match.Groups[7].Value))
                {
                    active = !match.Groups[6].Value.Contains("#");
                    ip = match?.Groups[7]?.Value?.Trim();
                    url = match?.Groups[8]?.Value?.Trim();
                }
                else
                {
                    throw new Exception("Could not parse host file entry: " + match.Value);
                }

                if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(ip))
                {
                    HostFileEntry entry = new HostFileEntry(url, ip, active);
                    results.Add(entry);
                }
            }
            return results;
        }
    }
}
