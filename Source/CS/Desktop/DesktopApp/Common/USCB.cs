using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USC.PcdLib.PCD.D8;
using USC.ToolsLib.Tools;
using System.Threading;

namespace USC.PCD
{
    public class USC8 : D8Reader, IPcd
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        public int OpenPcd()
        {
            int iRet;
            iRet = Open(100);
            Reset();
            return iRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        public int ClosePcd()
        {
            Thread.Sleep(100);
            Reset(0);
            return Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        public int FindNfcTag(out string uid)
        {
            char mode = (char)1;
            uint sLen = 255;
            byte[] buf = new byte[sLen];

            int iRet = DoubleCard(mode, ref buf);
            uid = ConverHDS.ByteArrayToString(buf, 7);
            return iRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        public int Rats(out string ats)
        {
            uint sLen = 255;
            byte[] rats = new byte[sLen];
            int iRet = Pro_reset(ref sLen, rats);
            ats = ConverHDS.ByteArrayToString(rats, (int)sLen);
            return iRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sApdu">发送数据</param>
        /// <param name="rApdu">接收数据，和期望的sw</param>
        /// <returns>0成功</returns>
        public int NfcBmpDataExchange(string sApdu, ref string rApdu)
        {
            //string rSW = "";
            return APDU(sApdu, ref rApdu);
            //string wantingSW = rApdu;
            //return APDU(sApdu, ref rApdu, ref rSW, wantingSW);
        }
    }
}
