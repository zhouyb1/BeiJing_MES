using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using Ayma.Util;

namespace Ayma.Application.WebApi.Model
{
    public class WxPayDaya
    {

        public WxPayDaya()
        {
            
        }

        public SortedDictionary<string, object> wx_values = new SortedDictionary<string, object>();
        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
        */
        public void SetValue(string key, object value)
        {
            wx_values[key] = value;
        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
        public object GetValue(string key)
        {
            object o = null;
            wx_values.TryGetValue(key, out o);
            return o;
        }

        /**
         * 判断某个字段是否已设置
         * @param key 字段名
         * @return 若字段key已被设置，则返回true，否则返回false
         */
        public bool IsSet(string key)
        {
            object o = null;
            wx_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws NoNullAllowedException
        **/
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == wx_values.Count)
            {
                throw new NoNullAllowedException("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in wx_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    throw new NoNullAllowedException("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new NoNullAllowedException("WxPayData字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }

        /**
        * @接收从微信后台POST过来的数据(未验证签名)
        * @return 经转换得到的Dictionary
        * @throws NoNullAllowedException
        */
        public SortedDictionary<string, object> GetRequest()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = HttpContext.Current.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();
            
            #region 记录日志
            //var log = LogFactory.GetLogger("webapi");
            //var logMessage = new LogMessage { OperationTime = DateTime.Now };

            //logMessage.Url = "Ayma.Util.WxPayData";
            //logMessage.Class = "WxPayData.cs";
            //logMessage.Ip = Net.Ip;
            //logMessage.Host = Net.Host;
            //logMessage.Browser = Net.Browser;

            //logMessage.Content = builder.ToString();
            //string strMessage = new LogFormat().InfoFormat(logMessage);
            //log.Info(strMessage); 
            #endregion

            if (string.IsNullOrEmpty(builder.ToString()))
            {
                throw new NoNullAllowedException("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(builder.ToString());
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                wx_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            return wx_values;
        }

        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws NoNullAllowedException
        */
        public SortedDictionary<string, object> FromXml(string xml, string key)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new NoNullAllowedException("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                wx_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //2015-06-29 错误是没有签名
                if (wx_values["return_code"] != "SUCCESS")
                {
                    return wx_values;
                }
                CheckSign(key);//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new NoNullAllowedException(ex.Message);
            }

            return wx_values;
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in wx_values)
            {
                if (pair.Value == null)
                {
                    throw new NoNullAllowedException("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }


        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson()
        {
            string jsonStr = wx_values.ToJson();
            return jsonStr;
        }

        /**
        * @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
        */
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in wx_values)
            {
                if (pair.Value == null)
                {
                    throw new NoNullAllowedException("WxPayData内部含有值为null的字段!");
                }

                str += string.Format("{0}={1}<br>", pair.Key, pair.Value.ToString());
            }
            return str;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign(string key)
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + key;
            return Md5Helper.Encrypt(str, 32).ToUpper();
        }

        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign(string key)
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                throw new NoNullAllowedException("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                throw new NoNullAllowedException("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = MakeSign(key);

            if (cal_sign == return_sign)
            {
                return true;
            }

            throw new NoNullAllowedException("WxPayData签名验证错误!");
        }

        /**
        * @获取Dictionary
        */
        public SortedDictionary<string, object> GetValues()
        {
            return wx_values;
        }

        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}