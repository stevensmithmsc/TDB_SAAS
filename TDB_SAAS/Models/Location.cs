using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Location
    {
        public short ID { get; set; }

        public string LocationName { get; set; }

        public bool TLoc { get; set; }

        public Nullable<short> MaxP { get; set; }

        public string Comments { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public Location()
        {
            Sessions = new HashSet<Session>();
        }
    }
}