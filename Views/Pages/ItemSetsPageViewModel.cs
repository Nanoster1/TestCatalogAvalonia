using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using DynamicData;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive.Linq;
using DynamicData.Binding;
using System.Reactive.Disposables;

namespace TestCatalogAvalonia.Views.Pages
{
    public class ItemSetsPageViewModel: ViewModelBase, IActivatableViewModel
    {
        private SourceCache<ItemSet, string> _allItemSets;
        private ReadOnlyObservableCollection<ItemSet> _activeItemSets;

        public ItemSetsPageViewModel(SourceCache<ItemSet, string> allItemSets)
        {
            _allItemSets = allItemSets;

            this.WhenActivated(disposables => 
            {
                var filterSearch = this.WhenValueChanged(x => x.SearchString)
                    .Throttle(TimeSpan.FromMilliseconds(200))
                    .Select(BuildFilterSearch);

                _allItemSets.Connect()
                .Filter(filterSearch)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _activeItemSets)
                .Subscribe()
                .DisposeWith(disposables);
            });
        }

        [Reactive] public ItemSet SelectedSet { get; set; }
        [Reactive] public string SearchString { get; set; }

        public ReadOnlyObservableCollection<ItemSet> ActiveItemSets => _activeItemSets;
        public ViewModelActivator Activator => new();

        private static Func<ItemSet, bool> BuildFilterSearch(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return itemSet => true;
            return itemSet => itemSet.Name.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
