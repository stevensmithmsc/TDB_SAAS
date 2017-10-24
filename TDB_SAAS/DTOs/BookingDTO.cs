using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.DTOs
{
    public class BookingDTO
    {
        public int StaffID { get; set; }
        public int SessID { get; set; }
        public short OutcomeID { get; set; }
        public String BkComments { get; set; }
    }
}