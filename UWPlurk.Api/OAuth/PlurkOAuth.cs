using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UWPlurk.Api.Http;
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
        }

        public PlurkOAuth(string appKey = "", string appSecret = "", string tokenContent = "", string tokenSecret = "") 
            : this(appKey, appSecret)
        {
            token.content = tokenContent;
            token.secret = tokenSecret;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves a request token from Plurk and stores it in the current OAuthToken.
        /// </summary>
        public async Task<OAuthToken> GetRequestToken()
        {
            // Parameters for requesting token
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("oauth_callback", "oob");                        // Plurk seems omit this parameter

            // Prepare HTTP request and sent
            HttpRequestMessage request = createRequestMessage(Constants.URL_REQUEST_TOKEN, "POST", parameters);
            
            // Send Http request and get response
            string response = await HttpManager.SendRequestAsync(request);
            
            token = OAuthUtil.GetTokenFromResponse(response);

            return token;
        }

        /// <summary>
        /// Returns the URL to the authorization page.
        /// Method GetRequestToken() must be called before calling this method.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizeTokenUrl()
        {
            return GetAuthorizeTokenUrl(this.deviceId, this.model);
        }

        /// <summary>
        /// Returns the URL to the authorization page with device ID and model attached for multi login purpose.
        /// Method GetRequestToken() must be called before calling this method.
        /// </summary>
        /// <param name="newDeviceID">Device ID of host machine.</param>
        /// <param name="newModel">Model name of host machine.</param>
        /// <returns></returns>
        public string GetAuthorizeTokenUrl(string newDeviceID, string newModel)
        {
            StringBuilder sb = new StringBuilder();
            // Append with token
            // TODO: Platform specific authorization URL?
            string authorizeBase = Constants.URL_AUTHORIZE_BASE;

            sb.Append(String.Format("{0}?oauth_token={1}", authorizeBase, token.content) );

            // Append device ID and model for multi login purpose
            if (!String.IsNullOrEmpty(newDeviceID))
                sb.Append(String.Format("{0}&deviceid={1}", sb.ToString(), newDeviceID) );

            if (!String.IsNullOrEmpty(newModel)) {
                newModel = newModel.Replace(' ', '+'); // replace space to '+' per Plurk API suggests 
                sb.Append(String.Format("{0}&model={1}", sb.ToString(), newModel));
            }

            return sb.ToString();
        }

        public async Task<OAuthToken> GetAccessToken(string verifier)
        {
            // Parameters for requesting token
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("oauth_token", token.content);
            param.Add("oauth_verifier", verifier);

            // Prepare HTTP request and sent
            HttpRequestMessage request = createRequestMessage(Constants.URL_ACCESS_TOKEN, "POST", param);
            // Send Http request and get response
            string response = await HttpManager.SendRequestAsync(request);
            token = OAuthUtil.GetTokenFromResponse(response);

            return token;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Create HTTP Request object with specified OAuth parameters and URL.
        /// </summary>
        /// <param name="url">Target URL of request.</param>
        /// <param name="method">HTTP request Method, accept POST/GET/PUT.</param>
        /// <param name="param">OAuth parameters.</param>
        /// <returns>The created HttpRequestMessage.</returns>
        private HttpRequestMessage createRequestMessage(string url, string method, Dictionary<string, string> param)
        {
            HttpRequestMessage request;
            Uri targetUri = new Uri(url);

            switch (method.ToUpper().Trim())
            {
                case "POST":
                    request = new HttpRequestMessage(HttpMethod.Post, targetUri);
                    break;
                case "PUT":
                    request = new HttpRequestMessage(HttpMethod.Put, targetUri);
                    break;
                case "GET":
                default:
                    request = new HttpRequestMessage(HttpMethod.Get, targetUri);
                    break;
            }

            // Add parameter required for signature
            param.Add("oauth_consumer_key", appKey);                   // App key of this app
            param.Add("oauth_nonce", OAuthUtil.GetNonce());
            param.Add("oauth_signature_method", "HMAC-SHA1");          // Singnature method accepted by Plurk
            param.Add("oauth_timestamp", OAuthUtil.GetTimeStamp());
            param.Add("oauth_version", "1.0");                         // Must be 1.0    

            // Generate the OAuth signature
            string signature = OAuthUtil.GetSignature(appSecret, token.secret, method, url, param);
            param.Add("oauth_signature", signature);

            // Build up authorization Header
            StringBuilder sb = new StringBuilder("OAuth realm=\"\"");
            foreach (KeyValuePair<string, string> content in param)
            {
                if (content.Key.StartsWith("oauth_"))
                {
                    sb.Append(", ").Append(content.Key).Append("=\"").Append(OAuthUtil.UrlEncode(content.Value)).Append("\"");
                }
            }
            string authorization = sb.ToString();
            request.Headers.Add("Authorization", authorization);

            request.Content = new HttpFormUrlEncodedContent(param);
            request.Content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/x-www-form-urlencoded");

            return request;
        }

        #endregion


    }
}
