using Microsoft.Identity.Client;
using Polliade.Services.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Polliade.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private string _id;
        public string Id { get => _id; set => SetProperty(ref _id, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _description, value); }

        private string _description;
        public string Description { get => _description; set => SetProperty(ref _description, value); }

        public UserDetailsViewModel()
        {
            Title = "Details";
            _navigationService = DependencyService.Resolve<INavigationService>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            if (!(parameter is AuthenticationResult result))
                await _navigationService.NavigateToAsync<UserSigninViewModel>();
            else
            {
                Id = result.Account.HomeAccountId.ToString();
                Name = result.Account.Username;
                Description = result.Account.ToString();
            }
            await base.InitializeAsync(parameter);
        }

        public async Task Logout()
        {
            var accounts = await App.AuthService.PCA.GetAccountsAsync();
            while (accounts.Any())
            {
                await App.AuthService.PCA.RemoveAsync(accounts.FirstOrDefault());
                accounts = await App.AuthService.PCA.GetAccountsAsync();
            }
            await _navigationService.NavigateToAsync<UserSigninViewModel>();
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower()))
                    return account;
            }
            return null;
        }
    }
}
