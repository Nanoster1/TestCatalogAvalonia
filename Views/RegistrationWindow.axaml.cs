using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TestCatalogAvalonia.ViewModels;
using MessageBox.Avalonia;
using System.ComponentModel;

namespace TestCatalogAvalonia.Views
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
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


        private void RegistrationWindow_Closing(object? sender, CancelEventArgs e) => (DataContext as RegistrationWindowViewModel).RegistrationWindow_Closing();


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            var tbxName = this.FindControl<TextBox>("Name");
            if (string.IsNullOrEmpty(tbxName.Text))
                MessageBoxManager.GetMessageBoxStandardWindow("Error", "You didn't write a name").Show();
            else
                (DataContext as RegistrationWindowViewModel).btnAccept_Click(this, tbxName.Text);
        }
    }
}
