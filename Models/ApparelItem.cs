using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using TestCatalogAvalonia.Converters;

namespace TestCatalogAvalonia.Models
{
    public class ApparelItem: ReactiveObject, ICloneable<ApparelItem>
    {
        [Reactive, JsonProperty] public string Name { get; set; } = string.Empty;

        [JsonProperty] public List<string> Tags { get; set; } = new List<string>();

        [Reactive, JsonProperty] public string ImageSource { get; set; } = "/Assets/ImageNotFound.jpg";

        public void Save()
        {
            var imagePath = $"{FileWorker.GetApparellItemFolder(Name)}\\{Name}.png";
            new Bitmap(ImageSource).Save(imagePath);
            ImageSource = imagePath.TrimStart(Environment.CurrentDirectory.ToCharArray());
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText($"{FileWorker.GetApparellItemFolder(Name)}\\{Name}.json", json);
        }

        public static ApparelItem? GetApparelItem(string path) => JsonConvert.DeserializeObject<ApparelItem>(File.ReadAllText(path));

        public ApparelItem Clone()
        {
            return new() 
            {
                Name = Name,
                Tags = Tags,
                ImageSource = ImageSource
            };
        }
    }
}
