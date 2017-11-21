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
            var teams = db.Teams.Include(t => t.Members).OrderBy(t => t.TeamName);//.Include(t => t.Cohort).Include(t => t.Finance).Include(t => t.Leader);
            return View(teams);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Include(t => t.Boroughs).Include(t => t.Services).Include(t => t.Members).SingleOrDefault(t => t.ID == id);
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
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Fullname");
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
                // Add leader as member
                if (team.LeaderID != null && team.LeaderID != 0)
                {
                    var mem = new TeamMember();
                    mem.Team = team;
                    mem.StaffID = (int)team.LeaderID;
                    mem.Active = true;
                    mem.Created = DateTime.Now;
                    mem.Creator = userperson;
                    mem.Modified = DateTime.Now;
                    mem.Modifier = userperson;
                    team.Members.Add(mem);
                }
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CohortID = new SelectList(db.Cohorts, "ID", "Code", team.CohortID);
            ViewBag.FinanceCode = new SelectList(db.CostCentres, "Code", "CCName", team.FinanceCode);
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Fullname", team.LeaderID);
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Include(t => t.Members).Include(t => t.Boroughs).Include(t => t.Services).SingleOrDefault(t => t.ID == id);
            if (team == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Borough> allBoro = db.Boroughs;
            IEnumerable<Service> allMHC = db.Services.Where(s => s.Level == ServiceLevel.Site && s.Display);
            TeamFormViewModel viewModel = new TeamFormViewModel(team, allBoro, allMHC);

            ViewBag.CohortID = new SelectList(db.Cohorts, "ID", "Code", team.CohortID);
            ViewBag.FinanceCode = new SelectList(db.CostCentres, "Code", "CCName", team.FinanceCode);
            ViewBag.LeaderID = new SelectList(team.Members.Select(m => m.Staff), "ID", "Fullname", team.LeaderID);
            List<int> members = team.Members.Select(m => m.StaffID).ToList();
            ViewBag.StaffList = db.People.Where(p => !members.Contains(p.ID)).Where(p => p.ID > 0);
            List<int> servs = team.Services.Select(s => s.ID).ToList();
            ViewBag.ServiceList = db.Services.Where(s => !servs.Contains(s.ID) && s.Level == ServiceLevel.Speciality && s.Display).OrderBy(s => s.ServiceName);
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

                foreach (var mhc in team.MHCs)
                {
                    Service s = db.Services.Single(serv => serv.ID == mhc.Service.ID);
                    if (mhc.Selected)
                    {
                        if (!teamInDB.Services.Contains(s)) teamInDB.Services.Add(s);
                    }
                    else
                    {
                        if (teamInDB.Services.Contains(s)) teamInDB.Services.Remove(s);
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
                        if (mem.Main)
                        {
                            var staffID = memberInDB.StaffID;
                            var otherRecords = db.People.Single(p => p.ID == staffID).MemberOf.Where(m => m.ID != mem.ID);
                            foreach(var otherRecord in otherRecords)
                            {
                                if (otherRecord.Main)
                                {
                                    otherRecord.Main = false;
                                    otherRecord.Modified = DateTime.Now;
                                    otherRecord.Modifier = userperson;
                                }
                            }
                        }
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
            ViewBag.LeaderID = new SelectList(db.People, "ID", "Fullname", team.LeaderID);
            List<int> members = team.Members.Select(m => m.StaffID).ToList();
            ViewBag.StaffList = db.People.Where(p => !members.Contains(p.ID)).Where(p => p.ID > 0);
            List<int> servs = team.Services.Select(s => s.ID).ToList();
            ViewBag.ServiceList = db.Services.Where(s => !servs.Contains(s.ID) && s.Level == ServiceLevel.Speciality && s.Display).OrderBy(s => s.ServiceName);
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
