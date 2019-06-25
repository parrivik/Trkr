using System;
using Newtonsoft.Json;
using Xamarin.Essentials;


namespace Trkr.RestServices.DataConstruct
{
    public class LocationData
    {

        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("velocity")]
        public double Velocity { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }


    }
}
