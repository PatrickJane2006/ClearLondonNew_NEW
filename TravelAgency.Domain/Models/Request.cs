using System;

namespace TravelAgency.Domain.Models
{
    public class Request
    {
        public Guid Id { get; set; }

        public Guid Id_User { get; set; }

        public string Description { get; set; }

        public string Path_Img { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
