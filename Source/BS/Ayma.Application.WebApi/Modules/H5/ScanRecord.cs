using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Util;
using Nancy;

namespace Ayma.Application.WebApi.Modules.H5
{
    public class ProductOrderApi : BaseApi
    {
        private BarCodeHandleIBLL bll = new BarCodeHandleBLL();
        /// <summary>
        /// 注册接口方法
        /// </summary>
        public ProductOrderApi()
            : base("/webapi/ScanRecord")
        {
            Get["/GetBarCodeInfo"] = GetBarCodeInfo;

        }

        /// <summary>
        /// 同步订单至MES
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Response GetBarCodeInfo(dynamic _)
        {
            var req = this.GetReqData().ToJObject();
            if (req["barCode"].IsEmpty())
            {
                return Fail("参数barCode为空");
            }
            if (req["printfTime"].IsEmpty())
            {
                return Fail("参数printfTime为空");
            }
            var barCode = req["barCode"].ToString();
            var printfTime = req["printfTime"].ToString();

            var goodsInfo = bll.GetGoodsInfoByCode(barCode, printfTime);
            if (goodsInfo==null)
            {
                return Fail("没有数据");
            }
            return Success(goodsInfo);
        }
    }
}