﻿using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace TravelAgency.Domain.ModelsDb
{

    [Table("picture_services")]
    public class Picture_servicesDb
    {
        [Column("id")]

        public Guid Id { get; set; }

        [Column("id_service")]

        public Guid Id_Service { get; set; }

        [Column("path_img")]

        public string Path_Img { get; set; }


    }
}