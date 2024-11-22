using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAgency.Domain.Enum;
using TravelAgency.Domain.Models;


namespace TravelAgency.Domain.ModelsDb
{

    [Table("users")]
    public class UsersDb
    {
        [Column("id")]

        public Guid Id { get; set; }

        [Column("login")]

        public string Login { get; set; }

        [Column("password")]

        public string Password { get; set; }

        [Column("email")]

        public string Email { get; set; }

        [Column("role")]

        public Role Role { get; set; }

        [Column("path_img")]

        public string Path_Img { get; set; }


        [Column("createdAt", TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }

        public ICollection<RequestsDb> requestsDb { get; set; }
        public ICollection<OrdersDb> ordersDb { get; set; }
    }
}
