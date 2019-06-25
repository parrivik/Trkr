using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Trkr.RestServices.DataConstruct;
using System.Collections.Generic;



namespace Trkr.RestServices
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<UserData> CheckIfUserExists(string uri)
        {
            UserData userData = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string context = await response.Content.ReadAsStringAsync();
                    userData = JsonConvert.DeserializeObject<UserData>(context);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return userData;
        }


        public async Task<UserData> CreateUser(string uri, UserData content)
        {
            UserData userData = null;

            var values = new Dictionary<string, string>
            {
                {"email",content.Email},
                {"password",content.Password}
            };

            var json = JsonConvert.SerializeObject(content);
            var parameter = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await _client.PostAsync(uri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                string context = await response.Content.ReadAsStringAsync();
                userData = JsonConvert.DeserializeObject<UserData>(context);
            }

            return userData;

        }



        public async Task<LocationData> CreateLocation(string uri, LocationData locationData)
        {
            var json = JsonConvert.SerializeObject(locationData);
            HttpResponseMessage response = await _client.PostAsync(uri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            // genauere fehlermeldung as string
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                string context = await response.Content.ReadAsStringAsync();
                locationData = JsonConvert.DeserializeObject<LocationData>(context);
            }
            return locationData;
        }


        public async Task<LocationData> GetAllLocationData(string uri)
        {
            LocationData locationData = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string contect = await response.Content.ReadAsStringAsync();
                    locationData = JsonConvert.DeserializeObject<LocationData>(contect);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return locationData;
        }




    }
}
