using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class SearchUserResult
    {
        public int counts { get; set; }
        public User[] users { get; set; }
    }
}
