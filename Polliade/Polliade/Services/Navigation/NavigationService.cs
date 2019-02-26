using Polliade.Services.UserAuth;
using Polliade.ViewModels;
using Polliade.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Polliade.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _pages = new Dictionary<Type, Type>
        {
            {typeof(UserDetailsViewModel), typeof(UserDetailsPage)},
            {typeof(UserLoginViewModel), typeof(UserLoginPage)},
            {typeof(UserSignupViewModel), typeof(UserSignupPage)},
        };

        public async Task InitializeAsync()
        {
            var user = UserAuthService.Instance.GetLoggedInUserAsync();
            if (user != null)
            {
                await NavigateToAsync<UserDetailsViewModel>(user);
                return;
            }
            await NavigateToAsync<UserLoginViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel =>
            InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel =>
            InternalNavigateToAsync(typeof(TViewModel), parameter);

        public async Task NavigateBackAsync()
        {
            if (Application.Current.MainPage is TabbedPage tabbedPage)
                await tabbedPage.CurrentPage.Navigation.PopAsync(true);
        }

        public async Task NavigateToRootAsync()
        {
            if (Application.Current.MainPage is TabbedPage tabbedPage)
                await tabbedPage.CurrentPage.Navigation.PopToRootAsync(true);
        }

        public Task NavigateToModalAsync<TViewModel>() where TViewModel : BaseViewModel =>
            InternalNavigateToModalAsync(typeof(TViewModel), null);

        public Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel =>
            InternalNavigateToModalAsync(typeof(TViewModel), parameter);

        public async Task NavigateFromModalAsync()
        {
            if (Application.Current.MainPage is TabbedPage tabbedPage)
                await tabbedPage.CurrentPage.Navigation.PopModalAsync(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            if (viewModelType == typeof(UserDetailsViewModel) || viewModelType == typeof(UserLoginViewModel) || viewModelType == typeof(UserSignupViewModel))
            {
                var mainPage = new MainPage();
                InitializeTabNavigation(mainPage);
                Application.Current.MainPage = mainPage;
            }
            else
            {
                var page = CreatePage(viewModelType);
                await (page.BindingContext as BaseViewModel)?.InitializeAsync(parameter);
                if (Application.Current.MainPage is TabbedPage tabbedPage)
                    await tabbedPage.CurrentPage.Navigation.PushAsync(page);
            }
        }

        private void InitializeTabNavigation(TabbedPage tabbedPage)
        {
            var details = CreatePage(typeof(UserDetailsViewModel));
            var login = CreatePage(typeof(UserLoginViewModel));
            var signup = CreatePage(typeof(UserSignupViewModel));

            tabbedPage.Children.Add(new NavigationPage(details));
            tabbedPage.Children.Add(new NavigationPage(login));
            tabbedPage.Children.Add(new NavigationPage(signup));
        }

        private async Task InternalNavigateToModalAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType);
            await (page.BindingContext as BaseViewModel)?.InitializeAsync(parameter);
            if (Application.Current.MainPage is TabbedPage tabbedPage)
                await tabbedPage.CurrentPage.Navigation.PushModalAsync(page, true);
        }

        private Page CreatePage(Type viewModelType)
        {
            if (_pages.ContainsKey(viewModelType))
            {
                var page = Activator.CreateInstance(_pages[viewModelType]) as Page;
                page.BindingContext = Activator.CreateInstance(viewModelType);
                return page;
            }
            throw new Exception($"Cannot locate page for view model {viewModelType}");
        }
    }
}
