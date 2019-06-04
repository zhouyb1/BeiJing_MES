using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace DesktopApp
{
    class FaceRecognition
    {
        public static string Port(string strUrl, string staData)
        {
            try
            {
                string url = strUrl;
                string postData = staData;

                if (true)//UrlIsExist(strUrl))
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "Post";
                    //request.Method = "PROPFIND";
                    request.ContentType = "application/x-www-form-urlencoded";
                    //request.ContentLength = postData.Length;
                    
                    StreamWriter sw = new StreamWriter(request.GetRequestStream());
                    sw.Write(postData,0,postData.Length);
                    sw.Flush();

                    WebResponse response = request.GetResponse();
                    Stream s = response.GetResponseStream();

                    StreamReader sr = new StreamReader(s, Encoding.GetEncoding("UTF-8"));
                    string strTemp = sr.ReadToEnd();
                    sw.Dispose();
                    sw.Close();
                    sr.Dispose();
                    sr.Close();
                    s.Dispose();
                    s.Close();
                    return strTemp;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        /// <summary>
        /// 采用https协议访问网络
        /// </summary>
        /// <param name="URL">url</param>
        /// <param name="strPostdata">发送的数据</param>
        /// <returns>服务端返回的数据</returns>
        public static string RequestWithHttps(string strUrl, string strPostdata)
        {
            string data = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Timeout = 3000000;
                request.Method = "post";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";

                /*判断是否需要发送数据*/
                if (!string.IsNullOrEmpty(strPostdata))
                {
                    byte[] buffer = encoding.GetBytes(strPostdata);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                response = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), encoding);

                data = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                data = "";
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (reader != null)
                    reader.Close();
            }

            return data;
        }

        /// <param name="POSTURL">请求提交的地址 如
        /// <param name="PostData">提交的数据(字符串)</param>
        /// <returns></returns>
        public static string RequestData(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }

        /// <summary>
        /// POST请求与获取结果
        /// </summary>
        public static string HttpPost(string Url, string postDataStr)
        {
            try
            {
                postDataStr = postDataStr.Replace("+", "%2B");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 6000000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postDataStr.Length;
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(postDataStr);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                return retString;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUrl">FTP地址</param>
        /// <returns></returns>
        public static Stream Info(string ftpUrl)
        {
            try
            {
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl));
                reqFtp.UseBinary = true;
                FtpWebResponse respFtp = (FtpWebResponse)reqFtp.GetResponse();
                Stream stream = respFtp.GetResponseStream();
                return stream;
            }
            catch (Exception)
            {
                throw;
            }
        } 

        /// <summary>
        /// 判断json格式
        /// </summary>
        /// <param name="strtemp"></param>
        /// <returns></returns>
        public static bool json(string strtemp)
        {
            try
            {
                JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DateTime ConvertToDateTime(string timestamp)
        {
            System.DateTime time = System.DateTime.MinValue;
            //精确到毫秒
            //时间戳转成时间
            DateTime start = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            try
            {
                time = timestamp.Length == 10 ? start.AddSeconds(long.Parse(timestamp)) : start.AddMilliseconds(long.Parse(timestamp));
                return time;
            }
            catch (Exception ex)
            {
                return start;//转换失败
            }
        }

        /// <summary>
        /// 图片转base64
        /// </summary>
        /// <param name="fileadd"></param>
        /// <returns></returns>
        public static string Get_zjdz(string fileadd)
        {
            FileStream fs = new FileStream(fileadd, FileMode.Open);
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] fl = new Byte[filelength];
            fs.Read(fl, 0, filelength);
            fs.Close();

            string bbc1 = Convert.ToBase64String(fl);

            string bbc2 = HttpUtility.UrlEncode(bbc1, Encoding.UTF8);

            return bbc2;
        }

        //static void Main(string[] args)
        //{
        //    // 脸部识别
        //    // FaceDetect.detect();
        //    // 图片转 base64
        //    string base64Str = ImageToBase64("C:/Users/Administrator/Desktop/image.png");
        //    // base64转 图片
        //    Base64ToImage(base64Str);
        //}
        /// <summary>
        /// base64 转 Image
        /// </summary>
        /// <param name="base64"></param>
        public static void Base64ToImage(string base64)
        {
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            Image mImage = Image.FromStream(memStream);
            Bitmap bp = new Bitmap(mImage);
            bp.Save("C:/Users/Administrator/Desktop/" + DateTime.Now.ToString("yyyyMMddHHss") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//注意保存路径
        }

        /// <summary>
        /// Image 转成 base64
        /// </summary>
        /// <param name="fileFullName"></param>
        public static string ImageToBase64(string fileFullName)
        {
            try
            {
                Bitmap bmp = new Bitmap(fileFullName);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length]; ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length); ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 图片 转为    base64编码的文本
        /// </summary>
        /// <param name="bmp">待转的Bitmap</param>
        /// <returns>转换后的base64字符串</returns>
        public static  String ImgToBase64String(Image bmp)
        {
            String strbaser64 = String.Empty;
            var btarr = convertByte(bmp);
            strbaser64 = Convert.ToBase64String(btarr);

            return strbaser64;
        }

        /// <summary>
        /// Image转byte[]
        /// </summary>
        /// <param name="img">Img格式数据</param>
        /// <returns>byte[]格式数据</returns>
        public static byte[] convertByte(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            byte[] bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }

        private static bool UrlIsExist(string URL)
        {
            try
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(URL);
                request.Timeout = 10000;
                System.Net.WebResponse response = request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>   
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法)   
        /// </summary>   
        /// <param name="address">文件上传到的服务器</param>   
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param>   
        /// <param name="saveName">文件上传后的名称</param>   
        /// <returns>成功返回1，失败返回0</returns>   
        public static string HttpUploadFile(string address, string fileNamePath, string saveName)
        {
            int returnValue = 0;


            int pos = fileNamePath.LastIndexOf("\\");
            if (saveName == "")
                saveName = fileNamePath.Substring(pos + 1);

            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);     //时间戳   
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");     //请求头部信息   
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\";");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);     // 根据uri创建HttpWebRequest对象   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";     //对发送的数据不使用缓存   
            httpReq.AllowWriteStreamBuffering = false;     //设置获得响应的超时时间（300秒）   
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            string sReturnString = "";
            try
            {
                //每次上传4k  
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();         //发送请求头部消息   
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    //Application.DoEvents();
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳   
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();         //获取服务器端的响应   
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                //读取服务器端返回的消息  
                StreamReader sr = new StreamReader(s);
                sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
            }
            catch (Exception ex)
            {
                sReturnString = ex.Message;
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return sReturnString;

        }



        public static bool uploadFileByHttp(string webUrl, string localFileName)
        {
            // 检查文件是否存在
            if (!System.IO.File.Exists(localFileName))
            {
                MessageBox.Show("{0} does not exist!", localFileName);
                return false;
            }
            try
            {
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myWebClient.UploadFile(webUrl, "POST", localFileName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>  
        /// WebClient上传文件至服务器，默认不自动改名  
        /// </summary>  
        /// <param name="fileNamePath">文件名，全路径格式</param>  
        /// <param name="uriString">服务器文件夹路径</param>
        /// <param name="Name">文件重命名</param>
        public static void UpLoadFile(string fileNamePath, string uriString,string Name)
        {
            UpLoadFile(fileNamePath, uriString, false, Name);
        }
        /// <summary>  
        /// WebClient上传文件至服务器  
        /// </summary>  
        /// <param name="fileNamePath">文件名，全路径格式</param>  
        /// <param name="uriString">服务器文件夹路径</param>  
        /// <param name="IsAutoRename">是否自动按照时间重命名</param>  
        /// <param name="Name">文件重命名</param>
        private static void UpLoadFile(string fileNamePath, string uriString, bool IsAutoRename, string Name)
        {
            string fileName = fileNamePath.Substring(fileNamePath.LastIndexOf("\\") + 1);
            string NewFileName = fileName;
            if (IsAutoRename)
            {
                NewFileName = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + fileNamePath.Substring(fileNamePath.LastIndexOf("."));
            }
            else
            {
                NewFileName = Name;
            }
            string fileNameExt = fileName.Substring(fileName.LastIndexOf(".") + 1);
            //if (uriString.EndsWith("/") == false) uriString = uriString + "/";
            //if (!Directory.Exists(uriString))//如果不存在就创建file文件夹
            //{
            //    Directory.CreateDirectory(uriString);
            //}
            uriString = uriString + NewFileName;
            /**/
            /// 创建WebClient实例  
            System.Net.WebClient myWebClient = new WebClient();

           // myWebClient.Credentials = new NetworkCredential("10.1.31.218", "0.");

             myWebClient.Credentials = CredentialCache.DefaultCredentials;
            // 要上传的文件  
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            byte[] postArray = r.ReadBytes((int)fs.Length);

            Stream postStream = myWebClient.OpenWrite(uriString, "PUT");
            try
            {
                //使用UploadFile方法可以用下面的格式  
                if (postStream.CanWrite)
                {
                    postStream.Write(postArray, 0, postArray.Length);
                    postStream.Close();
                    fs.Dispose();
                    //  log.AddLog("上传日志文件成功！", "Log");
                    //  basicInfo.writeLogger("上传日志文件成功！" );
                }
                else
                {
                    postStream.Close();
                    fs.Dispose();
                }
            }
            catch (Exception err)
            {
                postStream.Close();
                fs.Dispose();
                throw err;
            }
            finally
            {
                postStream.Close();
                fs.Dispose();
            }
        }

    }
}
