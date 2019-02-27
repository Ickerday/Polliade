using System.Threading.Tasks;

namespace Polliade.ViewModels
{
    public class UserSigninViewModel : BaseViewModel
    {
        public UserSigninViewModel()
        {
            Title = "Sign in";
        }

        public override Task InitializeAsync(object parameter)
        {
            // TODO
            return base.InitializeAsync(parameter);
        }
    }
}
