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
    public class CohortsController : Controller
    {
        private TrainDB db = new TrainDB();

        // GET: Cohorts
        public ActionResult Index()
        {
            var cohorts = db.Cohorts.OrderBy(c => c.Number);
            return View(cohorts.ToList());
        }

        // GET: Cohorts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cohort cohort = db.Cohorts.Find(id);
            if (cohort == null)
            {
                return HttpNotFound();
            }
            return View(cohort);
        }

        // GET: Cohorts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cohorts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Code,CohortDescription,Number,Notes")] Cohort cohort)
        {
            if (ModelState.IsValid)
            {
                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                cohort.Creator = userperson;
                cohort.Created = DateTime.Now;
                cohort.Modifier = userperson;
                cohort.Modified = DateTime.Now;
                db.Cohorts.Add(cohort);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cohort);
        }

        // GET: Cohorts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cohort cohort = db.Cohorts.Find(id);
            if (cohort == null)
            {
                return HttpNotFound();
            }
            return View(cohort);
        }

        // POST: Cohorts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Code,CohortDescription,Number,Notes")] Cohort cohort)
        {
            if (ModelState.IsValid)
            {
                Cohort cohortInDB = db.Cohorts.Single(c => c.ID == cohort.ID);
                cohortInDB.Code = cohort.Code;
                cohortInDB.CohortDescription = cohort.CohortDescription;
                cohortInDB.Number = cohort.Number;
                cohortInDB.Notes = cohort.Notes;

                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                cohortInDB.Modifier = userperson;
                cohortInDB.Modified = DateTime.Now;
                // db.Entry(cohort).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cohort);
        }

        // GET: Cohorts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cohort cohort = db.Cohorts.Find(id);
            if (cohort == null)
            {
                return HttpNotFound();
            }
            return View(cohort);
        }

        // POST: Cohorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cohort cohort = db.Cohorts.Find(id);
            db.Cohorts.Remove(cohort);
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
