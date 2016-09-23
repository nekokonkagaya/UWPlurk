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
        public const string APP_USERS = "/Users";

        public const string APP_USERS_ME = APP_USERS + "/me";

        public const string APP_USERS_UPDATE = APP_USERS + "/update";

        public const string APP_USERS_UPDATEPICTURE = APP_USERS + "/updatePicture";

        public const string APP_USERS_GETKARMASTATS = APP_USERS + "/getKarmaStats";
        #endregion

        #region Profile
        public const string APP_PROFILE = "/Profile";

        public const string APP_PROFILE_GETOWNPROFILE = APP_PROFILE + "/getOwnProfile";

        public const string APP_PROFILE_GETPUBLICPROFILE = APP_PROFILE + "/getPublicProfile";
        #endregion

        #region Real time notifications
        public const string APP_REALTIME_GETUSERCHANNEL = "/Realtime/getUserChannel";
        #endregion

        #region Polling
        public const string APP_POLLING = "/Polling";

        public const string APP_POLLING_GETPLURKS = APP_POLLING + "/getPlurks";

        public const string APP_POLLING_GETUNREADCOUNT = APP_POLLING + "/getUnreadCount";
        #endregion

        #region Timeline
        public const string APP_TIMELINE = "/Timeline";

        public const string APP_TIMELINE_GETPLURK = APP_TIMELINE + "/getPlurk";

        public const string APP_TIMELINE_GETPLURKS = APP_TIMELINE + "/getPlurks";

        public const string APP_TIMELINE_GETUNREADPLURKS = APP_TIMELINE + "/getUnreadPlurks";

        public const string APP_TIMELINE_GETPUBLICPLURKS = APP_TIMELINE + "/getPublicPlurks";

        public const string APP_TIMELINE_PLURKADD= APP_TIMELINE + "/plurkAdd";

        public const string APP_TIMELINE_PLURKDELETE = APP_TIMELINE + "/plurkDelete";

        public const string APP_TIMELINE_PLURKEDIT = APP_TIMELINE + "/plurkEdit";

        public const string APP_TIMELINE_TOGGLECOMMENTS = APP_TIMELINE + "/toggleComments";

        public const string APP_TIMELINE_MUTEPLURKS = APP_TIMELINE + "/mutePlurks";

        public const string APP_TIMELINE_UNMUTEPLURKS = APP_TIMELINE + "/unmutePlurks";

        public const string APP_TIMELINE_FAVORITEPLURKS = APP_TIMELINE + "/favoritePlurks";

        public const string APP_TIMELINE_UNFAVORITEPLURKS = APP_TIMELINE + "/unfavoritePlurks";

        public const string APP_TIMELINE_REPLURK = APP_TIMELINE + "/replurk";

        public const string APP_TIMELINE_UNREPLURK = APP_TIMELINE + "/unreplurk";

        public const string APP_TIMELINE_MARKASREAD = APP_TIMELINE + "/markAsRead";

        public const string APP_TIMELINE_UPLOADPICTURE = APP_TIMELINE + "/uploadPicture";

        public const string APP_TIMELINE_REPORTABUSE = APP_TIMELINE + "/reportAbuse";
        #endregion

        #region Responses
        public const string APP_RESPONSES = "/Responses";

        public const string APP_RESPONSES_GET = APP_RESPONSES + "/get";

        public const string APP_RESPONSES_RESPONSEADD = APP_RESPONSES + "/responseAdd";

        public const string APP_RESPONSES_RESPONSEDELETE = APP_RESPONSES + "/responseDelete";
        #endregion

        #region FriendsFans
        public const string APP_FRIENDSFANS = "/Responses";

        public const string APP_FRIENDSFANS_GETFRIENDSBYOFFSET = APP_FRIENDSFANS + "/getFriendsByOffset";

        public const string APP_FRIENDSFANS_GETFANSBYOFFSET = APP_FRIENDSFANS + "/getFansByOffset";

        public const string APP_FRIENDSFANS_GETFOLLOWINGBYOFFSET = APP_FRIENDSFANS + "/getFollowingByOffset";
        #endregion

        /// <summary>
        /// URL prefix for custom emoticons.
        /// </summary>
        public const string EMO_URL_PREFIX = "https://emos.plurk.com";
    }
}
