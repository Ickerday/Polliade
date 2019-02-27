using Polliade.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polliade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        public ICommand LogoutButtonClickedCommand => new Command(async () => await Logout());

        public UserDetailsPage() =>
            InitializeComponent();

        public async Task Logout()
        {
            if (!(BindingContext is UserDetailsViewModel vm))
                return;

            await vm.Logout();
        }
    }
}