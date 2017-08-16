using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Service
    {
        public int ID { get; set; }

        public string ServiceName { get; set; }

        public ServiceLevel Level { get; set; }

        public bool Display { get; set; }

        public Nullable<int> ParentID { get; set; }
        public virtual Service Parent { get; set; }
        public virtual ICollection<Service> ChildServices { get; set; }

        public virtual ICollection<Person> Staff { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public Service()
        {
            ChildServices = new HashSet<Service>();
            Staff = new HashSet<Person>();
            Teams = new HashSet<Team>();
        }

    }
}