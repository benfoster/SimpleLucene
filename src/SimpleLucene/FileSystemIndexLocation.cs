using System;
using System.IO;

namespace SimpleLucene
{
    /// <summary>
    /// Represents a file sytem based index location
    /// </summary>
    public class FileSystemIndexLocation : BaseIndexLocation
    {
        private readonly DirectoryInfo directoryInfo;

        public FileSystemIndexLocation(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null)
            {
                throw new ArgumentNullException("directoryInfo");
            }

            this.directoryInfo = directoryInfo;
        }

        public override string GetPath()
        {
            return directoryInfo.FullName;
        }
    }
}
