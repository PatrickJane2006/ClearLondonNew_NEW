using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid Id_User { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid Id_Service { get; set; }

    }
}
