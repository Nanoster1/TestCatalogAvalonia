using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestCatalogAvalonia.Models
{
    public class ApparelItem
    {
        public ApparelItem(string name = "NameNotFound")
        {
            Name = name;
        }


        private string name;
        [JsonProperty]
        public string Name { get { return name; } set { name = value; NameUpdate(value); } }


        public List<string> Tags { get; set; } = new List<string>();



        private string imageSource = "/Assets/ImageNotFound.jpg";
        public string ImageSource { get { return imageSource; } set { imageSource = value; ImageSourceUpdate(value); } }



        public event Action<string> ImageSourceUpdate = (string value) => { };

        public event Action<string> NameUpdate = (string value) => { };

        public static void SaveApparelItem(ApparelItem item, string oldName)
        {
            var json = JsonConvert.SerializeObject(item);
            File.WriteAllText($"{FileWorker.ApparellItemFolder(item.Name)}\\{item.Name}.json", json);
        }
        public static ApparelItem? GetApparelItem(string path)
        {
            return JsonConvert.DeserializeObject<ApparelItem>(File.ReadAllText(path));
        }
        public static void DeleteApparelItem(string name)
        {

            Directory.Delete(FileWorker.ApparellItemFolder(name).FullName, true);
        }
    }
}
