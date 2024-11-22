using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Domain.ModelsDb
{

    [Table("countries")]
    public class CountriesDb
    {
        [Column("id")]

        public Guid Id { get; set; }

        [Column("name")]

        public string Name { get; set; }

        [Column("path_img")]

        public string Path_img { get; set; }

        [Column("count_services")]

        public int Count_Services { get; set; }

        [Column("createdAt", TypeName = "timestamp")]

        public DateTime CreatedAt { get; set; }

        public ServicesDb Services { get; set; }


    }
}
