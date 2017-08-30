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
    public class CostCentresController : Controller
    {
        private TrainDB db = new TrainDB();

        // GET: CostCentres
        public ActionResult Index()
        {
            var costCentres = db.CostCentres;
            return View(costCentres.ToList());
        }

        // GET: CostCentres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCentre costCentre = db.CostCentres.Find(id);
            if (costCentre == null)
            {
                return HttpNotFound();
            }
            return View(costCentre);
        }

        // GET: CostCentres/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: CostCentres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,CCName,Enbld")] CostCentre costCentre)
        {
            if (ModelState.IsValid)
            {
                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                costCentre.Creator = userperson;
                costCentre.Created = DateTime.Now;
                costCentre.Modifier = userperson;
                costCentre.Modified = DateTime.Now;
                db.CostCentres.Add(costCentre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(costCentre);
        }

        // GET: CostCentres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCentre costCentre = db.CostCentres.Find(id);
            if (costCentre == null)
            {
                return HttpNotFound();
            }
            
            return View(costCentre);
        }

        // POST: CostCentres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code,CCName,Enbld")] CostCentre costCentre)
        {
            if (ModelState.IsValid)
            {
                CostCentre ccInDB = db.CostCentres.Single(c => c.Code == costCentre.Code);
                ccInDB.CCName = costCentre.CCName;
                ccInDB.Enbld = costCentre.Enbld;

                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                ccInDB.Modifier = userperson;
                ccInDB.Modified = DateTime.Now;

                //db.Entry(costCentre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(costCentre);
        }

        // GET: CostCentres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCentre costCentre = db.CostCentres.Find(id);
            if (costCentre == null)
            {
                return HttpNotFound();
            }
            return View(costCentre);
        }

        // POST: CostCentres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CostCentre costCentre = db.CostCentres.Find(id);
            db.CostCentres.Remove(costCentre);
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
