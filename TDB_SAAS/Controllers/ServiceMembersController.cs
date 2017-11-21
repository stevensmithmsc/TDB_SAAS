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
    public class ServiceMembersController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/ServiceMembers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ServiceMembers
        [ResponseType(typeof(ServiceMemberDTO))]
        public async Task<IHttpActionResult> PostServiceMember(ServiceMemberDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person staffMember = await db.People.SingleOrDefaultAsync(p => p.ID == dto.StaffID);
            Service theService = await db.Services.SingleOrDefaultAsync(s => s.ID == dto.ServiceID);

            staffMember.Services.Add(theService);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", dto, dto);

        }

        // PUT: api/ServiceMembers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ServiceMembers/5
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
