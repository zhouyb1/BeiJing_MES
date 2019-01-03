﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Tools
{
    /// <summary>
    /// 获取文件图标
    /// </summary>
   public class ExtractIcon
    {
        [DllImport("Shell32.dll")]
        private static extern int SHGetFileInfo
        (
        string pszPath,
        uint dwFileAttributes,
        out   SHFILEINFO psfi,
        uint cbfileInfo,
        SHGFI uFlags
        );

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public SHFILEINFO(bool b)
            {
                hIcon = IntPtr.Zero; iIcon = 0; dwAttributes = 0; szDisplayName = ""; szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
            public string szTypeName;
        };

        private ExtractIcon()
        {
        }

        private enum SHGFI
        {
            SmallIcon = 0x00000001,
            LargeIcon = 0x00000000,
            Icon = 0x00000100,
            DisplayName = 0x00000200,
            Typename = 0x00000400,
            SysIconIndex = 0x00004000,
            UseFileAttributes = 0x00000010
        }

        /// <summary>
        /// 获取文件图标
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <param name="bSmall">如果true，表示小图标，否则表示大图标</param>
        /// <returns>返回文件图标</returns>
        public static Icon GetIcon(string strPath, bool bSmall)
        {
            SHFILEINFO info = new SHFILEINFO(true);
            int cbFileInfo = Marshal.SizeOf(info);
            SHGFI flags;
            if (bSmall)
                flags = SHGFI.Icon | SHGFI.SmallIcon | SHGFI.UseFileAttributes;
            else
                flags = SHGFI.Icon | SHGFI.LargeIcon | SHGFI.UseFileAttributes;

            SHGetFileInfo(strPath, 256, out   info, (uint)cbFileInfo, flags);
            return Icon.FromHandle(info.hIcon);
        }   

    }
}
