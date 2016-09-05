using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api
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

        /// <summary>
        /// 
        /// </summary>
        public string callbackConfirmed { get; set; }
        #endregion
    }
}
