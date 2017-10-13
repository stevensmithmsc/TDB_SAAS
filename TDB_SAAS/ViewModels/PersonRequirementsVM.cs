using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class PersonRequirementsVM
    {
        public Person Person { get; set; }

        public Requirement[] Reqs { get; set; }

    }
}