using HostHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HostHelper.Services
{
    public static class HostFileService
    {
        private const string _regexHost = @"(([#\s]+)([\d]+\.[\d]+\.[\d]+\.[\d]+)[\W]+([\S]+))|(([#\s]+)(::[\d])[\W]+([\S]+))";
        public static List<HostFileEntry> GetAllHosts()
        {
            if (!File.Exists(Properties.Settings.Default.HostFile))
                return null;

            var text = File.ReadAllText(Properties.Settings.Default.HostFile);
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
