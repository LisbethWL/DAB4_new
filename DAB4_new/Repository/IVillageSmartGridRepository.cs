using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAB4_new.Models;

namespace DAB4_new.Repository
{
    public interface IVillageSmartGridRepository
    {
        IList<VillageSmartGrid> GetAll();
        Task<VillageSmartGrid> Get(string id);
        void Save(VillageSmartGrid traider);
        void Update(VillageSmartGrid trader);
        void Delete(string id);
    }
}
