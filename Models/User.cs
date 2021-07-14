using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
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
            File.WriteAllText($"{Environment.CurrentDirectory}\\ActiveUser.txt", name);
            string jsonStr = JsonConvert.SerializeObject(new User(name));
            File.WriteAllText(FileWorker.UserFolder.FullName + "\\UserInfo.json", jsonStr);
        }
        public static string? ActiveUser 
        {
            get => File.ReadAllText($"{Environment.CurrentDirectory}\\ActiveUser.txt");
            set => File.WriteAllText($"{Environment.CurrentDirectory}\\ActiveUser.txt", value);
        }
    }
}
