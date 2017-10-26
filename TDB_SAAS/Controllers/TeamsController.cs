using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;
using TDB_SAAS.ViewModels;

namespace TDB_SAAS.Controllers
{
    public class TeamsController : Controller
    {
        private TrainDB db = new TrainDB();

        // GET: Teams
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.Members);//.Include(t => t.Cohort).Include(t => t.Finance).Include(t => t.Leader);
            return View(teams);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.CohortID = new SelectList(db.Cohorts, "ID", "Code");
            ViewBag.FinanceCode = new SelectList(db.CostCentres, "Code", "CCName");
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Forename");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeamName,FinanceCode,LeaderID,CohortID,NoTrain")] Team team)
        {
            if (ModelState.IsValid)
            {
                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                team.Creator = userperson;
                team.Created = DateTime.Now;
                team.Modifier = userperson;
                team.Modified = DateTime.Now;
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CohortID = new SelectList(db.Cohorts, "ID", "Code", team.CohortID);
            ViewBag.FinanceCode = new SelectList(db.CostCentres, "Code", "CCName", team.FinanceCode);
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Forename", team.LeaderID);
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Include(t => t.Members).Include(t => t.Boroughs).SingleOrDefault(t => t.ID == id);
            if (team == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Borough> allBoro = db.Boroughs;
            TeamFormViewModel viewModel = new TeamFormViewModel(team, allBoro);

            ViewBag.CohortID = new SelectList(db.Cohorts, "ID", "Code", team.CohortID);
            ViewBag.FinanceCode = new SelectList(db.CostCentres, "Code", "CCName", team.FinanceCode);
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Forename", team.LeaderID);
            return View(viewModel);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamFormViewModel team)
        {
            if (ModelState.IsValid)
            {
                Team teamInDB = db.Teams.Include(t => t.Boroughs).Single(t => t.ID == team.ID);
                teamInDB.TeamName = team.TeamName;
                teamInDB.FinanceCode = team.FinanceCode;
                teamInDB.LeaderID = team.LeaderID;
                teamInDB.CohortID = team.CohortID;
                teamInDB.NoTrain = team.NoTrain;

                Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
                teamInDB.Modifier = userperson;
                teamInDB.Modified = DateTime.Now;
                //               db.Entry(team).State = EntityState.Modified;

                foreach (var bs in team.Boroughs)
                {
                    Borough bo = db.Boroughs.Single(b => b.ID == bs.Boro.ID);
                    if (bs.Selected)
                    {
                        if (!teamInDB.Boroughs.Contains(bo)) teamInDB.Boroughs.Add(bo);
                    }
                    else
                    {
                        if (teamInDB.Boroughs.Contains(bo)) teamInDB.Boroughs.Remove(bo);
                    }
                }

                foreach (var mem in team.Members)
                {
                    var memberInDB = db.TeamMembers.Single(m => m.ID == mem.ID);
                    bool changed = false;
                    if (memberInDB.Active != mem.Active)
                    {
                        memberInDB.Active = mem.Active;
                        changed = true;
                    }
                    if (memberInDB.Main != mem.Main)
                    {
                        //if set to main all other records for this person shouldn't be!
                        memberInDB.Main = mem.Main;
                        changed = true;
                    }
                    if (changed)
                    {
                        memberInDB.Modified = DateTime.Now;
                        memberInDB.Modifier = userperson;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CohortID = new SelectList(db.Cohorts, "ID", "Code", team.CohortID);
            ViewBag.FinanceCode = new SelectList(db.CostCentres, "Code", "CCName", team.FinanceCode);
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Forename", team.LeaderID);
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
