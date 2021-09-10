using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Views.Pages
{
    public class AllCatalogPage : ReactiveUserControl<AllCatalogPageViewModel>
    {
        public AllCatalogPage()
        {
            
            this.WhenActivated(async disposables =>
            {
                InitializeComponent();

                ViewModel?
                .ShowAddingWindow
                .RegisterHandler(async (context) =>
                {
                    var window = new AddingWindow() { DataContext = context.Input };
                    await window.ShowDialog(VisualRoot as Window);
                    context.SetOutput(Unit.Default);
                })
                .DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CellComponent_DoubleTapped(object? sender, RoutedEventArgs e)
        {
            ViewModel?.EditItem();
        }

    }
}
