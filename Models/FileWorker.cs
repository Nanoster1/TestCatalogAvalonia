using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestCatalogAvalonia.Models
{
    public static class FileWorker
    {
        public static DirectoryInfo UserFolder
        {
            get
            {
                string path = $"{Environment.CurrentDirectory}\\Users\\{User.ActiveUser}";
                return Directory.CreateDirectory(path);
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
        public static DirectoryInfo WardrobeFolder
        {
            get
            {
                string path = $"{UserFolder}\\Wardrobe";
                return Directory.CreateDirectory(path);
            }
        }
        public static DirectoryInfo ApparellItemFolder(string name)
        { 
            string path = $"{WardrobeFolder}\\{name}";
            return Directory.CreateDirectory(path);
        }
        public static FileInfo TagsFile
        {
            get
            {
                var path = $"{UserFolder}\\Tags.txt";
                if (File.Exists(path))
                    return new FileInfo(path);
                File.Create(path).Close();
                var standartTags = new string[] { "!Outerwear", "Jackets", "Sweaters", "T-shirts", "Dresses", "!Underwear", "Pants", "Lingerie", "Jeans", "Skirts", "!Headwear", "Hats", "Caps", "!Accessories", "Bracelets", "Glasses", "Gloves", "Jewelry", "!Shoes" };
                File.WriteAllLines(path, standartTags);
                return new FileInfo(path);
            }
        }
        public static Task<string> CopyImageAsync(string startPath, string name)
        {
           return Task.Run(() =>
           {
               string folderPath = $"{ApparellItemFolder(name).FullName}";
               string endPath = $"{folderPath}\\{name}.{startPath.Split(".")[^1]}";
               File.Copy(startPath, endPath);
               return endPath;
           });
        }
    }
}
