using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USC.PCD;
using USC.ToolsLib.Tools;
using Tools;
using System.Threading;
using USC.Tools;

namespace Tag
{
    class NfcTag
    {
        IPcd pcd;
        bool isFindNfcTag;

        public NfcTag(IPcd _pcd)
        {
            pcd = _pcd;
            isFindNfcTag = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version">传出的是string型的 NfcTagVersion</param>
        /// <returns>0成功</returns>
        public int GetNfcTagVersion(out string version)
        {
            int iRet = 0x00;
            string uid, ats;
            if (!isFindNfcTag)
            { //寻找nfc标签
                iRet = pcd.OpenPcd();
                if (0x00 != iRet)
                {//打开pcd失败 

                }


                iRet = pcd.FindNfcTag(out uid);
                if (0x00 != iRet)
                {//连接nfcTag失败 

                }
                Loger.Printf("   UID:" + uid, USC.Tools.DL.ERROR, true);

                iRet = pcd.Rats(out ats);
                if (0x00 != iRet)
                {//激活nfcTag失败 

                }
                Loger.Printf("   ATS:" + ats, USC.Tools.DL.ERROR, true);
            }

            //发送查询指令

            string sendApdu = "AA55 0300 00";
            string recvApdu = "";
            version = "";

            Loger.Printf("-> " + sendApdu, USC.Tools.DL.ERROR, true);

            int st = pcd.NfcBmpDataExchange(sendApdu, ref recvApdu);
            if (st == 0)
            {
                Loger.Printf("-> " + recvApdu, USC.Tools.DL.ERROR, true);
                if (recvApdu.Substring(0, 6) == "AA5504")
                {
                    int verLen = Convert.ToInt32(recvApdu.Substring(6, 2), 16);
                    string verStr = recvApdu.Substring(8, verLen * 2);
                    Loger.Printf("   " + verStr, USC.Tools.DL.ERROR, true);
                    byte[] ver = ConverHDS.StringToByteArray(verStr);
                    verStr = ConverHDS.ASCIIArrayToString(ver, verLen);
                    Loger.Printf("   " + verStr, USC.Tools.DL.ERROR, true);
                    version = verStr;
                }

            }
            else
            {
                Loger.Printf("NG @ 发送 " + sendApdu, USC.Tools.DL.ERROR, true);
                return 1;
            }



            //关闭连接
            pcd.ClosePcd();
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmpDataBuf"></param>
        /// <param name="length"></param>
        /// <returns>0成功</returns>
        public int SendBmp2NfcTag(byte[] bmpDataBuf, int bmpDataBufLength)
        {
            int iRet = 0x00;
            string uid, ats;
            if (!isFindNfcTag)
            { //寻找nfc标签
                iRet = pcd.OpenPcd();
                if (0x00 != iRet)
                {//打开pcd失败 

                }
                //Thread.Sleep(300);

                iRet = pcd.FindNfcTag(out uid);
                if (0x00 != iRet)
                {//连接nfcTag失败 

                }
                Loger.Printf("   UID:" + uid, USC.Tools.DL.ERROR, true);

                iRet = pcd.Rats(out ats);
                if (0x00 != iRet)
                {//激活nfcTag失败 

                }
                Loger.Printf("   ATS:" + ats, USC.Tools.DL.ERROR, true);
            }

            int st;
            string sendApdu, recvApdu;
            //发送查询指令

            sendApdu = "AA55 0300 00";
            recvApdu = "";

            Loger.Printf("-> " + sendApdu, USC.Tools.DL.ERROR, true);

            st = pcd.NfcBmpDataExchange(sendApdu, ref recvApdu);
            if (st == 0)
            {
                Loger.Printf("-> " + recvApdu, USC.Tools.DL.ERROR, true);
                if (recvApdu.Substring(0, 6) == "AA5504")
                {
                    int verLen = Convert.ToInt32(recvApdu.Substring(6, 2), 16);
                    string verStr = recvApdu.Substring(8, verLen * 2);
                    Loger.Printf("   " + verStr, USC.Tools.DL.ERROR, true);
                    byte[] ver = ConverHDS.StringToByteArray(verStr);
                    verStr = ConverHDS.ASCIIArrayToString(ver, verLen);
                    Loger.Printf("   " + verStr, USC.Tools.DL.ERROR, true);
                }

            }
            else
            {
                Loger.Printf("NG @ 发送 " + sendApdu, USC.Tools.DL.ERROR, true);
                return 1;
            }

            //发送刷屏指令
            //HEAD	CMD	LEN	DATA
            //2byte	1byte	1byte	1byte	    1byte	                  2bytes
            //0xAA55	0x01	0x04	屏幕尺寸：  0x21：2.13寸
            //                                    0x29：2.9寸
            //                                    0x42：4.2寸
            //                                    0x75：7.5寸	屏幕颜色：
            //                                    0x00：黑白屏
            //                                    0x01：三色屏	刷屏数据的总长度n：1≤n≤65535

            byte[] tempLen = BitConverter.GetBytes(bmpDataBufLength);
            byte[] dataLen = { 0, 0 };
            dataLen[0] = tempLen[1];
            dataLen[1] = tempLen[0];

            sendApdu = "AA5501040000";
            recvApdu = "";

            sendApdu = sendApdu + ConverHDS.ByteArrayToString(dataLen, 2);

            Loger.Printf("-> " + sendApdu, USC.Tools.DL.ERROR, true);

            st = pcd.NfcBmpDataExchange(sendApdu, ref recvApdu);
            if (st == 0)
            {
                Loger.Printf("-> " + recvApdu, USC.Tools.DL.ERROR, true);
            }
            else
            {
                Loger.Printf("NG @ 发送 " + sendApdu, USC.Tools.DL.ERROR, true);
                return 1;
            }

            //发送刷屏数据
            byte slitCntTemp = 0;//当前发送从长度
            //byte slitCnt = (byte)Convert.ToInt16(textBox1.Text);//每一帧的长度。在界面上输入。
            byte slitCnt = 110;//每一帧的长度。 此参数可以在1~255内修改。
            byte[] data2send = new byte[slitCnt + 4];//每次发送数据的数组

            int LeftLength = bmpDataBufLength; //待发送数据的剩余长度
            int bmpIndex = 0x00;//图片数据数组的索引（已经发送到哪里了）

            int stepCnt = (bmpDataBufLength + slitCnt - 1) / slitCnt;//需要分几步写入

            int i;
            for (i = 0; i < stepCnt; i++)
            {
                sendApdu = "";
                recvApdu = "";

                if (LeftLength >= slitCnt)
                {
                    slitCntTemp = slitCnt;
                    LeftLength -= slitCnt;
                }
                else   //剩余长度不及一整帧数据。  
                {
                    slitCntTemp = (byte)LeftLength; //最后一帧的长度为剩余长度
                }

                data2send[0] = 0xAA;
                data2send[1] = 0x55;
                data2send[2] = 0x02;
                data2send[3] = slitCntTemp;

                for (byte i1 = 0; i1 < slitCntTemp; i1++)
                {
                    data2send[i1 + 4] = bmpDataBuf[bmpIndex];
                    bmpIndex++;
                }

                sendApdu = ConverHDS.ByteArrayToString(data2send, slitCntTemp + 4);
                string ssssn = i.ToString();

                Loger.Printf("-> " + sendApdu, USC.Tools.DL.ERROR, true);

                st = pcd.NfcBmpDataExchange(sendApdu, ref recvApdu);
                if (st == 0)
                {
                    Loger.Printf("-> " + recvApdu, USC.Tools.DL.ERROR, true);
                }
                else
                {
                    Loger.Printf("NG @ 发送 " + sendApdu, USC.Tools.DL.ERROR, true);
                    return 1;
                }



            }


            //关闭连接
            pcd.ClosePcd();

            return 0;
        }
    }
}
