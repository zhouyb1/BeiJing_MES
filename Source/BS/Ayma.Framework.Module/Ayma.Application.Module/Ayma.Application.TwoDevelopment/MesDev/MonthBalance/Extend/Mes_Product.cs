using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 物料转换关系
    /// </summary>
    public class Mes_Product
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string M_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string M_GoodsName { get; set; }

        /// <summary>
        /// 原料数量
        /// </summary>
        public decimal? M_Qty { get; set; }

        /// <summary>
        /// 转换后物料编码
        /// </summary>
        public string M_SecGoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string M_SecGoodsName { get; set; }


        /// <summary>
        /// 转换数量
        /// </summary>
        public decimal? M_SecQty { get; set; }
    }

    /// <summary>
    /// 物料转换单使用商品
    /// </summary>
    public class Mes_UseProduct
    {

        /// <summary>
        /// 物料编码
        /// </summary>
        public string M_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string M_GoodsName { get; set; }


        /// <summary>
        /// 物料级别
        /// </summary>
        public int M_Level { get; set; }


        /// <summary>
        /// 数量
        /// </summary>
        public decimal? M_Qty { get; set; }

        /// <summary>
        /// 加权平均单价
        /// </summary>
        /// <returns></returns>
        public decimal? M_GoodsPrice { get; set; }


        /// <summary>
        /// 子物料
        /// </summary>
        public List<Mes_UseProduct> ChildUseProducts { get; set; }
    }
}