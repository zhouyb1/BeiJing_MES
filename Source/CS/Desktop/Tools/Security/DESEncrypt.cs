/*******************************************************************************
 * Copyright © 2018 Sfliao.Framework 版权所有
 * Author: Sfliao
 * Description: 桌面应用快速开发
 
*********************************************************************************/
using System;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    /// <summary>
    /// DES加密、解密帮助类
    /// </summary>
    public class DESEncrypt
    {
        private static string key = "ayma###***";
        private static string ccbKey = "c1f7da727d7b34ee3b1f036f020111";

        #region ========加密========
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">需要加密的内容</param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, key);
        }

        /// <summary>
        /// 加密验签（建行）
        /// </summary>
        /// <param name="textStr"></param>
        /// <param name="ccbKey"></param>
        /// <returns></returns>
        public static string EncryptData(string textStr)
        {
            return EncryptData(textStr, ccbKey);
        }



        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">需要加密的内容</param> 
        /// <param name="sKey">秘钥</param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Helper.Hash(sKey).ToUpper().Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Helper.Hash(sKey).ToUpper().Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        public static string EncryptData(string textStr, string ccbKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.GetEncoding("iso-8859-1").GetBytes(textStr);
            des.Key = ASCIIEncoding.ASCII.GetBytes(ccbKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text">需要解密的内容</param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                return Decrypt(Text, key);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 解密数据(建行)
        /// </summary>
        /// <param name="encryptData"></param>
        /// <returns></returns>
        public static string DecryptData(string encryptData)
        {
            if (!string.IsNullOrEmpty(encryptData))
            {
                return DecryptData(encryptData, key);
            }
            return "";
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">需要解密的内容</param> 
        /// <param name="sKey">秘钥</param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Helper.Hash(sKey).ToUpper().Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Helper.Hash(sKey).ToUpper().Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        public static string DecryptData(string encryptData, string ccbKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = encryptData.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(encryptData.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(ccbKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }
}
