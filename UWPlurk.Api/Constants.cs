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

        #region Users
        public const string APP_USERS = APP_URL_BASE + "/Users";

        public const string APP_USERS_ME = APP_USERS + "/me";

        public const string APP_USERS_UPDATE = APP_USERS + "/update";

        public const string APP_USERS_UPDATEPICTURE = APP_USERS + "/updatePicture";

        public const string APP_USERS_GETKARMASTATS = APP_USERS + "/getKarmaStats";
        #endregion

        #region Profile
        public const string APP_PROFILE = APP_URL_BASE + "/Profile";

        public const string APP_PROFILE_GETOWNPROFILE = APP_PROFILE + "/getOwnProfile";

        public const string APP_PROFILE_GETPUBLICPROFILE = APP_PROFILE + "/getPublicProfile";
        #endregion

        #region Real time notifications
        public const string APP_REALTIME_GETUSERCHANNEL = APP_URL_BASE + "/Realtime/getUserChannel";
        #endregion

        #region Polling
        public const string APP_POLLING = APP_URL_BASE + "/Polling";

        public const string APP_POLLING_GETPLURKS = APP_POLLING + "/getPlurks";

        public const string APP_POLLING_GETUNREADCOUNT = APP_POLLING + "/getUnreadCount";
        #endregion

        #region Timeline
        public const string APP_TIMELINE = APP_URL_BASE + "/Timeline";

        public const string APP_TIMELINE_GETPLURK = APP_TIMELINE + "/getPlurk";

        public const string APP_TIMELINE_GETPLURKS = APP_TIMELINE + "/getPlurks";
        #endregion
    }
}
