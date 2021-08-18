using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace TestCatalogAvalonia.Views.Pages
{
    public class AllCatalogPage : UserControl
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

    }
}
