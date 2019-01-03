
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tools
{
    /// <summary>
    /// Name:文件操作类
    /// Description:提供文件的相关操作，可以将对象
    /// 保存在文件中。
    /// Version:1.0.0.0
    /// </summary>
   public class KBFile
    {
        #region var
        /// <summary>
        /// 读取文件方式
        /// </summary>
        public enum readType { ReadLine, ReadAll };
        /// <summary>
        /// 写文件方式
        /// </summary>
        public enum writeType { OverWriteSeek, OverWriteLine, AppendSeek, AppendLine };
        private string strPath = ""; //文件路径
        #endregion
        #region property
        /// <summary>
        /// 设置或获取文件路径
        /// </summary>
        public string path
        {
            get { return strPath; }
            set { strPath = value; }
        }
        #endregion
        #region method
        /// <summary>
        /// 读文本文件
        /// <param name="rt">文件打开方式</param>
        /// </summary>
        /// <returns>返回文本文件内容,异常返回空</returns>
        public string openFile(readType rt)
        {
            try
            {
                //判断文件是否存在,不存在返回空字符串
                if (System.IO.File.Exists(strPath) == false)
                    return "";
                else
                {
                    //创建StreamReader对象sr
                    StreamReader sr = new StreamReader(strPath, System.Text.Encoding.UTF8, true);
                    string str = "";
                    switch (rt)
                    {
                        case readType.ReadAll:
                            str = sr.ReadToEnd();
                            break;
                        case readType.ReadLine:
                            str = sr.ReadLine();
                            break;
                    }
                    sr.Close();
                    return str;
                }

            }
            catch (FileNotFoundException)
            { //找不到文件异常
                return "";
            }

            catch (FileLoadException )
            { return ""; }
            catch (DirectoryNotFoundException)
            { return ""; }
            catch (UnauthorizedAccessException )
            { return ""; }
            catch (IOException )
            { return ""; }

        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <returns></returns>
        public string openFile()
        {
            try
            {
                if (System.IO.File.Exists(strPath) == false) //指定路径文件不存在
                    return "";
                FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                byte[] b = new byte[1024];
                string s = "";
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    System.Text.Encoding.GetEncoding("gb2312");
                    s = s + System.Text.Encoding.Default.GetString(b);

                }
                return s;
            }
            catch (IOException)
            {
                return "";
            }
        }
        /// <summary>
        /// 读取文本文件内容
        /// </summary>
        /// <returns>以字符串的形式返回文本文件内容</returns>
        public string readFileToString()
        {
            try
            {
                //创建文件对象
                FileInfo f = new FileInfo(path);
                //创建读文件流对象
                StreamReader reader = f.OpenText();
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 将指定文件内容读入字节数组中.
        /// </summary>
        /// <returns>成功返回值到数组中,否则返回null</returns>
        public byte[] readFile()
        {
            try
            {
                if (System.IO.File.Exists(strPath) == false) //指定路径文件不存在
                    return null;
                FileStream fs = new FileStream(strPath, FileMode.Open);
                byte[] b = new byte[fs.Length];
                fs.Read(b, 0, b.Length);
                fs.Flush();
                fs.Close();
                return b;
            }
            catch (IOException)
            {
                return null;
            }
        }
        /// <summary>
        /// 将指定参数内容写入指定
        /// 文本文件中,如果指定路径不存在,
        /// 则创建路径
        /// </summary>
        /// <param name="content">写入文本文件的内容</param>
        /// <param name="wt"></param>
        /// <returns>写入成功返回1,否则返回0</returns>
        public int writeFile(string content, writeType wt)
        {
            try
            {
                //创建写入流对象sw
                StreamWriter sw;
                //根据不同写入方式进行写入
                switch (wt)
                {
                    case writeType.AppendSeek:  //追加方式写入
                        sw = System.IO.File.AppendText(strPath);
                        sw.Write(content); //写入数据
                        sw.Flush(); //清空缓冲区
                        sw.Close(); //关闭写入流
                        return 1;
                    case writeType.AppendLine:  //追加方式写入
                        sw = System.IO.File.AppendText(strPath);
                        sw.WriteLine(content); //写入数据
                        sw.Flush(); //清空缓冲区
                        sw.Close(); //关闭写入流
                        return 1;
                    case writeType.OverWriteLine:
                        sw = new StreamWriter(strPath);
                        sw.WriteLine(content); //写入数据
                        sw.Flush(); //清空缓冲区
                        sw.Close(); //关闭写入流
                        return 1;
                    case writeType.OverWriteSeek:
                        sw = new StreamWriter(strPath);
                        sw.Write(content); //写入数据
                        sw.Flush(); //清空缓冲区
                        sw.Close(); //关闭写入流
                        return 1;
                }
                return 0;
            }
            catch (IOException)
            {
                return 0;
            }
        }
        /// <summary>
        /// 将指定内容写入指定路径下的文件中
        /// </summary>
        /// <param name="content">写入内容</param>
        /// <returns>写入成功返回1,否则返回0</returns>
        public int writeFile(string content)
        {
            try
            {
                System.Text.Encoding.GetEncoding("gb2312");
                //创建文件流对象,以追加方式进行数据写入
                FileStream fs = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                //写入数据
                fs.Write(System.Text.Encoding.Default.GetBytes(content), 0, System.Text.Encoding.Default.GetByteCount(content));
                fs.Flush(); //清空缓冲区
                fs.Close();//关闭文件流对象
                return 1;
            }
            catch (IOException)
            {
                return 0;
            }
        }
        /// <summary>
        /// 将字节数组内容写入指定路径文件中.
        /// </summary>
        /// <param name="content">字节数组内容</param>
        /// <returns>写入成功返回1,否则返回0</returns>
        public int writeFile(byte[] content)
        {
            try
            {               
                //创建文件流对象,以追加方式进行数据写入
                FileStream fs = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                //写入数据               
                fs.Write(content, 0, content.Length);
                fs.Flush(); //清空缓冲区
                fs.Close();//关闭文件流对象
                return 1;
            }
            catch (IOException)
            {
                return 0;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }
        /// <summary>
        /// 将序列化对象写入文件
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns>写入成功返回1，否则返回0</returns>
        public int writeObjectToFile(object obj)
        { 
            FileStream fs = new FileStream(strPath, FileMode.Create);           
            try
            {
               
                 BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 从文件中读取序列化对象
        /// </summary>
        /// <returns>成功返回序列化对象，否则返回空对象</returns>           
        public object readObjectFromFile()
        {
            

            BinaryFormatter bf = new BinaryFormatter();
            Stream st = null;
            try
            {
                st= new FileStream(strPath, FileMode.Open, FileAccess.Read);
                object obj;
                obj = (object)bf.Deserialize(st);
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                st.Close();
            }
        }
        /// <summary>
        /// 根据路径找文件
        /// </summary>
        /// <param name="strDirectory">指定路径</param>
        /// <returns>以字符串数组返回文件</returns>
        public string[] getFiles(string strDirectory)
        {
            string strTmp = "";
            try
            {
                //判断指定的文件夹是否存在
                DirectoryInfo di = new DirectoryInfo(strDirectory);
                if (di.Exists == false) //表示存在的不是文件夹，是文件
                {
                    //判断是否是文件
                    FileInfo fi = new FileInfo(strDirectory);
                    if (fi.Exists == false) //表示不是文件，路径是非法的。
                        return null;
                    else
                    {
                        strTmp = strTmp + strDirectory + "|";
                    }
                }
                else
                {
                    int i = 0;
                    for (i = 0; i < di.GetDirectories().GetLength(0); i++)
                    {
                        getFiles(di.GetDirectories()[i].FullName);
                    }
                }
                return strTmp.Split('|');

            }
            catch (Exception)
            {
                return null;
            }
        }
      
       /// <summary>
        /// 根据指定路径找出子文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static string[] GetDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists == false) return null; //表示路径不存在
            DirectoryInfo[] dirs = dir.GetDirectories();
            string[] strTemp = new string[dirs.Length];//保存子文件夹

            for (int i = 0; i < dirs.Length; i++)
            {
                strTemp[i] = dirs[i].Name;
            }
            return strTemp;


        }
        #endregion
    }
}
