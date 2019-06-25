using System;
using Newtonsoft.Json;

namespace Trkr.RestServices.DataConstruct
{
    public class UserData
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
