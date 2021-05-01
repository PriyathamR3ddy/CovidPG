using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGReservation.Models
{
    public class PGRegisterVM: PGRegistration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}