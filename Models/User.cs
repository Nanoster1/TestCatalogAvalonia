using Newtonsoft.Json;
using System;
using System.IO;
namespace TestCatalogAvalonia.Models
{
    public class User
    {
        public User(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public static User GetUser()
        {
            string jsonStr = File.ReadAllText(FileWorker.UserInfo.FullName);
            var user = JsonConvert.DeserializeObject<User>(jsonStr);
            return user;
        }
        public static void SaveUser(string name)
        {
            ActiveUser = new User(name);
            string jsonStr = JsonConvert.SerializeObject(new User(name));
            File.WriteAllText(FileWorker.UserFolder.FullName + "\\UserInfo.json", jsonStr);
        }
        public static User ActiveUser { get; set; } = new(string.Empty);
    }
}
