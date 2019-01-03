using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public class untCommon
    {
        /// <summary>
        /// 消息对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static void InfoMsg(string txt)
        {
            MessageBox.Show(txt, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 错误对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static void ErrorMsg(string txt)
        {

            MessageBox.Show(txt, " 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        /// <summary>
        /// 问题对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static bool QuestionMsg(string txt)
        {
            if (MessageBox.Show(txt, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
