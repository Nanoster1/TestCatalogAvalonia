using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TestCatalogAvalonia.ViewModels;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                try
                {
                    desktop.MainWindow = new WelcomeWindow
                    {
                        DataContext = new WelcomeWindowViewModel(),
                    };
                }
                catch
                {
                    desktop.MainWindow = new RegistrationWindow()
                    {
                        DataContext = new RegistrationWindowViewModel(),
                    };
                }
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
