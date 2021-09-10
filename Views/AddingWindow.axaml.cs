using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Collections;
using System.Reactive.Disposables;

namespace TestCatalogAvalonia.Views
{
    public partial class AddingWindow : ReactiveWindow<AddingWindowViewModel>
    {
        public AddingWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(RegisterHandlers);
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

        private void AddingWindow_ElementPrepared(object? sender, ItemsRepeaterElementPreparedEventArgs e)
        {
            if (ViewModel.ActiveTags.Contains(e.Element.DataContext))
                ((CheckBox)e.Element).IsChecked = true;
        }

        private void cbx_AddTag_Click(object? sender, RoutedEventArgs e)
        {
            var cbx = sender as CheckBox;
            ViewModel?.cbx_AddTag_Click(cbx.DataContext as string, cbx.IsChecked.Value);
        }

        private void RegisterHandlers(CompositeDisposable disposables)
        {
            ViewModel
            .DisplayedAlert
            .RegisterHandler(async context =>
            {
                await MessageBoxManager.GetMessageBoxStandardWindow("Error", context.Input).ShowDialog(this);
                context.SetOutput(Unit.Default);
            })
            .DisposeWith(disposables);

            ViewModel
            .DisplayedQuestion
            .RegisterHandler(async context =>
            {
                var result = await MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ContentTitle = "Warning",
                    ContentMessage = context.Input,
                    ButtonDefinitions = ButtonEnum.OkCancel,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                })
                .ShowDialog(this);
                if (result == ButtonResult.Cancel) context.SetOutput(false);
                else context.SetOutput(true);
            })
            .DisposeWith(disposables);
        }
    }
}
