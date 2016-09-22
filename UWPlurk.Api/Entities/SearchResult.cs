using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class SearchResult
    {
        public bool has_more { get; set; }
        public string error { get; set; }
        public int? last_offset { get; set; }
        public Dictionary<string, User> users { get; set; }
        public Plurk[] plurks { get; set; }
    }
}
