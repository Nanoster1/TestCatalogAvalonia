using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using TestCatalogAvalonia.Models;

namespace TestCatalogAvalonia.Views
{
    public class RegistrationWindowViewModel : ViewModelBase
    {
        private IEnumerable<User> _users;

        public RegistrationWindowViewModel(IEnumerable<User> Users)
        {
            _users = Users;
            AcceptUser = ReactiveCommand.CreateFromTask(_acceptUser);
            UserName = string.Empty;
            ShowWelcomeWindow = new();
            DisplayAlert = new();
        }

        [Reactive] public string UserName { get; set; }

        public Interaction<WelcomeWindowViewModel, Unit> ShowWelcomeWindow { get; }

        public Interaction<string, Unit> DisplayAlert { get; }

        public IReactiveCommand AcceptUser { get; }

        public ValidationContext ValidationContext => new();

        private async Task _acceptUser()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                await DisplayAlert.Handle(@"You didn't write a name");
                return;
            }
            else if (_users.Select(x => x.Name).Contains(UserName))
            {
                await DisplayAlert.Handle(@"You can't use this name");
                return;
            }
            User.SaveUser(UserName);
            await ShowWelcomeWindow.Handle(new WelcomeWindowViewModel());
        }
    }
}
