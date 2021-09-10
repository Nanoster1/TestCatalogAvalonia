using Avalonia.Controls;
using DynamicData;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using TestCatalogAvalonia.Models;

namespace TestCatalogAvalonia.Views
{
    public class AddingWindowViewModel : ViewModelBase
    {
        private readonly SourceCache<ApparelItem, string> _allItems;
        private ApparelItem _oldItem;

        public AddingWindowViewModel(ApparelItem item, SourceCache<ApparelItem, string> allItems, bool isCretingItem)
        {
            Item = item;
            _oldItem = item.Clone();
            AllTags = new(Services.GetAllTags().SelectMany(x => x.Subtags));
            ActiveTags = new(item.Tags);
            _allItems = allItems;
            IsCreatingItem = isCretingItem;
            DisplayedAlert = new();
            DisplayedQuestion = new();
        }

        private bool IsCreatingItem { get; }


        [Reactive] public ObservableCollection<string> AllTags { get; set; }

        [Reactive] public ObservableCollection<string> ActiveTags { get; set; }

        [Reactive] public ApparelItem Item { get; set; }

        public Interaction<string, Unit> DisplayedAlert { get; }
        public Interaction<string, bool> DisplayedQuestion { get; }

        public async void btn_FileOpen_Click(Window window)
        {
            var dialog = new OpenFileDialog();
            Item.ImageSource = (await dialog.ShowAsync(window)).FirstOrDefault();
        }


        public async void btn_SaveItem_Click(Window window)
        {
            if (Item.ImageSource == new ApparelItem().ImageSource || Item.ImageSource == null || Item.ImageSource == string.Empty)
            {
                await DisplayedAlert.Handle("Please, choose image");
                await MessageBoxManager.GetMessageBoxStandardWindow("Error", "Please, choose image").ShowDialog(window);
                return;
            }
            else if (Item.Name == string.Empty || Item.Name == null)
            {
                await DisplayedAlert.Handle("Please, write name");
                return;
            }
            if (IsCreatingItem)
            {
                if (_allItems.Keys.Any(x => x == Item.Name))
                {
                    var result = await DisplayedQuestion.Handle("An element with this name already exists, replace it?");
                    if (!result) return;
                    _allItems.RemoveKey(Item.Name);
                }
                _allItems.AddOrUpdate(Item);
            }
            else
            {
                if (_allItems.KeyValues.Any(x => x.Value != Item && x.Key == Item.Name))
                {
                    await MessageBoxManager.GetMessageBoxStandardWindow("Error", "An element with this name already exists").ShowDialog(window);
                    return;
                }
            }
            Item.Tags = ActiveTags.ToList();
            window.Close();
        }

        public void cbx_AddTag_Click(string tag, bool isAdded)
        {
            if (isAdded)
                ActiveTags.Add(tag);
            else
                ActiveTags.Remove(tag);
        }
    }
}
