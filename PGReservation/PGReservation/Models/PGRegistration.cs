

using System.ComponentModel.DataAnnotations;

namespace PGReservation.Models
{
    public class PGRegistration
    {
        [Key]
        public int PGID { set; get; }
        public string PGName { set; get; }
        public string ContactPerson { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string  State { set; get; }
        public  string District { set; get; }
        public string  City { set; get; }
        public string PinCode { set; get; }
        public string   GmapLocation { set; get; }
        public string NoOfBeds { set; get; }
    }
}