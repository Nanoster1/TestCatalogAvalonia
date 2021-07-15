
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;
using MessageBox.Avalonia;

namespace TestCatalogAvalonia.ViewModels
{
    public class AddingWindowViewModel: ViewModelBase
    {
        public AddingWindowViewModel(ApparelItem item, ObservableCollection<ApparelItem> allItems, bool isCretingItem)
        {
            OldItem = item;
            Item = item;
            ActiveTags = new ObservableCollection<string>(item.Tags);
            Item.ImageSourceUpdate += (string value) => ImageSource = value;
            Item.NameUpdate += (string value) => LabelName = value;
            ImageSource = Item.ImageSource;
            LabelName = Item.Name;
            AllItems = allItems;
            IsCreatingItem = isCretingItem;
        }

        
        private bool IsCreatingItem { get; }


        private ObservableCollection<string> allTags = new ObservableCollection<string>(Services.GetAllTags());
        public ObservableCollection<string> AllTags { get => allTags; set => this.RaiseAndSetIfChanged(ref allTags, value); }


        private ObservableCollection<string> activeTags;
        public ObservableCollection<string> ActiveTags { get => activeTags; set => this.RaiseAndSetIfChanged(ref activeTags, value); }


        private ObservableCollection<ApparelItem> AllItems { get; }


        private ApparelItem item;
        public ApparelItem Item { get => item; set => this.RaiseAndSetIfChanged(ref item, value); }
        private ApparelItem OldItem { get; set; }


        private string imageSource;
        public string ImageSource { get => imageSource; private set => this.RaiseAndSetIfChanged(ref imageSource, value); }


        private string labelName;
        public string LabelName { get => labelName; private set => this.RaiseAndSetIfChanged(ref labelName, value); }


        public async void btn_FileOpen_Click(Window window)
        {
            var dialog = new OpenFileDialog();
            Item.ImageSource = (await dialog.ShowAsync(window)).FirstOrDefault();
        }


        public async void btn_SaveItem_Click(Window window)
        {
            if (Item.ImageSource == new ApparelItem().ImageSource)
                MessageBoxManager.GetMessageBoxStandardWindow("Error", "Please, choose image").ShowDialog(window);
            else 
            { 
                string oldName = Item.Name;
                Item.Name = LabelName;
                Item.Tags = ActiveTags.ToList();
                window.Close();
                await CopyImageAsync(oldName);
                ApparelItem.SaveApparelItem(Item, oldName);
                AddingItem();
            }
        }
        private Task CopyImageAsync(string oldName)
        {
            return Task.Run(() =>
            {
                item.ImageSource = FileWorker.CopyImage(item.ImageSource, "Temporary");
                ApparelItem.DeleteApparelItem(oldName);
                item.ImageSource = FileWorker.CopyImage(item.ImageSource, item.Name);
                ApparelItem.DeleteApparelItem("Temporary");
            });
        }
        private void AddingItem() //Add Dialog (Repetitive name)
        {
            if (IsCreatingItem)
            {
                AllItems.Add(Item);
            }
            else
            {
                AllItems.Remove(OldItem);
                AllItems.Add(Item);
            }
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
