using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace DataAccess.SqlServer.Extend
{
  /// <summary>
  /// 配置数据库参数信息
  /// </summary>
  [Serializable]
  public  class DBConfig
    {
        #region var
        private string strService = "";  // 定义服务器名称
        private string strDataBase = ""; //定义数据库名称
        private string strUser = "";  //定义登陆用户名
        private string strPassword = ""; //定义登陆用户密码
        private int intType = 0;  //定义连接类型       
        #endregion
        #region property
        /// <summary>
        /// 连接服务器名称
        /// </summary>
        public string serviceName
        {
            get { return strService; }
            set { strService = value; }
        }
        /// <summary>
        /// 连接数据库名称
        /// </summary>
        public string databaseName
        {
            get { return strDataBase; }
            set { strDataBase = value; }
        }
        /// <summary>
        /// 连接数据库用户名称
        /// </summary>
        public string userName
        {
            get { return strUser; }
            set { strUser = value; }
        }
        /// <summary>
        /// 连接数据库密码
        /// </summary>
        public string password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
        /// <summary>
        /// 连接数据库类型
        /// </summary>
        public int type
        {
            get { return intType; }
            set { intType = value; }
        }

      #endregion
        #region method
        /// <summary>
        /// 将配置对象写入数据库
        /// </summary>
        /// <returns></returns>
        public int setConfig()
        {
            try
            {
                KBFile file = new KBFile();
                file.path = System.Windows.Forms.Application.StartupPath + "\\dbConfig.kb";

                if (file.writeObjectToFile(this) > 0)
                {
                    CrypToFile cy = new CrypToFile();
                    int count = file.path.Split('.').Length;

                    string fileExt = file.path.Split('.')[count - 1];
                    string fileName = file.path.Substring(0, file.path.Length - fileExt.Length - 1);

                    if (cy.EncryptData(file.path, fileName + "pwd." + fileExt, "KingBoy"))
                    {
                        System.IO.File.Delete(file.path);
                        System.IO.File.Copy(fileName + "pwd." + fileExt, file.path, true);
                        System.IO.File.Delete(fileName + "pwd." + fileExt);
                        return 1;
                    }
                    else
                        return -2;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                return -1;
            }
        }
      
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public DBConfig getConfig()
        {
            try
            {
               KBFile file = new KBFile();
               file.path = System.Windows.Forms.Application.StartupPath + "\\dbConfig.kb";
               CrypToFile cy = new CrypToFile();
                int count = file.path.Split('.').Length;

                string fileExt = file.path.Split('.')[count - 1];
                string fileName = file.path.Substring(0, file.path.Length - fileExt.Length - 1);

                if (cy.DecryptData(file.path, fileName + "pwd." + fileExt, "KingBoy"))
                {
                    file.path = fileName + "pwd." + fileExt;
                    DBConfig con = (DBConfig)file.readObjectFromFile();
                    System.IO.File.Delete(file.path);
                    return con;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
