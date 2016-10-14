using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api
{
    /// <summary>
    /// This class represent all constants used in UWPlurk API.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Base URL of Plurk.
        /// </summary>
        public const string URL_BASE = "http://www.plurk.com";

        public const string APP_URL_BASE = URL_BASE + "/APP";

        #region OAuth
        private const string OAUTH_URL_BASE = URL_BASE + "/OAuth";

        /// <summary>
        /// String URL for requesting Token.
        /// </summary>
        public const string URL_REQUEST_TOKEN = OAUTH_URL_BASE + "/request_token";

        /// <summary>
        /// String URL for authorize requested Token (desktop version).
        /// </summary>
        public const string URL_AUTHORIZE_BASE = OAUTH_URL_BASE + "/authorize";

        /// <summary>
        /// String URL for authorize requested Token (mobile version).
        /// </summary>
        public const string URL_AUTHORIZE_MOBILEBASE = URL_BASE + "/m/authorize";

        /// <summary>
        /// String URL for obtaining access Token.
        /// </summary>
        public const string URL_ACCESS_TOKEN = OAUTH_URL_BASE + "/access_token";
        #endregion

        #region App Categories
        public const string APP_USERS = "/Users";

        public const string APP_PROFILE = "/Profile";

        public const string APP_REALTIME = "/Realtime";

        public const string APP_POLLING = "/Polling";

        public const string APP_TIMELINE = "/Timeline";

        public const string APP_RESPONSES = "/Responses";

        public const string APP_FRIENDSFANS = "/FriendsFans";

        public const string APP_ALERTS = "/Alerts";

        public const string APP_EMOTICONS = "/Emoticons";

        public const string APP_BLOCK = "/Blocks";

        public const string APP_CLIQUE = "/Cliques";

        public const string APP_PLURKTOP = "/PlurkTop";
        #endregion

        /// <summary>
        /// URL prefix for custom emoticons.
        /// </summary>
        public const string EMO_URL_PREFIX = "https://emos.plurk.com";
    }
}
