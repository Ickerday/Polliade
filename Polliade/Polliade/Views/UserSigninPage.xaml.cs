using Microsoft.Identity.Client;
using Polliade.Services.Navigation;
using Polliade.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polliade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoginPage : ContentPage
    {
        private readonly INavigationService _navigationService;

        public UserLoginPage()
        {
            InitializeComponent();

            _navigationService = DependencyService.Resolve<INavigationService>();
        }

        private async void OnSignupButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await App.AuthService.PCA.AcquireTokenAsync(
                    new[] { "" },
                    string.Empty,
                    UIBehavior.SelectAccount,
                    string.Empty,
                    null,
                    AppSettings.GetAuthorityForPolicy(Policy.SignUpSignIn),
                    App.UIParent);

                if (result == null)
                    throw new ArgumentException(nameof(result));

                await _navigationService.NavigateToAsync<UserDetailsViewModel>(result);
            }
            catch (MsalException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message) && ex.Message.Contains("AADB2C90118"))
                    await OnForgotPassword();

                if (ex.ErrorCode != "authentication_canceled")
                    await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
            }
        }

        private async Task OnForgotPassword()
        {
            try
            {
                var accounts = await App.AuthService.PCA.GetAccountsAsync();
                var result = await App.AuthService.PCA
                    .AcquireTokenAsync(
                    scopes: AppSettings.Scopes,
                    account: accounts.FirstOrDefault(),
                    behavior: UIBehavior.SelectAccount,
                    extraQueryParameters: string.Empty,
                    extraScopesToConsent: null,
                    authority: AppSettings.GetAuthorityForPolicy(Policy.ResetPassword));
            }
            catch (MsalException)
            {
                // Do nothing - ErrorCode will be displayed in OnLoginButtonClicked
            }
        }
    }
}