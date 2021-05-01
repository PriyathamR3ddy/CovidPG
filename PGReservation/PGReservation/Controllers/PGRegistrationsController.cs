
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PGReservation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace PGReservation.Controllers
{
    public class PGRegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public PGRegistrationsController()
        {

        }

        public PGRegistrationsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
        public async Task<ActionResult> Create([Bind(Include = "PGID,PGName,ContactPerson,Phone,Address,State,District,City,PinCode,GmapLocation,NoOfBeds,FirstName,LastName,Email")] PGRegisterVM pGRegistration)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = pGRegistration.Email, Email = pGRegistration.Email, FirstName = pGRegistration.FirstName, LastName = pGRegistration.LastName };
                var result = await UserManager.CreateAsync(user, ConfigurationManager.AppSettings["DefaultPwd"]);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, "PGAdmin");

                    PGRegistration pg = new PGRegistration();
                    pg.Address = pGRegistration.Address;
                    pg.City = pGRegistration.City;
                    pg.ContactPerson = pGRegistration.FirstName+ " "+pGRegistration.LastName;
                    pg.District = pGRegistration.District;
                    pg.GmapLocation = pGRegistration.GmapLocation;
                    pg.NoOfBeds = pGRegistration.NoOfBeds;
                    pg.PGID = pGRegistration.PGID;
                    pg.PGName = pGRegistration.PGName;
                    pg.Phone = pGRegistration.Phone;
                    pg.PinCode = pGRegistration.PinCode;
                    pg.State = pGRegistration.State;
                    pg.UserId = user.Id;
                    try
                    {
                        db.PgRegistrations.Add(pg);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    db.UserPg.Add(new UserPG() { UserId = user.Id, PGID = pg.PGID });

                    for (int i = 1; i <= Convert.ToInt32(pGRegistration.NoOfBeds); i++)
                    {
                        PGBeds pGBeds = new PGBeds();
                        pGBeds.BedNo = pGRegistration.PGName + "-" + i;
                        pGBeds.BedStatus = "Available";
                        pGBeds.PgRegistration = pg;
                        db.PgBeds.Add(pGBeds);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
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
            var user = UserManager.FindById(pGRegistration.UserId);

            PGRegisterVM pgvm = new PGRegisterVM();

            pgvm.Address = pGRegistration.Address;
            pgvm.City = pGRegistration.City;
            pgvm.ContactPerson = pGRegistration.ContactPerson;
            pgvm.District = pGRegistration.District;
            pgvm.Email = user.Email;
            pgvm.FirstName = user.FirstName;
            pgvm.GmapLocation = pGRegistration.GmapLocation;
            pgvm.LastName = user.LastName; 
            pgvm.NoOfBeds = pGRegistration.NoOfBeds;
            pgvm.PGID = pGRegistration.PGID;
            pgvm.PGName = pGRegistration.PGName;
            pgvm.Phone = pGRegistration.Phone;
            pgvm.PinCode = pGRegistration.PinCode;
            pgvm.State = pGRegistration.State;
            
            if (pGRegistration == null)
            {
                return HttpNotFound();
            }
            return View(pgvm);
        }

        public PartialViewResult PgBedInfo(int? PGID)
        {
            var model = db.PgBeds.Where(x => x.PgRegistration.PGID == PGID).ToList();
            return PartialView("PgBedsPartialView", model);
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
        public async Task<ActionResult> Edit([Bind(Include = "PGID,PGName,ContactPerson,Phone,Address,State,District,City,PinCode,GmapLocation,NoOfBeds,FirstName,LastName,Email")] PGRegisterVM pGRegistration)
        {
            if (ModelState.IsValid)
            {
                var pgreg = db.PgRegistrations.FirstOrDefault(x => x.PGID == pGRegistration.PGID);
                pgreg.Address = pGRegistration.Address;
                pgreg.City = pGRegistration.City;
                pgreg.ContactPerson = pGRegistration.FirstName + " " + pGRegistration.LastName;
                pgreg.District = pGRegistration.District;
                pgreg.GmapLocation = pGRegistration.GmapLocation;
                pgreg.NoOfBeds = pGRegistration.NoOfBeds;
                pgreg.PGID = pGRegistration.PGID;
                pgreg.PGName = pGRegistration.PGName;
                pgreg.Phone = pGRegistration.Phone;
                pgreg.PinCode = pGRegistration.PinCode;
                pgreg.State = pGRegistration.State;

                db.PgRegistrations.Add(pgreg);
                db.Entry(pGRegistration).State = EntityState.Modified;
                db.SaveChanges();

                ApplicationUser userModel = UserManager.FindById(pgreg.UserId);
                userModel.FirstName = pGRegistration.FirstName;
                userModel.LastName = pGRegistration.LastName;
                var result = await UserManager.UpdateAsync(userModel);

                //db.UserPg.Add(new UserPG() { UserId = userModel.Id,  PGID= pgreg.PGID });
                var pgbeds = db.PgBeds.Where(x => x.PgRegistration.PGID == pGRegistration.PGID)?.Select(x => x.BedID);
                var isbedsoccupied = db.PgBedPatientInfo.Where(x => pgbeds != null && pgbeds.Contains(x.PgBed.BedID)).Count() > 0;
                if (!isbedsoccupied)
                {
                    for (int i = 1; i <= Convert.ToInt32(pGRegistration.NoOfBeds); i++)
                    {
                        PGBeds pGBeds = new PGBeds();
                        pGBeds.BedNo = pGRegistration.PGName + "-" + i;
                        pGBeds.BedStatus = "Available";
                        pGBeds.PgRegistration = pgreg;
                        db.PgBeds.Add(pGBeds);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            // AddErrors(result);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
