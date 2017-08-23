using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class Emoticons
    {
        [JsonProperty("custom")]
        public Dictionary<string, List<string>> Custom { get; set; }

        [JsonProperty("karma")]
        public Dictionary<string, List<List<string>>> Karma { get; set; }

        [JsonProperty("recuited")]
        public Dictionary<string, List<List<string>>> Recuited { get; set; }
    }
}
