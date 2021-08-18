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
            this.WhenActivated(d => d(this.ViewModel
                .ShowAddingWindow
                .RegisterHandler(async context => 
                {
                    var window = new AddingWindow() { ViewModel = context.Input };
                    await window.ShowDialog(this);
                    context.SetOutput(Unit.Default);
                })));
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
             }).ShowDialog(this);
            if (answer == ButtonResult.Yes)
            {
                ViewModel.OnClosing();
                _saveConfirm = true;
            }
            this.Close();
        }

        private async void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            
        }

        private void tc_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            TabItem item = (TabItem)sender;
            TabControl control = (TabControl)item.Parent;
            var items = control
                .Items
                .OfType<TabItem>()
                .Where(x => x != item)
                .Select(x => (x.Content as UserControl)?.DataContext as IDisposable);
            ViewModel.DisposeAllUselessThreads(items);
        }
    }
}
