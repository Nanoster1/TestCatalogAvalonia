using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.ViewModels
{
    class WelcomeWindowViewModel: ViewModelBase
    {
        public WelcomeWindowViewModel() 
        { 

        }
        public void WelcomeWindow_Closing(Window window)
        {
            var mainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            mainWindow.Show();
        }
        public User User { get; private set; } = User.GetUser();
        public string Greeting => $"Hi, {User.Name}";
        public string timeIMG
        {
            get
            {
                if (DateTime.Now.Hour >= new DateTime().AddHours(6).Hour && 
                    DateTime.Now.Hour <= new DateTime().AddHours(21).Hour)
                    return "/Assets/WelcomeIMG/Sun.png";
                return "/Assets/WelcomeIMG/Moon.png";
            }
        }
    }
}
