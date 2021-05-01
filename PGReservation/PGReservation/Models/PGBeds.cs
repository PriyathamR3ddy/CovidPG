

using System.ComponentModel.DataAnnotations;

namespace PGReservation.Models
{
    public class PGBeds
    {
        // public int PGId { set; get; }
        [Key]
        public int BedID { set; get; }
        [Display(Name = "Bed No.")]
        public string BedNo { set; get; }
        [Display(Name = "Bed Status")]
        public string BedStatus { get; set; }
        [Display(Name = "Pg Registration")]
        public virtual PGRegistration PgRegistration { get; set; }
    }
}