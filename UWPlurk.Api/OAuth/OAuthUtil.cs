using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.OAuth
{
    /// <summary>
    /// OAuth Utility class for PLurk OAuth use.
    /// </summary>
    public class OAuthUtil
    {
        /// <summary>
        /// Generates an unique string.
        /// </summary>
        /// <returns></returns>
        public static string GetNonce()
        {
            Random rand = new Random();
            int nonce = rand.Next(1000000000);
            return nonce.ToString("d8");        // return 64bit string, 
        }

        /// <summary>
        /// Returns the number of seconds since January 1, 1970 00:00:00 GMT (a.k.a. Unix Time).
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generates an OAuth signature (respose to "oauth_signature") from all provided parameters.
        /// </summary>
        /// <param name="appSecret">Application secret string.</param>
        /// <param name="tokenSecret">Token secret string.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="uri">Normalized service URI.</param>
        /// <param name="param">Parameters used in request.</param>
        /// <returns>HMAC-SHA1 signed 64bit encoded string.</returns>
        public static string GetOathSignature(string appSecret, string tokenSecret, string method, string uri, Dictionary<string, string> param)
        {
            // Build up base string
            StringBuilder sb = new StringBuilder();
            sb.Append(method).Append('&').Append(Uri.EscapeUriString(uri)).Append('&');

            // Sort the params
            List<string> keys = param.Keys.ToList();
            keys.Sort(StringComparer.Ordinal);

            // Append all keys
            string prefix = "";
            for (int i = 0; i < keys.Count; i++)
            {
                sb.Append(prefix).Append(keys[i]).Append("%3D").Append(Uri.EscapeUriString(Uri.EscapeUriString(param[keys[i]])));
                prefix = "%26";
            }

            string source = sb.ToString();
            string key = Uri.EscapeUriString(appSecret) + "&" + Uri.EscapeUriString(tokenSecret);

            HMACSHA1 hashProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] result = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(source));

            return Convert.ToBase64String(result);
        }
    }

}

