using System;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.ViewModels
{
    public class WelcomeWindowViewModel : ViewModelBase
    {
        public WelcomeWindowViewModel()
        {

        }
        public void WelcomeWindow_Closing()
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
