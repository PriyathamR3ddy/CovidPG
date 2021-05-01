
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PGReservation.Models;

namespace PGReservation.Controllers
{
    public class PGRegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PGRegistrations
        public ActionResult Index()
        {
            return View(db.PgRegistrations.ToList());
        }

        // GET: PGRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGRegistration pGRegistration = db.PgRegistrations.Find(id);
            if (pGRegistration == null)
            {
                return HttpNotFound();
            }
            return View(pGRegistration);
        }

        // GET: PGRegistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PGRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PGID,PGName,ContactPerson,Phone,Address,State,District,City,PinCode,GmapLocation,NoOfBeds")] PGRegistration pGRegistration)
        {
            if (ModelState.IsValid)
            {
                db.PgRegistrations.Add(pGRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pGRegistration);
        }

        // GET: PGRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGRegistration pGRegistration = db.PgRegistrations.Find(id);
            if (pGRegistration == null)
            {
                return HttpNotFound();
            }
            return View(pGRegistration);
        }

        public PartialViewResult PgBedInfo(int? PGID)
        {
            //if (PGID.HasValue)
            //{
                var model = db.PgBeds.Select(x => x.PgRegistration.PGID == PGID);
                return PartialView("PgBedsPartialView", model);
           // }

            
        }

        public ActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient([Bind(Include = "PGBedPatientId,PatientName,PatientPhone,PatientAddress,State,District,City,Pincode,PatientIdTypeValue,PatientStatus,Notes,PatientAdmittedOnDate,PatientDischargedOnDate")] PGBedPatientInfo patientInfo)
        {
            if (ModelState.IsValid)
            {
                db.PgBedPatientInfo.Add(patientInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientInfo);
        }


        // POST: PGRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PGID,PGName,ContactPerson,Phone,Address,State,District,City,PinCode,GmapLocation,NoOfBeds")] PGRegistration pGRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pGRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pGRegistration);
        }

        // GET: PGRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGRegistration pGRegistration = db.PgRegistrations.Find(id);
            if (pGRegistration == null)
            {
                return HttpNotFound();
            }
            return View(pGRegistration);
        }

        // POST: PGRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PGRegistration pGRegistration = db.PgRegistrations.Find(id);
            db.PgRegistrations.Remove(pGRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
