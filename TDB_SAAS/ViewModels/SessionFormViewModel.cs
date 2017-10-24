using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class SessionFormViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Course")]
        public Nullable<int> CourseID { get; set; }

        [Display(Name = "Trainer")]
        public Nullable<int> TrainerID { get; set; }

        [Display(Name = "Location")]
        public Nullable<short> LocationID { get; set; }

        [Display(Name = "Date")]
        public Nullable<DateTime> CourseDate { get; set; }

        [Display(Name = "Start Time")]
        public Nullable<TimeSpan> StartTime { get; set; }

        [Display(Name = "End Time")]
        public Nullable<TimeSpan> EndTime { get; set; }

        [Display(Name = "Maximum People on Course")]
        public short MaxP { get; set; }

        public string Notes { get; set; }

        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Location> Locations { get; set; }

        public SessionFormViewModel()
        {

        }

        public SessionFormViewModel(Session session)
        {
            this.ID = session.ID;
            this.CourseID = session.CourseID;
            this.TrainerID = session.TrainerID;
            this.LocationID = session.LocationID;
            if (session.Strt != null)
            {
                this.CourseDate = ((DateTime)session.Strt).Date;
                this.StartTime = ((DateTime)session.Strt).TimeOfDay;
                if (session.Endt != null) this.EndTime = ((DateTime)session.Endt).TimeOfDay;
            }
            this.MaxP = session.MaxP;
            this.Notes = session.Notes;
        }
    }
}