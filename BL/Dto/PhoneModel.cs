﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class PhoneModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int TypeId { get; set; }
        public int ContactId { get; set; }

    }
}
