using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.PlurkAPI
{
    /// <summary>
    /// This class stores OAuth Token.
    /// </summary>
    public sealed class OAuthToken
    {
        #region "Properties"
        string content { get; set; }
        string secret { get; set; }
        #endregion
    }
}
