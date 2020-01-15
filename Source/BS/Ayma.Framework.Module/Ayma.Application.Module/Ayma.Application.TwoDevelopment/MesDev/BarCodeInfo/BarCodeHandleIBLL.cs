using Ayma.Application.TwoDevelopment.MesDev.BarCodeInfo;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public  interface BarCodeHandleIBLL
    {
        Mes_ScanCodeEntity GetGoodsInfoByCode(string code, string printfTime);
    }
}
