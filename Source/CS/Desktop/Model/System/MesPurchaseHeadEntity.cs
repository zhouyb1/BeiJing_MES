using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_PurchaseHead表映射类
    /// </summary>
    public partial class MesPurchaseHeadEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string P_PurchaseNo{ set; get; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string P_StockCode{ set; get; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string P_StockName{ set; get; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string P_SupplyCode{ set; get; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string P_SupplyName{ set; get; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo{ set; get; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime P_OrderDate{ set; get; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
   
        /// </summary>
        public string P_Status{ set; get; }
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
        public string P_UpdateDate{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public DateTime P_Remark{ set; get; }
        /// <summary>
        /// 删除人
        /// </summary>
        public string P_DeleteBy{ set; get; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime P_DeleteDate{ set; get; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string P_UploadBy{ set; get; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime P_UploadDate{ set; get; }
        #endregion
    }
}
