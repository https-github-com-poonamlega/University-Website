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
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private eventsdb db1 = new eventsdb();

        // GET: Events
        public ActionResult Index()
        {
            if (User.IsInRole("IsAdmin"))
                return View(db.Events.ToList());
            else
            {

                //var myrecords = db.Volunteers.Where(x => x.User_Id == uuid );//myrecords
                //var myServices= 
                return View("UserIndex", db.Events.ToList());
            }
            
        }

        //public PartialViewResult Add(int id)
        //{
        //    ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Id");

        //    return PartialView("_addreaction");
        //}

        //// POST: Events_User_Relation/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public PartialViewResult Add([Bind(Include = "Id,Event_Id,User_Id,Like_Dislike,Interest,Comment")] Events_User_Relation events_User_Relation)
        //{
        //    String uemail = User.Identity.Name;
        //    User u = db.Users.SingleOrDefault(us => us.Email == uemail);
        //    int uuid = u.User_Id;
        //    events_User_Relation.User_Id = uuid;
        //    if (ModelState.IsValid)
        //    {
        //        db1.Events_User_Relation.Add(events_User_Relation);
        //        db1.SaveChanges();

        //    }


        //    ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Id", events_User_Relation.Event_Id);
        //    return PartialView("_addreaction",events_User_Relation);
        //}


        public ActionResult Cards()
        {
            return View(db.Events.ToList());
        }
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_Id,Event_Name,Start_Date,End_Date,Category,Participated_User")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_Id,Event_Name,Start_Date,End_Date,Category,Participated_User")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
