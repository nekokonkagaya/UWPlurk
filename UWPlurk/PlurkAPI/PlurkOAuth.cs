using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.PlurkAPI
{
    class PlurkOAuth
    {
        #region "Private Regions"
        Uri requestTokenURI;
        Uri authorizeTokenURI;
        Uri accessTokenURI;

        // Device ID, for same account access on mutilple device
        private string deviceID { get; set; }
        // Name of device accessing API
        private string model { get; set; }
        #endregion

        #region "Constant Fields"
        private static string appKey = "";      // fill app key here
        private static string appSecret = "";   // fill app secret here
        private const string baseURL = "http://www.plurk.com";

        private const string requestTokenPath = "/OAuth/request_token";
        private const string authorizeTokenPath = "/OAuth/authorize";
        private const string authorizeTokenPathMobile = "/m/authorize"; // For mobile use
        private const string accessTokenPath = "/OAuth/access_token";

        #endregion

        #region "Constructor"
        PlurkOAuth()
        {
            requestTokenURI = new Uri(baseURL + requestTokenPath);
            authorizeTokenURI = new Uri(baseURL + authorizeTokenPath);
            accessTokenURI = new Uri(baseURL + accessTokenPath);
        }
        #endregion
    }
}
