using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace USC.Tools
{
    class MyLog
    {
        #region 日志记录、支持其他线程访问
        public delegate void LogAppendDelegate(Color color, string text, string textHeader);

        private System.Windows.Forms.RichTextBox rtbRemote;

        public System.Windows.Forms.RichTextBox RtbRemote
        {
            get { return rtbRemote; }
            set { rtbRemote = value; }
        }

        private int rtbRemoteCnt = 0;

        private MyLog() { }
        public MyLog(System.Windows.Forms.RichTextBox _rtbRemote)
        {
            rtbRemote = _rtbRemote;
        }

        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text, string textHeader = "")
        {
            rtbRemoteCnt++;
            rtbRemote.AppendText("\n");
            rtbRemote.AppendText(string.Format("[{0:d3}] ", rtbRemoteCnt) + textHeader);

            rtbRemote.SelectionFont = new Font("微软雅黑", 12, FontStyle.Bold);
            rtbRemote.SelectionColor = color;
            rtbRemote.AppendText(text);

            rtbRemote.Select(rtbRemote.TextLength, 0);//光标定位到文本最后
            rtbRemote.ScrollToCaret();//滚动到光标处
        }
        /// <summary> 
        /// 显示错误日志 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            rtbRemote.Invoke(la, Color.Red, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary> 
        /// 显示警告信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            rtbRemote.Invoke(la, Color.Violet, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary> 
        /// 显示信息 
        /// </summary> 
        /// <param name="text"></param> 
        //public void LogMessage(string text)
        //{
        //    LogAppendDelegate la = new LogAppendDelegate(LogAppend);
        //    rtbRemote.Invoke(la, Color.Black, DateTime.Now.ToString("HH:mm:ss ") + text);
        //}

        public void LogMessage(string text)
        {
            Color color = Color.Black;
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);

            if (text.Contains("绿色"))
            {
                color = Color.Green;
            }
            if (text.Contains("蓝色"))
            {
                color = Color.Blue;
            }

            rtbRemote.Invoke(la, color, text, DateTime.Now.ToString("HH:mm:ss.fff "));
        }

        public void LogMessageInit()
        {
            rtbRemoteCnt = 0;
            rtbRemote.Text = "";

            rtbRemote.SelectionFont = new Font("微软雅黑", 12, FontStyle.Bold);

            rtbRemote.Select(rtbRemote.TextLength, 0);//光标定位到文本最后
            rtbRemote.ScrollToCaret();//滚动到光标处
        }

        #endregion
    }
}
