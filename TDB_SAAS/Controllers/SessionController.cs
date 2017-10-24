using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;
using TDB_SAAS.ViewModels;

namespace TDB_SAAS.Controllers
{
    public class SessionController : Controller
    {
        private TrainDB _context;

        public SessionController()
        {
            _context = new TrainDB();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var viewModel = new SessionFormViewModel();
            viewModel.Courses = _context.Courses;
            viewModel.Trainers = _context.People.Where(p => p.Flags.Any(f => f.ID == "TRN"))
                                                .Select(p => new Trainer { ID = p.ID, FName = p.Forename, SName = p.Surname })
                                                .ToList();
            viewModel.Locations = _context.Locations.Where(l => l.TLoc);

            return View("SessionForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var session = _context.Sessions.SingleOrDefault(s => s.ID == id);

            if (session == null) return HttpNotFound();

            var viewModel = new SessionFormViewModel(session);
            viewModel.Courses = _context.Courses;
            viewModel.Trainers = _context.People.Where(p => p.Flags.Any(f => f.ID == "TRN"))
                                         .Select(p => new Trainer { ID = p.ID, FName = p.Forename, SName = p.Surname })
                                         .ToList();
            viewModel.Locations = _context.Locations.Where(l => l.TLoc);

            return View("SessionForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SessionFormViewModel sess)
        {
            if (!ModelState.IsValid)
            {
                sess.Courses = _context.Courses;
                sess.Trainers = _context.People.Where(p => p.Flags.Any(f => f.ID == "TRN"))
                                             .Select(p => new Trainer { ID = p.ID, FName = p.Forename, SName = p.Surname })
                                             .ToList();
                sess.Locations = _context.Locations.Where(l => l.TLoc);
                return View("SessionForm", sess);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            if (sess.ID == 0)
            {
                Session newSession = new Models.Session();
                newSession.CourseID = sess.CourseID;
                newSession.TrainerID = sess.TrainerID;
                newSession.LocationID = sess.LocationID;
                if (sess.CourseDate != null)
                {
                    newSession.Strt = ((DateTime)(sess.CourseDate)).Date.Add((TimeSpan)(sess.StartTime));
                    if (sess.EndTime != null)
                    {
                        newSession.Endt = ((DateTime)(sess.CourseDate)).Date.Add((TimeSpan)(sess.EndTime));
                    } else
                    {
                        if (sess.CourseID != null && sess.CourseID != 0)
                        {
                            var course = _context.Courses.Single(c => c.ID == sess.CourseID);
                            if (course.Length != null)
                            {
                                double length = (double)(course.Length);
                                newSession.Endt = ((DateTime)(newSession.Strt)).AddMinutes(length);
                            }
                        }
                    }
                }
                if (sess.MaxP != 0)
                {
                    newSession.MaxP = sess.MaxP;
                } else
                {
                    if (sess.LocationID != null && sess.LocationID != 0)
                    {
                        var location = _context.Locations.Single(l => l.ID == sess.LocationID);
                        newSession.MaxP = (short)location.MaxP;
                    }
                }
                newSession.Notes = sess.Notes;

                newSession.Created = DateTime.Now;
                newSession.Creator = userperson;
                newSession.Modified = DateTime.Now;
                newSession.Modifier = userperson;
                _context.Sessions.Add(newSession);
            }
            else
            {
                var sessionInDb = _context.Sessions.Single(s => s.ID == sess.ID);

                sessionInDb.CourseID = sess.CourseID;
                sessionInDb.TrainerID = sess.TrainerID;
                sessionInDb.LocationID = sess.LocationID;
                if (sess.CourseDate == null)
                {
                    sessionInDb.Strt = null;
                    sessionInDb.Endt = null;
                } else
                {
                    sessionInDb.Strt = ((DateTime)(sess.CourseDate)).Date.Add((TimeSpan)(sess.StartTime));
                    if (sess.EndTime != null)
                    {
                        sessionInDb.Endt = ((DateTime)(sess.CourseDate)).Date.Add((TimeSpan)(sess.EndTime));
                    }
                    else
                    {
                        if (sess.CourseID != null && sess.CourseID != 0)
                        {
                            var course = _context.Courses.Single(c => c.ID == sess.CourseID);
                            if (course.Length != null)
                            {
                                double length = (double)(course.Length);
                                sessionInDb.Endt = ((DateTime)(sessionInDb.Strt)).AddMinutes(length);
                            }
                        }
                    }
                }
                sessionInDb.MaxP = sess.MaxP;
                sessionInDb.Notes = sess.Notes;

                sessionInDb.Modified = DateTime.Now;
                sessionInDb.Modifier = userperson;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Session");
        }

        public ActionResult Attendances(int id)
        {
            Attendance[] viewModel = _context.Sessions.Single(s => s.ID == id).Attendances.ToArray();
            ViewBag.Statuses = _context.Statuses.Where(s => s.Attendance);
            ViewBag.Session = _context.Sessions.Single(s => s.ID == id);
            var courseID = _context.Sessions.SingleOrDefault(s => s.ID == id).CourseID;
            ViewBag.StaffList = _context.Requirements.Where(r => r.StatusID == 1 && r.CourseID == courseID).Select(r => r.Staff);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAttendances(Attendance[] attendances)
        {
            if (!ModelState.IsValid)
            {
                Attendance[] viewModel = attendances;
                ViewBag.Statuses = _context.Statuses.Where(s => s.Attendance);
                ViewBag.Session = _context.Sessions.Single(s => s.ID == attendances[0].SessID);
                ViewBag.StaffList = _context.Sessions.Single(s => s.ID == attendances[0].SessID).Course.RequiredBy.Where(r => r.StatusID == 1).Select(r => r.Staff);
                
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
                        } else
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

            return RedirectToAction("Index", "Session");
        }

        // GET: Session
        public ActionResult Index()
        {
            var sessions = _context.Sessions.ToList();

            return View(sessions);
        }
    }
}