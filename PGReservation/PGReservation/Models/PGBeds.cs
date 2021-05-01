

using System.ComponentModel.DataAnnotations;

namespace PGReservation.Models
{
    public class PGBeds
    {
        // public int PGId { set; get; }
        [Key]
        public int BedID { set; get; }
        public string BedNo { set; get; }
        public string BedStatus { get; set; }
        public virtual PGRegistration PgRegistration { get; set; }
    }
}