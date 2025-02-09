using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Pesistence.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
       public string? Name { get; set; }
       public string? LastName { get; set; }
       public string? CollegeId { get; set; }
       public string? ProfilePicture { get; set; }
       public DateTime? Created_At { get; set; }
    }
}
