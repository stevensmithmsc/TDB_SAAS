using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Course
    {
        public int ID { get; set; }

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

        //public bool External { get; set; }

        //public Nullable<bool> Paris { get; set; }

        //public bool Obselete { get; set; }

        //public bool Child_Health { get; set; }

        //public bool BAU { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public Course()
        {
            this.PreReqs = new HashSet<Course>();
            this.ReqFor = new HashSet<Course>();
            this.Flags = new HashSet<CFlag>();
        }
    }
}