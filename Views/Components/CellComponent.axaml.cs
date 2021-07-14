using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TestCatalogAvalonia.Views.Components
{
    public partial class CellComponent : UserControl
    {
        public CellComponent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}
