using Akavache;
using Microsoft.Identity.Client;
using Polliade.Models;
using System;

namespace Polliade.Services.UserAuth
{
    public class UserAuthService
    {
        public PublicClientApplication PCA;

        private static string UserCacheKey => $"{nameof(User)}_logged_in";
        private readonly DateTimeOffset DefaultExpiryDate = DateTime.Now + TimeSpan.FromDays(1d);

        private IBlobCache _cache = BlobCache.Secure;

        internal UserAuthService()
        {
            PCA = new PublicClientApplication(AppSettings.ApplicationId)
            {
                RedirectUri = AppSettings.msalRedirectUrl,
                ValidateAuthority = false,
            };
        }

        //public async void OnSignInSignOut(object sender, EventArgs e)
        //{
        //    var accounts = await PCA.GetAccountsAsync();
        //    try
        //    {
        //        // 
        //        if (await IsUserLoggedIn())
        //        {
        //            var ar = await PCA.AcquireTokenAsync(AppSettings.Scopes, GetAccountByPolicy(accounts, AppSettings.PolicySignUpSignIn), App.UiParent);
        //            UpdateUserInfo(ar);
        //            UpdateSignInState(true);
        //        }
        //        else
        //        {
        //            while (accounts.Any())
        //            {
        //                await PCA.RemoveAsync(accounts.FirstOrDefault());
        //                accounts = await PCA.GetAccountsAsync();
        //            }
        //            UpdateSignInState(false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Checking the exception message 
        //        // should ONLY be done for B2C
        //        // reset and not any other error.
        //        if (ex.Message.Contains("AADB2C90118"))
        //            OnPasswordReset();
        //        // Alert if any exception excludig user cancelling sign-in dialog
        //        else if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
        //            await Application.Current.MainPage.DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
        //    }
        //}

        //private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        //{
        //    foreach (var account in accounts)
        //    {
        //        string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
        //        if (userIdentifier.EndsWith(policy.ToLower()))
        //            return account;
        //    }
        //    return null;
        //}

        //private string Base64UrlDecode(string s)
        //{
        //    s = s.Replace('-', '+').Replace('_', '/');
        //    s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
        //    var byteArray = Convert.FromBase64String(s);
        //    var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
        //    return decoded;
        //}

        //public void UpdateUserInfo(AuthenticationResult ar)
        //{
        //    var user = ParseIdToken(ar.IdToken);
        //    lblName.Text = user["name"]?.ToString();
        //    lblId.Text = user["oid"]?.ToString();
        //}

        //private JObject ParseIdToken(string idToken)
        //{
        //    // Get the piece with actual user info
        //    idToken = idToken.Split('.')[1];
        //    idToken = Base64UrlDecode(idToken);
        //    return JObject.Parse(idToken);
        //}

        //private async void OnCallApi(object sender, EventArgs e)
        //{
        //    IEnumerable<IAccount> accounts = await PCA.GetAccountsAsync();
        //    try
        //    {
        //        Debug.WriteLine($"Calling API {App.ApiEndpoint}");
        //        AuthenticationResult ar = await PCA.AcquireTokenSilentAsync(AppSettings.Scopes, GetAccountByPolicy(accounts, AppSettings.PolicySignUpSignIn), AppSettings.Authority, false);
        //        string token = ar.AccessToken;

        //        // Get data from API
        //        var client = new HttpClient();
        //        var message = new HttpRequestMessage(HttpMethod.Get, AppSettings.ApiEndpoint);
        //        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        //        var response = await client.SendAsync(message);
        //        string responseString = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            lblApi.Text = $"Response from API {AppSettings.ApiEndpoint} | {responseString}";
        //        }
        //        else
        //        {
        //            lblApi.Text = $"Error calling API {AppSettings.ApiEndpoint} | {responseString}";
        //        }
        //    }
        //    catch (MsalUiRequiredException ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert($"Session has expired, please sign out and back in.", ex.ToString(), "Dismiss");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
        //    }
        //}

        //async void OnEditProfile(object sender, EventArgs e)
        //{
        //    IEnumerable<IAccount> accounts = await PCA.GetAccountsAsync();
        //    try
        //    {
        //        // KNOWN ISSUE:
        //        // User will get prompted 
        //        // to pick an IdP again.
        //        var ar = await PCA.AcquireTokenAsync(AppSettings.Scopes, GetAccountByPolicy(accounts, AppSettings.PolicyEditProfile), UIBehavior.NoPrompt, string.Empty, null, AppSettings.AuthorityEditProfile, App.UiParent);
        //        UpdateUserInfo(ar);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Alert if any exception excludig user cancelling sign-in dialog
        //        if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
        //            await Application.Current.MainPage.DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
        //    }
        //}
        //async void OnPasswordReset()
        //{
        //    try
        //    {
        //        AuthenticationResult ar = await PCA.AcquireTokenAsync(AppSettings.Scopes, (IAccount)null, UIBehavior.NoPrompt, string.Empty, null, AppSettings.AuthorityPasswordReset, App.UiParent);
        //        UpdateUserInfo(ar);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Alert if any exception excludig user cancelling sign-in dialog
        //        if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
        //            await Application.Current.MainPage.DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
        //    }
        //}

        //void UpdateSignInState(bool isSignedIn)
        //{
        //    btnSignInSignOut.Text = isSignedIn ? "Sign out" : "Sign in";
        //    btnEditProfile.IsVisible = isSignedIn;
        //    btnCallApi.IsVisible = isSignedIn;
        //    slUser.IsVisible = isSignedIn;
        //    lblApi.Text = "";
        //}
    }
}
