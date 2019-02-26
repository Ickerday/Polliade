using Polliade.ViewModels;
using System.Threading.Tasks;

namespace Polliade.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task NavigateBackAsync();
        Task NavigateToRootAsync();
        Task NavigateToModalAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task NavigateFromModalAsync();
    }
}
