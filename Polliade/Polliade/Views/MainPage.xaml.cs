using Polliade.Services.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polliade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        private readonly INavigationService _navigationService;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}