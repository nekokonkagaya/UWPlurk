using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class UnreadStats
    {
        public int all { get; set; }
        public int my { get; set; }
        public int @private { get; set; }
        public int responded { get; set; }
    }
}
