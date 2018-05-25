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
using DAB4_new.Models;

namespace DAB4_new.Controllers
{
    public class ProsumerInfoesController : ApiController
    {
        private DAB4_newContext db = new DAB4_newContext();

        // GET: api/ProsumerInfoes
        public IQueryable<ProsumerInfo> GetProsumerInfoes()
        {
            return db.ProsumerInfoes;
        }

        // GET: api/ProsumerInfoes/5
        [ResponseType(typeof(ProsumerInfo))]
        public async Task<IHttpActionResult> GetProsumerInfo(int id)
        {
            ProsumerInfo prosumerInfo = await db.ProsumerInfoes.FindAsync(id);
            if (prosumerInfo == null)
            {
                return NotFound();
            }

            return Ok(prosumerInfo);
        }

        // PUT: api/ProsumerInfoes/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] ProsumerInfo prosumerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prosumerInfo.Id)
            {
                return BadRequest();
            }

            db.Entry(prosumerInfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProsumerInfoExists(id))
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

        // POST: api/ProsumerInfoes
        [ResponseType(typeof(ProsumerInfo))]
        public async Task<IHttpActionResult> PostProsumerInfo([FromBody] ProsumerInfo prosumerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProsumerInfoes.Add(prosumerInfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prosumerInfo.Id }, prosumerInfo);
        }

        // DELETE: api/ProsumerInfoes/5
        [ResponseType(typeof(ProsumerInfo))]
        public async Task<IHttpActionResult> DeleteProsumerInfo(int id)
        {
            ProsumerInfo prosumerInfo = await db.ProsumerInfoes.FindAsync(id);
            if (prosumerInfo == null)
            {
                return NotFound();
            }

            db.ProsumerInfoes.Remove(prosumerInfo);
            await db.SaveChangesAsync();

            return Ok(prosumerInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProsumerInfoExists(int id)
        {
            return db.ProsumerInfoes.Count(e => e.Id == id) > 0;
        }
    }
}