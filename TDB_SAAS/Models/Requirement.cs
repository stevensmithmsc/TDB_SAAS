using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Requirement
    {
        public int ID { get; set; }

        public int StaffID { get; set; }
        public virtual Person Staff { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        public short StatusID { get; set; }
        public virtual Status Status { get; set; }

        public string Comments { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }
    }
}