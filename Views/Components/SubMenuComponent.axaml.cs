using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace TestCatalogAvalonia.Views.Components
{
    public partial class SubMenuComponent : UserControl
    {
        public SubMenuComponent()
        {
            InitializeComponent();
        }
        public EventHandler<RoutedEventArgs> CheckBoxBinding { get; set; } = (object sender, RoutedEventArgs e) => { };
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
