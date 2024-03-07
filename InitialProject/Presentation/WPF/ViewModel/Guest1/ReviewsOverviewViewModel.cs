using InitialProject.Domen.CustomClasses;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class ReviewsOverviewViewModel : INotifyPropertyChanged
    {
        private readonly ReviewInfoService reviewInfoService;
        private ObservableCollection<ReviewInfoDTO> reviews;
        private string strAverageRate;
        private int reviewsNumber;

        public ObservableCollection<ReviewInfoDTO> Reviews
        {
            get { return reviews; }
            set
            {
                if (reviews != value)
                {
                    reviews = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrAverageRate
        {
            get { return strAverageRate; }
            set
            {
                if (strAverageRate != value)
                {
                    strAverageRate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ReviewsNumber
        {
            get { return reviewsNumber; }
            set
            {
                if (reviewsNumber != value)
                {
                    reviewsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReviewsOverviewViewModel(int userId)
        {
            reviewInfoService = new ReviewInfoService();
            Reviews = new ObservableCollection<ReviewInfoDTO>(reviewInfoService.GetReviewInfo(userId));
            InitializeAverageRate();
            ReviewsNumber = Reviews.Count;
        }
        private void InitializeAverageRate()
        {
            double averageRate = CalculateAverageRate();
            if (double.IsNaN(averageRate))
            {
                StrAverageRate = "-";
            }
            else
            {
                StrAverageRate = averageRate.ToString();
            }
        }
        private double CalculateAverageRate()
        {
            double sum = 0;
            foreach (var review in Reviews)
            {
                sum += review.Hygiene + review.FollowingRules;
            }
            double result = Math.Round(sum / Reviews.Count, 1);
            return result;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
