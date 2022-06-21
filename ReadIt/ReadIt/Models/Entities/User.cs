using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadIt.Models.Entities
{
    public class User : IdentityUser <int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public string username { get; set; }
    }
}

