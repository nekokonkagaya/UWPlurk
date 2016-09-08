using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api.OAuth
{
    /// <summary>
    /// This class stores OAuth Token, following standard of plurk API v2.0 use.
    /// </summary>
    public sealed class OAuthToken
    {
        #region "Properties"
        /// <summary>
        /// Content of token, representing oauth_token.
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Secret of token, representing oauth_token_secret.
        /// </summary>
        public string secret { get; set; }
        #endregion

        /// <summary>
        /// Default constructor, create a empty token.
        /// </summary>
        public OAuthToken()
        {
            content = null;
            secret = null;
        }

        /// <summary>
        /// Create a token with specific data.
        /// </summary>
        /// <param name="tokenContent">Token content.</param>
        /// <param name="TokenSecret">Token Secret.</param>
        public OAuthToken(string tokenContent, string TokenSecret)
        {
            content = tokenContent;
            secret = TokenSecret;
        }
    }

}
