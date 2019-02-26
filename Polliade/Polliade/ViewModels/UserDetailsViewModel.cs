using Polliade.Models;
using Polliade.Services.Navigation;
using Polliade.Services.UserAuth;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Polliade.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UserDetailsViewModel()
        {
            _navigationService = DependencyService.Resolve<INavigationService>();
        }

        public override Task InitializeAsync(object parameter)
        {
            if (!(parameter is User user))
                throw new ArgumentException(nameof(parameter));

            Id = user.Id;
            Name = user.Name;
            Description = user.Description;

            return base.InitializeAsync(parameter);
        }

        private async Task LogoutButton_ClickedAsync(object sender, EventArgs e) =>
            await UserAuthService.Instance.Logout().ContinueWith(async _ =>
                await _navigationService.NavigateToAsync<UserLoginViewModel>());
    }
}
