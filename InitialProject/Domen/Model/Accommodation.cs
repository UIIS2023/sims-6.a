using InitialProject.Validation;
using Microsoft.VisualStudio.Services.Profile;
using System;
using System.Collections.Generic;

namespace InitialProject.Domen.Model
{
    public enum AccommodationType { Apartment = 0, House = 1, Shack = 2 }
    public class Accommodation : ValidationBase,ISerializable
    {
        public int UserId { get; set; } //ID Vlasnika smestaja
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType TypeOfAccommodation { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysBeforeCancelling { get; set; }
        public List<AccommodationImage> Images { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int AccommodationID { get; set; }
        public bool RecentlyRenovated { get; set; }
        public Accommodation(int userId, string name, Location location, AccommodationType type, int maxGuestNumber,
            int minReservationDays, int daysBeforeCancelling, List<AccommodationImage> images,
            List<Reservation> reservations, bool recentlyRenovated)
        {
            UserId = userId;
            Name = name;
            Location = location;
            TypeOfAccommodation = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            DaysBeforeCancelling = daysBeforeCancelling;
            Images = images;
            Reservations = reservations;
            RecentlyRenovated = recentlyRenovated;
        }
        public Accommodation()
        {
            UserId = 0; //defaultna vrednost pre kreiranja sigin forme za vise korisnika
            AccommodationID = 0;
            Name = "";
            Location = new Location();
            TypeOfAccommodation = 0;
            MaxGuestNumber = 0;
            MinReservationDays = 0;
            DaysBeforeCancelling = 0;
            Images = new List<AccommodationImage>();
            Reservations = new List<Reservation>();
            RecentlyRenovated = false;
        }
        public string[] ToCSV()
        {

            string[] csvValues = {
                AccommodationID.ToString(),
                UserId.ToString(),
                Name,
                Location.ToString(),
                TypeOfAccommodation.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                DaysBeforeCancelling.ToString(),
                RecentlyRenovated.ToString(),

            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            AccommodationID = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Name = values[2];
            Location = Location.fromStringToLocation(values[3]);
            TypeOfAccommodation = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            DaysBeforeCancelling = Convert.ToInt32(values[7]);
            RecentlyRenovated = Convert.ToBoolean(values[8]);
        }


        public string AccommodationName
        {
            get => Name;
            set
            {
                if (value != Name)
                {
                    Name = value;
                    OnPropertyChanged("AccommodationName");
                }
            }
        }
        public string AccommodationCountry
        {
            get { return Location.Country; }
            set
            {
                if (value != Location.Country)
                {
                    Location.Country = value;
                    OnPropertyChanged("AccommodationCountry");
                }
            }
        }

        public string AccommodationCity
        {
            get { return Location.City; }
            set
            {
                if (value != Location.City)
                {
                    Location.City = value;
                    OnPropertyChanged("AccommodationCity");
                }
            }
        }



        public int AccommodationMaxGuests
        {
            get => MaxGuestNumber;
            set
            {
                if (value != MaxGuestNumber)
                {
                    MaxGuestNumber = value;
                    OnPropertyChanged("AccommodationMaxGuests");
                }
            }
        }

        public int AccommodationReservationMinDays
        {
            get => MinReservationDays;
            set
            {
                if (value != MinReservationDays)
                {

                    MinReservationDays = value;
                    OnPropertyChanged("AccommodationReservationMinDays");
                }
            }
        }

        public int AccommodationCancelationDays
        {
            get => DaysBeforeCancelling;
            set
            {
                if (value != DaysBeforeCancelling)
                {
                    DaysBeforeCancelling = value;
                    OnPropertyChanged("AccommodationCancelationDays");
                }
            }
        }

        public AccommodationType AccommodationType
        {
            get { return TypeOfAccommodation; }
            set
            {
                if (value != TypeOfAccommodation)
                {
                    TypeOfAccommodation = value;
                    OnPropertyChanged("AccommodationType");
                }
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                this.ValidationErrors["AccommodationName"] = "Name cannot be empty.";
            }
            if (string.IsNullOrWhiteSpace(this.Location.Country))
            {
                this.ValidationErrors["AccommodationCountry"] = "Country must be selected";
            }
            if (string.IsNullOrWhiteSpace(this.Location.City))
            {
                this.ValidationErrors["AccommodationCity"] = "City must be selected";
            }
            if(this.MaxGuestNumber == 0)
            {
                this.ValidationErrors["AccommodationMaxGuests"] = "Please enter a number";
            }
            if(this.DaysBeforeCancelling == 0)
            {
                this.ValidationErrors["AccommodationCancelationDays"] = "Please enter a number";
            }
            if(this.MinReservationDays== 0)
            {
                this.ValidationErrors["AccommodationReservationMinDays"] = "Please enter a number";
            }
            if (this.AccommodationType.ToString() != "Appartment" && this.AccommodationType.ToString() !="Shack" && this.AccommodationType.ToString() != "House")
            {
                this.ValidationErrors["AccommodationType"] = "Invalid type";
            }
        }

    }
}
