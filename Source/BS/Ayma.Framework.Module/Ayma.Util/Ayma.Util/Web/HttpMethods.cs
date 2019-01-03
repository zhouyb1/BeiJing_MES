using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Util
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.03.07
    /// 描 述：mvc过滤模式
    /// </summary>
    public class HttpMethods
    {

        /// <summary>
        /// 创建HttpClient
        /// </summary>
        /// <returns></returns>
        public static HttpClient CreateHttpClient(string url, IDictionary<string, string> cookies = null)
        {
            HttpClient httpclient;
            HttpClientHandler handler = new HttpClientHandler();
            var uri = new Uri(url);
            if (cookies != null)
            {
                foreach (var key in cookies.Keys)
                {
                    string one = key + "=" + cookies[key];
                    handler.CookieContainer.SetCookies(uri, one);
                }
            }
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                httpclient = new HttpClient(handler);
            }
            else
            {
                httpclient = new HttpClient(handler);
            }
            return httpclient;
        }
        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="jsonData">请求参数</param>
        /// <returns></returns>
        public static string Post(string url,string jsonData)
        {
            HttpClient httpClient = CreateHttpClient(url);
            var postData = new StringContent(jsonData);
            postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Task<string> result = httpClient.PostAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }
        public static string XmlPost(string url, string jsonData)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = false;
            req.UserAgent = "ADCSDK";
            req.ContentType = "application/xml";



            byte[] postData = Encoding.ASCII.GetBytes(jsonData);
            var reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            var rsp = (HttpWebResponse)req.GetResponse();
            var encoding = String.IsNullOrEmpty(rsp.CharacterSet)
                ? Encoding.GetEncoding("UTF-8")
                : Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string DoPost(string url, IDictionary<string, string> parameters)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = true;
            req.UserAgent = "ADCSDK";
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

            byte[] postData = Encoding.ASCII.GetBytes((string)BuildPostData(parameters));
            var reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            var rsp = (HttpWebResponse)req.GetResponse();
            var encoding = String.IsNullOrEmpty(rsp.CharacterSet)
                ? Encoding.GetEncoding("UTF-8")
                : Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="req">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, byte[] req)
        {
            HttpClient httpClient = CreateHttpClient(url);
            var postData = new ByteArrayContent(req);
            Task<string> result = httpClient.PostAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }

        /// <summary>
        /// get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            HttpClient httpClient = CreateHttpClient(url);
            Task<string> result = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
            return result.Result;
        }

        public static string DoGet(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.KeepAlive = true;
            req.UserAgent = "ADCSDK";
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            var rsp = (HttpWebResponse)req.GetResponse();
            if (String.IsNullOrEmpty(rsp.CharacterSet))
                return GetResponseAsString(rsp,Encoding.UTF8);
            var encoding = String.IsNullOrEmpty(rsp.CharacterSet)
                ? Encoding.GetEncoding("utf-8")
                : Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        private static string BuildPostData(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var postData = new StringBuilder();
            var hasParam = false;
            var dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                var name = dem.Current.Key;
                var value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(value)) continue;
                if (hasParam)
                {
                    postData.Append("&");
                }
                postData.Append(name);
                postData.Append("=");
                postData.Append(Uri.EscapeDataString(value));
                hasParam = true;
            }
            return postData.ToString();
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        private static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            var result = new StringBuilder();
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                if (stream != null) reader = new StreamReader(stream, encoding);

                // 每次读取不大于256个字符，并写入字符串
                var buffer = new char[256];
                int readBytes;
                while (reader != null && (readBytes = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    result.Append(buffer, 0, readBytes);
                }
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }

            return result.ToString();
        }
    }
}
