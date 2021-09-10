using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;

namespace TestCatalogAvalonia.Views.Components
{
    public class ItemSetComponentViewModel: ViewModelBase
    {
        public ItemSetComponentViewModel(ItemSet selectedSet, bool isNewSet)
        {
            ItemSet = selectedSet;

            IsEditorMode = isNewSet;

            ChangeViewingMode = ReactiveCommand.Create(() => 
            {
                IsEditorMode = !IsEditorMode;
            });
        }
        [Reactive] public ItemSet ItemSet { get; set; }
        public IReactiveCommand ChangeViewingMode { get; private set; }
        [Reactive] public bool IsEditorMode { get; private set; } 
    }
}
