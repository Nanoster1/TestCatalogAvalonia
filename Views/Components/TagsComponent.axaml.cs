using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views.Pages;

namespace TestCatalogAvalonia.Views.Components
{
    public partial class TagsComponent : UserControl
    {
        public TagsComponent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void cbx_AddMainTag_Click(object? sender, RoutedEventArgs e)
        {
            var mainTag = (sender as CheckBox).DataContext as Tag;
            mainTag.IsActive = !mainTag.IsActive;
            (DataContext as AllCatalogPageViewModel).MainTag_Click(mainTag.IsActive, mainTag.Subtags);
        }

        private void cbx_AddTag_Click(object? sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;
            (DataContext as AllCatalogPageViewModel).cbx_AddTag_Click(box.Name, box.IsChecked.Value);
        }
    }
}
