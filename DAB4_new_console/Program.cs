using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAB4_new_console;
using Newtonsoft.Json;


namespace DAB4_new_console
{
    class Program
    {
        static string _url = "http://localhost:53135/api/ProsumerInfoes/";
        static HttpClient _client = new HttpClient();

        public static async Task<HttpResponseMessage> Put(int id, Prosumer value)
        {
            var content = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var x = new Uri(_url + id);
            Console.WriteLine("Put: " + x);

            var response = await _client.PutAsync(new Uri(_url + id), byteContent);
            return response;
        }
        public static async Task<HttpResponseMessage> Delete(int id)
        {
            var response = await _client.DeleteAsync(new Uri(_url + id));
            return response;
        }

        public static async Task<HttpResponseMessage> Post(Prosumer value)
        {
            var content = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var x = new Uri(_url);
            //Console.WriteLine("Post: " + x);

            var response = await _client.PostAsync(new Uri(_url), byteContent);

            //Console.WriteLine(response);
            return response;
        }
        public static async Task<Prosumer> Get(int id)
        {
            var response = await _client.GetStringAsync(new Uri(_url + id));
            return JsonConvert.DeserializeObject<Prosumer>(response);
        }
        public static async Task<List<Prosumer>> GetAll()
        {
            var response = await _client.GetStringAsync(new Uri(_url));
            return JsonConvert.DeserializeObject<List<Prosumer>>(response);
        }


        static void Main(string[] args)
        {
            while (true)
            {
                Console.ReadKey();
                Console.Clear();
                RunProsumers();
            }

            void RunProsumers()
            {
                foreach (var v in GetAll().Result)
                {
                    Delete(v.Id).Wait();
                }

                int privateProduced = 0;
                int privateConsumed = 0;
                int businessProduced = 0;
                int businessConsumed = 0;
                int villageProduced = 0;
                int villageConsumed = 0;
                int villageDifference = 0;

                List<Prosumer> pl = new List<Prosumer>();
                List<Prosumer> bl = new List<Prosumer>();

                Random rnd = new Random();

                
                Prosumer nationalProsumer = new Prosumer(45, "National power grid");

                //Set up private prosumers
                for (int i = 0; i < 33; i++)
                {
                    Prosumer privateProsumer = new Prosumer(i, "Private");
                    privateProsumer.ConsumedkW = rnd.Next(0, 100);
                    privateProsumer.ProducedkW = rnd.Next(0, 100);
                    privateProsumer.DifferencekW = privateProsumer.ProducedkW - privateProsumer.ConsumedkW;
                    pl.Add(privateProsumer);
                    try
                    {
                        var test = Get(i).Result;
                        Put(i, privateProsumer).Wait();
                    }
                    catch (Exception)
                    {
                        Post(privateProsumer).Wait();
                    }
                }

                //Set up business prosumers
                for (int j = 33; j < 45; j++)
                {
                    Prosumer businessProsumer = new Prosumer(j, "Business");
                    businessProsumer.ConsumedkW = rnd.Next(0, 500);
                    businessProsumer.ProducedkW = rnd.Next(0, 500);
                    businessProsumer.DifferencekW = businessProsumer.ProducedkW - businessProsumer.ConsumedkW;
                    bl.Add(businessProsumer);
                    try
                    {
                        var test = Get(j).Result;
                        Put(j, businessProsumer).Wait();
                    }
                    catch (Exception)
                    {
                        Post(businessProsumer).Wait();
                    }
                }


                //should be whatever is leftover from the prosumers
                nationalProsumer.ConsumedkW = 0;

                //should be whatever the prosumers need
                nationalProsumer.ProducedkW = 0;

                pl.ForEach(item => Console.WriteLine(item.Type + " with id: " + "\t" + item.Id + "\t" + " consumed: " + "\t" + item.ConsumedkW + "\t" + " and produced: " + "\t" + item.ProducedkW + "\t" + "difference is: " + item.DifferencekW));
                pl.ForEach(item => privateConsumed += item.ConsumedkW);
                pl.ForEach(item => privateProduced += item.ProducedkW);

                Console.WriteLine();

                bl.ForEach(item => Console.WriteLine(item.Type + " with id: " + "\t" + item.Id + "\t" + " consumed: " + "\t" + item.ConsumedkW + "\t" + " and produced: " + "\t" + item.ProducedkW + "\t" + "difference is: " + item.DifferencekW));
                bl.ForEach(item => businessConsumed += item.ConsumedkW);
                bl.ForEach(item => businessProduced += item.ProducedkW);

                Console.WriteLine("\nPrivate sector consumed: " + privateConsumed);
                Console.WriteLine("Private sector produced: " + privateProduced);
                Console.WriteLine("\nBusiness sector consumed: " + businessConsumed);
                Console.WriteLine("Business sector produced: " + businessProduced);

                villageConsumed = privateConsumed + businessConsumed;
                villageProduced = privateProduced + businessProduced;

                if (villageConsumed < villageProduced)
                {
                    nationalProsumer.ProducedkW = villageProduced - villageConsumed;
                }

                if (villageConsumed > villageProduced)
                {
                    nationalProsumer.ConsumedkW = villageConsumed - villageProduced;
                }

                Console.WriteLine("\nNational power grid consumed: " + nationalProsumer.ConsumedkW + " and produced: " + nationalProsumer.ProducedkW);

                villageDifference = villageProduced - villageConsumed;

                Console.WriteLine(villageDifference);

                Post(nationalProsumer).Wait();
            }
        }
    }
}
