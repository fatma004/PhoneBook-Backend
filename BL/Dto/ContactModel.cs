using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PicturePath { get; set; }
        public string? Email { get; set; }
        public string UserId { get; set; }
        public virtual List<PhoneModel> Phones { get; set; }

    }
}
