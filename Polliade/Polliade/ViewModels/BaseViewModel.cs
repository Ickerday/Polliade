using System.Threading.Tasks;

namespace Polliade.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        private bool _isVisible;
        public bool IsVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }

        protected BaseViewModel() { }

        public virtual Task InitializeAsync(object parameter) =>
            Task.FromResult(false);
    }
}
