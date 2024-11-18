using System;

namespace TravelAgency.Domain.Models
{
    public class Service
    {
       
        public Guid Id { get; set; }

        public Guid Id_country { get; set; }

        public string City { get; set; }

        public string Name_Company { get; set; }

        public decimal Cleaning_Apartment_Price { get; set; }

        public decimal Cleaning_Home_Price { get; set; }

        public decimal Cleaning_Area_Price { get; set; }

        public decimal Cleaning_Office_Price { get; set; }

        public decimal Cleaning_Construction_Price { get; set; }

        public decimal Cleaning_Garden_Price { get; set; }

        public string Path_Img { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
