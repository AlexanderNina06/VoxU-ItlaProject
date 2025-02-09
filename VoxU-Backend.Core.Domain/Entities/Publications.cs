﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Domain.Entities
{
    public class Publications
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? Created_At { get; set; }
        public string? UserId { get; set; }
        public int? userPicture { get; set; }
        public int? userName { get; set; }
        public ICollection<Comments>? Comments {get; set;}

    }
}
