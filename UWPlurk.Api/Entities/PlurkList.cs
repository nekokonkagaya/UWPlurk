using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class PlurkList
    {
        public Plurk[] plurks { get; set; }
        public Dictionary<string, User> plurk_users { get; set; }
    }
}
