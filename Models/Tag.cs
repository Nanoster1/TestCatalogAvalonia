using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Models
{
    public class Tag: ReactiveObject
    {
        public Tag(string name) { Name = name; }

        [Reactive] public string Name { get; private set; }
        [Reactive] public List<string> Subtags { get; private set; } = new();
        [Reactive] public bool IsActive { get; set; }

        public static async Task<IEnumerable<string>> ConvertToStrings(IEnumerable<Tag> tags)
        {
            return await Task.Run(() =>
            {
                List<string> tagsStr = new(); 
                foreach (var tag in tags)
                {
                    tagsStr.Add($"!{tag.Name}");
                    tagsStr.AddRange(tag.Subtags);
                }
                return tagsStr;
            });
        }

        public static async void SaveTags(IEnumerable<Tag> tags)
        {
            var tagsStr = await ConvertToStrings(tags);
            await File.WriteAllLinesAsync(FileWorker.TagsFile.FullName, tagsStr);
        }
    }
}
