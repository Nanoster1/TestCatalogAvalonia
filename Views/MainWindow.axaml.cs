using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.ComponentModel;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System.Threading.Tasks;
using System.Reactive;
using System.Linq;
using System;
using System.Reactive.Disposables;

namespace TestCatalogAvalonia.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        private bool _saveConfirm = false;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(dispoables => 
            {
                ViewModel
                    .ShowAddingWindow
                    .RegisterHandler(async context =>
                    {
                        var window = new AddingWindow() { ViewModel = context.Input };
                        await window.ShowDialog(this);
                        context.SetOutput(Unit.Default);
                    })
                    .DisposeWith(dispoables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            if (!_saveConfirm) return;
            e.Cancel = true;
            var answer = await MessageBoxManager
             .GetMessageBoxStandardWindow(new MessageBoxStandardParams
             {
                 ContentTitle = "Warning",
                 ContentMessage = "You want to save data?",
                 ButtonDefinitions = ButtonEnum.YesNo,
                 WindowStartupLocation = WindowStartupLocation.CenterOwner
             })
             .ShowDialog(this);
            if (answer == ButtonResult.Yes)
            {
                ViewModel.OnClosing();
                _saveConfirm = true;
            }
            this.Close();
        }

        private void Menu_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null || e.AddedItems.Count == 0 ||
                e.AddedItems[0] is not TabItem item || item.Content is not UserControl page) return;
            GC.Collect(); //dispose useless threads
            switch (item.Tag)
            {
                case "0":
                    page.DataContext = null; //In work
                    break;
                case "1":
                    page.DataContext = ViewModel.AllCatalogPageContent;
                    break;
                case "2":
                    page.DataContext = ViewModel.ItemSetsPageContent; //In work
                    break;
            }
        }
    }
}
