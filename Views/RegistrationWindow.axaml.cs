using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TestCatalogAvalonia.ViewModels;

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
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            var tbxName = this.FindControl<TextBox>("Name");
            if (string.IsNullOrEmpty(tbxName.Text))
                tbxName.Text = "¬ведите им€";
            else
                (DataContext as RegistrationWindowViewModel).btnAccept_Click(this,tbxName.Text);
        }
    }
}
