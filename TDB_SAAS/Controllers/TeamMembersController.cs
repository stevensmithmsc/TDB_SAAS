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
using TDB_SAAS.Models;
using TDB_SAAS.DTOs;

namespace TDB_SAAS.Controllers
{
    public class TeamMembersController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/TeamMembers
        public IQueryable<TeamMember> GetTeamMembers()
        {
            return db.TeamMembers;
        }

        // GET: api/TeamMembers/5
        [ResponseType(typeof(TeamMember))]
        public async Task<IHttpActionResult> GetTeamMember(int id)
        {
            TeamMember teamMember = await db.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return Ok(teamMember);
        }

        // PUT: api/TeamMembers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamMember.ID)
            {
                return BadRequest();
            }

            db.Entry(teamMember).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
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

        // POST: api/TeamMembers
        [ResponseType(typeof(TeamMemberDTO))]
        public async Task<IHttpActionResult> PostTeamMember(TeamMemberDTO teamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person userperson = await db.People.SingleOrDefaultAsync(p => p.ID == 0);

            if (teamMember.Main)
            {
                var staffID = teamMember.StaffID;
                var otherRecords = db.People.Single(p => p.ID == staffID).MemberOf;
                foreach (var otherRecord in otherRecords)
                {
                    if (otherRecord.Main)
                    {
                        otherRecord.Main = false;
                        otherRecord.Modified = DateTime.Now;
                        otherRecord.Modifier = userperson;
                    }
                }
            }

            TeamMember mem = new TeamMember();
            mem.StaffID = teamMember.StaffID;
            mem.TeamID = teamMember.TeamID;
            mem.Active = teamMember.Active;
            mem.Main = teamMember.Main;
            mem.Created = DateTime.Now;
            mem.Creator = userperson;
            mem.Modified = DateTime.Now;
            mem.Modifier = userperson;

            db.TeamMembers.Add(mem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mem.ID }, teamMember);
        }

        // DELETE: api/TeamMembers/5
        [ResponseType(typeof(TeamMember))]
        public async Task<IHttpActionResult> DeleteTeamMember(int id)
        {
            TeamMember teamMember = await db.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            db.TeamMembers.Remove(teamMember);
            await db.SaveChangesAsync();

            return Ok(teamMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamMemberExists(int id)
        {
            return db.TeamMembers.Count(e => e.ID == id) > 0;
        }
    }
}