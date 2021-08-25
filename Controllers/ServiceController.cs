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
    public class ServiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Service
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Admin);
            if (User.IsInRole("IsAdmin"))
                return View(services.ToList());
            else
            {

                //var myrecords = db.Volunteers.Where(x => x.User_Id == uuid );//myrecords
                //var myServices= 
                return View("UserIndex", services.ToList());
            }

        }

        // GET: Service/ReportService
        public ActionResult ReportService()
        {
            var services = from s in db.Services.Include(s => s.Admin)
                           where (DbFunctions.TruncateTime(s.Start_Date) >= System.DateTime.Today.Date)
                           orderby s.Start_Date
                           select s; ;
            if (User.IsInRole("IsAdmin"))
            {
                ViewBag.Message = "Current Ongoing Services";
                return View(services.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        // POST: Service/ReportService
        [HttpPost]
        public ActionResult ReportService(DateTime fromdate, DateTime Todate)
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
        // GET: Service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "First_Name");
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Service_Id,Admin_Id,Service_Name,Service_Description,Reqired_Volunteer,Start_Date,Participated_Volunteer")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "First_Name", service.Admin_Id);
            return View(service);
        }

        // GET: Service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "First_Name", service.Admin_Id);
            return View(service);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Service_Id,Admin_Id,Service_Name,Service_Description,Reqired_Volunteer,Start_Date,Participated_Volunteer")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "First_Name", service.Admin_Id);
            return View(service);
        }

        // GET: Service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Service/Participate/5
        public ActionResult Participate(int? id)
        {
            String uemail = User.Identity.Name;
            User u = db.Users.SingleOrDefault(us => us.Email == uemail);
            int uuid = u.User_Id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer v = db.Volunteers.Where(x => x.User_Id == uuid
                     && x.Service_Id == id
                             ).FirstOrDefault();
            if (v != null)
            {
                ViewBag.Flag = "AlreadyExist";
                return View("ParticipationDetails", v);
            }
            else
            {
                Volunteer volunteer = new Volunteer() { User_Id = uuid };

                return View(volunteer);
            }
        }

        // POST: Service/Participate/5
        [HttpPost, ActionName("Participate")]
        [ValidateAntiForgeryToken]
        public ActionResult ParticipateConfirmed(int id, [Bind(Include = "Volunteer_Id,User_Id")] Volunteer volunteer)
        {
            Service service = db.Services.Find(id);

            Volunteer v = db.Volunteers.Where(x => x.User_Id == volunteer.User_Id
                      && x.Service_Id == id
                              ).FirstOrDefault();
            if (v == null)
            {

                if (service.Participated_Volunteer < service.Reqired_Volunteer)
                {
                    volunteer.Service_Id = id;
                    //Volunteer volunteer = new Volunteer() { Service_Id = id, User_Id = Int16.Parse(User.Identity.GetUserId())};
                    db.Volunteers.Add(volunteer);
                    db.SaveChanges();
                    service.Participated_Volunteer += 1;
                    db.Entry(service).State = EntityState.Modified;
                    db.SaveChanges();
                    // ViewBag.Message = "Participated for service" + id + "Sucessfully";
                    return View("ParticipationDetails", volunteer);
                }
                else
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {


                return View("ParticipationDetails", v);
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
