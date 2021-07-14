using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using TestCatalogAvalonia.Views;

namespace TestCatalogAvalonia.ViewModels
{
    public class RegistrationWindowViewModel: ViewModelBase
    {
        public void btnAccept_Click(Window window, string name)
        {
            User.SaveUser(name);
            window.Close();
        }

        public async void RegistrationWindow_Closing()
        {
            var welcomeWindow = new WelcomeWindow()
            {
                DataContext = new WelcomeWindowViewModel()
            };
            welcomeWindow.Show();
        }
    }
}
