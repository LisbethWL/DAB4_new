using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DAB4_new
{
    public class DocumentDBSeeder
    {
        public static void Preseed()
        {
            DocumentClient client = new DocumentClient(new Uri(Properties.Settings.Default.EndpointURL), Properties.Settings.Default.PrimaryKey);

            client.CreateDatabaseIfNotExistsAsync(new Database() { Id = Properties.Settings.Default.TraderDBName }).Wait();
            
            client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(Properties.Settings.Default.TraderDBName),
                new DocumentCollection() { Id = Properties.Settings.Default.TraderDBColl }).Wait();
        }
    }
}