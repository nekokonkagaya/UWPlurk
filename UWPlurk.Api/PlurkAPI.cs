using Newtonsoft.Json;
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
        private string defaultHttpMethod = "POST";
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
        public async Task<User> Me()
        {
            string response = await SendRequest(Constants.APP_USERS + "/me", "POST", null);
            return CreateEntity<User>(response);

        }

        /// <summary>
        /// Update a user's information (such as display name, email or privacy).
        /// All arguments of the function is optional.
        /// </summary>
        /// <param name="full_name">Optional, change full name if provided.</param>
        /// <param name="email">Optional, change email if provided.</param>
        /// <param name="display_name">Optional, change display name if provided. Can be empty and full unicode but must be shorter than 15 characters.</param>
        /// <param name="privacy">Optional, User's privacy settings. The option can be world (whole world can view the profile) or only_friends (only friends can view the profile).</param>
        /// <param name="date_of_birth">Optional, Date of Birth for user.</param>
        /// <returns></returns>
        public async Task<User> Update(string full_name, string email, string display_name, string privacy, DateTime date_of_birth)
        {
            Dictionary<String, string> param = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(full_name) )
                param.Add("full_name", full_name);

            if (!string.IsNullOrEmpty(email))
                param.Add("email", email);

            if (!string.IsNullOrEmpty(display_name))
                param.Add("display_name", display_name);

            if (!string.IsNullOrEmpty(privacy))
                param.Add("privacy", privacy);

            if (null != date_of_birth)
            {
                // Process date format to YYYYY-MM-DD
                var formatDate = String.Format("YYYY-MM-DD", date_of_birth);
                param.Add("date_of_birth", formatDate);
            }

            string response = await SendRequest(Constants.APP_USERS + "/update", "POST", param);
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
                string response = await SendRequest(Constants.APP_USERS + "/updatePicture", "POST", param);
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
            string response = await SendRequest(Constants.APP_USERS + "/getKarmaStats", "POST", null);
            return CreateEntity<KarmaStats>(response);
        }
        #endregion

        #region Profile
        #endregion

        #region Private Methods
        private T CreateEntity<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion
    }
}
