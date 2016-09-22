using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class ConciseUser
    {
        public string nick_name { get; set; }
        public string display_name { get; set; }
        public string full_name { get; set; }

        public override string ToString()
        {
            return display_name ?? (full_name ?? nick_name);
        }
    }
}
