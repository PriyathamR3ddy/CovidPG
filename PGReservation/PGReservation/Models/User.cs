using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGReservation.Models
{
    public class User
    {
        public int UserId { set; get; }

        public string UserName { set; get; }

        public string Email { set; get; }

        public string Phone { set; get; }

        public string Password { set; get; }
    }
}