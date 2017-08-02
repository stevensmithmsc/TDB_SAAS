using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.ViewModels
{
    public class Trainer
    {
        public int ID { get; set; }
        public String FName { get; set; }
        public String SName { get; set; }
        public String Name { get { return FName + ' ' + SName; } }
    }
}