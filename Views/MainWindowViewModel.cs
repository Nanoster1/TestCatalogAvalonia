using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views.Pages;

namespace TestCatalogAvalonia.Views
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly SourceCache<ApparelItem, string> _allItems = new(item => item.Name);
        private readonly SourceCache<Tag, string> _allTags = new(tag => tag.Name);
        private readonly SourceCache<ItemSet, string> _allItemSets = new(itemSet => itemSet.Name);

        public MainWindowViewModel()
        {
            var allItems = Services.GetAllApparelItems();
            var allTags = Services.GetAllTags();
            var allItemSets = Services.GetAllItemSets();

            allItemSets.ForEach(itemSet => itemSet.FillItems(allItems));

            _allItems.Edit(x => x.Load(allItems));
            _allTags.Edit(x => x.Load(allTags));
            _allItemSets.Edit(x => x.Load(allItemSets));

            User = User.ActiveUser;

            ShowAddingWindow = new();
        }

        [Reactive] public User User { get; private set; } 

        public Interaction<AddingWindowViewModel, Unit> ShowAddingWindow { get; }

        public AllCatalogPageViewModel AllCatalogPageContent => new(_allItems, _allTags);

        public ItemSetsPageViewModel ItemSetsPageContent => new(_allItemSets);

        public async void btn_AddingItem_Click()
        {
           await ShowAddingWindow.Handle(new AddingWindowViewModel(new ApparelItem(), _allItems, true));
        }

        public void OnClosing()
        {
            var items = _allItems.Items;
            var tags = _allTags.Items;
            foreach (var item in items)
            {
                item.Save();
            }
            Tag.SaveTags(tags);
        }
    }
}
