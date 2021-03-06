﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;
using TDB_SAAS.ViewModels;

namespace TDB_SAAS.Controllers
{
    public class PersonController : Controller
    {
        private TrainDB _context;

        public PersonController()
        {
            _context = new TrainDB();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var viewModel = new PersonFormViewModel(new Person(), _context.Flags, _context.Boroughs, _context.Services.Where(s => s.Level == ServiceLevel.Site && s.Display));
            ViewBag.TitleID = new SelectList(_context.Titles, "ID", "TitleValue");
            ViewBag.FinanceCode = new SelectList(_context.CostCentres, "Code", "CCName");
            ViewBag.SubjectiveID = new SelectList(_context.Subjectives, "Code", "Subname");
            ViewBag.CohortID = new SelectList(_context.Cohorts.OrderBy(c => c.Number), "ID", "CohortDescription");
            ViewBag.LineManID = new SelectList(_context.People.Where(p => p.Flags.Select(f => f.ID).Contains("LMR")), "ID", "FullName");
            ViewBag.TeamList = _context.Teams.OrderBy(t => t.TeamName);
            ViewBag.ServiceList = _context.Services.Where(s => s.Level == ServiceLevel.Speciality && s.Display).OrderBy(s => s.ServiceName);

            return View("PersonForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PersonFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TitleID = new SelectList(_context.Titles, "ID", "TitleValue");
                ViewBag.FinanceCode = new SelectList(_context.CostCentres, "Code", "CCName");
                ViewBag.SubjectiveID = new SelectList(_context.Subjectives, "Code", "Subname");
                ViewBag.CohortID = new SelectList(_context.Cohorts.OrderBy(c => c.Number), "ID", "CohortDescription");
                ViewBag.LineManID = new SelectList(_context.People.Where(p => p.Flags.Select(f => f.ID).Contains("LMR")), "ID", "FullName");
                List<int> teams = _context.People.SingleOrDefault(p => p.ID == viewModel.ID).MemberOf.Select(m => m.TeamID).ToList();
                ViewBag.TeamList = _context.Teams.Where(p => !teams.Contains(p.ID)).OrderBy(t => t.TeamName);
                List<int> servs = _context.People.SingleOrDefault(p => p.ID == viewModel.ID).Services.Select(s => s.ID).ToList();
                ViewBag.ServiceList = _context.Services.Where(s => !servs.Contains(s.ID) && s.Level == ServiceLevel.Speciality && s.Display).OrderBy(s => s.ServiceName);
                return View("PersonForm", viewModel);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);
            
            if (viewModel.ID == 0)
            {
                Person person = new Person();

                person.TitleID = viewModel.TitleID;
                person.Forename = viewModel.Forename;
                person.MiddleName = viewModel.MiddleName;
                person.Surname = viewModel.Surname;
                person.PreferredName = viewModel.PreferredName;
                person.Gender = viewModel.Gender;
                person.JobTitle = viewModel.JobTitle;
                person.FinanceCode = viewModel.FinanceCode;
                person.SubjectiveID = viewModel.SubjectiveID;
                person.Phone = viewModel.Phone;
                person.Email = viewModel.Email;
                person.Comments = viewModel.Comments;
                person.CohortID = viewModel.CohortID;
                foreach(var fs in viewModel.Flags.Where(f => f.Selected))
                {
                    person.Flags.Add(_context.Flags.Single(f => f.ID == fs.Flag.ID));
                }
                foreach(var bs in viewModel.Boroughs.Where(b => b.Selected))
                {
                    person.Boroughs.Add(_context.Boroughs.Single(b => b.ID == bs.Boro.ID));
                }
                foreach(var mhc in viewModel.MHCs.Where(m => m.Selected))
                {
                    person.Services.Add(_context.Services.Single(s => s.ID == mhc.Service.ID));
                }
                if (person.TitleID != 0 && person.Gender == null)
                {
                    person.Gender = _context.Titles.SingleOrDefault(t => t.ID == person.TitleID).DefaultGender;
                }

                person.Created = DateTime.Now;
                person.Creator = userperson;
                person.Modified = DateTime.Now;
                person.Modifier = userperson;
                _context.People.Add(person);
            }
            else
            {
                var personInDb = _context.People.Single(p => p.ID == viewModel.ID);

                personInDb.TitleID = viewModel.TitleID;
                personInDb.Forename = viewModel.Forename;
                personInDb.MiddleName = viewModel.MiddleName;
                personInDb.Surname = viewModel.Surname;
                personInDb.PreferredName = viewModel.PreferredName;
                personInDb.Gender = viewModel.Gender;
                personInDb.JobTitle = viewModel.JobTitle;
                personInDb.FinanceCode = viewModel.FinanceCode;
                personInDb.SubjectiveID = viewModel.SubjectiveID;
                personInDb.Phone = viewModel.Phone;
                personInDb.Email = viewModel.Email;
                personInDb.Comments = viewModel.Comments;
                personInDb.CohortID = viewModel.CohortID;

                foreach (var fs in viewModel.Flags)
                {
                    Flag flg = _context.Flags.Single(f => f.ID == fs.Flag.ID);
                    if (fs.Selected)
                    {
                        if (!personInDb.Flags.Contains(flg)) personInDb.Flags.Add(flg);
                    } else
                    {
                        if (personInDb.Flags.Contains(flg)) personInDb.Flags.Remove(flg);
                    }
                }

                foreach (var bs in viewModel.Boroughs)
                {
                    Borough bo = _context.Boroughs.Single(b => b.ID == bs.Boro.ID);
                    if (bs.Selected)
                    {
                        if (!personInDb.Boroughs.Contains(bo)) personInDb.Boroughs.Add(bo);
                    } else
                    {
                        if (personInDb.Boroughs.Contains(bo)) personInDb.Boroughs.Remove(bo);
                    }
                }

                foreach (var mhc in viewModel.MHCs)
                {
                    Service s = _context.Services.Single(serv => serv.ID == mhc.Service.ID);
                    if (mhc.Selected)
                    {
                        if (!personInDb.Services.Contains(s)) personInDb.Services.Add(s);
                    }
                    else
                    {
                        if (personInDb.Services.Contains(s)) personInDb.Services.Remove(s);
                    }
                }

                foreach (var mem in viewModel.Memberships)
                {
                    var memberInDB = _context.TeamMembers.Single(m => m.ID == mem.ID);
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
                            var otherRecords = _context.People.Single(p => p.ID == staffID).MemberOf.Where(m => m.ID != mem.ID);
                            foreach (var otherRecord in otherRecords)
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

                personInDb.Modified = DateTime.Now;
                personInDb.Modifier = userperson;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Person");
        }

        public ActionResult Edit(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.ID == id);
            if (person == null) return HttpNotFound();

            var viewModel = new PersonFormViewModel(person, _context.Flags, _context.Boroughs, _context.Services.Where(s => s.Level == ServiceLevel.Site && s.Display));
            ViewBag.TitleID = new SelectList(_context.Titles, "ID", "TitleValue");
            ViewBag.FinanceCode = new SelectList(_context.CostCentres, "Code", "CCName");
            ViewBag.SubjectiveID = new SelectList(_context.Subjectives, "Code", "Subname");
            ViewBag.CohortID = new SelectList(_context.Cohorts.OrderBy(c => c.Number), "ID", "CohortDescription");
            ViewBag.LineManID = new SelectList(_context.People.Where(p => p.Flags.Select(f => f.ID).Contains("LMR")), "ID", "FullName");
            List<int> teams = person.MemberOf.Select(m => m.TeamID).ToList();
            ViewBag.TeamList = _context.Teams.Where(t => !teams.Contains(t.ID)).OrderBy(t => t.TeamName);
            List<int> servs = person.Services.Select(s => s.ID).ToList();
            ViewBag.ServiceList = _context.Services.Where(s => !servs.Contains(s.ID) && s.Level == ServiceLevel.Speciality && s.Display).OrderBy(s => s.ServiceName);
            return View("PersonForm", viewModel);
        }

        // GET: Person/Requirements/5
        public ActionResult Requirements(int id)
        {

            var viewModel = new PersonRequirementsVM();
            viewModel.Person = _context.People.SingleOrDefault(p => p.ID == id);
            viewModel.Reqs = _context.People.SingleOrDefault(p => p.ID == id).Requirements.ToArray();
            ViewBag.Statuses = _context.Statuses.Where(s => s.Requirement);
            var requiredCourses = _context.People.SingleOrDefault(p => p.ID == id).Requirements.Select(r => r.Course).ToList();
            ViewBag.Courses = _context.Courses.ToList().Except(requiredCourses);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateReqs(Requirement[] reqs)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PersonRequirementsVM();
                viewModel.Person = _context.People.SingleOrDefault(p => p.ID == reqs[0].StaffID);
                viewModel.Reqs = reqs;
                ViewBag.Statuses = _context.Statuses.Where(s => s.Requirement);
                ViewBag.Courses = _context.Courses;
                return View("Requirements", viewModel);
            }

            for (int i = 0; i < reqs.Count(); i++)
            {
                int reqID = reqs[i].ID;
                var reqInDB = _context.Requirements.SingleOrDefault(r => r.ID == reqID);
                bool changed = false;
                Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

                if (reqInDB.StatusID != reqs[i].StatusID)
                {
                    reqInDB.StatusID = reqs[i].StatusID;
                    changed = true;
                }

                if (reqInDB.Comments != reqs[i].Comments)
                {
                    reqInDB.Comments = reqs[i].Comments;
                    changed = true;
                }

                if (changed)
                {
                    reqInDB.Modifier = userperson;
                    reqInDB.Modified = DateTime.Now;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Person");
        }

        public ActionResult Attendances(int id)
        {
            ViewBag.Staff = _context.People.SingleOrDefault(p => p.ID == id);
            Attendance[] viewModel = _context.People.SingleOrDefault(p => p.ID == id).Attendances.ToArray();
            ViewBag.Statuses = _context.Statuses.Where(s => s.Attendance);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAttendances(Attendance[] attendances)
        {
            if (!ModelState.IsValid)
            {
                Attendance[] viewModel = attendances;
                ViewBag.Staff = _context.People.SingleOrDefault(p => p.ID == attendances[0].StaffID);
                ViewBag.Statuses = _context.Statuses.Where(s => s.Attendance);

                return View("Attendances", viewModel);
            }

            for (int i = 0; i < attendances.Count(); i++)
            {
                int attID = attendances[i].ID;
                var attendanceInDB = _context.Attendances.SingleOrDefault(a => a.ID == attID);
                bool changed = false;
                Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

                if (attendanceInDB.OutcomeID != attendances[i].OutcomeID)
                {
                    attendanceInDB.OutcomeID = attendances[i].OutcomeID;
                    changed = true;
                    if (attendances[i].OutcomeID == 6)
                    {
                        attendanceInDB.Canceller = userperson;
                        attendanceInDB.Cancelled = DateTime.Now;
                    }

                    // update linked requirement!
                    int StaffID = attendanceInDB.StaffID;
                    int CourseID = (int)attendanceInDB.Sess.CourseID;
                    var req = _context.Requirements.SingleOrDefault(r => r.StaffID == StaffID && r.CourseID == CourseID);
                    short OutID = attendances[i].OutcomeID;
                    var outcome = _context.Statuses.SingleOrDefault(s => s.ID == OutID);
                    if (req != null)
                    {
                        if (outcome.Requirement)
                        {
                            req.Status = outcome;
                        }
                        else
                        {
                            req.StatusID = (short)1;
                        }
                        req.Modifier = userperson;
                        req.Modified = DateTime.Now;
                    }
                }

                if (attendanceInDB.Comments != attendances[i].Comments)
                {
                    attendanceInDB.Comments = attendances[i].Comments;
                    changed = true;
                }

                if (changed)
                {
                    attendanceInDB.Modifier = userperson;
                    attendanceInDB.Modified = DateTime.Now;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Person");
        }

        public ActionResult TNA(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.ID == id);
            if (person == null) return HttpNotFound();
            ViewBag.person = person;
            TNA TNA = person.TNA;
            if (TNA == null)
            {
                TNA = new TNA();
                TNA.StaffID = person.ID;
            }

            var TrainerList = _context.People.Where(p => p.Flags.Any(f => f.ID == "TRN"))
                                             .Select(p => new Trainer { ID = p.ID, FName = p.Forename, SName = p.Surname })
                                             .ToList();
            ViewBag.TrainerID = new SelectList(TrainerList, "ID", "Name");
            ViewBag.OutcomeID = new SelectList(_context.Statuses.Where(s => s.TNA_OUT), "ID", "StatusDesc");

            return View(TNA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTNA(TNA tNA)
        {
            var TNAInDb = _context.TNAs.SingleOrDefault(t => t.StaffID == tNA.StaffID);
            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            if (TNAInDb == null)
            {
                TNAInDb = new Models.TNA();
                TNAInDb.StaffID = tNA.StaffID;
                TNAInDb.Creator = userperson;
                TNAInDb.Created = DateTime.Now;
                _context.TNAs.Add(TNAInDb);
            }

            TNAInDb.DateReceived = tNA.DateReceived;
            TNAInDb.TrainerID = tNA.TrainerID;
            TNAInDb.ContactDate = tNA.ContactDate;
            TNAInDb.ContactOutcome = tNA.ContactOutcome;
            TNAInDb.OutcomeID = tNA.OutcomeID;
            TNAInDb.Modified = DateTime.Now;
            TNAInDb.Modifier = userperson;

            _context.SaveChanges();

            return RedirectToAction("Index", "Person");
        }

        public ActionResult RA(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.ID == id);
            if (person == null) return HttpNotFound();
            ViewBag.person = person;
            RA RA = person.RA;
            if (RA == null)
            {
                RA = new RA();
                RA.StaffID = person.ID;
            }

            ViewBag.PDSRoleID = new SelectList(_context.Statuses.Where(s => s.RA_PDS), "ID", "StatusDesc");
            ViewBag.PLUSUpdatedID = new SelectList(_context.Statuses.Where(s => s.RA_PLUS), "ID", "StatusDesc");
            ViewBag.ESRUpdatedID = new SelectList(_context.Statuses.Where(s => s.RA_ESR), "ID", "StatusDesc");

            return View(RA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRA(RA rA)
        {
            var RAInDb = _context.RAs.SingleOrDefault(r => r.StaffID == rA.StaffID);
            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            if (RAInDb == null)
            {
                RAInDb = new Models.RA();
                RAInDb.StaffID = rA.StaffID;
                RAInDb.Creator = userperson;
                RAInDb.Created = DateTime.Now;
                _context.RAs.Add(RAInDb);
            }

            RAInDb.UUID = rA.UUID;
            RAInDb.PDSRoleID = rA.PDSRoleID;
            RAInDb.PLUSUpdatedID = rA.PLUSUpdatedID;
            RAInDb.ESRUpdatedID = rA.ESRUpdatedID;
            RAInDb.EGifL3 = rA.EGifL3;
            RAInDb.Declaration = rA.Declaration;
            RAInDb.GoLiveApproved = rA.GoLiveApproved;
            RAInDb.GLALocked = rA.GLALocked;
            RAInDb.CHGoLiveAprv = rA.CHGoLiveAprv;
            RAInDb.CHGLALocked = rA.CHGLALocked;
            RAInDb.AccountCreated = rA.AccountCreated;
            RAInDb.AddCITRIX = rA.AddCITRIX;
            RAInDb.PasswordEmailed = rA.PasswordEmailed;
            RAInDb.AccessToPlus = rA.AccessToPlus;
            RAInDb.UUIDAddESR = rA.UUIDAddESR;
            RAInDb.FullyCompliant = rA.FullyCompliant;
            RAInDb.RAComments = rA.RAComments;
            
            RAInDb.Modified = DateTime.Now;
            RAInDb.Modifier = userperson;

            _context.SaveChanges();

            return RedirectToAction("Index", "Person");
        }

        public ActionResult TeamApprov(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.ID == id);
            if (person == null) return HttpNotFound();
            ViewBag.Staff = person;
            TeamApproval[] viewModel = person.TeamApprovals.ToArray();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTeamApprov(TeamApproval[] teamApprovals)
        {
            if (!ModelState.IsValid)
            {
                TeamApproval[] viewModel = teamApprovals;
                ViewBag.Staff = _context.People.SingleOrDefault(p => p.ID == teamApprovals[0].StaffID);

                return View("TeamApprov", viewModel);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            for (int i = 0; i < teamApprovals.Count(); i++)
            {
                int aprvID = teamApprovals[i].ID;
                var approval = _context.TeamApprovals.SingleOrDefault(t => t.ID == aprvID);
                bool changed = false;

                if (approval.Team != teamApprovals[i].Team || approval.StartDate != teamApprovals[i].StartDate
                    || approval.EndDate != teamApprovals[i].EndDate || approval.Details != teamApprovals[i].Details)
                {
                    approval.Team = teamApprovals[i].Team;
                    approval.StartDate = teamApprovals[i].StartDate;
                    approval.EndDate = teamApprovals[i].EndDate;
                    approval.Details = teamApprovals[i].Details;
                    changed = true;
                }

                if (changed)
                {
                    approval.Modified = DateTime.Now;
                    approval.Modifier = userperson;
                }

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Person");
        }

        // GET: Person/Details
        public ActionResult Details(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.ID == id);
            if (person == null) return HttpNotFound();

            return View(person);
        }

        // GET: Person
        public ActionResult Index()
        {
            var people = _context.People.Where(p => p.ID > 0).Include(p => p.Title).Include(p => p.Cohort);

            return View(people);
        }
        
    }
}