using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class ServiceTeamsController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/ServiceTeams
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ServiceTeams/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ServiceTeams
        [ResponseType(typeof(ServiceTeamDTO))]
        public async Task<IHttpActionResult> PostServiceMember(ServiceTeamDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team theTeam = await db.Teams.SingleOrDefaultAsync(t => t.ID == dto.TeamID);
            Service theService = await db.Services.SingleOrDefaultAsync(s => s.ID == dto.ServiceID);

            theTeam.Services.Add(theService);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", dto, dto);

        }

        // PUT: api/ServiceTeams/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ServiceTeams/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
