using System;
using Ayma.Application.TwoDevelopment.MesDev.BarCodeInfo;
using Ayma.Util;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public class BarCodeHandleBLL:BarCodeHandleIBLL
    {
        private BarCodeHandleService service = new BarCodeHandleService();
        public Mes_ScanCodeEntity GetGoodsInfoByCode(string code, string printfTime)
        {
            try
            {
                //获取二维码
              return  service.GetGoodsInfoByCode(code, printfTime);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
    }
}
