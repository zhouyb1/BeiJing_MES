using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    class Barcode
    {
        [DllImport("MakeQRBarcode.dll")]
        public static extern int Make(Byte[] lpszText, Int32 nDataLen, Int32 nErrLevel, Int32 nMask, Int32 nBarEdition, string lpszFileName, Int32 nScale);

        [DllImport("MakeQRBarcode.dll")]
        public static extern int MixText(string szSrcBmpFileName, string szDstBmpFileName, string szText, int lFontSize, long lTxtHeight, int lHmargin, int lVmargin, int lTxtCntOneLine);
    }
}
