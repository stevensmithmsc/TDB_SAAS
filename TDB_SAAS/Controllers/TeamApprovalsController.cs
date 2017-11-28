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
    public class TeamApprovalsController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/TeamApprovals
        public IQueryable<TeamApproval> GetTeamApprovals()
        {
            return db.TeamApprovals;
        }

        // GET: api/TeamApprovals/5
        [ResponseType(typeof(TeamApproval))]
        public async Task<IHttpActionResult> GetTeamApproval(int id)
        {
            TeamApproval teamApproval = await db.TeamApprovals.FindAsync(id);
            if (teamApproval == null)
            {
                return NotFound();
            }

            return Ok(teamApproval);
        }

        // PUT: api/TeamApprovals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeamApproval(int id, TeamApproval teamApproval)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamApproval.ID)
            {
                return BadRequest();
            }

            db.Entry(teamApproval).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamApprovalExists(id))
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

        // POST: api/TeamApprovals
        [ResponseType(typeof(TeamApproval))]
        public async Task<IHttpActionResult> PostTeamApproval(TeamApprovalDTO teamApproval)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeamApproval tA = new TeamApproval();
            Person userperson = await db.People.SingleOrDefaultAsync(p => p.ID == 0);

            tA.StaffID = teamApproval.StaffID;
            tA.Team = teamApproval.Team;
            tA.StartDate = teamApproval.StartDate;
            tA.EndDate = teamApproval.EndDate;
            tA.Details = teamApproval.Details;
            tA.Created = DateTime.Now;
            tA.Creator = userperson;
            tA.Modified = DateTime.Now;
            tA.Modifier = userperson;

            db.TeamApprovals.Add(tA);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tA.ID }, teamApproval);
        }

        // DELETE: api/TeamApprovals/5
        [ResponseType(typeof(TeamApproval))]
        public async Task<IHttpActionResult> DeleteTeamApproval(int id)
        {
            TeamApproval teamApproval = await db.TeamApprovals.FindAsync(id);
            if (teamApproval == null)
            {
                return NotFound();
            }

            db.TeamApprovals.Remove(teamApproval);
            await db.SaveChangesAsync();

            return Ok(teamApproval);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamApprovalExists(int id)
        {
            return db.TeamApprovals.Count(e => e.ID == id) > 0;
        }
    }
}