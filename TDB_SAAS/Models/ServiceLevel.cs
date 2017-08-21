using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public enum ServiceLevel : short
    {
        Site = 1,
        Division = 2,
        Directorate = 3,
        Department = 4,
        Speciality = 5,
        Other = 6
    }
}