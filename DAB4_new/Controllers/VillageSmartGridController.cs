using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DAB4_new.Repository;
using DAB4_new.Models;
using Exception = System.Exception;

namespace DAB4_new.Controllers
{
    public class VillageSmartGridController : ApiController
    {
        private readonly IVillageSmartGridRepository repo = new VillageSmartGridRepository();

        /// <summary>
        /// Get api/VillageSmartGrid
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VillageSmartGrid> Get()
        {
            return repo.GetAll().AsEnumerable();
        }

        /// <summary>
        /// Get/api/VillageSmartGrid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VillageSmartGrid> Get(string id)
        {
            try
            {
                return await repo.Get(id);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// Post/api/VillageSmartGrid
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody] VillageSmartGrid value)
        {
            try
            {
                repo.Save(value);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// Put/api/villageSmartGrid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(String id, [FromBody] VillageSmartGrid value)
        {
            try
            {
                repo.Update(value);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete/api/VillageSmartGrid
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            try
            {
                repo.Delete(id);
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}