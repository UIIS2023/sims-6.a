using InitialProject.Presentation.WPF.ViewModel.Guide;

namespace InitialProject.Services
{
    public interface INavigationService
    {
        void NavigateTo(ViewModelBase viewModel);
    }
}