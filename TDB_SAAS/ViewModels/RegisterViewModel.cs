using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class RegisterViewModel
    {
        public Session Sess { get; set; }
        public Attendance[] Bookings { get; set; }

        public RegisterViewModel()
        {
        }

        public RegisterViewModel(Session sess)
        {
            this.Sess = sess;
            int bks = sess.Attendances.Where(a => a.OutcomeID != 7).Count() + sess.AvailablePlaces;
            this.Bookings = new Attendance[bks];
            int i = 0;
            foreach(Attendance atnd in sess.Attendances.Where(a => a.OutcomeID != 6 && a.OutcomeID != 7))
            {
                this.Bookings[i] = atnd;
                i++;
            }
            for (int j = 0; j < sess.AvailablePlaces; j++)
            {
                this.Bookings[i] = new Attendance();
                this.Bookings[i].ID = 0;
                i++;
            }
            foreach (Attendance atnd in sess.Attendances.Where(a => a.OutcomeID == 6))
            {
                this.Bookings[i] = atnd;
                i++;
            }
        }

        
    }
}