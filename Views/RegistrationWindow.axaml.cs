using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MessageBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Disposables;

namespace TestCatalogAvalonia.Views
{
    public partial class RegistrationWindow : ReactiveWindow<RegistrationWindowViewModel>
    {
        public RegistrationWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(RegisterHandlers);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void RegisterHandlers(CompositeDisposable disposables)
        {
            ViewModel
                .ShowWelcomeWindow
                .RegisterHandler(context =>
                {
                    WelcomeWindow window = new();
                    window.ViewModel = context.Input;
                    window.Show();
                    this.Close();
                    context.SetOutput(Unit.Default);
                })
                .DisposeWith(disposables);

            ViewModel
                .DisplayAlert
                .RegisterHandler(context =>
                {
                    MessageBoxManager.GetMessageBoxStandardWindow("Error", context.Input).ShowDialog(this);
                    context.SetOutput(Unit.Default);
                })
                .DisposeWith(disposables);
        }
    }
}
