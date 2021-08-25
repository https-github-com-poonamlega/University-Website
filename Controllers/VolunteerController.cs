using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using websitee.Models;

namespace websitee.Controllers
{
    public class VolunteerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Volunteer
        public ActionResult Index()
        {
            var volunteers = db.Volunteers.Include(v => v.Service).Include(v => v.User);
            return View(volunteers.ToList());
        }


        // GET: Volunteer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // GET: Volunteer/Create
        public ActionResult Create()
        {
            ViewBag.Service_Id = new SelectList(db.Services, "Service_Id", "Service_Name");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name");
            return View();
        }

        // POST: Volunteer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Volunteer_Id,User_Id,Service_Id")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Volunteers.Add(volunteer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Service_Id = new SelectList(db.Services, "Service_Id", "Service_Name", volunteer.Service_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", volunteer.User_Id);
            return View(volunteer);
        }

        // GET: Volunteer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Service_Id = new SelectList(db.Services, "Service_Id", "Service_Name", volunteer.Service_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", volunteer.User_Id);
            return View(volunteer);
        }

        // POST: Volunteer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Volunteer_Id,User_Id,Service_Id")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Service_Id = new SelectList(db.Services, "Service_Id", "Service_Name", volunteer.Service_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", volunteer.User_Id);
            return View(volunteer);
        }

        // GET: Volunteer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);

            Service service = db.Services.Find(volunteer.Service_Id);

            db.Volunteers.Remove(volunteer);
            service.Participated_Volunteer -= 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Volunteer/ReportService
        public ActionResult ReportVolunteer()
        {
            var volunteer = db.Volunteers.Include(v => v.Service).Include(v => v.User);

            if (User.IsInRole("IsAdmin"))
            {


                return View(volunteer.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        // POST: Volunteer/ReportService
        [HttpPost]
        public ActionResult ReportVolunteer(DateTime fromdate, DateTime Todate)
        {
            var services = from a in db.Services where (DbFunctions.TruncateTime(a.Start_Date) >= fromdate.Date && DbFunctions.TruncateTime(a.Start_Date) <= Todate.Date) select a;
            if (User.IsInRole("IsAdmin"))
            {
                ViewBag.Message = "Showing Services  From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
                return View(services.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
    }
}
