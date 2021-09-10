using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TestCatalogAvalonia.Views.Components
{
    public partial class ItemSetComponent : UserControl
    {
        public ItemSetComponent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
