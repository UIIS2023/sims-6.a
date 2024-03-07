namespace InitialProject.Domen.CustomClasses
{
    using System;
    using static InitialProject.Domen.Model.ComplexTourRequest;

    public class ComplexTourRequestDTO
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime StartingDateTime { get; set; }

        public ComplexTourRequestDTO(int tourId, string name, Status status, DateTime date) {
            TourId = tourId;
            Name = name;
            Status = status;
            StartingDateTime = date;
        }

    }
}
