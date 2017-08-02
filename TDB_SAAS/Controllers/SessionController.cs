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

        // GET: Session
        public ActionResult Index()
        {
            var sessions = _context.Sessions.ToList();

            return View(sessions);
        }
    }
}