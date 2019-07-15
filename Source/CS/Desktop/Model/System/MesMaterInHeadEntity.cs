using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_MaterInHead表映射类
    /// </summary>
    public partial class MesMaterInHeadEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public int M_OrderKind { set; get; }
        /// <summary>
        /// 入库单号
        /// </summary>
        public string M_MaterInNo{ set; get; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string M_StockCode{ set; get; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string M_StockName{ set; get; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string M_Kind { set; get; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string M_OrderNo{ set; get; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime M_OrderDate{ set; get; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        public int M_Status { set; get; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string M_CreateBy{ set; get; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime M_CreateDate{ set; get; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string M_UpdateBy{ set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? M_UpdateDate{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string M_Remark{ set; get; }
        /// <summary>
        /// 删除人
        /// </summary>
        public string M_DeleteBy{ set; get; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? M_DeleteDate{ set; get; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string M_UploadBy{ set; get; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? M_UploadDate{ set; get; }

        #endregion
    }
}
