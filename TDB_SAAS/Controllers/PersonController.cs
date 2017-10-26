using System;
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
            var viewModel = new PersonFormViewModel(new Person(), _context.Flags, _context.Boroughs);
            ViewBag.TitleID = new SelectList(_context.Titles, "ID", "TitleValue");
            ViewBag.FinanceCode = new SelectList(_context.CostCentres, "Code", "CCName");
            ViewBag.SubjectiveID = new SelectList(_context.Subjectives, "Code", "Subname");
            ViewBag.CohortID = new SelectList(_context.Cohorts, "ID", "CohortDescription");

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
                ViewBag.CohortID = new SelectList(_context.Cohorts, "ID", "CohortDescription");
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

            var viewModel = new PersonFormViewModel(person, _context.Flags, _context.Boroughs);
            ViewBag.TitleID = new SelectList(_context.Titles, "ID", "TitleValue");
            ViewBag.FinanceCode = new SelectList(_context.CostCentres, "Code", "CCName");
            ViewBag.SubjectiveID = new SelectList(_context.Subjectives, "Code", "Subname");
            ViewBag.CohortID = new SelectList(_context.Cohorts, "ID", "CohortDescription");
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

        // GET: Person
        public ActionResult Index()
        {
            var people = _context.People.Where(p => p.ID > 0).Include(p => p.Title).Include(p => p.Cohort);

            return View(people);
        }
    }
}