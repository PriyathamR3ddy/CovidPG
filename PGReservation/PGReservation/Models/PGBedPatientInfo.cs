using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace PGReservation.Models
{
    public class PGBedPatientInfo
    {
        public int PGBedPatientId { set; get; }
       // public int BedId { set; get; }
        public string PatientName { set; get; }
         public string PatientPhone { set; get; }
        public string PatientAddress { set; get; }

        public string State { set; get; }
        public string District { set; get; }
        public string City {  set; get; }
        public string Pincode { set; get; }
      //  public int PatientTypeId { set; get; }
        public string PatientIdTypeValue { set; get; }
        public string PatientStatus { set; get; }
        public string Notes { set; get; }
        public DateTime PatientAdmittedOnDate { set; get; }
        public DateTime PatientDischargedOnDate { set; get; }

        public virtual PGBeds PgBed { get; set; }
        public virtual PatientIdType PatientType { get; set; }
    }
}