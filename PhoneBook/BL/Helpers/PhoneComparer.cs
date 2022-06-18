using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class PhoneComparer: IEqualityComparer<Phone>
    {
        public bool Equals(Phone x, Phone y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            //If either one of the object refernce is null, return false
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }
            //Comparing all the properties one by one
            return x.Id == y.Id && x.Number == y.Number && x.TypeId == y.TypeId;
        }
        public int GetHashCode([DisallowNull] Phone obj)
        {
            if (obj == null)
            {
                return 0;
            }
            //Get the ID hash code value
            int IDHashCode = obj.Id.GetHashCode();
            //Get the Number HashCode Value
            int NumberHashCode = obj.Number == null ? 0 : obj.Number.GetHashCode();
            //Get the TypeId HashCode Value
            int TypeIdHashCode = obj.TypeId.GetHashCode();
            return IDHashCode ^ NumberHashCode ^ TypeIdHashCode;
        }

    }
}
