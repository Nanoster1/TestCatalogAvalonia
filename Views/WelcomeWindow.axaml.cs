using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Threading;
using System.Threading.Tasks;
using TestCatalogAvalonia.ViewModels;

namespace TestCatalogAvalonia.Views
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            StopWelcome();
        }
        private async void StopWelcome()
        {
            await Task.Run(() => Thread.Sleep(5000));
            this.Close();
        }
        private void WelcomeWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as WelcomeWindowViewModel)?.WelcomeWindow_Closing();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
