using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    public class UserPlurk
    {
        public Plurk plurk { get; set; }
        public User user { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}: {3}",
                plurk.posted.ToString("yyyy/MM/dd HH:mm:ss"),
                user.DisplayName ?? user.NickName,
                plurk.qualifier ?? plurk.qualifier_translated,
                plurk.content_raw);
        }
    }
}
