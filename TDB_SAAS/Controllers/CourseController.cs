using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDB_SAAS.Models;
using TDB_SAAS.ViewModels;

namespace TDB_SAAS.Controllers
{
    public class CourseController : Controller
    {
        private TrainDB _context;

        public CourseController() { 
            _context = new TrainDB();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var AllFlags = _context.CFlags;
            var AllCourses = _context.Courses;
            var viewModel = new CourseFormViewModel(new Course(), AllFlags, AllCourses);

            return View("CourseForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var course = _context.Courses
                                 .Include(c => c.Flags)
                                 .Include(c => c.PreReqs)
                                 .Include(c => c.ReqFor)
                                 .SingleOrDefault(c => c.ID == id);
            if (course == null) return HttpNotFound();

            var AllFlags = _context.CFlags;
            var AllCourses = _context.Courses;
            var viewModel = new CourseFormViewModel(course, AllFlags, AllCourses);

            return View("CourseForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CourseFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View("CourseForm", viewModel);
            }

            Person userperson = _context.People.SingleOrDefault(p => p.ID == 0);

            if (viewModel.ID == 0)
            {
                Course course = new Course();

                course.CourseName = viewModel.CourseName;
                course.Length = viewModel.Length;
                course.Notes = viewModel.Notes;

                foreach (var fs in viewModel.Flags.Where(f => f.Selected))
                {
                    course.Flags.Add(_context.CFlags.Single(f => f.ID == fs.Flag.ID));
                }

                foreach (var cs in viewModel.PreReqs.Where(c => c.Selected))
                {
                    course.PreReqs.Add(_context.Courses.Single(c => c.ID == cs.PreReq.ID));
                }

                course.Created = DateTime.Now;
                course.Creator = userperson;
                course.Modified = DateTime.Now;
                course.Modifier = userperson;
                _context.Courses.Add(course);
            }
            else
            {
                var courseInDb = _context.Courses.Include(c => c.Flags).Include(c => c.PreReqs).Single(c => c.ID == viewModel.ID);

                courseInDb.CourseName = viewModel.CourseName;
                courseInDb.Length = viewModel.Length;
                courseInDb.Notes = viewModel.Notes;

                foreach (var fs in viewModel.Flags)
                {
                    CFlag flg = _context.CFlags.Single(f => f.ID == fs.Flag.ID);
                    if (fs.Selected)
                    {
                        if (!courseInDb.Flags.Contains(flg)) courseInDb.Flags.Add(flg);
                    }
                    else
                    {
                        if (courseInDb.Flags.Contains(flg)) courseInDb.Flags.Remove(flg);
                    }
                }

                foreach (var cs in viewModel.PreReqs)
                {
                    Course cse = _context.Courses.Single(c => c.ID == cs.PreReq.ID);
                    if (cs.Selected)
                    {
                        if (!courseInDb.PreReqs.Contains(cse)) courseInDb.PreReqs.Add(cse);
                    }
                    else
                    {
                        if (courseInDb.PreReqs.Contains(cse)) courseInDb.PreReqs.Remove(cse);
                    }
                }

                courseInDb.Modified = DateTime.Now;
                courseInDb.Modifier = userperson;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Course");
        }

        // GET: Course
        public ActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Flags).ToList();

            return View(courses);
        }


    }
}