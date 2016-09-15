using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    /// <summary>
    /// Data structure representation of plurk responses.
    /// </summary>
    public class Responses
    {
        public Dictionary<string, User> friends { get; set; }
        public int responses_seen { get; set; }
        public Plurk[] responses { get; set; }
    }
}
