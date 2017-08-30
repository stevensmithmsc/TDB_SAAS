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
    public class BoroughsController : Controller
    {
        private TrainDB db = new TrainDB();

        // GET: Boroughs
        public ActionResult Index()
        {
            var boroughs = db.Boroughs.OrderBy(b => b.BoroughName);
            return View(boroughs.ToList());
        }

        // GET: Boroughs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borough borough = db.Boroughs.Find(id);
            if (borough == null)
            {
                return HttpNotFound();
            }
            return View(borough);
        }

        // GET: Boroughs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boroughs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BoroughName,LongName,ShortName")] Borough borough)
        {
            if (ModelState.IsValid)
            {
                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                borough.Creator = userperson;
                borough.Created = DateTime.Now;
                borough.Modifier = userperson;
                borough.Modified = DateTime.Now;
                db.Boroughs.Add(borough);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borough);
        }

        // GET: Boroughs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borough borough = db.Boroughs.Find(id);
            if (borough == null)
            {
                return HttpNotFound();
            }
            return View(borough);
        }

        // POST: Boroughs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BoroughName,LongName,ShortName")] Borough borough)
        {
            if (ModelState.IsValid)
            {
                Borough boroughInDB = db.Boroughs.Single(b => b.ID == borough.ID);
                boroughInDB.BoroughName = borough.BoroughName;
                boroughInDB.LongName = borough.LongName;
                boroughInDB.ShortName = borough.ShortName;

                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                boroughInDB.Modifier = userperson;
                boroughInDB.Modified = DateTime.Now;
                //db.Entry(borough).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(borough);
        }

        // GET: Boroughs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borough borough = db.Boroughs.Find(id);
            if (borough == null)
            {
                return HttpNotFound();
            }
            return View(borough);
        }

        // POST: Boroughs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Borough borough = db.Boroughs.Find(id);
            db.Boroughs.Remove(borough);
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
