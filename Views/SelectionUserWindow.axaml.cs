using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.Views
{
    public partial class SelectionUserWindow : ReactiveWindow<SelectionUserWindowViewModel>
    {
        public SelectionUserWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UserClick(object? sender, RoutedEventArgs e)
        {
            ((SelectionUserWindowViewModel)DataContext).SelectUser(((Button)sender).DataContext as User);
            this.Close();
        }
        private void NewUserClick(object? sender, RoutedEventArgs e)
        {
            ((SelectionUserWindowViewModel)DataContext).AddNewUser();
            this.Close();
        }
    }
}
