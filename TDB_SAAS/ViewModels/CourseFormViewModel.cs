using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class CourseFormViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Course Name")]
        [Required]
        [MaxLength(80)]
        public string CourseName { get; set; }

        public Nullable<short> Length { get; set; }

        public string Notes { get; set; }

        public FlagSelector[] Flags { get; set; }

        public CourseSelector[] PreReqs { get; set; }

        public IEnumerable<Course> ReqFor { get; set; }

        public CourseFormViewModel()
        {

        }

        public CourseFormViewModel(Course course, IEnumerable<CFlag> AllFlags, IEnumerable<Course> AllCourses)
        {
            this.ID = course.ID;
            this.CourseName = course.CourseName;
            this.Length = course.Length;
            this.Notes = course.Notes;
            this.Flags = new FlagSelector[AllFlags.Count()];
            int i = 0;
            foreach (CFlag f in AllFlags)
            {
                FlagSelector fs = new FlagSelector(f);
                fs.Selected = course.Flags.Contains(f);
                Flags[i] = fs;
                i++;
            }
            IEnumerable<Course> RequireableCourses = AllCourses.Where(c => c.ID != course.ID).Except(course.ReqFor);
            this.PreReqs = new CourseSelector[RequireableCourses.Count()];
            i = 0;
            foreach (Course c in RequireableCourses)
            {
                CourseSelector cs = new CourseSelector(c);
                cs.Selected = course.PreReqs.Contains(c);
                PreReqs[i] = cs;
                i++;
            }
            this.ReqFor = course.ReqFor;
        }

        public class FlagSelector
        {
            public CFlag Flag { get; set; }
            public bool Selected { get; set; }

            public FlagSelector(CFlag f)
            {
                this.Flag = f;
                this.Selected = false;
            }

            public FlagSelector()
            {
            }
        }

        public class CourseSelector
        {
            public Course PreReq { get; set; }
            public bool Selected { get; set; }

            public CourseSelector(Course prereq)
            {
                this.PreReq = prereq;
                this.Selected = false;
            }

            public CourseSelector()
            {
            }
        }

    }
}