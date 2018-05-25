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
    public class TraderInfoesController : ApiController
    {
        private DAB4_newContext db = new DAB4_newContext();

        // GET: api/TraderInfoes
        public IQueryable<TraderInfo> GetTraderInfoes()
        {
            return db.TraderInfoes;
        }

        // GET: api/TraderInfoes/5
        [ResponseType(typeof(TraderInfo))]
        public async Task<IHttpActionResult> GetTraderInfo(int id)
        {
            TraderInfo traderInfo = await db.TraderInfoes.FindAsync(id);
            if (traderInfo == null)
            {
                return NotFound();
            }

            return Ok(traderInfo);
        }

        // PUT: api/TraderInfoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTraderInfo(int id, TraderInfo traderInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traderInfo.Id)
            {
                return BadRequest();
            }

            db.Entry(traderInfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraderInfoExists(id))
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

        // POST: api/TraderInfoes
        [ResponseType(typeof(TraderInfo))]
        public async Task<IHttpActionResult> PostTraderInfo(TraderInfo traderInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TraderInfoes.Add(traderInfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = traderInfo.Id }, traderInfo);
        }

        // DELETE: api/TraderInfoes/5
        [ResponseType(typeof(TraderInfo))]
        public async Task<IHttpActionResult> DeleteTraderInfo(int id)
        {
            TraderInfo traderInfo = await db.TraderInfoes.FindAsync(id);
            if (traderInfo == null)
            {
                return NotFound();
            }

            db.TraderInfoes.Remove(traderInfo);
            await db.SaveChangesAsync();

            return Ok(traderInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TraderInfoExists(int id)
        {
            return db.TraderInfoes.Count(e => e.Id == id) > 0;
        }
    }
}