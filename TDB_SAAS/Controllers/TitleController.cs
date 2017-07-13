using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;
using TDB_SAAS.ViewModels;

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

        public ActionResult Edit(int id)
        {
            var title = _context.Titles.SingleOrDefault(t => t.ID == id);
            if (title == null) return HttpNotFound();

            var viewModel = new TitleFormViewModel(title);
            

            return View("TitleForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TitleFormViewModel title)
        {
            if (!ModelState.IsValid)
            {
                return View("TitleForm", title);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            if (title.ID == 0)
            {
                Title newTitle = new Title();
                newTitle.TitleValue = title.TitleValue;
                newTitle.DefaultGender = title.DefaultGender;
                int[] genderArray = title.Genders.Where(g => g.Selected).Select(g => g.GID).ToArray();
                newTitle.AvailGenders = String.Join(";", genderArray);
                newTitle.Created = DateTime.Now;
                newTitle.Creator = userperson;
                newTitle.Modified = DateTime.Now;
                newTitle.Modifier = userperson;
                _context.Titles.Add(newTitle);
            }
            else
            {
                var titleInDb = _context.Titles.Single(t => t.ID == title.ID);

                titleInDb.TitleValue = title.TitleValue;
                titleInDb.DefaultGender = title.DefaultGender;
                int[] genderArray = title.Genders.Where(g => g.Selected).Select(g => g.GID).ToArray();
                titleInDb.AvailGenders = String.Join(";", genderArray);
                titleInDb.Modified = DateTime.Now;
                titleInDb.Modifier = userperson;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Title");
        }

        // GET: Title
        public ActionResult Index()
        {
            var titles = _context.Titles.ToList();
            return View(titles);
        }
    }
}