using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;

namespace TDB_SAAS.Controllers
{
    public class LocationController : Controller
    {
        private TrainDB _context;

        public LocationController()
        {
            _context = new TrainDB();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var newLocation = new Location();

            return View("LocationForm", newLocation);
        }

        public ActionResult Edit(int id)
        {
            var location = _context.Locations.SingleOrDefault(l => l.ID == id);

            if (location == null) return HttpNotFound();

            return View("LocationForm", location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Location location)
        {
            if (!ModelState.IsValid)
            {

                return View("LocationForm", location);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            if (location.ID == 0)
            {
                location.Created = DateTime.Now;
                location.Creator = userperson;
                location.Modified = DateTime.Now;
                location.Modifier = userperson;
                _context.Locations.Add(location);
            }
            else
            {
                var locationInDb = _context.Locations.Single(l => l.ID == location.ID);

                locationInDb.LocationName = location.LocationName;
                locationInDb.TLoc = location.TLoc;
                locationInDb.MaxP = location.MaxP;
                locationInDb.Comments = location.Comments;

                locationInDb.Modified = DateTime.Now;
                locationInDb.Modifier = userperson;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Location");
        }

        // GET: Location
        public ActionResult Index()
        {
            var locations = _context.Locations;
            return View(locations);
        }
    }
}