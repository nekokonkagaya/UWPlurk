using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Plurk
    {
        public long id { get; set; }

        /// <summary>
        /// The unique Plurk id, used for identification of the plurk.
        /// </summary>
        public long plurk_id { get; set; }

        /// <summary>
        /// The English qualifier.
        /// </summary>
        public string qualifier { get; set; }

        /// <summary>
        /// Only set if the language is not English, will be the translated qualifier.
        /// </summary>
        public string qualifier_translated { get; set; }

        /// <summary>
        /// Specifies what type of plurk it is and if the plurk has been responded by the user. 
        /// The value of plurk_type is only correct when calling getPlurks with "responded" filter (this is done for perfomance and caching reasons). 
        /// </summary>
        public int plurk_type { get; set; }

        /// <summary>
        /// Which timeline does this Plurk belong to.
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// Who is the owner/poster of this plurk. For anonymous plurk, this will be overrided by user "anonymous" (uid: 99999). 
        /// </summary>
        public long owner_id { get; set; }

        /// <summary>
        /// How many of the responses have the user read. This is automatically updated when fetching responses or marking a plurk as read. 
        /// </summary>
        public int responses_seen { get; set; }

        /// <summary>
        /// How many responses does the plurk have. 
        /// </summary>
        public int response_count { get; set; }

        /// <summary>
        /// If set to 1, then responses are disabled for this plurk.
        /// If set to 2, then only friends can respond to this plurk.
        /// </summary>
        public byte no_comments { get; set; }

        /// <summary>
        /// is_unread: Specifies if the plurk is read (=1), unread (=0) or muted(=2).
        /// </summary>
        public byte is_unread { get; set; }

        /// <summary>
        /// If the Plurk is public limited_to is null. If the Plurk is posted to a user's friends then limited_to is [0]. 
        /// If limited_to is [1,2,6,3] then it's posted only to these user ids. 
        /// </summary>
        public string limited_to { get; set; }

        /// <summary>
        /// Language identifier of plurk.
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// The raw content as user entered it, useful when editing plurks or if you want to format the content differently. 
        /// </summary>
        public string content_raw { get; set; }
        public string content { get; set; }
        public DateTime posted { get; set; }

        public int favorite_count { get; set; }

        /// <summary>
        /// List of ids of users who liked given plurk (can be truncated). 
        /// </summary>
        public string[] favorers { get; set; }

        /// <summary>
        /// True if current user has liked given plurk.
        /// </summary>
        public bool favorite { get; set; }

        /// <summary>
        /// True if plurk can be replurked. 
        /// </summary>
        public bool replurkable { get; set; }
    }
}
