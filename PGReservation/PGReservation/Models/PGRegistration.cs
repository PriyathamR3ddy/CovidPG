

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGReservation.Models
{
    public class PGRegistration
    {
        [Key]
        public int PGID { set; get; }
        [Display(Name = "PG Name")]
        public string PGName { set; get; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string  State { set; get; }
        public  string District { set; get; }
        public string  City { set; get; }
        public string PinCode { set; get; }
        [Display(Name = "GMap Location")]
        public string   GmapLocation { set; get; }
        [Display(Name = "No.of Beds")]
        public string NoOfBeds { set; get; }

        public string UserId { get; set; }

        [NotMapped]
        public string AvailableBeds { get; set; }
    }
}