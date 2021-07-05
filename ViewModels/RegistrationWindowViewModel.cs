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
    class RegistrationWindowViewModel: ViewModelBase
    {
        public void btnAccept_Click(Window window, string name)
        {
            User.SaveUser(name);
            window.Closed += RegistrationWindow_Closed;
            window.Close();
        }
        private async void RegistrationWindow_Closed(object? sender, EventArgs e)
        {
            var welcomeWindow = new WelcomeWindow()
            {
                DataContext = new WelcomeWindowViewModel()
            };
            welcomeWindow.Show();
            await Task.Run(() => Thread.Sleep(3000));
            welcomeWindow.Close();
        }
    }
}
