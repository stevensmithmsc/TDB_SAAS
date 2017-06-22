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
            var titles = _context.Titles.ToList();
            var viewModel = new PersonFormViewModel
            {
                Titles = titles
            };

            return View("PersonForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Person person)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PersonFormViewModel
                {
                    Person = person,
                    Titles = _context.Titles.ToList()
                };

                return View("PersonForm", viewModel);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);
            
            if (person.ID == 0)
            {
                person.Created = DateTime.Now;
                person.Creator = userperson;
                person.Modified = DateTime.Now;
                person.Modifier = userperson;
                _context.People.Add(person);
            }
            else
            {
                var personInDb = _context.People.Single(p => p.ID == person.ID);

                personInDb.Title = person.Title;
                personInDb.Forename = person.Forename;
                personInDb.MiddleName = person.MiddleName;
                personInDb.Surname = person.Surname;
                personInDb.PreferredName = person.PreferredName;
                personInDb.Gender = person.Gender;
                personInDb.JobTitle = person.JobTitle;
                personInDb.Phone = person.Phone;
                personInDb.Email = person.Email;
                personInDb.Comments = person.Comments;
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

            var viewModel = new PersonFormViewModel
            {
                Person = person,
                Titles = _context.Titles.ToList()
            };

            return View("PersonForm", viewModel);
        }

        // GET: Person
        public ActionResult Index()
        {
            var people = _context.People.Where(p => p.ID > 0).Include(p => p.Title).ToList();

            return View(people);
        }
    }
}