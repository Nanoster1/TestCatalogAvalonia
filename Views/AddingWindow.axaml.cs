using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.ViewModels;

namespace TestCatalogAvalonia.Views
{
    public partial class AddingWindow : Window
    {
        public AddingWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void AddingWindow_ElementPrepared(object? sender, ItemsRepeaterElementPreparedEventArgs e)
        {
            if ((DataContext as AddingWindowViewModel).ActiveTags.Contains(e.Element.DataContext))
                (e.Element as CheckBox).IsChecked = true;
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

        private void cbx_AddTag_Click(object? sender, RoutedEventArgs e)
        {
            var cbx = sender as CheckBox;
            (DataContext as AddingWindowViewModel).cbx_AddTag_Click(cbx.DataContext as string, cbx.IsChecked.Value);
        }
        private void SetIsCheckedProperty(object? sender, EventArgs e)
        {
            
            var checkboxes = this.LogicalChildren.Where(x => x is CheckBox);
            
        }
    }
}
