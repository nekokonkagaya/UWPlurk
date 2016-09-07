using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace UWPlurk.Api.Http
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
                // Debug coding
                string response = "Error:" + ex.HResult.ToString("X") + " Exception: " + ex.Message;
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
            var content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            return await SendPostRequest(url, content);
        }

        public async static Task<string> SendPostRequest(string url, HttpStringContent content)
        {
            try
            {

                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                mSent.Content = content;
                HttpResponseMessage response = await httpClient.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Debug coding
                string response = "Error:" + ex.HResult.ToString("X") + " Exception: " + ex.Message;
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
            HttpResponseMessage response;

            try
            {
                response = await httpClient.SendRequestAsync(request);
                response.EnsureSuccessStatusCode();

                string responseTxt = await response.Content.ReadAsStringAsync();
                return responseTxt;
            }
            catch (Exception ex)
            {
                // Debug coding
                string responseTxt =  "Error:" + ex.HResult.ToString("X") + " Exception: " + ex.Message;
                return responseTxt;
            }
        }
    }
}
