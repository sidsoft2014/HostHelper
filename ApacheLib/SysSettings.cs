using ApacheLib.Interfaces;
using System;

namespace ApacheLib
{
    public static class SysSettings
    {
        public static IAppSettings AppSettings { get; private set; }
        public static IFileService FileService { get; private set; }

        public static void Init(IFileService fileService, IAppSettings appSettings)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");
            else if (appSettings == null)
                throw new ArgumentNullException("appSettings");

            FileService = fileService;
            AppSettings = appSettings;

        }
    }
}
