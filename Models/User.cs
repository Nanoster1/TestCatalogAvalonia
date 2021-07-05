using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace TestCatalogAvalonia.Models
{
    class User
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
            string jsonStr = JsonConvert.SerializeObject(new User(name));
            File.WriteAllText(FileWorker.UserFolder.FullName + "\\UserInfo.json", jsonStr);
        }
    }
}
