using Ayma.Application.TwoDevelopment.MesDev.BarCodeInfo;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public  interface BarCodeHandleIBLL
    {
        ProductsInfo GetGoodsInfoByCode(string code, string printfTime);
    }
}
