
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
            List<PGRegistration> pglist = new List<PGRegistration>();
            if (User.IsInRole("SuperAdmin"))
                pglist = db.PgRegistrations.ToList();
            else
            {
                string userId = User.Identity.GetUserId();
                pglist = db.PgRegistrations.Where(x => x.UserId != null && x.UserId == userId).ToList();
            }

            foreach(var pg in pglist)
            {
                pg.AvailableBeds = db.PgBeds.Where(x => x.PgRegistration.PGID == pg.PGID && x.BedStatus == "Available").Count() + "/" + pg.NoOfBeds;
            }

            return View(pglist);
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
                IdentityResult result = new IdentityResult();
                ApplicationUser usercheck = UserManager.FindByEmail(pGRegistration.Email);
                if (usercheck == null)
                {
                    usercheck = new ApplicationUser { UserName = pGRegistration.Email, Email = pGRegistration.Email, FirstName = pGRegistration.FirstName, LastName = pGRegistration.LastName };
                    result = await UserManager.CreateAsync(usercheck, ConfigurationManager.AppSettings["DefaultPwd"]);
                    if (result.Succeeded)
                    {
                        await this.UserManager.AddToRoleAsync(usercheck.Id, "PGAdmin");
                    }
                }

                //if (result.Succeeded)
                {
                    PGRegistration pg = new PGRegistration();
                    pg.Address = pGRegistration.Address;
                    pg.City = pGRegistration.City;
                    pg.ContactPerson = pGRegistration.FirstName + " " + pGRegistration.LastName;
                    pg.District = pGRegistration.District;
                    pg.GmapLocation = pGRegistration.GmapLocation;
                    pg.NoOfBeds = pGRegistration.NoOfBeds;
                    pg.PGID = pGRegistration.PGID;
                    pg.PGName = pGRegistration.PGName;
                    pg.Phone = pGRegistration.Phone;
                    pg.PinCode = pGRegistration.PinCode;
                    pg.State = pGRegistration.State;
                    pg.UserId = usercheck.Id;
                    try
                    {
                        db.PgRegistrations.Add(pg);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    db.UserPg.Add(new UserPG() { UserId = usercheck.Id, PGID = pg.PGID });

                    int num = -1;
                    if (int.TryParse(pGRegistration.NoOfBeds, out num))
                    {
                        for (int i = 1; i <= Convert.ToInt32(pGRegistration.NoOfBeds); i++)
                        {
                            PGBeds pGBeds = new PGBeds();
                            pGBeds.BedNo = pGRegistration.PGName + "-" + i;
                            pGBeds.BedStatus = "Available";
                            pGBeds.PgRegistration = pg;
                            db.PgBeds.Add(pGBeds);
                            db.SaveChanges();
                        }
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

                db.PgRegistrations.Attach(pgreg);
                db.Entry(pgreg).State = EntityState.Modified;
                db.SaveChanges();

                ApplicationUser userModel = UserManager.FindById(pgreg.UserId);
                userModel.FirstName = pGRegistration.FirstName;
                userModel.LastName = pGRegistration.LastName;
                var result = await UserManager.UpdateAsync(userModel);

                //db.UserPg.Add(new UserPG() { UserId = userModel.Id,  PGID= pgreg.PGID });
                var pgbeds = db.PgBeds.Where(x => x.PgRegistration.PGID == pGRegistration.PGID)?.Select(x => x.BedID).ToList();
                var isbedsoccupied = pgbeds.Count == 0 ? false : (db.PgBedPatientInfo?.Where(x => pgbeds != null && pgbeds.Contains(x.PgBed.BedID))?.Count() > 0);
                if (!isbedsoccupied)
                {
                    var deletepgbeds = db.PgBeds.Where(x => x.PgRegistration.PGID == pGRegistration.PGID);
                    db.PgBeds.RemoveRange(deletepgbeds);
                    db.SaveChanges();

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

        public ActionResult BedInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return RedirectToAction("Index","PGBeds", new {PgId = id});
        }

        // POST: PGRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pgbedslst = db.PgBeds.Where(x => x.PgRegistration.PGID == id)?.Select(y => y.BedID).ToList();
            if (!db.PgBedPatientInfo.Any(x => pgbedslst.Contains(x.PgBed.BedID)))
            {
                db.PgBeds.RemoveRange(db.PgBeds.Where(x => x.PgRegistration.PGID == id));
                db.SaveChanges();
                PGRegistration pGRegistration = db.PgRegistrations.Find(id);
                db.PgRegistrations.Remove(pGRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Cannot delete", "Error! Unable to delete the PG. PG is in use");
                return View(db.PgRegistrations.Find(id));
            }
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
