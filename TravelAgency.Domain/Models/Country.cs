using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Domain.Models
{
    public class Country
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path_img { get; set; }

        public int Count_Services { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
