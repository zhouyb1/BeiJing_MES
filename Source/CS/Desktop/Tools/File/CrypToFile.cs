using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

namespace Tools
{
    /// <summary>
    /// 文件加密
    /// </summary>
   public class CrypToFile
    {
        /// <summary>
        /// 将字符串转变成字节数组
        /// </summary>
        /// <param name="strPWD">字符串</param>
        /// <returns>返回字节数组</returns>
        private byte[] getByte(string strPWD)
        {
            try
            { 
              //将字符串转换为字符数组
                char[] temp = strPWD.ToCharArray();
                byte[] btemp = new byte[16];

                int i = 0;
                //初始化数组
                for (i = 0; i < 16; i++)
                    btemp[i] = 0;
                //根据密码长度，去前16位，后面免去
                if (temp.Length < 16) //16位以内密码
                for (i = 0; i < temp.Length; i++)
                    btemp[i] = Convert.ToByte(temp[i]);
            else //超过32位密码，自取16位
                for (i = 0; i < 16; i++)
                    btemp[i] = Convert.ToByte(temp[i]);
                return btemp;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="inName">加密文件路径</param>
        /// <param name="outName">加密后文件存放路径</param>
        ///<param name="strPWD">密码</param>
        /// <returns>true表示加密成功，false表示加密失败！</returns>
        public bool EncryptData(string inName, string outName,string strPWD)
        {
            try
            {
                //判断加密文件是否存在
                if (System.IO.File.Exists(inName) == false)
                    //加密源文件不存在！
                    return false;
                byte[] rijnKey=getByte(strPWD);
                byte[] rijnIV = getByte(strPWD);
                //创建加密文件的加密源文件fin和目标文件流fout.
                FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outName, FileMode.Create, FileAccess.Write);
                fout.SetLength(0);
                //创建字节数组变量存储文件读写所需的临时空间
                byte []bin = new byte[100]; 
                 //定义保存字节写入的总长度变量fdlen
                long rdlen = 0;
                //定义被加密文件流的长度变量totlen
                long totlen = fin.Length; 
                //定义变量保存在某一时刻写入字节数len.
                int len = 0;
                //创建加密机制对象
                SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();
                CryptoStream encStream = new CryptoStream(fout, rijn.CreateEncryptor(rijnKey, rijnIV), CryptoStreamMode.Write);

                //读取源文件，加密后写入目标文件fout
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen =Convert.ToInt32(rdlen + len);
                }
                encStream.Close();
                fout.Close();
                fin.Close();
                return true;
            }
            catch (Exception )
            {
               
                return false;
            }
        }
        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="fileName">加密文件路径</param>        
        ///<param name="strPWD">密码</param>
        /// <returns>true表示加密成功，false表示加密失败！</returns>
        public bool EncryptData(string fileName, string strPWD)
        {
            try
            {
                //判断加密文件是否存在
                if (System.IO.File.Exists(fileName) == false)
                    //加密源文件不存在！
                    return false;
                byte[] rijnKey = getByte(strPWD);
                byte[] rijnIV = getByte(strPWD);
                //创建加密文件的加密源文件fin和目标文件流fout.
                FileStream fin = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);                
               
                //定义被加密文件流的长度变量totlen
                long totlen = fin.Length;
                //定义变量保存在某一时刻写入字节数len.
                int len = 0;
                //创建加密机制对象
                //创建字节数组变量存储文件读写所需的临时空间
                byte[] bin = new byte[totlen];
                
                SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();
                CryptoStream encStream = new CryptoStream(fin, rijn.CreateEncryptor(rijnKey, rijnIV), CryptoStreamMode.Write);
                //读取源文件，加密后写入目标文件
                len = fin.Read(bin,0,bin.Length);
                fin.Flush();
                fin.SetLength(0);
                encStream.Write(bin, 0, len); 
                encStream.Close();
                fin.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 文件解密
        /// </summary>
        /// <param name="inName">加密文件路径</param>
        /// <param name="outName">加密后的文件路径</param>
        ///<param name="strPWD">密码</param>
        /// <returns>true表示解密成功，false表示解密失败！</returns>
        public bool DecryptData(string inName, string outName,string strPWD)
        {
            try
            {
                //判断解密文件是否存在
                if (System.IO.File.Exists(inName) == false)
                    //解密源文件不存在！
                    return false;
                byte[] rijnKey =getByte(strPWD);
                byte[] rijnIV =getByte(strPWD);
                //创建加密文件的加密源文件fin和目标文件流fout.
                FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outName, FileMode.Create, FileAccess.Write);
                fout.SetLength(0);
                //创建字节数组变量存储文件读写所需的临时空间
                byte []bin = new byte[100];
                //定义保存字节写入的总长度变量fdlen
                long rdlen = 0;
                //定义被加密文件流的长度变量totlen
                long totlen = fin.Length;
                //定义变量保存在某一时刻写入字节数len.
                int len = 0;
                //创建加密机制对象
                SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();
                CryptoStream encStream = new CryptoStream(fout, rijn.CreateDecryptor(rijnKey, rijnIV), CryptoStreamMode.Write);
                //读取源文件，加密后写入目标文件fout
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen = Convert.ToInt32(rdlen + len);
                }
                encStream.Close();
                fout.Close();
                fin.Close();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }
        /// <summary>
        /// 文件解密
        /// </summary>
        /// <param name="fileName">解密文件路径</param>       
        ///<param name="strPWD">密码</param>
        /// <returns>true表示解密成功，false表示解密失败！</returns>
        public bool DecryptData(string fileName,string strPWD)
        {
            try
            {
                //判断解密文件是否存在
                if (System.IO.File.Exists(fileName) == false)
                    //解密源文件不存在！
                    return false;
                byte[] rijnKey = getByte(strPWD);
                byte[] rijnIV = getByte(strPWD);
                //创建加密文件的加密源文件fin和目标文件流fout.
                FileStream fin = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                              
               
                //定义被加密文件流的长度变量totlen
                long totlen = fin.Length;
                //定义变量保存在某一时刻写入字节数len.
                int len = 0;
                //创建字节数组变量存储文件读写所需的临时空间
                byte[] bin = new byte[totlen];
                //创建加密机制对象
                SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();
                CryptoStream encStream = new CryptoStream(fin, rijn.CreateDecryptor(rijnKey, rijnIV), CryptoStreamMode.Write);
                //读取源文件，加密后写入目标文件fout
                len = fin.Read(bin, 0, bin.Length);
                fin.Flush();
                fin.SetLength(0);
                encStream.Write(bin, 0, len);                
                encStream.Close();               
                fin.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// 加密指定文件夹中所有文件
        /// </summary>
        /// <param name="folderPath">指定路径</param>
        /// <param name="strPwd">加密密码</param>
        public void EncryptFolder(string folderPath,string strPwd)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists == true) //表示的是目录
            {
                //获取当前目录下所有子目录
                int i = 0;
                for (i = 0; i < di.GetDirectories().Length; i++)
                {
                    EncryptFolder(di.GetDirectories()[i].FullName,strPwd);
                }
                //加密当前文件夹下所有文件
                //根据用户选择的路径，获取相关文件集合
                FileInfo fi = new FileInfo(folderPath);
                FileInfo []files=di.GetFiles();
                for (i = 0; i < di.GetFiles().Length; i++)
                {
                    fi = files[i];
                    if (EncryptData(fi.FullName, fi.FullName.Split('.')[0] + "_pwd." + fi.FullName.Split('.')[1], strPwd) == false)
                    {
                        
                        return;
                    }
                    else
                    {
                        fi.Delete();
                    }
                }

            }
        }
        /// <summary>
        /// 解密指定文件夹中所有文件
        /// </summary>
        /// <param name="folderPath">指定路径</param>
        /// <param name="strPwd">解密密码</param>
        public void DecryptFolder(string folderPath, string strPwd)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists == true) //表示的是目录
            {
                //获取当前目录下所有子目录
                int i = 0;
                for (i = 0; i < di.GetDirectories().Length; i++)
                {
                    DecryptFolder(di.GetDirectories()[i].FullName, strPwd);
                }
                //加密当前文件夹下所有文件
                //根据用户选择的路径，获取相关文件集合
                FileInfo fi = new FileInfo(folderPath);
                FileInfo[] files = di.GetFiles();
               for (i = 0; i < di.GetFiles().Length; i++)
                {
                    fi = files[i];
                    if (DecryptData(fi.FullName, fi.FullName.Split('.')[0].Substring(0, fi.FullName.Split('.')[0].Length - 4) + "." + fi.FullName.Split('.')[1], strPwd) == false)
                    {
                       
                        return;
                    }
                    else
                    {
                        fi.Delete();
                    }
                }

            }
        }
    }
   
    /// <summary>
    /// 内存流数据加密
    /// </summary>
    class CryptoMemoryStream
    {
/// <summary>
/// 将字节数组转换为字符串
/// </summary>
/// <param name="b">字节数据</param>
/// <returns>返回转换后的字符串</returns>
    public static string GetFromByte(byte[] b)
    {
        string str = "";
        for (int i = 0; i < b.Length; i++)
        {
            str = str + Convert.ToChar(b[i]).ToString();
        }
        return str;
    }
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();
        // Encrypt the string.
        public byte[] Encrypt(string PlainText)
        {
            // Create a memory stream.
            MemoryStream ms = new MemoryStream();
            // Create a CryptoStream using the memory stream and the 
            // CSP DES key.  
            CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);
            // Create a StreamWriter to write a string
            // to the stream.
            StreamWriter sw = new StreamWriter(encStream);
            // Write the plaintext to the stream.
            sw.WriteLine(PlainText);

            //Close the StreamWriter and CryptoStream.
            sw.Close();
            encStream.Close();

            // Get an array of bytes that represents
            // the memory stream.

            byte[] buffer = ms.ToArray();
            // Close the memory stream.
            ms.Close();

            // Return the encrypted byte array.
            return buffer;
        } //Encrypt

        // Decrypt the byte array.
        public string Decrypt(byte[] CypherText)
        {    // Create a memory stream to the passed buffer.

            MemoryStream ms = new MemoryStream(CypherText);
            // Create a CryptoStream using the memory stream and the 
            // CSP DES key. 
            //Dim encStream As New CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
            CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
            // Create a StreamReader for reading the stream.
            // Dim sr As New StreamReader(encStream);
            StreamReader sr = new StreamReader(encStream);
            // Read the stream as a string.
            //Dim val As String = sr.ReadLine();
            string val = sr.ReadLine();
            // Close the streams.
            sr.Close();
            encStream.Close();
            ms.Close();
            return val;
        }//Decrypt
    }//CryptoMemoryStream
}
