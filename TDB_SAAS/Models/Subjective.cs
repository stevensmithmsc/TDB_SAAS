using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Subjective
    {
        public int Code { get; set; }

        public string SubName { get; set; }

        public virtual ICollection<Person> Staff { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public Subjective()
        {
            Staff = new HashSet<Person>();
        }
    }
}