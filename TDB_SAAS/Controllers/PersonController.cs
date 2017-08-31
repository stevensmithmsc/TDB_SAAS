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
            var flags = _context.Flags.ToList();
            var viewModel = new PersonFormViewModel(new Person(), flags);
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

            var viewModel = new PersonFormViewModel(person, _context.Flags.ToList());
            ViewBag.TitleID = new SelectList(_context.Titles, "ID", "TitleValue");
            ViewBag.FinanceCode = new SelectList(_context.CostCentres, "Code", "CCName");
            ViewBag.SubjectiveID = new SelectList(_context.Subjectives, "Code", "Subname");
            ViewBag.CohortID = new SelectList(_context.Cohorts, "ID", "CohortDescription");
            return View("PersonForm", viewModel);
        }

        // GET: Person
        public ActionResult Index()
        {
            var people = _context.People.Where(p => p.ID > 0).Include(p => p.Title).Include(p => p.Cohort);

            return View(people);
        }
    }
}