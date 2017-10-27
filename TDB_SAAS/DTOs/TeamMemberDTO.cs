using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.DTOs
{
    public class TeamMemberDTO
    {
        public int StaffID { get; set; }

        public int TeamID { get; set; }

        public bool Main { get; set; }

        public bool Active { get; set; }
    }
}