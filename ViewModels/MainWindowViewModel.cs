using System;
using System.Collections.Generic;
using System.Text;
using TestCatalogAvalonia.Views;
using TestCatalogAvalonia.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Linq;
using System.Collections.Specialized;
using Avalonia.Controls;

namespace TestCatalogAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {

        }


        private ObservableCollection<ApparelItem> allItems = new ObservableCollection<ApparelItem>(Services.GetAllApparelItems());
        public ObservableCollection<ApparelItem> AllItems { get => allItems; set => this.RaiseAndSetIfChanged(ref allItems, value); }


        private User user = Services.GetUser();
        public User User { get => user; set => this.RaiseAndSetIfChanged(ref user, value); }


        private ObservableCollection<Tag> allTags = new ObservableCollection<Tag>(Services.GetAllTags());
        public ObservableCollection<Tag> AllTags { get => allTags; set => this.RaiseAndSetIfChanged(ref allTags, value); }


        AllCatalogPageViewModel AllCatalogPageContent { get => new AllCatalogPageViewModel() { AllItems = AllItems, AllTags = AllTags }; }


        public void btn_AddingItem_Click(Window window)
        {
            var addingWindow = new AddingWindow() { DataContext = new AddingWindowViewModel(new ApparelItem(), AllItems, true) };
            addingWindow.ShowDialog(window);
        }

    }
}
