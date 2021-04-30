

namespace PGReservation.Models
{
    public class PGBeds
    {
       // public int PGId { set; get; }
        public int BedID { set; get; }
        public int BedNo { set; get; }
        public string BedStatus { get; set; }
        public virtual PGRegistration PgRegistration { get; set; }
    }
}