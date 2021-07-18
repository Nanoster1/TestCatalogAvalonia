using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.ViewModels;
using TestCatalogAvalonia.ViewModels.AllCatalogPage;

namespace TestCatalogAvalonia.Views.Pages
{
    public partial class AllCatalogPage : UserControl
    {
        public AllCatalogPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CellComponent_DoubleTapped(object? sender, RoutedEventArgs e)
        {
            (DataContext as AllCatalogPageViewModel).EditItem(this.Parent.Parent.Parent.Parent as Window);
        }

        private void NumberOfPage_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            (DataContext as AllCatalogPageViewModel).ChangePage((int)(sender as Border).DataContext);
        }
    }
}
