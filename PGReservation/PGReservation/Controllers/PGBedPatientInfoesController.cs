using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PGReservation.Models;

namespace PGReservation.Controllers
{
    public class PGBedPatientInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PGBedPatientInfoes
        public ActionResult Index(int? bedId)
        {
            return View(db.PgBedPatientInfo.ToList().FindAll(x => x.PgBed.BedID == bedId));
        }

        // GET: PGBedPatientInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGBedPatientInfo pGBedPatientInfo = db.PgBedPatientInfo.Find(id);
            if (pGBedPatientInfo == null)
            {
                return HttpNotFound();
            }
            return View(pGBedPatientInfo);
        }

        // GET: PGBedPatientInfoes/Create
        public ActionResult Create(int? id)
        {
            PGBedPatientInfo mdl = new PGBedPatientInfo();
            mdl.BedID =  id.Value;
            return View(mdl);
        }

        // POST: PGBedPatientInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PGBedPatientId,PatientName,PatientPhone,PatientAddress,State,District,City,Pincode,PatientIdTypeValue,PatientStatus,Notes,PatientAdmittedOnDate,PatientDischargedOnDate,BedID")] PGBedPatientInfo pGBedPatientInfo)
        {
            if (ModelState.IsValid)
            {
                db.PgBedPatientInfo.Add(pGBedPatientInfo);
                db.SaveChanges();
                return RedirectToAction("Index","PGRegistrations");
            }

            return View(pGBedPatientInfo);
        }

        // GET: PGBedPatientInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGBedPatientInfo pGBedPatientInfo = db.PgBedPatientInfo.Find(id);
            if (pGBedPatientInfo == null)
            {
                return HttpNotFound();
            }
            return View(pGBedPatientInfo);
        }

        // POST: PGBedPatientInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PGBedPatientId,PatientName,PatientPhone,PatientAddress,State,District,City,Pincode,PatientIdTypeValue,PatientStatus,Notes,PatientAdmittedOnDate,PatientDischargedOnDate")] PGBedPatientInfo pGBedPatientInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pGBedPatientInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pGBedPatientInfo);
        }

        // GET: PGBedPatientInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGBedPatientInfo pGBedPatientInfo = db.PgBedPatientInfo.Find(id);
            if (pGBedPatientInfo == null)
            {
                return HttpNotFound();
            }
            return View(pGBedPatientInfo);
        }

        // POST: PGBedPatientInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PGBedPatientInfo pGBedPatientInfo = db.PgBedPatientInfo.Find(id);
            db.PgBedPatientInfo.Remove(pGBedPatientInfo);
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
