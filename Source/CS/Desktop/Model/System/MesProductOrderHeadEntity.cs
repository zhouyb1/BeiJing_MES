using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_ProductOrderHead表映射类
    /// </summary>
    public partial class MesProductOrderHeadEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo{ set; get; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime P_OrderDate{ set; get; }
        /// <summary>
        /// 车站ID
        /// </summary>
        public string P_OrderStationID{ set; get; }
        /// <summary>
        /// 车站名称
        /// </summary>
        public string P_OrderStationName{ set; get; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string P_CreateBy{ set; get; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime P_CreateDate{ set; get; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string P_UpdateBy{ set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime P_UpdateDate{ set; get; }
        #endregion
    }
}
