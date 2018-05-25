using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAB4_new.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.SystemFunctions;

namespace DAB4_new.Repository
{
    public class VillageSmartGridRepository : IVillageSmartGridRepository
    {

        private static readonly string DatabaseId = Properties.Settings.Default.TraderDBName;
        private static readonly string CollectionId = Properties.Settings.Default.TraderDBColl;

        private static readonly string EndpointUrl = Properties.Settings.Default.EndpointURL;
        private static readonly string PrimaryKey = Properties.Settings.Default.PrimaryKey;
        private static DocumentClient _client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

        public IList<VillageSmartGrid> GetAll()
        {
            try
            {
                return _client
                .CreateDocumentQuery<VillageSmartGrid>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId)).ToList();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<VillageSmartGrid> Get(string id)
        {
            try
            {
                return await _client.ReadDocumentAsync<VillageSmartGrid>(
                    UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
            }
            catch (Exception e)
            { 
                throw e;
            }
        }

        public void Save(VillageSmartGrid trader)
        {
            try
            {
                _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), trader);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public void Update(VillageSmartGrid trader)
        {
            try
            {
                _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, trader.Id), trader);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public void Delete(string id)
        {
            try
            {
                _client.DeleteDatabaseAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
    }
}