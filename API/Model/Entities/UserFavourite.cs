using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Entities
{
    public class UserFavourite
    {
        public int MediaId { get; set; }

        public Media MyProperty { get; set; }
    }
}