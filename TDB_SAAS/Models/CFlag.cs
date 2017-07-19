using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class CFlag
    {
        [Display(Name = "Code")]
        public String ID { get; set; }

        [Display(Name = "Description")]
        public String TheFlag { get; set; }

        public ICollection<Course> Courses { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public CFlag()
        {
            this.Courses = new HashSet<Course>();
        }
    }
}