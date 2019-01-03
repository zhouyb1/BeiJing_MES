using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ayma.Util
{
    public class SMSHelper
    {
        private static string CORPID = "8080";//企业ID
        private static string USERNAME = "8080";//用户ID
        private static string PASSWORD = "80808080";//密码
        private static string URL = "http://211.147.252.14/?";

        //查询短信余额
        //http://211.147.252.14/Balance.asp?CORPID=8080&USERNAME=8080&PASSWORD=80808080

        public static string Send(string mobile, string content)
        {
            string http = URL + "CORPID=" + CORPID + "&USERNAME=" + USERNAME + "&PASSWORD=" + PASSWORD + "&EXTNO=&MOBILE=" + mobile + "&CONTENT=" + HttpUtility.UrlEncode(content, Encoding.GetEncoding("GB2312"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; Maxthon 2.0)";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("GB2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;


        }

        public static string Http_post(string mobile, string content)
        {
            string postData = "CORPID=8080&USERNAME=8080&PASSWORD=80808080&EXTNO=&MOBILE=15999941174&CONTENT=测试短信【爱码电子】";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest =
            (HttpWebRequest)WebRequest.Create("http://211.147.252.14");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int status = (int)myResponse.StatusCode;
            reader.Close();
            return result;
        }
    }
}
