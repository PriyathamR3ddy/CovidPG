

using System.Collections;
using System.Collections.Generic;

namespace PGReservation.Models
{
    public class PatientRecords
    {
        public int PatientRecordsId { set; get; }
       // public int PGBedPatientId { set; get; }
        public string FileName { set; get; }
        public string FilePath { set; get; }
        public virtual ICollection<PGBedPatientInfo> PgBedPatientInfo { set; get; }
    }
}