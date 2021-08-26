using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using websitee.Models;
using websitee.ViewModels;
namespace websitee.Controllers
{
    public class GrivanancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        List<SelectListItem> Type = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "Complaint", Value = "Complaint"},
            new SelectListItem{ Text = "Suggestion", Value = "Suggestion"},

        };
        List<SelectListItem> Status = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "New", Value = "New"},



        };



        public ActionResult Report()
        {
            //var grivanaces = from g in db.Grivanances where (g.Status == "New"|| g.Status == "In Progress") orderby g.Created_On select g;


            return View();
        }


        List<SelectListItem> UserCompletionStatus = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "Complete", Value = "Complete"},



        };
        List<SelectListItem> AdminStatus = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "In Progress", Value = "In Progress"},
            new SelectListItem{ Text = "Resolved", Value = "Resolved"},


        };



        // GET: Grivanances
        public ActionResult Index()
        {
            if (User.IsInRole("IsAdmin"))
            {
                var grivanaces = from g in db.Grivanances where (g.Status == "New" || g.Status == "In Progress") orderby g.Created_On select g;
                return View(grivanaces.ToList());

            }
            else
            {
                String uemail = User.Identity.Name;
                User u = db.Users.SingleOrDefault(us => us.Email == uemail);
                int uuid = u.User_Id;
                var grivanaces = from g in db.Grivanances where (g.User_Id == uuid) orderby g.Status, g.Created_On select g;
                return View("UserIndex", db.Grivanances.ToList());
            }

        }
        public ActionResult List()
        {
            return View(db.Grivanances.ToList());


        }



        // GET: Service/ReportService
        public ActionResult ReportGrivanance()
        {

            if (User.IsInRole("IsAdmin"))
            {
                ViewBag.Message = "History of Grievance";
                return View(db.Grivanances.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        // POST: Service/ReportService
        [HttpPost]
        public ActionResult ReportGrivanance(DateTime fromdate, DateTime Todate)
        {
            var grivanances = from a in db.Grivanances where (DbFunctions.TruncateTime(a.Created_On) >= fromdate.Date && DbFunctions.TruncateTime(a.Created_On) <= Todate.Date) select a;
            if (User.IsInRole("IsAdmin"))
            {
                ViewBag.Message = "Showing  Grievance  From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
                return View(grivanances.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }




        // GET: Grivanances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grivanance grivanance = db.Grivanances.Find(id);
            if (grivanance == null)
            {
                return HttpNotFound();
            }
            return View(grivanance);
        }

        // GET: Grivanances/Create
        public ActionResult Create()
        {
            ViewBag.Type = Type;
            ViewBag.Status = Status;
            return View("CreateGrivence");
        }

        // POST: Grivanances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGrivenceViewModel cgv)
        {
            String uemail = User.Identity.Name;
            User u = db.Users.SingleOrDefault(us => us.Email == uemail);
            int uuid = u.User_Id;
            String status = "New";
            DateTime created_On = System.DateTime.Now;
            DateTime expected_Resolution_Date = created_On.AddDays(3);
            Grivanance g = new Grivanance() { User_Id = uuid, Topic = cgv.Topic, Details = cgv.Details, Sub_Topic = cgv.Sub_Topic, Type = cgv.Type, Status = status, Created_On = created_On, Expected_Resolution_Date = expected_Resolution_Date };


            // grivanance.Type = "Complaint";

            if (ModelState.IsValid)
            {


                db.Grivanances.Add(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateGrivence", g);
        }

        // GET: Grivanances/Resolve/5
        public ActionResult Resolve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grivanance grivanance = db.Grivanances.Find(id);
            if (grivanance == null)
            {
                return HttpNotFound();
            }
            grivanance.Status = "In Progress";
            db.Entry(grivanance).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Status = AdminStatus;
            return View(grivanance);
        }

        // POST: Grivanances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resolve(Grivanance grivanance)
        {

            if (ModelState.IsValid)
            {
                grivanance.Status = "Resolved";
                db.Entry(grivanance).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(grivanance);
        }





        // GET: Grivanances/Delete/5
        public ActionResult Complete(int? id)
        {

            String uid = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grivanance grivanance = db.Grivanances.Find(id);
            if (grivanance == null)
            {
                return HttpNotFound();
            }
            return View(grivanance);
        }

        // POST: Grivanances/Delete/5
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteConfirmed(int id)
        {
            Grivanance grivanance = db.Grivanances.Find(id);
            grivanance.Status = "Complete";
            db.SaveChanges();
            return RedirectToAction("Index");
        }














        // GET: Grivanances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grivanance grivanance = db.Grivanances.Find(id);
            if (grivanance == null)
            {
                return HttpNotFound();
            }
            return View(grivanance);
        }

        // POST: Grivanances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grivanance grivanance = db.Grivanances.Find(id);
            db.Grivanances.Remove(grivanance);
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