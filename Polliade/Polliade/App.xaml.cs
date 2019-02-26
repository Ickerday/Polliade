using Fusillade;
using Polliade.Services.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Polliade
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencySetup.Initialize();
        }

        protected override async void OnStart()
        {
            Akavache.Registrations.Start("Polliade");
            await InitializeNavigation();
        }

        protected override void OnSleep() { }

        protected override void OnResume() =>
            NetCache.Speculative.ResetLimit(1048576 * 5/*MB*/);

        private async Task InitializeNavigation()
        {
            var navigationService = DependencyService.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }
    }
}
