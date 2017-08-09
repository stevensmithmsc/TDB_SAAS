using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Course
    {
        public int ID { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        public Nullable<short> Length { get; set; }

        public string Notes { get; set; }

        public ICollection<Course> PreReqs { get; set; }
        public ICollection<Course> ReqFor { get; set; }

        public ICollection<CFlag> Flags { get; set; }

        //public short Template { get; set; }

        //public Nullable<int> FullColour { get; set; }

        //public Nullable<int> BookedColour { get; set; }

        //public Nullable<int> EmptyColour { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public ICollection<Requirement> RequiredBy { get; set; }

        public int NumberRequired { get { return RequiredBy.Where(r => r.StatusID == 1).Count(); } }
        public int NumberCompleted { get { return RequiredBy.Where(r => r.StatusID == 5).Count(); ; } }
        public int PlacesAvailable { get { return Sessions.Where(s => s.Strt > DateTime.Now).Sum(s => s.AvailablePlaces); } }
        public int PlacesBooked { get { return Sessions.Where(s => s.Strt > DateTime.Now).Sum(s => s.UnoutcommedBookings); ; } }

        public Course()
        {
            this.PreReqs = new HashSet<Course>();
            this.ReqFor = new HashSet<Course>();
            this.Flags = new HashSet<CFlag>();
            this.Sessions = new HashSet<Session>();
            this.RequiredBy = new HashSet<Requirement>();
        }
    }
}