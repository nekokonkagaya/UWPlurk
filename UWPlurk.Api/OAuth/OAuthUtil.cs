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

        #region Private Properties
        private static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-.~";
        #endregion

        /// <summary>
        /// Generates an unique string.
        /// </summary>
        /// <returns></returns>
        public static string getNonce()
        {
            Random rand = new Random();
            int nonce = rand.Next(1000000000);
            return nonce.ToString("d8");        // return 64bit string, 
        }

        /// <summary>
        /// Returns the number of seconds since January 1, 1970 00:00:00 GMT (a.k.a. Unix Time).
        /// </summary>
        /// <returns></returns>
        public static string getTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
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
        public static string getSignature(string appSecret, string tokenSecret, string method, string uri, Dictionary<string, string> param)
        {
            // Build up base string
            StringBuilder sb = new StringBuilder();
            sb.Append(method).Append('&').Append(urlEncode(uri)).Append('&');

            // Sort the params in alphametical order
            List<string> keys = param.Keys.ToList();
            keys.Sort(StringComparer.Ordinal);

            // Append all keys
            string prefix = "";
            for (int i = 0; i < keys.Count; i++)
            {
                sb.Append(prefix).Append(keys[i]).Append("%3D").Append(urlEncode(urlEncode(param[keys[i]]) ) );
                prefix = "%26";
            }

            string source = sb.ToString();
            string key = urlEncode(appSecret) + "&" + urlEncode(tokenSecret);

            HMACSHA1 hashProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] result = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(source));

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Gets the OAuth standard encoding of a string.
        /// </summary>
        /// <param name="srcUrl">String to be encoded.</param>
        /// <returns></returns>
        private static string urlEncode(string srcUrl)
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

