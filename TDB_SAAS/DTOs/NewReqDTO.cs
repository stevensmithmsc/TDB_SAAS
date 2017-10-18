using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.DTOs
{
    public class NewReqDTO
    {
        public int StaffID { get; set; }

        public int CourseID { get; set; }

        public string Comments { get; set; }
    }
}