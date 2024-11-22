using System;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAgency.Domain.Models;


namespace TravelAgency.Domain.ModelsDb
{
    [Table("requests")]
    public class RequestsDb
    {
        [Column("id")]

        public Guid Id { get; set; }

        [Column("id_user")]

        public Guid Id_User { get; set; }

        [Column("description")]

        public string Description { get; set; }

        [Column("path_img")]

        public string Path_Img { get; set; }

        [Column("status")]

        public int Status { get; set; }

        [Column("createdAt", TypeName = "timestamp")]

        public DateTime CreatedAt { get; set; }

        public UsersDb UsersDb { get; set; }


    }
}
