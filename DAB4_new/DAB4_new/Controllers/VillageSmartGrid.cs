using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAB4_new.Models;

namespace DAB4_new.Controllers
{
    public class VillageSmartGrid : ApiController
    {
        private VillageSmartGridContext db = new VillageSmartGridContext();
        /// <summary>
        /// Returns bool value if the grid is there. 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private bool GridInfo(int a)
        {
            return db.VillageSmartGrid.Count(b => b.Id == a) > 0;
        }
        /// <summary>
        /// Dispose function, used to dispose things, hæh.
        /// </summary>
        /// <param name="a"></param>
        protected override void Dispose(bool a)
        {
            if (a)
            {
                db.Dispose();
            }

            base.Dispose(a);

        }


        // GET api/values
        public IQueryable<Models.VillageSmartGrid> GetVillageSmartGrid()
        {
            return db.VillageSmartGrid;
        }

        // GET api/values/5
        [ResponseType(typeof(Models.VillageSmartGrid))]
        public IHttpActionResult GetVillageSmartGrid(int id)
        {
            Models.VillageSmartGrid villageSmartGrid = db.VillageSmartGrid.Find(id);
            if (villageSmartGrid == null)
            {
                return NotFound();
            }

            return Ok(villageSmartGrid);
        }

        // POST api/values
        public IHttpActionResult PostVillageSmartGrid(Models.VillageSmartGrid villageSmartGrid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VillageSmartGrid.Add(villageSmartGrid);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GridInfo(villageSmartGrid.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }

            return CreatedAtRoute("API", new { id = villageSmartGrid.Id }, villageSmartGrid);
        }

        // PUT api/values/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVillgeSmartGrid(int id, Models.VillageSmartGrid villageSmartGrid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != villageSmartGrid.Id)
            {
                return BadRequest();
            }

            db.Entry(villageSmartGrid).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!GridInfo(id))
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

        // DELETE api/values/5
        public IHttpActionResult DeleteVillageSmartGrid(int id)
        {
            Models.VillageSmartGrid villageSmartGrid = db.VillageSmartGrid.Find(id);
            if (villageSmartGrid == null)
            {
                return NotFound();
            }

            db.VillageSmartGrid.Remove(villageSmartGrid);
            db.SaveChanges();

            return Ok(villageSmartGrid);
        }

    }
}
