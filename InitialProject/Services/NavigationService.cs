using InitialProject.Presentation.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace InitialProject.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _contentControl;

        public NavigationService(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }

        public void NavigateTo(ViewModelBase viewModel)
        {
            _contentControl.Content = viewModel;
        }

    }

}
