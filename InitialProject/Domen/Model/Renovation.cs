using InitialProject.CustomClasses;
using InitialProject.Validation;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public class Renovation :ValidationBase, ISerializable
    {
        private int _renovationId;
        private int _accommodationId;
        private string _accommodationName;
        private DateRange _dateRange;
        private string _description;
        private bool _isFinished;
        private int _days;

        public Renovation()
        {
            DateRange = new DateRange();
            IsFinished = false;
        }
        public Renovation(int renovationId, int accommodationId, string accommodationName, DateRange dateRange, bool isFinished)
        {
            _renovationId = renovationId;
            _accommodationId = accommodationId;
            _accommodationName = accommodationName;
            _dateRange = dateRange;
            _isFinished = isFinished;
        }



        public int RenovationId { get => _renovationId; set => _renovationId = value; }
        public int AccommodationId { get => _accommodationId; set => _accommodationId = value; }

        public string AccommodationName { get => _accommodationName; set => _accommodationName = value; }
        public DateRange DateRange { get => _dateRange; set => _dateRange = value; }
        //public string Description { get => _description; set => _description = value; }
        public bool IsFinished { get => _isFinished; set => _isFinished = value; }

        public string[] ToCSV()
        {

            string[] csvValues = {
                RenovationId.ToString(),
                AccommodationId.ToString(),
                AccommodationName,
                DateRange.ToString(),
                Description,
                IsFinished.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            RenovationId = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            AccommodationName = values[2];
            DateRange = DateRange.fromStringToDateRange(values[3]);
            Description = values[4];
            IsFinished = Convert.ToBoolean(values[5]);
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public int Days
        {
            get { return _days; }
            set
            {
                _days = value;
                OnPropertyChanged("Days");
            }
        }



        protected override void ValidateSelf()
        {
            if (this._days == 0)
            {
                this.ValidationErrors["Days"] = "Please enter a number";
            }
            if (string.IsNullOrWhiteSpace(this._description))
            {
                this.ValidationErrors["Description"] = "Write description please";
            }
        }
    }
}
