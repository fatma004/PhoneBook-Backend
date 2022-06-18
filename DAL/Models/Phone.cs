

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int TypeId { get; set; }

        [JsonIgnore, ForeignKey("TypeId")]
        public Type Type { get; set; }
        public int ContactId { get; set; }

        [JsonIgnore, ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}
