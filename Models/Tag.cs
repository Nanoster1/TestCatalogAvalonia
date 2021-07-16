using System.Collections.ObjectModel;

namespace TestCatalogAvalonia.Models
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public ObservableCollection<string> Subtags { get; private set; } = new ObservableCollection<string>();
        public bool IsActive { get; set; } = false;
    }
}
