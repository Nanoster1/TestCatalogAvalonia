using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace TestCatalogAvalonia.Views.Pages
{
    public partial class ItemSetsPage : ReactiveUserControl<ItemSetsPageViewModel>
    {
        public ItemSetsPage()
        {
            this.WhenActivated(disposables => 
            {
                InitializeComponent();
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
