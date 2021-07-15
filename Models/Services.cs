using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Models
{
    public static class Services
    {
        public static Task<List<ApparelItem>> GetApparelItemsAsync()
        {
            return new Task<List<ApparelItem>>(GetAllApparelItems);
        }
        public static List<ApparelItem> GetAllApparelItems()
        {
            List<ApparelItem> apparelItems = new List<ApparelItem>();
            var folders = FileWorker.WardrobeFolder.GetDirectories();
            for (int i = 0; i < folders.Length; i++)
            {
                apparelItems.Add(ApparelItem.GetApparelItem(folders[i].GetFiles().FirstOrDefault(x => x.FullName.Contains(".json")).FullName));
            }
            return apparelItems;
        }
        public static User GetUser()
        {
            string jsonStr = File.ReadAllText(FileWorker.UserInfo.FullName);
            var user = JsonConvert.DeserializeObject<User>(jsonStr);
            return user;
        }
        public static List<Tag> GetAllTags()
        {
            var tags = File.ReadAllLines(FileWorker.TagsFile.FullName);
            var Tags = new List<Tag>();
            foreach(var tag in tags)
            {
                if (tag.Contains("!")) { Tags.Add(new Tag(tag.Replace("!", ""))); }
                else { Tags[^1].Subtags.Add(tag); }
            }
            return Tags;
        }
    }
}
