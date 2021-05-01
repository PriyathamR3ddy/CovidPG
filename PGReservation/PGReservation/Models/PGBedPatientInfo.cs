using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace PGReservation.Models
{
    public class PGBedPatientInfo
    {
        public PGBedPatientInfo()
        {
            PgBed = new PGBeds();
        }
        [Key]
        public int PGBedPatientId { set; get; }
        // public int BedId { set; get; }
        [Display(Name = "Patient Name")]
        public string PatientName { set; get; }
        [Display(Name = "Patient Phone")]
        public string PatientPhone { set; get; }
        [Display(Name = "Patient Address")]
        public string PatientAddress { set; get; }
        public string State { set; get; }
        public string District { set; get; }
        public string City {  set; get; }
        public string Pincode { set; get; }
        [Display(Name = "Patient TypeID")]
        public int PatientIdType { set; get; }
        [Display(Name = "PatientID Type Value")]
        public string PatientIdTypeValue { set; get; }
        [Display(Name = "Status")]
        public string PatientStatus { set; get; }
        public string Notes { set; get; }
        [Display(Name = "Admitted On Date")]
        public DateTime PatientAdmittedOnDate { set; get; }
        [Display(Name = "Discharged On Date")]
        public DateTime PatientDischargedOnDate { set; get; }
        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }
        public string Gender { get; set; }
        public virtual PGBeds PgBed { set; get; }
        public virtual PatientIdType PatientType { get; set; }

        [NotMapped]
        public int BedID { get; set; }
    }
}