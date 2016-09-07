using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPlurk.Api.OAuth;

namespace UWPlurk.Api
{
    /// <summary>
    /// Plurk API manager class
    /// </summary>
    public class PlurkAPI : PlurkOAuth
    {
        #region Constructor
        public PlurkAPI(string appKey, string appSecret) 
            : base(appKey, appSecret)
        {

        }

        public PlurkAPI(string appKey, string appSecret, string tokenContent, string tokenSecret) 
            : base(appKey, appSecret, tokenContent, tokenSecret, OAuthTokenType.Empty)
        {

        }

        #endregion
    }
}
