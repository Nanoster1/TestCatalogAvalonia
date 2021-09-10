using DynamicData;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Models
{
    public class ItemSet: ISaved
    {
        [Reactive] public string Name { get; set; } = string.Empty;

        [Reactive, JsonProperty] public List<string> ImageSources { get; set; } = new();

        [JsonProperty] public List<string> ItemNames { get; private set; } = new();

        [JsonIgnore] public SourceCache<ApparelItem, string> Items { get; private set; } = new(x => x.Name);

        public void FillItems(IEnumerable<ApparelItem> allItems)
        {
            foreach (var name in ItemNames)
            {
                if (allItems.All(item => item.Name != name))
                {
                    ItemNames.Remove(name);
                    continue;
                }
                Items.AddOrUpdate(allItems.First(item => item.Name == name));
            }
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            var path = Path.Combine(FileWorker.GetItemSetFolder(Name).FullName, $"{Name}.json");
            File.WriteAllText(path, json);
        }
    }
}
