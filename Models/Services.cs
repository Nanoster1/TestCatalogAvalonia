using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Models
{
    public static class Services
    {
        public static Task<List<ApparelItem>> GetApparelItemsAsync()
        {
            return new Task<List<ApparelItem>>(GetAllApparelItems);
        }
        /// <summary>
        /// This method is only used in the main window to get all items in SourceCache, 
        /// then pass _allItems where needed, 
        /// it will be more efficient than this method and will update items everywhere
        /// </summary>
        /// <returns>Returns all items directly from files</returns>
        public static List<ApparelItem> GetAllApparelItems()
        {
            var apparelItems = new List<ApparelItem>();
            var folders = FileWorker.WardrobeFolder.GetDirectories();
            foreach(var folder in folders)
            {
                apparelItems.Add(ApparelItem.GetApparelItem(folder?.GetFiles().First(x => x.FullName.Contains(".json"))?.FullName));
            }
            return apparelItems;
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>All users from their folders</returns>
        public static IEnumerable<User> GetUsers()
        {
            var userFiles = FileWorker.AllUsersFolder.GetDirectories().SelectMany(x => x.GetFiles("UserInfo.json"));
            var users = userFiles
                .Select(x => File.ReadAllText(x.FullName))
                .Select(x => JsonConvert.DeserializeObject<User>(x))
                .Where(x => x != null);
            return users;
        }

        /// <summary>
        /// This method is only used in the main window to get all items in SourceCache, 
        /// then pass _allTags where needed, 
        /// it will be more efficient than this method and will update items everywhere
        /// </summary>
        /// <returns>Returns all tags directly from files</returns>
        public static List<Tag> GetAllTags()
        {
            var tags = File.ReadAllLines(FileWorker.TagsFile.FullName);
            var Tags = new List<Tag>();
            foreach (var tag in tags)
            {
                if (tag.Contains("!")) { Tags.Add(new Tag(tag.Replace("!", ""))); }
                else { Tags[^1].Subtags.Add(tag); }
            }
            return Tags;
        }
    }
}
