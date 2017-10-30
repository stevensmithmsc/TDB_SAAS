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

namespace TDB_SAAS.Controllers
{
    public class CFlagsController : ApiController
    {
        private TrainDB db = new TrainDB();

        // GET: api/CFlags
        public IQueryable<CFlag> GetCFlags()
        {
            return db.CFlags;
        }

        // GET: api/CFlags/5
        [ResponseType(typeof(CFlag))]
        public async Task<IHttpActionResult> GetCFlag(int id)
        {
            CFlag cFlag = await db.CFlags.FindAsync(id);
            if (cFlag == null)
            {
                return NotFound();
            }

            return Ok(cFlag);
        }

        // PUT: api/CFlags/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCFlag(int id, CFlag cFlag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cFlag.ID)
            {
                return BadRequest();
            }

            db.Entry(cFlag).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CFlagExists(id))
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

        // POST: api/CFlags
        [ResponseType(typeof(CFlag))]
        public async Task<IHttpActionResult> PostCFlag(CFlag cFlag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CFlags.Add(cFlag);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cFlag.ID }, cFlag);
        }

        // DELETE: api/CFlags/5
        [ResponseType(typeof(CFlag))]
        public async Task<IHttpActionResult> DeleteCFlag(int id)
        {
            CFlag cFlag = await db.CFlags.FindAsync(id);
            if (cFlag == null)
            {
                return NotFound();
            }

            db.CFlags.Remove(cFlag);
            await db.SaveChangesAsync();

            return Ok(cFlag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CFlagExists(int id)
        {
            return db.CFlags.Count(e => e.ID == id) > 0;
        }
    }
}