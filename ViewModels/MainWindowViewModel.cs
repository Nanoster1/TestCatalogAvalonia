using System;
using System.Collections.Generic;
using System.Text;
using TestCatalogAvalonia.Views;
using TestCatalogAvalonia.Models;
using System.Threading;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
           
        }

        public string Greeting => "Welcome to Avalonia!";
    }
}
