using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class GuestReviewFormViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly UserToReviewRepository _userToReviewRepository;

        public int GuestId { get; set; }
        public int AccommodationId { get; set; }

        

        private string _ownerComment;

        private bool _isReviewd;
        /*
        public string OwnerComment
        {
            get => _ownerComment;
            set
            {
                if (value != _ownerComment)
                {
                    _ownerComment = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public int HygieneGrade
        {
            get => _hygieneGrade;
            set
            {
                if (value != _hygieneGrade)
                {
                    _hygieneGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        public int RuleFollowingGrade
        {
            get => _ruleFollowingGrade;
            set
            {
                if (value != _ruleFollowingGrade)
                {
                    _ruleFollowingGrade = value;
                    OnPropertyChanged();
                }
            }

        }
        */
        public bool IsReviewd
        {
            get => _isReviewd;
            set
            {
                if(value != _isReviewd)
                {
                    _isReviewd = value;
                    OnPropertyChanged();
                }
            }
        }

        private GuestReview _guestReview = new GuestReview();
        public GuestReview NewGuestReview
        {
            get { return _guestReview; }
            set
            {
                if (value != _guestReview)
                {
                    _guestReview = value;
                    OnPropertyChanged("Accommodation");
                }
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public GuestReviewFormViewModel(int guestId, int accommodationId)
        {
            _userToReviewRepository = new UserToReviewRepository();
            IsReviewd = false;
            _guestReviewRepository = new GuestReviewRepository();
            GuestId = guestId;
            AccommodationId = accommodationId;

            SubmitCommand = new RelayCommand(SubmitReview);
            CancelCommand = new RelayCommand(CloseWindow);
        }


        public void SubmitReview(object parameter)
        {
            NewGuestReview.Validate();
            if (NewGuestReview.IsValid)
            {
            GuestReview newGuestReview = CreateReview();
            _guestReviewRepository.Save(newGuestReview);
            IsReviewd = true;
            //CloseWindow();
            }
            else
            {
                OnPropertyChanged(nameof(NewGuestReview));
            }
        }

        private GuestReview CreateReview()
        {
            return new GuestReview
            {
                GuestId = GuestId,
                AccommodationId = AccommodationId,
                Hygiene = NewGuestReview.HygieneGrade,
                RuleFollowing = NewGuestReview.RuleFollowingGrade,
                Comment = NewGuestReview.Comment,
            };
        }

        public void CloseWindow(object parameter)
        {
        }
    }
}
