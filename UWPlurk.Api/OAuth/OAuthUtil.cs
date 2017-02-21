using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace UWPlurk.Api.OAuth
{
    /// <summary>
    /// OAuth Utility class for PLurk OAuth use.
    /// </summary>
    public class OAuthUtil
    {

        private static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-.~";

        #region Public Methods

        /// <summary>
        /// Generates an unique string.
        /// </summary>
        /// <returns></returns>
        public static string GetNonce()
        {
            Random rand = new Random();
            int nonce = rand.Next(1, 99999999);
            return nonce.ToString("d8");        // return 64bit string, 
        }

        /// <summary>
        /// Returns the number of seconds since January 1, 1970 00:00:00 GMT (a.k.a. Unix Time).
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Math.Ceiling(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generates an OAuth signature from all provided parameters.
        /// </summary>
        /// <param name="appSecret">Application secret string.</param>
        /// <param name="tokenSecret">Token secret string.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="uri">Normalized service URI.</param>
        /// <param name="param">Parameters used in request.</param>
        /// <returns>HMAC-SHA1 signed 64bit encoded string.</returns>
        public static string GetSignature(string appSecret, string tokenSecret, string method, string uri, Dictionary<string, string> param)
        {
            // Build up base string
            StringBuilder sb = new StringBuilder();
            sb.Append(method).Append('&').Append(UrlEncode(uri)).Append('&');

            // Sort the params in alphametical order
            List<string> keys = param.Keys.ToList();
            keys.Sort(StringComparer.Ordinal);

            // Append all keys and params
            string prefix = "";
            for (int i = 0; i < keys.Count; i++)
            {
                sb.Append(prefix).Append(keys[i]).Append("%3D").Append(UrlEncode(UrlEncode(param[keys[i]]) ) );
                prefix = "%26";
            }

            string source = sb.ToString();
            string keyString = UrlEncode(appSecret) + "&" + UrlEncode(tokenSecret);

            // Commented to migrate to Windows.Security.Cryptography
            //HMACSHA1 hashProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            //byte[] result = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(source));
            var macAlProv = MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");

            var keyBuffer = CryptographicBuffer.ConvertStringToBinary(keyString, BinaryStringEncoding.Utf8);
            var key = macAlProv.CreateKey(keyBuffer);

            var sourceBuffer = CryptographicBuffer.ConvertStringToBinary(source, BinaryStringEncoding.Utf8);
            var signatureBuffer = CryptographicEngine.Sign(key, sourceBuffer);

            var signature = CryptographicBuffer.EncodeToBase64String(signatureBuffer);
            return signature;
        }

        /// <summary>
        /// Split response text from request token Url to OAuth Token instances.
        /// A empty token will be returned in case the response text is empty.
        /// </summary>
        /// <param name="responseTxt">Response Text.</param>
        /// <returns>OAuth Token.</returns>
        public static OAuthToken GetTokenFromResponse(string responseTxt)
        {
            OAuthToken resultToken = new OAuthToken();

            if (!String.IsNullOrEmpty(responseTxt))
            {
                string oauthToken = null;
                string oauthSecret = null;
                string[] keyValPairs = responseTxt.Split('&');

                for (int i = 0; i < keyValPairs.Length; i++)
                {
                    string[] splits = keyValPairs[i].Split('=');

                    switch (splits[0])
                    {
                        case "oauth_token":
                            oauthToken = splits[1];
                            break;
                        case "oauth_token_secret":
                            oauthSecret = splits[1];
                            break;
                    }

                }

                resultToken.content = oauthToken;
                resultToken.secret = oauthSecret;
            }

            return resultToken;
        }

        #endregion

        /// <summary>
        /// Gets the OAuth standard encoding of a string.
        /// </summary>
        /// <param name="srcUrl">String to be encoded.</param>
        /// <returns></returns>
        public static string UrlEncode(string srcUrl)
        {
            if (String.IsNullOrEmpty(srcUrl))
                return null;

            byte[] chars = Encoding.UTF8.GetBytes(srcUrl);

            StringBuilder sb = new StringBuilder();
            foreach (byte c in chars)
            {
                if (unreservedChars.IndexOf((char)c) >= 0)
                    sb.Append((char)c);
                else
                    sb.Append(String.Format("%{0:X2}", (int)c));
            }
            return sb.ToString();
        }
    }

}

