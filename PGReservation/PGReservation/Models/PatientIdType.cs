

using System.ComponentModel.DataAnnotations;

namespace PGReservation.Models
{
    public class PatientIdType
    {
        [Key]
        public int TypeId { set; get; }
      public string TypeName { set; get; }
    }
}