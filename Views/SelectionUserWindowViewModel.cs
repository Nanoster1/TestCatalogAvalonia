using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using Avalonia.Controls;
using System.Reactive;

namespace TestCatalogAvalonia.Views
{
    public class SelectionUserWindowViewModel : ViewModelBase
    {
        public SelectionUserWindowViewModel()
        {

        }

        [Reactive] public IEnumerable<User> Users { get; set; } = Services.GetUsers();

        public Interaction<RegistrationWindowViewModel, Unit> ShowRegistrationWindow { get; } = new();

        public void SelectUser(User user)
        {
            User.ActiveUser = user;
            WelcomeWindow window = new()
            {
                ViewModel = new()
            };
            window.Show();
        }

        public void AddNewUser()
        {
            RegistrationWindow window = new()
            {
                DataContext = new RegistrationWindowViewModel(Users)
            };
            window.Show();
        }
    }
}
