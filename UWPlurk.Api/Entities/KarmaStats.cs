using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class KarmaStats
    {
        public List<string> karma_trend { get; set; }
        public string karma_fall_reason { get; set; }
        public Single current_karma { get; set; }
        public Uri karma_graph { get; set; }
    }
}
