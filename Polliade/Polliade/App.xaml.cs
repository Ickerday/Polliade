using Microsoft.Identity.Client;
using Polliade.Services.Navigation;
using Polliade.Services.UserAuth;
using Polliade.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Polliade
{
    public partial class App : Application
    {
        public static UIParent UIParent;

        public static UserAuthService AuthService = new UserAuthService();
        private readonly INavigationService _navigationService;

        public App()
        {

            InitializeComponent();
            DependencySetup.Initialize();

            _navigationService = DependencyService.Resolve<INavigationService>();
        }

        protected override async void OnStart()
        {
            //Akavache.Registrations.Start(nameof(Polliade));
            await _navigationService.InitializeAsync();

            await GetAuthOrRedirectToSignin();
        }

        protected override void OnSleep() { }

        protected override void OnResume() { }


        private async Task GetAuthOrRedirectToSignin()
        {
            try
            {
                // Look for existing user
                var accounts = await AuthService.PCA.GetAccountsAsync();
                Debug.WriteLine(accounts);
                var result = await AuthService.PCA
                    .AcquireTokenSilentAsync(AppSettings.Scopes, accounts.FirstOrDefault(),
                    AppSettings.GetAuthorityForPolicy(Policy.SignUpSignIn), false);

                if (result == null)
                    throw new ArgumentException(nameof(result));
            }
            catch (Exception ex)
            {
                await _navigationService.NavigateToAsync<UserSigninViewModel>();
            }
        }
    }
}
