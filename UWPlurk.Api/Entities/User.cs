using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.Entities
{
    /// <summary>
    /// Representing plurk user data structure.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique user id.
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// The unique nick_name of the user, for example amix.
        /// </summary>
        public string nick_name { get; set; }

        /// <summary>
        /// The non-unique display name of the user, for example Amir S. Only set if it's non empty.
        /// </summary>
        public string display_name { get; set; }

        /// <summary>
        /// The user's full name, like Amir Salihefendic.
        /// </summary>
        public string full_name { get; set; }

        /// <summary>
        /// The user's birthday.
        /// </summary>
        public DateTime? date_of_birth { get; set; }

        /// <summary>
        /// 1 is male, 0 is female, 2 is not stating/other.
        /// </summary>
        public enum plurk_sex { female = 0, male = 1, others = 2 }
        public plurk_sex gender { get; set; }

        /// <summary>
        /// Can be not_saying, single, married, divorced, engaged, in_relationship, complicated, widowed, open_relationship
        /// </summary>
        public string relationship { get; set; }

        /// <summary>
        /// The user's location, a text string, for example Aarhus Denmark.
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// The profile title of the user.
        /// </summary>
        public string page_title { get; set; }

        /// <summary>
        /// User's karma value.
        /// </summary>
        public Single karma { get; set; }

        /// <summary>
        /// Specifies what the latest avatar (profile picture) version is.
        /// </summary>
        public int? avatar { get; set; }

        /// <summary>
        /// How many friends has the user recruited.
        /// </summary>
        public int recruited { get; set; }

        /// <summary>
        /// If 1 then the user has a profile picture, otherwise the user should use the default.
        /// </summary>
        public int? has_profile_image { get; set; }

        public int? profile_views { get; set; }
        public bool settings { get; set; }
        public bool is_premium { get; set; }

        /// <summary>
        /// Plurk Coin (Premium) user indicator. true=premium user otherwise is normal user.
        /// </summary>
        public bool premium { get; set; }

        public bool following { get; set; }
        public string timezone { get; set; }
        public int? last_logged_in { get; set; }
        public int? num_of_followers { get; set; }


        public enum avatar_size { small, medium, big }

        public string avatar_url(avatar_size size)
        {
            return get_avatar_url(size.ToString());
        }
        public string avatar_small { get { return get_avatar_url("small"); } }
        public string avatar_medium { get { return get_avatar_url("medium"); } }
        public string avatar_big { get { return get_avatar_url("big"); } }


        /// <summary>
        /// 
        /// </summary>
        public bool verified_account { get; set; }
        public int bday_privacy { get; set; }
        public string default_lang { get; set; }
        public int dateformat { get; set; }
        public bool email_confirmed { get; set; }

        /// <summary>
        /// Color of user name.
        /// </summary>
        public string name_color { get; set; }

        private string get_avatar_url(string size)
        {
            if (has_profile_image == 0)
            {
                return string.Format("http://www.plurk.com/static/default_{0}.jpg", size);
            }

            var ext = has_profile_image.HasValue && size == "big" ? "jpg" : "gif";
            if (avatar.HasValue)
            {
                return string.Format("http://avatars.plurk.com/{0}-{1}{2}.{3}", id, size, avatar, ext);
            }
            else
            {
                return string.Format("http://avatars.plurk.com/{0}-{1}.{2}", id, size, ext);
            }
        }

        public override string ToString()
        {
            return display_name ?? (full_name ?? nick_name);
        }
    }
}
