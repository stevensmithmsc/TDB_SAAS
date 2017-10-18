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
    public class RequirementsController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/Requirements
        public IQueryable<Requirement> GetRequirements()
        {
            return db.Requirements;
        }

        // GET: api/Requirements/5
        [ResponseType(typeof(Requirement))]
        public async Task<IHttpActionResult> GetRequirement(int id)
        {
            Requirement requirement = await db.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }

            return Ok(requirement);
        }

        // PUT: api/Requirements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequirement(int id, Requirement requirement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requirement.ID)
            {
                return BadRequest();
            }

            db.Entry(requirement).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementExists(id))
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

        // POST: api/Requirements
        [ResponseType(typeof(Requirement))]
        public async Task<IHttpActionResult> PostRequirement(NewReqDTO newReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Requirement requirement = new Requirement();
            requirement.StaffID = newReq.StaffID;
            requirement.CourseID = newReq.CourseID;
            requirement.StatusID = 1;
            requirement.Comments = newReq.Comments;

            Person userperson = db.People.SingleOrDefault(p => p.ID == 0);
            requirement.Created = DateTime.Now;
            requirement.Creator = userperson;
            requirement.Modified = DateTime.Now;
            requirement.Modifier = userperson;

            db.Requirements.Add(requirement);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requirement.ID }, newReq);
        }

        // DELETE: api/Requirements/5
        [ResponseType(typeof(Requirement))]
        public async Task<IHttpActionResult> DeleteRequirement(int id)
        {
            Requirement requirement = await db.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }

            db.Requirements.Remove(requirement);
            await db.SaveChangesAsync();

            return Ok(requirement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequirementExists(int id)
        {
            return db.Requirements.Count(e => e.ID == id) > 0;
        }
    }
}