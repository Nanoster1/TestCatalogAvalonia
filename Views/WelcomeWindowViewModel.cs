using System;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.Views
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
        public User User { get; private set; } = User.ActiveUser;
        public string Greeting => $"Hi, {User.Name}";
        public string TimeIMG
        {
            get
            {
                if (DateTime.Now.Hour >= 6 &&
                    DateTime.Now.Hour <= 21)
                    return "/Assets/WelcomeIMG/Sun.png";
                return "/Assets/WelcomeIMG/Moon.png";
            }
        }
    }
}
