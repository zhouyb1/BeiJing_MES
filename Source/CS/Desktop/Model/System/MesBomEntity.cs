using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_Bom表映射类
    /// </summary>
    public partial class MesBomEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// BOM日期
        /// </summary>
        public DateTime B_Date{ set; get; }
        /// <summary>
        /// BOM单号
        /// </summary>
        public string B_OrderNo{ set; get; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string B_GoodsCode{ set; get; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string B_GoodsName{ set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string B_Unit{ set; get; }
        /// <summary>
        /// 等级
        /// </summary>
        public string B_Grade{ set; get; }
        /// <summary>
        /// 下级物料编码
        /// </summary>
        public string B_SecGoodsCode{ set; get; }
        /// <summary>
        /// 下级物料名称
        /// </summary>
        public string B_SecGoodsName{ set; get; }
        /// <summary>
        /// 下级物料单位
        /// </summary>
        public string B_SecUnit{ set; get; }
        /// <summary>
        /// 下级物料数量
        /// </summary>
        public Double B_SecQty{ set; get; }
        /// <summary>
        /// 转换率
        /// </summary>
        public Double B_Conversion{ set; get; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string B_CreateBy{ set; get; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime B_CreateDate{ set; get; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string B_UpdateBy{ set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime B_UpdateDate{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string B_Remark{ set; get; }
        #endregion
    }
}
