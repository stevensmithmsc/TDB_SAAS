using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class SessionFormViewModel
    {
        public int ID { get; set; }

        public Nullable<int> CourseID { get; set; }

        public Nullable<int> TrainerID { get; set; }

        public Nullable<short> LocationID { get; set; }

        public Nullable<DateTime> CourseDate { get; set; }
        public Nullable<TimeSpan> StartTime { get; set; }
        public Nullable<TimeSpan> EndTime { get; set; }

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