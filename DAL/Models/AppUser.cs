using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
   public class AppUser : IdentityUser
    {
        public virtual ICollection<Contact>? Contacts { get; set; }
    }
}
