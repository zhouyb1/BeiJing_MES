using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Util;
using Nancy;

namespace Ayma.Application.WebApi.Modules.ProductOrder
{
    /// <summary>
    /// 同步ERP订单
    /// </summary>
    public class ProductOrderApi:BaseApi
    {
        private ProductOrderMakeIBLL orderBll = new ProductOrderMakeBLL();
        /// <summary>
        /// 注册接口方法
        /// </summary>
        public ProductOrderApi() : base("/ayma/api/ProductOrder")
        {
            Post["/SyncOrder"] = SyncOrder;
        }

        /// <summary>
        /// 同步订单至MES
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Response SyncOrder(dynamic arg)
        {
            #region 数据校验

            var submitOrder = this.GetReqData().ToJObject();
            var order = this.GetReqData<ProductOrder>();
            if (order == null)
            {
                return Fail("订单实体为空");
            }
            if (string.IsNullOrWhiteSpace(order.P_OrderNo))
            {
                return Fail("生产订单号为空");
            }
            if (string.IsNullOrWhiteSpace(order.P_OrderStationID))
            {
                return Fail("车站为空");
            }
            if (order.OrderDetails.Count == 0)
            {
                return Fail("订单明细为空");
            }
            if (order.P_OrderNo != order.OrderDetails[0].P_OrderNo)
            {
                return Fail("订单号不一致");
            }
            var orderEntity = orderBll.GetEntityByParam(order.P_OrderNo);
            if (orderEntity!=null)
            {
                return Fail("订单已存在");
            }
            #endregion

            //同步至db
            var mesOrder = DataHelper.Map<ProductOrder, Mes_ProductOrderHeadEntity>(order);
            var mesOrderDetail = DataHelper.Map<List<ProductOrderDetail>, List<Mes_ProductOrderDetailEntity>>(order.OrderDetails);
            orderBll.SaveEntity("", mesOrder, mesOrderDetail);
            return Success("订单同步成功");


        }
    }

    public class ProductOrder
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime? P_OrderDate { get; set; }
        /// <summary>
        /// 车站ID
        /// </summary>
        public string P_OrderStationID { get; set; }
        /// <summary>
        /// 车站名称
        /// </summary>
        public string P_OrderStationName { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string P_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? P_CreateDate { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? P_UseDate { get; set; }
        /// <summary>
        /// 生产单状态(0-生产中 1-入库 2-出库)
        /// </summary>
        public ErpEnums.PStatusEnum P_Status { get; set; }
        /// <summary>
        /// 订单明细
        /// </summary>
        public List<ProductOrderDetail> OrderDetails;

    }

    /// <summary>
    /// 订单明细
    /// </summary>
    public class ProductOrderDetail
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime? P_OrderDate { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string P_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string P_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string P_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? P_Qty { get; set; }
        #endregion
    }
}