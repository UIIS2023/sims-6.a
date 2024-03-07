using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class RenovationLevelsViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RenovationLevelsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
        }
        public void Close(object parameter)
        {
            if(parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
