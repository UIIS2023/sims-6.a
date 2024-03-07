using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static InitialProject.Domen.Model.TourPoint;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class CreateTourPointViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        public ICommand EditCommand { get; set; }

        public List<TourPoint> tempTourPoints { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand AddTourPointCommand { get; set; }

        private readonly ITourPointService tourPointService;

        public int nextTourPointId;

        public int Order = 1;

        private readonly int tourId;


        public CreateTourPointViewModel()
        {
            EditCommand = new RelayCommand(edit);
            DeleteCommand = new RelayCommand(delete);
            AddTourPointCommand = new RelayCommand(AddTourPoint);
            TourPoints = new ObservableCollection<TourPoint>();
        }

        public CreateTourPointViewModel(int tourID, List<TourPoint> tourPoints)
        {
            EditCommand = new RelayCommand(edit);
            DeleteCommand = new RelayCommand(delete);
            AddTourPointCommand = new RelayCommand(AddTourPoint);
            TourPoints = new ObservableCollection<TourPoint>(tourPoints);
            tourPointService = Injector.CreateInstance<ITourPointService>();
            nextTourPointId = tourPointService.FindNextId();
            tourId = tourID;
            tempTourPoints = tourPoints;
            CheckOrderId();
        }

        private TourPoint _selectedTourPoint;
        public TourPoint SelectedTourPoint
        {
            get { return _selectedTourPoint; }
            set
            {
                _selectedTourPoint = value;
                OnPropertyChanged(nameof(_selectedTourPoint));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(_Description));
            }
        }

        public void CheckOrderId()
        {
            if (tempTourPoints.Count > 0)
            {
                Order = tempTourPoints.OrderByDescending(point => point.Order).FirstOrDefault().Order;
                Order++;
            }
        }

        public void edit(object obj)
        {
            TourPoint selectedTourPoint = (TourPoint)obj;
        }

        public void delete(object obj)
        {
            TourPoint selectedTourPoint = (TourPoint)obj;

            int objectOrder = selectedTourPoint.Order;

            TourPoints.Remove(selectedTourPoint);
            tempTourPoints.Remove(selectedTourPoint);

            foreach (TourPoint tourPoint in tempTourPoints)
            {
                if (tourPoint.Order > objectOrder)
                {
                    tourPoint.Order--;
                }
            }

            TourPoints.Clear();

            foreach(var TourPoint in tempTourPoints)
            {
                TourPoints.Add(TourPoint);
            }

            Order = tempTourPoints.OrderByDescending(point => point.Order).FirstOrDefault().Order;
            Order++;




        }

        public void AddTourPoint(object obj) 
        {
            TourPoint tourPoint = new TourPoint() 
            {
                Id = nextTourPointId,
                Name = Name,
                TourId = tourId,
                CurrentStatus = Status.NotActive,
                Description = Description,
                Order = Order
            };
            Order++;
            nextTourPointId++;
            tempTourPoints.Add(tourPoint);
            TourPoints.Add(tourPoint);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
