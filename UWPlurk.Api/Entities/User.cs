using Newtonsoft.Json;
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
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The unique nick_name of the user, for example amix.
        /// </summary>
        [JsonProperty("nick_name")]
        public string NickName { get; set; }

        /// <summary>
        /// The non-unique display name of the user, for example Amir S. Only set if it's non empty.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The user's full name, like Amir Salihefendic.
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// The user's birthday.
        /// </summary>
        [JsonProperty("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 1 is male, 0 is female, 2 is not stating/other.
        /// </summary>
        public enum PlurkSex { female = 0, male = 1, others = 2 }
        [JsonProperty("gender")]
        public PlurkSex Gender { get; set; }

        /// <summary>
        /// Can be not_saying, single, married, divorced, engaged, in_relationship, complicated, widowed, open_relationship
        /// </summary>
        [JsonProperty("relationship")]
        public string Relationship { get; set; }

        /// <summary>
        /// The user's location, a text string, for example Aarhus Denmark.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// The profile title of the user.
        /// </summary>
        [JsonProperty("page_title")]
        public string PageTitle { get; set; }

        /// <summary>
        /// User's karma value.
        /// </summary>
        [JsonProperty("karma")]
        public Single Karma { get; set; }

        /// <summary>
        /// Specifies what the latest avatar (profile picture) version is.
        /// </summary>
        [JsonProperty("avatar")]
        public int? Avatar { get; set; }

        /// <summary>
        /// How many friends has the user recruited.
        /// </summary>
        [JsonProperty("recruited")]
        public int Recruited { get; set; }

        /// <summary>
        /// If 1 then the user has a profile picture, otherwise the user should use the default.
        /// </summary>
        [JsonProperty("has_profile_image")]
        public int? HasProfileImage { get; set; }

        [JsonProperty("profile_views")]
        public int? ProfileViews { get; set; }
        [JsonProperty("settings")]
        public bool Settings { get; set; }
        [JsonProperty("is_premium")]
        public bool IsPremium { get; set; }

        /// <summary>
        /// Plurk Coin (Premium) user indicator. true=premium user otherwise is normal user.
        /// </summary>
        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("following")]
        public bool Following { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("last_logged_in")]
        public int? LastLoggedIn { get; set; }
        [JsonProperty("num_of_followers")]
        public int? NoOfFollowers { get; set; }


        public enum AvatarSize { small, medium, big }

        public string AvatarUrl(AvatarSize size)
        {
            return getAvatarUrl(size.ToString());
        }

        public string AvatarSmall { get { return getAvatarUrl("small"); } }
        public string AvatarMedium { get { return getAvatarUrl("medium"); } }
        public string AvatarBig { get { return getAvatarUrl("big"); } }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("verified_account")]
        public bool VerifiedAccount { get; set; }

        [JsonProperty("bday_privacy")]
        public int BirthdayPrivacy { get; set; }
        [JsonProperty("default_lang")]
        public string DefaultLang { get; set; }
        [JsonProperty("dateformat")]
        public int DateFormat { get; set; }
        [JsonProperty("email_confirmed")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Color of user name.
        /// </summary>
        [JsonProperty("name_color")]
        public string NameColor { get; set; }

        private string getAvatarUrl(string size)
        {
            if (HasProfileImage == 0)
            {
                return string.Format("http://www.plurk.com/static/default_{0}.jpg", size);
            }

            var ext = HasProfileImage.HasValue && size == "big" ? "jpg" : "gif";
            if (Avatar.HasValue)
            {
                return string.Format("http://avatars.plurk.com/{0}-{1}{2}.{3}", Id, size, Avatar, ext);
            }
            else
            {
                return string.Format("http://avatars.plurk.com/{0}-{1}.{2}", Id, size, ext);
            }
        }

        public override string ToString()
        {
            return DisplayName ?? (FullName ?? NickName);
        }
    }
}
