using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class StatisticsByYearDTO
    {

        private int _accommodationId;

        private int _year;

        private int _numberOfReservations;

        private int _numberOfCanceledReservations;

        private int _numbeOfRescheduledReservations;

        private int _reccommendationForRenovations;

        private int _daysOccupied;


        public StatisticsByYearDTO() { } 

        public StatisticsByYearDTO(int accommodationId, int year, int numberOfReservations, int numberOfCanceledReservations, int numbeOfRescheduledReservations, int reccommendationForRenovations, int daysOccupied)
        {
            _accommodationId = accommodationId;
            _year = year;
            _numberOfReservations = numberOfReservations;
            _numberOfCanceledReservations = numberOfCanceledReservations;
            _numbeOfRescheduledReservations = numbeOfRescheduledReservations;
            _reccommendationForRenovations = reccommendationForRenovations;
            _daysOccupied = daysOccupied;
        }

        public int AccommodationId { get => _accommodationId; set => _accommodationId = value; }
        public int Year { get => _year; set => _year = value; }

        public int NumberOfReservations { get => _numberOfReservations; set => _numberOfReservations = value; }

        public int NumberOfCanceledReservations { get => _numberOfCanceledReservations; set => _numberOfCanceledReservations = value; }

        public int NumberOfRescheduledReservations { get => _numbeOfRescheduledReservations; set => _numbeOfRescheduledReservations = value; }

        public int ReccommendationForRenovations { get => _reccommendationForRenovations; set => _reccommendationForRenovations = value; }

        public int DaysOccupied { get => _daysOccupied; set => _daysOccupied = value; }
    }
}
