using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;

namespace TDB_SAAS.Controllers
{
    public class ServicesController : Controller
    {
        private TrainDB db = new TrainDB();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Parent);
            return View(services.ToList());
        }

        // GET: Services/Details/5
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

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.Services, "ID", "ServiceName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ServiceName,Level,Display,ParentID")] Service service)
        {
            if (ModelState.IsValid)
            {
                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                service.Creator = userperson;
                service.Created = DateTime.Now;
                service.Modifier = userperson;
                service.Modified = DateTime.Now;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.Services, "ID", "ServiceName", service.ParentID);
            return View(service);
        }

        // GET: Services/Edit/5
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
            ViewBag.ParentID = new SelectList(db.Services, "ID", "ServiceName", service.ParentID);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ServiceName,Level,Display,ParentID")] Service service)
        {
            if (ModelState.IsValid)
            {
                Service serviceInDB = db.Services.Single(s => s.ID == service.ID);
                serviceInDB.ServiceName = service.ServiceName;
                serviceInDB.Level = service.Level;
                serviceInDB.Display = service.Display;
                serviceInDB.ParentID = service.ParentID;

                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                serviceInDB.Modifier = userperson;
                serviceInDB.Modified = DateTime.Now;

                //db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Services, "ID", "ServiceName", service.ParentID);
            return View(service);
        }

        // GET: Services/Delete/5
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

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
