using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace UWPlurk.Api.Web
{
    /// <summary>
    /// Manager class holding http client to perform all http requests. 
    /// </summary>
    public class HttpManager
    {
        private static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Send HTTP GET request to specific URL and retrieve string response.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> GetRequestAsync(string url)
        {
            Uri uri = new Uri(url);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Send HTTP POST request to specific URL and retrieve string response.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<string> SendPostRequest(string url, string body)
        {
            try
            {

                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                mSent.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                HttpResponseMessage response = await httpClient.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Send customized HTTP request to specific URL and retrieve string response.
        /// </summary>
        /// <param name="request">Request body.</param>
        /// <returns>HTTP Response in string format.</returns>
        public async static Task<string> SendRequestAsync(HttpRequestMessage request)
        {
            try
            {
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
