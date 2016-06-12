using System;

namespace ApacheLib.Interfaces
{
    public interface IFileService
    {
        bool FileExists(string path);
        bool DirectoryExists(string path);
        string ReadAllText(string path);
        string[] ReadAllLines(string path);
    }
}
