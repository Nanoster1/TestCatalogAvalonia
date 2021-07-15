using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
