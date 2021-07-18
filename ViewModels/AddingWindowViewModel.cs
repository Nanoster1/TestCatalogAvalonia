
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;

namespace TestCatalogAvalonia.ViewModels
{
    public class AddingWindowViewModel : ViewModelBase
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


        private ObservableCollection<string> allTags = new ObservableCollection<string>(Services.GetAllTags().SelectMany(x => x.Subtags));
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
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Error", "Please, choose image").ShowDialog(window);
            }
            else if (IsCreatingItem && AllItems.Any(x => x.Name == LabelName))
            {
                var result = await MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ContentTitle = "Warning",
                        ContentMessage = "An element with this name already exists, replace it?",
                        ButtonDefinitions = ButtonEnum.OkCancel,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    }).ShowDialog(window);
                if (result == ButtonResult.Cancel)
                    return;
                AllItems.Remove(new ApparelItem(LabelName));
                AllItems.Add(Item);
                OldItem.Name = LabelName;
                SaveItem(window);
            }
            else
            {
                
                AllItems.Remove(OldItem);
                AllItems.Add(Item);
                SaveItem(window);
            }
        }
        private async void SaveItem(Window window)
        {
            string oldName = OldItem.Name;
            Item.Name = LabelName;
            Item.Tags = ActiveTags.ToList();
            window.Close();
            await CopyImageAsync(oldName);
            ApparelItem.SaveApparelItem(Item, oldName);
            
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


        public void cbx_AddTag_Click(string tag, bool isAdded)
        {
            if (isAdded)
                ActiveTags.Add(tag);
            else
                ActiveTags.Remove(tag);
        }
    }
}
