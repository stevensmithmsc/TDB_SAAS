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
    public class SubjectivesController : Controller
    {
        private TrainDB db = new TrainDB();

        // GET: Subjectives
        public ActionResult Index()
        {
            var subjectives = db.Subjectives;
            return View(subjectives.ToList());
        }

        // GET: Subjectives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjective subjective = db.Subjectives.Find(id);
            if (subjective == null)
            {
                return HttpNotFound();
            }
            return View(subjective);
        }

        // GET: Subjectives/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Subjectives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,SubName")] Subjective subjective)
        {
            if (ModelState.IsValid)
            {
                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                subjective.Creator = userperson;
                subjective.Created = DateTime.Now;
                subjective.Modifier = userperson;
                subjective.Modified = DateTime.Now;
                db.Subjectives.Add(subjective);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(subjective);
        }

        // GET: Subjectives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjective subjective = db.Subjectives.Find(id);
            if (subjective == null)
            {
                return HttpNotFound();
            }
            
            return View(subjective);
        }

        // POST: Subjectives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code,SubName")] Subjective subjective)
        {
            if (ModelState.IsValid)
            {
                Subjective subInDB = db.Subjectives.Single(s => s.Code == subjective.Code);
                subInDB.SubName = subjective.SubName;

                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                subInDB.Modifier = userperson;
                subInDB.Modified = DateTime.Now;

                //db.Entry(subjective).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(subjective);
        }

        // GET: Subjectives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjective subjective = db.Subjectives.Find(id);
            if (subjective == null)
            {
                return HttpNotFound();
            }
            return View(subjective);
        }

        // POST: Subjectives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subjective subjective = db.Subjectives.Find(id);
            db.Subjectives.Remove(subjective);
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
