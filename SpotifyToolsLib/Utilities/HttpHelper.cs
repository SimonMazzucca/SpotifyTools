﻿using SpotifyToolsLib.Spotify;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Utilities
{
    /// <summary>
    /// A helper class used as an interface for common HttpClient commands
    /// </summary>
    internal class HttpHelper
    {
        /// <summary>
        /// Downloads a url and reads its contents as a string using the get method
        /// </summary>
        public static async Task<string> Get(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { UseCookies = false })
            {
                HttpClient client = new HttpClient(handler);
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                return await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Below method puts cookie in header (UseCookies = false)
        /// https://stackoverflow.com/questions/12373738/how-do-i-set-a-cookie-on-httpclients-httprequestmessage
        /// </summary>
        public static async Task<string> Get(string url, IDictionary<string, string> headers)
        {
            using (HttpClientHandler handler = new HttpClientHandler { UseCookies = false })
            {
                HttpClient client = new HttpClient(handler);
                foreach (string key in headers.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, headers[key]);
                }

                HttpResponseMessage httpResponse = await client.GetAsync(url);
                return await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Downloads a url and reads its contents as a string, requires an authorization token
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> Get(string url, AuthenticationToken token, bool includeBearer = true)
        {
            HttpClient client = new HttpClient();
            if (includeBearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await client.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static async Task<string> Post(string url, Dictionary<string, string> postData = null)
        {
            HttpContent content = null;
            if (postData != null)
                content = new FormUrlEncodedContent(postData.ToArray<KeyValuePair<string, string>>());
            else
                content = null;

            HttpClient client = new HttpClient();

            var httpResponse = await client.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public static async Task<string> Post(string url, AuthenticationToken token, Dictionary<string, string> postData = null, bool includeBearer = true)
        {
            HttpContent content = null;
            if (postData != null)
                content = new FormUrlEncodedContent(postData.ToArray<KeyValuePair<string, string>>());
            else
                content = null;

            HttpClient client = new HttpClient();
            if (includeBearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await client.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public static async Task<string> Post(string url, AuthenticationToken token, string jsonString, bool includeBearer = true)
        {
            HttpContent content = new StringContent(jsonString);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeBearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await client.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// put data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public static async Task<string> Put(string url, AuthenticationToken token, Dictionary<string, string> postData = null, bool includeBearer = true)
        {
            HttpContent content = null;

            if (postData != null)
                content = new FormUrlEncodedContent(postData.ToArray<KeyValuePair<string, string>>());
            else
                content = null;

            HttpClient client = new HttpClient();
            if (includeBearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await client.PutAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// put data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public static async Task<string> Put(string url, AuthenticationToken token, string jsonString, bool includeBearer = true)
        {
            HttpContent content = new StringContent(jsonString);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeBearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await client.PutAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// http delete command
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public static async Task<string> Delete(string url, AuthenticationToken token, bool includeBearer = true)
        {
            HttpClient client = new HttpClient();
            if (includeBearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await client.DeleteAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
