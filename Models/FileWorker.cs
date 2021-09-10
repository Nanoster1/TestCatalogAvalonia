using System;
using System.IO;
using System.Linq;

namespace TestCatalogAvalonia.Models
{
    public static class FileWorker
    {
        public static DirectoryInfo AllUsersFolder
        {
            get
            {
                string path = Path.Combine(Environment.CurrentDirectory, "Users");
                return Directory.CreateDirectory(path);
            }
        }
        public static DirectoryInfo UserFolder
        {
            get
            {
                string path = Path.Combine(AllUsersFolder.FullName, User.ActiveUser.Name);
                return Directory.CreateDirectory(path);
            }
        }
        public static DirectoryInfo WardrobeFolder
        {
            get
            {
                string path = Path.Combine(UserFolder.FullName, "Wardrobe");
                return Directory.CreateDirectory(path);
            }
        }
        public static DirectoryInfo ItemSetsFolder
        {
            get
            {
                string path = Path.Combine(UserFolder.FullName, "ItemSets");
                return Directory.CreateDirectory(path);
            }
        }
        public static DirectoryInfo GetItemSetFolder(string name)
        {
            string path = Path.Combine(ItemSetsFolder.FullName, name);
            return Directory.CreateDirectory(path);
        }
        public static DirectoryInfo GetApparellItemFolder(string name)
        {
            string path = Path.Combine(WardrobeFolder.FullName, name);
            return Directory.CreateDirectory(path);
        }
        public static FileInfo? UserInfo
        {
            get
            {
                string path = Path.Combine(UserFolder.FullName, "UserInfo.json");
                if (File.Exists(path))
                    return new FileInfo(path);
                throw new Exception("User not found");
            }
        }
        public static FileInfo TagsFile
        {
            get
            {
                var path = Path.Combine(UserFolder.FullName, "Tags.txt");
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    var standartTags = new string[]
                    {
                    "!Outerwear", "Jackets", "Sweaters", "T-shirts", "Dresses",
                    "!Underwear", "Pants", "Lingerie", "Jeans", "Skirts",
                    "!Headwear", "Hats", "Caps",
                    "!Accessories", "Bracelets", "Glasses", "Gloves", "Jewelry",
                    "!Shoes", "Sneakers", "Boots"};
                    File.WriteAllLines(path, standartTags);
                }
                return new FileInfo(path);
            }
        }
    }
}
