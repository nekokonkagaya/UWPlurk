using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UWPlurk.Api.Web;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace UWPlurk.Api.OAuth
{
    /// <summary>
    /// This class responsible for OAuth requests required for Plurk API v2.0.
    /// And standard used is OAuth core 1.0a.
    /// </summary>
    public class PlurkOAuth
    {
        #region Private Regions
        Uri requestTokenURI;
        Uri accessTokenURI;

        /// <summary>
        /// Device ID, for same account access on mutilple device.
        /// </summary>
        private string deviceId { get; set; }

        /// <summary>
        /// Name of device accessing API.
        /// </summary>
        private string model { get; set; }

        /// <summary>
        /// OAuth Token for plurk API use.
        /// </summary>
        public OAuthToken token;

        private string appKey = "";      
        private string appSecret = "";  
        #endregion

        #region Constructor
        public PlurkOAuth() : this("", "")
        {

        }

        public PlurkOAuth(string appKey = "", string appSecret = "")
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            token = new OAuthToken();

            requestTokenURI = new Uri(Constants.URL_REQUEST_TOKEN);
            accessTokenURI = new Uri(Constants.URL_ACCESS_TOKEN);     
        }

        public PlurkOAuth(string appKey = "", string appSecret = "", string tokenContent = "", string tokenSecret = "", OAuthTokenType tokenState = OAuthTokenType.Empty) 
            : this(appKey, appSecret)
        {
            token.content = tokenContent;
            token.secret = tokenSecret;
            token.state = tokenState;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves a request token from Plurk and stores it in the current OAuthToken.
        /// </summary>
        public async Task<OAuthToken> GetRequestToken()
        {
            string method = "POST";

            // Parameters for requesting token
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("oauth_nonce", OAuthUtil.GetNonce());
            parameters.Add("oauth_consumer_key", appKey);                   // App key of this app
            parameters.Add("oauth_signature_method", "HMAC-SHA1");          // Singnature method accepted by Plurk
            parameters.Add("oauth_timestamp", OAuthUtil.GetTimeStamp());
            parameters.Add("oauth_version", "1.0");                         // Must be 1.0    

            // Generate the OAuth signature
            string signature = OAuthUtil.GetSignature(appSecret, token.secret, method, requestTokenURI.ToString(), parameters);
            parameters.Add("oauth_signature", signature);

            parameters.Add("oauth_callback", "oob");                        // Plurk omit this parameter

            // Prepare HTTP request and sent
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestTokenURI);

            // Possible paramter for authorization?
            StringBuilder sb = new StringBuilder("OAuth realm=\""+ requestTokenURI.ToString() + "\"");
            foreach(KeyValuePair<string, string> content in parameters)
            {
                if (content.Key.StartsWith("oauth_"))
                {
                    sb.Append(", ").Append(content.Key).Append("=\"").Append(OAuthUtil.UrlEncode(content.Value)).Append("\"");
                }
            }
            string authorization = sb.ToString();
            request.Headers.Add("Authorization", authorization);

            request.Content = new HttpFormUrlEncodedContent(parameters);
            request.Content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/x-www-form-urlencoded");
            
            // Send Http request and get response
            string response = await HttpManager.SendRequestAsync(request);


            token = OAuthUtil.GetTokenFromResponse(response);
            token.state = OAuthTokenType.Temporary;

            return token;
        }

        /// <summary>
        /// Returns the URL to the authorization page.
        /// Method GetRequestToken() must be called before calling this method.
        /// </summary>
        /// <returns></returns>
        public string getAuthorizeTokenUrl()
        {
            return getAuthorizeTokenUrl(this.deviceId, this.model);
        }

        /// <summary>
        /// Returns the URL to the authorization page with device ID and model attached for multi login purpose.
        /// Method GetRequestToken() must be called before calling this method.
        /// </summary>
        /// <param name="newDeviceID">Device ID of host machine.</param>
        /// <param name="newModel">Model name of host machine.</param>
        /// <returns></returns>
        public string getAuthorizeTokenUrl(string newDeviceID, string newModel)
        {
            StringBuilder sb = new StringBuilder();
            // Append with token
            // TODO: Platform specific authorization URL?
            string authorizeBase = Constants.URL_AUTHORIZE_BASE;

            sb.Append(String.Format("{0}?oauth_token={1}", authorizeBase, token.content) );

            // Append device ID and model for multi login purpose
            if (!String.IsNullOrEmpty(newDeviceID))
                sb.Append(String.Format("{0}?deviceid={1}", sb.ToString(), newDeviceID) );

            if (!String.IsNullOrEmpty(newModel)) {
                newModel = newModel.Replace(' ', '+'); // replace space to '+' as Plurk API suggests 
                sb.Append(String.Format("{0}?model={1}", sb.ToString(), newModel));
            }

            return sb.ToString();
        }
        #endregion

        #region Private Methods

        #endregion


    }
}
