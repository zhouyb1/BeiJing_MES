using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_CollarHead表映射类
    /// </summary>
    public partial class MesCollarHeadEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 领料单号
        /// </summary>
        public string C_CollarNo{ set; get; }
        /// <summary>
        /// 原仓库编码
        /// </summary>
        public string C_StockCode{ set; get; }
        /// <summary>
        /// 原仓库名称
        /// </summary>
        public string C_StockName{ set; get; }
        /// <summary>
        /// 调拨仓库编码
        /// </summary>
        public string C_StockToCode{ set; get; }
        /// <summary>
        /// 调拨仓库名称
        /// </summary>
        public string C_StockToName{ set; get; }
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
        public string C_CreateBy{ set; get; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime C_CreateDate{ set; get; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string C_UpdateBy{ set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime C_UpdateDate{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string C_Remark{ set; get; }
        /// <summary>
        /// 删除人
        /// </summary>
        public string M_DeleteBy{ set; get; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime M_DeleteDate{ set; get; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string M_UploadBy{ set; get; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime M_UploadDate{ set; get; }
        #endregion
    }
}
