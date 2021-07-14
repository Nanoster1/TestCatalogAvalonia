using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TestCatalogAvalonia.ViewModels;
using TestCatalogAvalonia.Views.Components;

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

        private void cbx_AddTag_Click(object? sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;
            (DataContext as AllCatalogPageViewModel).cbx_AddTag_Click(box.Name, box.IsChecked.Value);
        }

        private void CellComponent_DoubleTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            (DataContext as AllCatalogPageViewModel).EditItem(this.Parent.Parent.Parent.Parent as Window);
        }
    }
}
