using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class PersonFormViewModel
    {
        public IEnumerable<Title> Titles { get; set; }
        public Person Person { get; set; }
    }
}