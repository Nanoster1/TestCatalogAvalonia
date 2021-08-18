using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views.Pages;

namespace TestCatalogAvalonia.Views
{
    public class MainWindowViewModel : ViewModelBase
    {
        private SourceCache<ApparelItem, string> _allItems = new(x => x.Name);
        private SourceCache<Tag, string> _allTags = new(x => x.Name);

        public MainWindowViewModel()
        {
            _allItems.Edit(x => x.Load(Services.GetAllApparelItems()));
            _allTags.Edit(x => x.Load(Services.GetAllTags()));
            User = User.ActiveUser;
            ShowAddingWindow = new();
        }

        [Reactive] public User User { get; private set; } 

        public Interaction<AddingWindowViewModel, Unit> ShowAddingWindow { get; }

        AllCatalogPageViewModel AllCatalogPageContent => new AllCatalogPageViewModel(_allItems, _allTags);

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

        public void DisposeAllUselessThreads(IEnumerable<IDisposable> items)
        {
            foreach (var item in items)
            {
                item?.Dispose();
            }
            GC.Collect();
        }
    }
}
