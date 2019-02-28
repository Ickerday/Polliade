using Microsoft.Identity.Client;
using System;
using Xamarin.Forms;

namespace Polliade.ViewModels
{
    public class UserSigninViewModel : BaseViewModel
    {
        public UserSigninViewModel()
        {
            Title = "Sign in";
        }

        public async void SignInButtonClicked()
        {
            try
            {
                var result = await App.AuthService.PCA.AcquireTokenAsync(
                    AppSettings.Scopes,
                    string.Empty,
                    UIBehavior.SelectAccount,
                    string.Empty,
                    null,
                    AppSettings.GetAuthorityForPolicy(Policy.SignUpSignIn));
                if (result == null)
                    throw new ArgumentException(nameof(result));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Can't log in", ex.Message, "Ok");
            }
        }
    }
}
