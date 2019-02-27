using Polliade.Services.Navigation;
using Xamarin.Forms;

namespace Polliade
{
    public class DependencySetup
    {
        public static void Initialize()
        {
            DependencyService.Register<INavigationService, NavigationService>();
            //DependencyService.Register<IUserAuthService, UserAuthService>();
        }
    }
}
