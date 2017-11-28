using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.DTOs
{
    public class TeamApprovalDTO
    {
        public int StaffID { get; set; }

        public string Team { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Details { get; set; }
    }
}