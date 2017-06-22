using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;

namespace TDB_SAAS.Controllers
{
    public class TitleController : Controller
    {
        private TrainDB _context;

        public TitleController()
        {
            _context = new TrainDB();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Title
        public ActionResult Index()
        {
            var titles = _context.Titles.ToList();
            return View(titles);
        }
    }
}