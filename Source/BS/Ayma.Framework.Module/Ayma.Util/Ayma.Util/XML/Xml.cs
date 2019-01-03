using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ayma.Util
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.03.07
    /// 描 述：xml文件操作
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// 往项目里包含文件
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="aspxCsFileList"></param>
        public static void WriteCsproj(string csprojPath, string filePath, bool isContent)
        {
            //1。初始化一个xml实例
            XmlDocument xmlDoc = new XmlDocument();
            //2。导入指定xml文件
            xmlDoc.Load(csprojPath);
            //3。查找节点Project
            XmlNodeList root_childlist = xmlDoc.ChildNodes;
            XmlNode root_Project = null; ;
            foreach (XmlNode xn in root_childlist)
            {
                if (xn.Name == "Project")
                {
                    root_Project = xn;
                    break;
                }
            }
            //4。查找Project节点下的ItemGroup节点，确定内容节点Content和编译节点Compile所在的ItemGroup
            XmlNodeList childlist_Project = root_Project.ChildNodes;//根节点的字节点
            foreach (XmlNode xn in childlist_Project)
            {
                if (xn.Name == "ItemGroup")
                {
                    if (isContent)
                    {
                        if (xn.FirstChild.Name == "Content")
                        {
                            XmlElement xe_Content = xmlDoc.CreateElement("Content", xmlDoc.DocumentElement.NamespaceURI);//创建一个节点
                            xe_Content.SetAttribute("Include", filePath);//设置该节点genre属性
                            xn.AppendChild(xe_Content);
                            break;
                        }
                    }
                    else
                    {
                        if (xn.FirstChild.Name == "Compile")
                        {
                            XmlElement xe_Compile = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI);//创建一个节点
                            xe_Compile.SetAttribute("Include", filePath);//设置该节点genre属性
                            xn.AppendChild(xe_Compile);
                            break;
                        }
                    }
                }
            }
           
            //5。保存修改后的文件
            xmlDoc.Save(csprojPath);
        }

        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion
    }
}
