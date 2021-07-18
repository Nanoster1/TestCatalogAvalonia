using Avalonia.Controls;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.ViewModels.AllCatalogPage
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
                UpdateActiveItemsAsync();
                UpdateAllPages();
            }
        }


        private ObservableCollection<ApparelItem> activeItems = new ObservableCollection<ApparelItem>();
        public ObservableCollection<ApparelItem> ActiveItems { get => activeItems; set => this.RaiseAndSetIfChanged(ref activeItems, value); }


        private ApparelItem selectedItem;
        public ApparelItem SelectedItem { get => selectedItem; set => this.RaiseAndSetIfChanged(ref selectedItem, value); }


        private int pageNumber = 1;
        public int PageNumber { get => pageNumber; set { this.RaiseAndSetIfChanged(ref pageNumber, value); UpdateActiveItemsAsync(); } }


        private List<int> allPages;
        public List<int> AllPages { get => allPages; set => this.RaiseAndSetIfChanged(ref allPages, value); }


        private int selectedPage = 1;
        public int SelectedPage { get => selectedPage; set { this.RaiseAndSetIfChanged(ref selectedPage, value); UpdateActiveItemsAsync(); } }


        private ObservableCollection<Tag> allTags;
        public ObservableCollection<Tag> AllTags { get => allTags; set => this.RaiseAndSetIfChanged(ref allTags, value); }


        private ObservableCollection<string> activeTags;
        public ObservableCollection<string> ActiveTags
        {
            get => activeTags;
            set { this.RaiseAndSetIfChanged(ref activeTags, value); ActiveTags.CollectionChanged += CollectionChanged; UpdateActiveItemsAsync(); }
        }


        public bool filter = true;
        public bool Filter { get => filter; set => this.RaiseAndSetIfChanged(ref filter, value); }


        private async void UpdateActiveItemsAsync()
        {
            List<ApparelItem> newItems = new List<ApparelItem>();
            await Task.Run(() =>
            {
                if (AllItems != null)
                {
                    if (ActiveTags.Count == 0)
                        newItems = AllItems.Skip((PageNumber - 1) * 10).Take(10).OrderBy(x => x.Name[1]).ToList();
                    else if (Filter)
                        newItems = AllItems.Where(x => x.Tags.Any(x => ActiveTags.Contains(x))).Skip((PageNumber - 1) * 10).Take(10).OrderBy(x => x.Name[1]).ToList();
                    else
                        newItems = AllItems.Where(x => ActiveTags.All(u => x.Tags.Contains(u))).Skip((PageNumber - 1) * 10).Take(10).OrderBy(x => x.Name[1]).ToList();
                    UpdateAllPages();
                }
            });
            ActiveItems = new ObservableCollection<ApparelItem>(newItems);
        }

        private void UpdateAllPages()
        {
            double k = Math.Ceiling((double)(AllItems.Count) / 10);
            var pages = new List<int>();
            for (double i = 1; i <= k; i ++ )
            {
                pages.Add((int)i);
            }
            AllPages = pages;
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateActiveItemsAsync();
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

        public void MainTag_Click(bool isChecked, ObservableCollection<string> newTags)
        {
            if (isChecked)
            {
                var oldTags = ActiveTags.ToList();
                oldTags.AddRange(newTags);
                ActiveTags = new ObservableCollection<string>(oldTags.Distinct());
            }
            else if (!isChecked)
            {
                var oldTags = ActiveTags.ToList();
                oldTags.RemoveAll(x => newTags.Contains(x));
                ActiveTags = new ObservableCollection<string>(oldTags);
            }
        }

        public void ChangePage(int num)
        {
            if (PageNumber != num) PageNumber = num;
        }
    }

}
