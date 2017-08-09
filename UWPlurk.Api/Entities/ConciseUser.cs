using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class ConciseUser
    {
        [JsonProperty("nick_name")]
        public string NickName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        public override string ToString()
        {
            return DisplayName ?? (FullName ?? NickName);
        }
    }
}
