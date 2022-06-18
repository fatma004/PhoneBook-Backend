using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PicturePath { get; set; }
        public string? Email { get; set; }
        public string UserId { get; set; }

        [JsonIgnore, ForeignKey("UserId")]
        public AppUser User { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

    }
}
