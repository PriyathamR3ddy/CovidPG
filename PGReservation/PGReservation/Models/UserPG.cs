using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PGReservation.Models
{
    public class UserPG
    {
        [Key]
        public int UserPGId { set; get; }
        public string UserId { set; get; }
        public int PGID { set; get; }
        public virtual ApplicationUser user { get; set; }
        public virtual ICollection<PGRegistration> PgRegistrations { get; set; }
    }
}