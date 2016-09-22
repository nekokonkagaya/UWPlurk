using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class Emoticons
    {
        public Dictionary<string, List<List<string>>> karma { get; set; }
        public Dictionary<string, List<List<string>>> recuited { get; set; }
    }
}
