using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class Profile
    {
        public int friends_count { get; set; }
        public int fans_count { get; set; }
        public int unread_count { get; set; }
        public int alerts_count { get; set; }
        public User user_info { get; set; }
        public string privacy { get; set; }
        public Dictionary<string, User> plurks_users { get; set; }
        public Plurk[] plurks { get; set; }
    }
}
