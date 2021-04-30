using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGReservation.Models
{
    public class UserPG
    {
        public int UserPGId { set; get; }
       // public int UserId { set; get; }
        // public  int PGID { set; get; }
        public virtual User user { get; set; }
        public virtual ICollection<PGRegistration> PgRegistrations { get; set; }
    }
}