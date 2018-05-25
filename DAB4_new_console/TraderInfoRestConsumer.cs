using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAB4_new_console
{
    class TraderInfoRestConsumer
    {
        static HttpClient _client = new HttpClient();
        static string _url = "http://localhost:53135/api/TraderInfoes/";


        public static async Task<HttpResponseMessage> Put(int id, TradeInfoModel value)
        {
            var content = JsonConvert.SerializeObject(value);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync(new Uri(_url + id), byteContent);
            return response;
        }
        public static async Task<HttpResponseMessage> Delete(int id)
        {
            var response = await _client.DeleteAsync(new Uri(_url + id));
            return response;
        }

        public static async Task<HttpResponseMessage> Post(TradeInfoModel value)
        {
            var content = JsonConvert.SerializeObject(value);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var response = await _client.PostAsync(new Uri(_url), byteContent);

            return response;
        }
        public static async Task<TradeInfoModel> Get(int id)
        {
            var response = await _client.GetStringAsync(new Uri(_url + id));
            return JsonConvert.DeserializeObject<TradeInfoModel>(response);
        }
        public static async Task<List<TradeInfoModel>> GetAll()
        {
            var response = await _client.GetStringAsync(new Uri(_url));
            return JsonConvert.DeserializeObject<List<TradeInfoModel>>(response);
        }
    }
}
