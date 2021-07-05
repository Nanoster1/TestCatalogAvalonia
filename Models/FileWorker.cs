using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestCatalogAvalonia.Models
{
    static class FileWorker
    {
        public static DirectoryInfo UserFolder
        {
            get
            {
                string path = $"{Environment.CurrentDirectory}\\User";
                if (Directory.Exists(path))
                    return new DirectoryInfo(path);
                Directory.CreateDirectory(path);
                return new DirectoryInfo(path);
            }
        }
        public static FileInfo? UserInfo
        {
            get
            {
                string path = $"{UserFolder.FullName}\\UserInfo.json";
                if (File.Exists(path))
                    return new FileInfo(path);
                throw new Exception("User not found");
            }
        }
    }
}
