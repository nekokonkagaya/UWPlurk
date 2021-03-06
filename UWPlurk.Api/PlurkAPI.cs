﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPlurk.Api.Entities;
using UWPlurk.Api.OAuth;

namespace UWPlurk.Api
{
    /// <summary>
    /// Plurk API manager class.
    /// </summary>
    public class PlurkAPI : PlurkOAuth
    {
        #region Properties
        /// <summary>
        /// Default HTTP Method
        /// </summary>
        private string _HttpMethod = "POST";
        #endregion

        #region Constructor
        public PlurkAPI(string appKey, string appSecret) 
            : base(appKey, appSecret)
        {

        }

        public PlurkAPI(string appKey, string appSecret, string tokenContent, string tokenSecret) 
            : base(appKey, appSecret, tokenContent, tokenSecret)
        {

        }
        #endregion

        #region Users
        /// <summary>
        /// Returns information about current user, including page-title and user-about. 
        /// </summary>
        /// <returns></returns>
        public async Task<User> MyProfile()
        {
            string response = await SendRequest(Constants.APP_USERS + "/me", _HttpMethod, null);
            return CreateEntity<User>(response);

        }

        /// <summary>
        /// Update a user's information (such as display name, email or privacy).
        /// All arguments of the function is optional.
        /// </summary>
        /// <param name="fullName">Optional, change full name if provided.</param>
        /// <param name="email">Optional, change email if provided.</param>
        /// <param name="displayName">Optional, change display name if provided. Can be empty and full unicode but must be shorter than 15 characters.</param>
        /// <param name="privacy">Optional, User's privacy settings. The option can be world (whole world can view the profile) or only_friends (only friends can view the profile).</param>
        /// <param name="dateOfBirth">Optional, Date of Birth for user.</param>
        /// <returns></returns>
        public async Task<User> Update(string fullName, string email, string displayName, string privacy, DateTime dateOfBirth)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(fullName) )
                param.Add("full_name", fullName);

            if (!string.IsNullOrEmpty(email))
                param.Add("email", email);

            if (!string.IsNullOrEmpty(displayName))
                param.Add("display_name", displayName);

            if (!string.IsNullOrEmpty(privacy))
                param.Add("privacy", privacy);

            if (null != dateOfBirth)
            {
                // Process date format to YYYYY-MM-DD
                var formatDate = String.Format("YYYY-MM-DD", dateOfBirth);
                param.Add("date_of_birth", formatDate);
            }

            var response = await SendRequest(Constants.APP_USERS + "/update", _HttpMethod, param);
            return CreateEntity<User>(response);
        }

        /// <summary>
        /// Update a user's profile picture. API usershould do a multipart/form-data POST request to /API/Users/updatePicture. 
        /// </summary>
        /// <param name="profile_image">The new profile image.</param>
        /// <returns></returns>
        public async Task<User> UpdatePicture(string profile_image)
        {
            User result = null;

            // TODO: check image path exists or valid format
            if (!String.IsNullOrEmpty(profile_image))
            {
                Dictionary<string, string> param = new Dictionary<string, string> { { "profile_image", profile_image } };
                string response = await SendRequest(Constants.APP_USERS + "/updatePicture", _HttpMethod, param);
                result = CreateEntity<User>(response);
            }

            return result;

        }

        /// <summary>
        /// Returns info about current user's karma, including current karma, karma growth, karma graph and the latest reason why the karma has dropped. 
        /// </summary>
        /// <returns></returns>
        public async Task<KarmaStats> GetKarmaStats()
        {
            string response = await SendRequest(Constants.APP_USERS + "/getKarmaStats", _HttpMethod, null);
            return CreateEntity<KarmaStats>(response);
        }
        #endregion

        #region Profile
        /// <summary>
        /// Returns data that's private for the current user. 
        /// This can be used to construct a profile and render a timeline of the latest plurks. 
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetOwnProfile()
        {
            string response = await SendRequest(Constants.APP_PROFILE + "/getOwnProfile", _HttpMethod, null);
            return CreateEntity<User>(response);
        }

        /// <summary>
        /// Fetches public information such as a user's public plurks and basic information. 
        /// Fetches also if the current user is following the user, are friends with or is a fan. 
        /// </summary>
        /// <param name="user_id">The user_id of the public profile. Can be integer (like 34) or nick name (like amix).</param>
        /// <returns></returns>
        public async Task<User> GetPublicProfile(string user_id)
        {
            string response = await SendRequest(Constants.APP_PROFILE + "/getPublicProfile", _HttpMethod, null);
            return CreateEntity<User>(response);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Convert JSON String to instance object.
        /// </summary>
        /// <typeparam name="T">Class instance to convert.</typeparam>
        /// <param name="jsonString">JSON raw string.</param>
        /// <returns></returns>
        private T CreateEntity<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion
    }
}
