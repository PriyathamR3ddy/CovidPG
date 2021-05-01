using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PGReservation.Models;

namespace PGReservation.Controllers
{
    public class PGBedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PGBeds
        public ActionResult Index(int? PgId)
        {
            return View(db.PgBeds.Select(x=>x.PgRegistration.PGID == PgId).ToList());
        }

        // GET: PGBeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGBeds pGBeds = db.PgBeds.Find(id);
            if (pGBeds == null)
            {
                return HttpNotFound();
            }
            return View(pGBeds);
        }

        // GET: PGBeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PGBeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BedID,BedNo,BedStatus")] PGBeds pGBeds)
        {
            if (ModelState.IsValid)
            {
                db.PgBeds.Add(pGBeds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pGBeds);
        }

        // GET: PGBeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGBeds pGBeds = db.PgBeds.Find(id);
            if (pGBeds == null)
            {
                return HttpNotFound();
            }
            return View(pGBeds);
        }

        // POST: PGBeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BedID,BedNo,BedStatus")] PGBeds pGBeds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pGBeds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pGBeds);
        }

        // GET: PGBeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PGBeds pGBeds = db.PgBeds.Find(id);
            if (pGBeds == null)
            {
                return HttpNotFound();
            }
            return View(pGBeds);
        }

        // POST: PGBeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PGBeds pGBeds = db.PgBeds.Find(id);
            db.PgBeds.Remove(pGBeds);
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
