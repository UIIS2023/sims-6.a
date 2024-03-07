using InitialProject.Domen.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for UrlTable.xaml
    /// </summary>
    public partial class UrlTable : Window
    {
        public static ObservableCollection<AccommodationImage> Images { get; set; }

        
       
        public UrlTable(ObservableCollection<AccommodationImage> ImageList)
        {
            InitializeComponent();
            DataContext= this;

            // Images = new ObservableCollection<AccommodationImages>();
            Images = ImageList;
          
        }
    }
}
