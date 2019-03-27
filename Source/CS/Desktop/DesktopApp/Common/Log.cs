using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace USC.Tools
{
    public enum DL
    { //debugLevel
        DEBUG = 0,
        ERROR,
        INFO,
        APP,
        CHAINING,//
    }

    public class Loger
    {
        //最终调用
        public static void WriteLog(string strLog)
        {
            string sFilePath = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMM");
            string sFileName = "发送接收日志" + DateTime.Now.ToString("yyyyMMddHH") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }

            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }

            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + strLog);
            sw.Close();
            fs.Close();
        }

        private static DL d = DL.DEBUG;

        internal static DL DebugLevelN1
        {
            get { return d; }
            set { d = value; }
        }
        //分级整理
        private static string displayChainingStr = "";
        public static void Printf(string s8_Text, DL _dl = DL.ERROR, bool s8_LF = false) //=NULL
        {//路由中心
            if (d <= _dl)
            {
                displayChainingStr = displayChainingStr + s8_Text;

                if (s8_LF)
                {
                    WriteLog("  Td[" + Thread.CurrentThread.ManagedThreadId.ToString() + "] " + displayChainingStr);
                    displayChainingStr = "";
                }
            }
        }
    }
}
