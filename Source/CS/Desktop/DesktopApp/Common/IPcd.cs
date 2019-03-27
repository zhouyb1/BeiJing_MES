using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USC.PCD
{
    interface IPcd
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        int OpenPcd();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        int ClosePcd();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        int FindNfcTag(out string uid);//14443-3

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0成功</returns>
        int Rats(out string ats);//14443-4

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sApdu">发送数据</param>
        /// <param name="rApdu">接收数据，和期望的sw</param>
        /// <returns>0成功</returns>
        int NfcBmpDataExchange(string sApdu, ref string rApdu);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version">传出的是string型的 NfcTagVersion</param>
        /// <returns>0成功</returns>
        //int GetNfcTagVersion(out string version);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmpDataBuf"></param>
        /// <param name="length"></param>
        /// <returns>0成功</returns>
        //int SendBmp2NfcTag(byte[] bmpDataBuf, int length);
    }

    //abstract class IPcd
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns>0成功</returns>
    //    public abstract int OpenPcd();

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns>0成功</returns>
    //    public abstract int ClosePcd();

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns>0成功</returns>
    //    public abstract int Rats(out string ats);

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="sApdu">发送数据</param>
    //    /// <param name="rApdu">接收数据，和期望的sw</param>
    //    /// <returns>0成功</returns>
    //    public abstract int NfcBmpDataExchange(string sApdu, ref string rApdu);

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="version">传出的是string型的 NfcTagVersion</param>
    //    /// <returns>0成功</returns>
    //    public int GetNfcTagVersion(out string version)
    //    {
    //        version = "测试01";
    //        return 0;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="bmpDataBuf"></param>
    //    /// <param name="length"></param>
    //    /// <returns>0成功</returns>
    //    public int SendBmp2NfcTag(byte[] bmpDataBuf, int length)
    //    {
    //        return 0;
    //    }
    //}
}
