using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAgency.Domain.ModelsDb;



namespace TravelAgency.Domain.ModelsDb
{

    [Table("services")]
    public class ServicesDb
    {
        [Column("id")]

        public Guid Id { get; set; }

        [Column("id_country")]

        public Guid Id_country { get; set; }

        [Column("city")]

        public string City { get; set; }

        [Column("name_service")]

        public string Name_Service { get; set; }

        [Column("cleaning_apartment_price")]

        public decimal Cleaning_Apartment_Price { get; set; }

        [Column("cleaning_home_price")]
        public decimal Cleaning_Home_Price { get; set; }

        [Column("cleaning_area_price")]
        public decimal Cleaning_Area_Price { get; set; }


        [Column("cleaning_office_price")]

        public decimal Cleaning_Office_Price { get; set; }

        [Column("cleaning_construction_price")]

        public decimal Cleaning_Construction_Price { get; set; }

        [Column("cleaning_garden_price")]

        public decimal Cleaning_Garden_Price { get; set; }

        [Column("path_img")]

        public string Path_Img { get; set; }

        [Column("createdAt", TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }


        public ICollection<CountriesDb> countriesDb { get; set; }

        public ICollection<OrdersDb> ordersDb { get; set; }

        public Picture_servicesDb pictureServices { get; set; }

    }
}
