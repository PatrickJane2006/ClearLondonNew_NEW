using System;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAgency.Domain.Models;


namespace TravelAgency.Domain.ModelsDb
{

    [Table("orders")]
    public class OrdersDb
    {
        [Column("id")]

        public Guid Id { get; set; }

        [Column("id_user")]

        public Guid Id_User { get; set; }

        [Column("name")]

        public string Name { get; set; }

        [Column("price")]

        public decimal Price { get; set; }

        [Column("createdAt", TypeName = "timestamp")]

        public DateTime CreatedAt { get; set; }

        [Column("id_service")]

        public Guid Id_Service { get; set; }

        public UsersDb UsersDb { get; set; }

        public OrdersDb ordersDbs { get; set; }

      

    }
}
