using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.ViewModels
{
    public class AllCatalogPageViewModel : ViewModelBase
    {
        public AllCatalogPageViewModel()
        {
            ActiveTags = new ObservableCollection<string>();
        }


        private ObservableCollection<ApparelItem> allItems;
        public ObservableCollection<ApparelItem> AllItems
        {
            get => allItems;
            set
            {
                this.RaiseAndSetIfChanged(ref allItems, value);
                AllItems.CollectionChanged += CollectionChanged;
                UpdateActiveItems();
            }
        }


        private ObservableCollection<ApparelItem> activeItems = new ObservableCollection<ApparelItem>();
        public ObservableCollection<ApparelItem> ActiveItems { get => activeItems; set => this.RaiseAndSetIfChanged(ref activeItems, value); }


        private ApparelItem selectedItem;
        public ApparelItem SelectedItem { get => selectedItem; set => this.RaiseAndSetIfChanged(ref selectedItem, value); }


        private User user = Services.GetUser();
        public User User { get => user; set => this.RaiseAndSetIfChanged(ref user, value); }


        private ObservableCollection<Tag> allTags;
        public ObservableCollection<Tag> AllTags { get => allTags; set => this.RaiseAndSetIfChanged(ref allTags, value); }


        private ObservableCollection<string> activeTags;
        public ObservableCollection<string> ActiveTags
        {
            get => activeTags;
            set { this.RaiseAndSetIfChanged(ref activeTags, value); ActiveTags.CollectionChanged += CollectionChanged; } 
        }


        private double itemsWidth = 200;
        public double ItemsWidth { get => itemsWidth; set => this.RaiseAndSetIfChanged(ref itemsWidth, value); }


        private double itemsHeight = 250;
        public double ItemsHeight { get => itemsHeight; set => this.RaiseAndSetIfChanged(ref itemsHeight, value); }


        public bool filter = true;
        public bool Filter { get => filter; set => this.RaiseAndSetIfChanged(ref filter, value); }


        public void UpdateActiveItems()
        { 
            if (AllItems != null)
            {
                if (ActiveTags.Count == 0)
                    ActiveItems = AllItems;
                else if (Filter)
                    ActiveItems = new ObservableCollection<ApparelItem>(AllItems.Where(x => x.Tags.Any(x => ActiveTags.Contains(x))));
                else
                    ActiveItems = new ObservableCollection<ApparelItem>(AllItems.Where(x => ActiveTags.All(u => x.Tags.Contains(u))));
            }
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateActiveItems();
        }

        public void cbx_AddTag_Click(string tag, bool isChecked)
        {
            if (isChecked)
                ActiveTags.Add(tag);
            else
                ActiveTags.Remove(tag);
        }

        public void EditItem(Window window)
        {
            var addingWindow = new AddingWindow() { DataContext = new AddingWindowViewModel(SelectedItem, AllItems, false) };
            addingWindow.ShowDialog(window);
        }

        public void SetTagsVisibility(Controls controls)
        {
            controls[1].IsVisible = !controls[1].IsVisible;
        }

        public void MainTag_Click(bool isChecked, ObservableCollection<string> tags)
        {
            foreach (var tag in tags)
            {
                if (isChecked && !ActiveTags.Contains(tag))
                    ActiveTags.Add(tag);
                else if (!isChecked && ActiveTags.Contains(tag))
                    ActiveTags.Remove(tag);
            }
        }
    }
}
