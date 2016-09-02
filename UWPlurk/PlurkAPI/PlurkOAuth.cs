using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPlurk.PlurkAPI.OAuth;
using Windows.Security.Authentication.Web;
using Windows.Web.Http;

namespace UWPlurk.PlurkAPI
{
    /// <summary>
    /// This class responsible for OAuth requests required for Plurk API v2.0.
    /// And standard used is OAuth core 1.0a.
    /// </summary>
    public sealed partial class PlurkOAuth
    {
        #region "Private Regions"
        Uri requestTokenURI;
        Uri authorizeTokenURI;
        Uri accessTokenURI;

        /// <summary>
        /// Device ID, for same account access on mutilple device.
        /// </summary>
        private string deviceID { get; set; }

        /// <summary>
        /// Name of device accessing API.
        /// </summary>
        private string model { get; set; }

        /// <summary>
        /// OAuth Token for plurk API use.
        /// </summary>
        OAuthToken token;

        private string appKey = "";      // fill app key here
        private string appSecret = "";   // fill app secret here
        #endregion

        #region "Constant Fields"
        private const string baseURL = "http://www.plurk.com";

        private const string requestTokenPath = "/OAuth/request_token";
        private const string authorizeTokenPath = "/OAuth/authorize";
        private const string authorizeTokenPathMobile = "/m/authorize"; // For mobile use
        private const string accessTokenPath = "/OAuth/access_token";

        #endregion

        #region "Constructor"
        PlurkOAuth()
        {
            
        }

        PlurkOAuth(string appKey = "", string appSecret = "")
        {
            this.appKey = appKey;
            this.appSecret = appSecret;

            requestTokenURI = new Uri(baseURL + requestTokenPath);
            authorizeTokenURI = new Uri(baseURL + authorizeTokenPath);
            accessTokenURI = new Uri(baseURL + accessTokenPath);
        }
        #endregion

        #region "Public Methods"
        /// <summary>
        /// Retrieves a request token from Plurk and stores it in the current OAuthToken.
        /// </summary>
        public async void getRequestToken()
        {
            string method = "POST";

            // Parameters for requesting token
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("oauth_nonce", OAuthUtil.GetNonce());
            parameters.Add("oauth_consumer_key", appKey);                    // App key of this app
            parameters.Add("oauth_signature_method", "HMAC-SHA1");          // Singnature method accepted by Plurk
            parameters.Add("oauth_timestamp", OAuthUtil.GetTimeStamp());
            parameters.Add("oauth_version", "1.0");                         // Must be 1.0    

            // Generate the OAuth signature
            string signature = OAuthUtil.GetOathSignature(appSecret, token.secret, method, requestTokenURI.ToString(), parameters);
            parameters.Add("oauth_signature", signature);

            parameters.Add("oauth_callback", "oob");                        // Plurk omit this parameter

            // Sent the content 
            Task<String> waitingRepsonse = getResponseFromHttpPost(requestTokenURI, parameters);
            string response = await waitingRepsonse;
        }


        #endregion

        #region "Private Methods"
        private async Task<string> getResponseFromHttpPost(Uri targetUri, Dictionary<string, string> param)
        {

            HttpClient httpclient = new HttpClient();
            HttpFormUrlEncodedContent formcontent = new HttpFormUrlEncodedContent(param);

            try
            {
                HttpResponseMessage response = await httpclient.PostAsync(targetUri, formcontent);
            }
            catch (Exception ex)
            {

            }
           
            return "";
        }
        #endregion
    }
}
