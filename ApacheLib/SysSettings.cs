using ApacheLib.Interfaces;
using ApacheLib.Services;
using System;

namespace ApacheLib
{
    /// <summary>
    /// Sets the system specific services and initialises the library.
    /// This should be run before any other classes are initialised.
    /// </summary>
    public static class SysSettings
    {
        internal static IAppSettings AppSettings { get; private set; }
        internal static IFileService FileService { get; private set; }
        internal static VirtualHostService VHS;
        internal static HostFileService HFS;

        /// <summary>
        /// Set system specific services and initialise the library.
        /// </summary>
        /// <param name="fileService">The system specfic implementation of the IFileService</param>
        /// <param name="appSettings">The system specfic implementation of IAppSettings</param>
        public static void Init(IFileService fileService, IAppSettings appSettings)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");
            else if (appSettings == null)
                throw new ArgumentNullException("appSettings");

            FileService = fileService;
            AppSettings = appSettings;

            VHS = new VirtualHostService();
            HFS = new HostFileService();
        }
    }
}
