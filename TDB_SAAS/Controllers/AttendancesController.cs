using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TDB_SAAS.DTOs;
using TDB_SAAS.Models;

namespace TDB_SAAS.Controllers
{
    public class AttendancesController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/Attendances
        public IQueryable<Attendance> GetAttendances()
        {
            return db.Attendances;
        }

        // GET: api/Attendances/5
        [ResponseType(typeof(Attendance))]
        public async Task<IHttpActionResult> GetAttendance(int id)
        {
            Attendance attendance = await db.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        // PUT: api/Attendances/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendance.ID)
            {
                return BadRequest();
            }

            db.Entry(attendance).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Attendances
        [ResponseType(typeof(Attendance))]
        public async Task<IHttpActionResult> PostAttendance(BookingDTO booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sess = await db.Sessions.SingleOrDefaultAsync(s => s.ID == booking.SessID);

            // Session not found
            if (sess == null)
            {
                return BadRequest();
            }

            // Session Full
            if (sess.AvailablePlaces < 1)
            {
                return BadRequest();
            }

            // Already Booked
            if (booking.OutcomeID == 0)
            {
                var attnd = await db.Attendances.Where(a => a.OutcomeID == 0 && a.StaffID == booking.StaffID && a.SessID == booking.SessID).ToListAsync();
                            if (attnd.Count() != 0)
                            {
                                return BadRequest();
                            }
            }

            Person userperson = db.People.SingleOrDefault(p => p.ID == 0);

            Attendance attendance = new Attendance();
            attendance.SessID = booking.SessID;
            attendance.StaffID = booking.StaffID;
            attendance.OutcomeID = booking.OutcomeID;
            attendance.Comments = booking.BkComments;
            attendance.Booked = DateTime.Now;
            attendance.Booker = userperson;
            attendance.Modified = DateTime.Now;
            attendance.Modifier = userperson;

            db.Attendances.Add(attendance);

            //Update linked requirement
            int StaffID = attendance.StaffID;
            int CourseID = (int)sess.CourseID;
            var req = db.Requirements.SingleOrDefault(r => r.StaffID == StaffID && r.CourseID == CourseID);
            short OutID = booking.OutcomeID;
            var outcome = db.Statuses.SingleOrDefault(s => s.ID == (OutID == 0 ? 2 : OutID));
            if (req != null)
            {
                if (outcome.Requirement)
                {
                    req.Status = outcome;
                }
                else
                {
                    req.StatusID = (short)1;
                }
                req.Modifier = userperson;
                req.Modified = DateTime.Now;
            }

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = attendance.ID }, booking);
        }

        // DELETE: api/Attendances/5
        [ResponseType(typeof(Attendance))]
        public async Task<IHttpActionResult> DeleteAttendance(int id)
        {
            Attendance attendance = await db.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            db.Attendances.Remove(attendance);
            await db.SaveChangesAsync();

            return Ok(attendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceExists(int id)
        {
            return db.Attendances.Count(e => e.ID == id) > 0;
        }
    }
}