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
        //UserName va a representar la matricula
        // User Va a representar el nombre de usuario
        
        // Falta Biografia 
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
