using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USC.ToolsLib.Tools;
using GS.PCSC;
using GS.SCard;
using USC.PCD;

namespace PCD
{
    class ACR122UA9 : PCSCReader, IPcd
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        public int OpenPcd()
        {
            try
            {
                Disconnect();
                Connect("ACS ACR122 0");
                ActivateCard();
            }
            catch (WinSCardException ex)
            {
                //Display(ex.WinSCardFunctionName + " Error 0x" + ex.Status.ToString("X08") + ": " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        public int ClosePcd()
        {
            Disconnect();
            return 0;
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

            int iRet = 0;
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
            int iRet = 0;
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
            if ("" == rApdu)
            {
                rApdu = "9000";
            }

            string sApdu_ = "";
            string rApdu_ = rApdu;

            string tmpApdu = "D44001" + sApdu.Replace(" ", "");
            int tmpApduLen = tmpApdu.Length / 2;
            string sApduLen = string.Format("{0:X2}", tmpApduLen);
            sApdu_ = "FF000000" + sApduLen + tmpApdu;

            int iRet = APDU(sApdu_, ref rApdu_);
            //缺少去掉的过程
            rApdu = rApdu_.Substring(6, rApdu_.Length - 10);
            if ("00" == rApdu_.Substring(4, 2))
            {
            }
            else
            {
                return 1;
            }

            return iRet;
            //string wantingSW = rApdu;
            //return APDU(sApdu, ref rApdu, ref rSW, wantingSW);
        }
    }
}
