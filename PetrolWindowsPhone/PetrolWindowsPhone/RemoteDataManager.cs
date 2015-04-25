namespace PetrolWindowsPhone
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PetrolWindowsPhone.Model;

    class RemoteDataManager
    {
        private HttpClient client;
        private const string basicUrl = "https://petrol-web.herokuapp.com";

        public RemoteDataManager()
        {
            this.client = new HttpClient();
        }

        public async Task<Station> GetStationInfo(string city, string address)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(basicUrl + "/station" + "/" + city + "/" + address);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contentAsJson = this.DeserializeJson<Station>(content);
                return contentAsJson;
            }
            else
            {
                return new Station();
            }
        }

        public async Task<List<Station>> GetTopStations(int top, bool sorted)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(basicUrl + "/stations?top=" + top + "&sorted=" + sorted);
          //  Uri requestUri = new Uri(basicUrl + "/stations?top=" + top + "&sorted=" + sorted);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var contentAsJson = this.DeserializeJson<List<Station>>(content);
            return contentAsJson;
        }

        private T DeserializeJson<T>(string json)
        {
            var contentAsJson = JsonConvert.DeserializeObject<T>(json);
            return contentAsJson;

        }

        public async Task AddStation(string facebookToken, string city, string address, double liters, double kilometers)
        {
            Uri requestUri = new Uri(basicUrl + "/refuel");
            var values = new Dictionary<string, string>
                 {
                    { "user_id", facebookToken },
                    { "city",city},
                    {"address",address},
                    {"liters", String.Format("{0}",liters)},
                    {"kilometers", String.Format("{0}",kilometers)}
                 };
            var requestContent = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(requestUri, requestContent);
            int i = 0;
           // var content = await response.Content.ReadAsStringAsync();
           // var contentAsJson = this.DeserializeJson<List<Station>>(content);
        }
    }
}