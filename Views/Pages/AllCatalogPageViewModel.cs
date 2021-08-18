using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using TestCatalogAvalonia.Models;

namespace TestCatalogAvalonia.Views.Pages
{
    public class AllCatalogPageViewModel : ViewModelBase
    {
        private readonly SourceCache<ApparelItem, string>? _allItems = new(x => x.Name);
        private SourceCache<Tag, string> _allTags = new(x => x.Name);
        private readonly ReadOnlyObservableCollection<ApparelItem> _activeItems;
        private readonly ReadOnlyObservableCollection<Tag> _allTagsCollection;
        public readonly IDisposable _cleanUpItems;
        public readonly IDisposable _cleanUpTags;

        public AllCatalogPageViewModel(SourceCache<ApparelItem, string> allItems, SourceCache<Tag, string> allTags)
        {
            _allItems = allItems;
            _allTags = allTags;

            var filterSearch = this.WhenValueChanged(x => x.SearchString)
                .Throttle(TimeSpan.FromMilliseconds(400))
                .Select(BuildFilterSearch);

            var filterTags = this.WhenValueChanged(x => x.ActiveTags)
                .Select(BuildFilterTags);

            var pager = PageParameters.WhenAnyValue(x => x.PageSize, x => x.CurrentPage, (size, page) => new PageRequest(page, size))
                .StartWith(new PageRequest(1, 10))
                .DistinctUntilChanged();

            // filter, sort, page and bind to observable collection
            _cleanUpItems = _allItems.Connect()
                .Filter(filterSearch) // apply search filter
                .Filter(filterTags) //apply tags filter
                .Sort(SortExpressionComparer<ApparelItem>.Ascending(x => x.Name), SortOptimisations.ComparesImmutableValuesOnly)
                .Page(pager)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Do(x => PageParameters.Update(x.Response))
                .Bind(out _activeItems)        // update observable collection bindings
                .DisposeMany()          // dispose when no longer required
                .Subscribe();

            _cleanUpTags = _allTags.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _allTagsCollection)
                .DisposeMany()
                .Subscribe();
        }


        [Reactive] public ApparelItem? SelectedItem { get; set; }

        [Reactive] private string? ActiveTags { get; set; } = string.Empty;

        [Reactive] public string SearchString { get; set; } = string.Empty;

        [Reactive] public bool IsExecute { get; private set; } = false;

        public ReadOnlyObservableCollection<Tag> AllTags => _allTagsCollection;

        public ReadOnlyObservableCollection<ApparelItem>? ActiveItems => _activeItems;

        public PageParameters PageParameters { get; private set; } = new PageParameters(1, 10);



        private static Func<ApparelItem, bool> BuildFilterSearch(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return item => true;
            return item => item.Name.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase) ||
                           item.Tags.Any(x => x.Contains(searchString));   //Ability to search by tags in SearchString
        }

        private static Func<ApparelItem, bool> BuildFilterTags(string activeTags)
        {
            if (string.IsNullOrWhiteSpace(activeTags)) return item => true;
            return item => item.Tags.Any(x => activeTags.Contains(x));
        }

        public override void Dispose()
        {
            _cleanUpItems.Dispose();
            _cleanUpTags.Dispose();
        }

        public void cbx_AddTag_Click(string tag, bool isChecked)
        {
            if (string.IsNullOrWhiteSpace(tag)) return;
            else if (isChecked)
                ActiveTags += " " + tag;
            else
                ActiveTags = ActiveTags?.Replace(" " + tag, string.Empty);
        }

        public void EditItem(Window window)
        {
            var addingWindow = new AddingWindow() { DataContext = new AddingWindowViewModel(SelectedItem, _allItems, false) };
            addingWindow.ShowDialog(window);
        }

        public void SetTagsVisibility(Controls controls)
        {
            controls[1].IsVisible = !controls[1].IsVisible;
        }

        public void MainTag_Click(bool isChecked, List<string> newTags)
        {
            if (newTags.Count == 0) return;
            else if (isChecked)
                ActiveTags += " " + string.Join(" ", newTags);
            else
                ActiveTags = ActiveTags?.Replace(" " + string.Join(" ", newTags), string.Empty);
        }
    }
}
