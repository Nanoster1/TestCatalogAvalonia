using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using System.Threading;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Views
{
    public partial class WelcomeWindow : ReactiveWindow<WelcomeWindowViewModel>
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
            await Task.Delay(5000);
            this.Close();
        }
        private void WelcomeWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel?.WelcomeWindow_Closing();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
