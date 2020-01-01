namespace Ayma.Application.TwoDevelopment.MesDev.ExtensionModel
{
    /// <summary>
    /// 生产计划
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 生产产品编码
        /// </summary>
        public string F_GoodsCode { get; set; }


        /// <summary>
        /// 生产数量
        /// </summary>
        public float? F_Qty { get; set; }
    }

    /// <summary>
    /// 配方
    /// </summary>
    public class ProductBom
    {
        /// <summary>
        /// 配方ID
        /// </summary>
        public string F_ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string F_ParentID { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string F_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string F_GoodsName { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public int F_Kind { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string F_Unit { get; set; }


        /// <summary>
        ///包装单位
        /// </summary>
        public string F_Unit2 { get; set; }

        /// <summary>
        ///包装数量
        /// </summary>
        public float F_UnitQty { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public float? F_Price { get; set; }


        /// <summary>
        /// 车班入库编码仓库
        /// </summary>
        public string F_InStockCode { get; set; }

        /// <summary>
        /// 原料仓库仓库名称
        /// </summary>
        public string F_InStockName { get; set; }


        /// <summary>
        /// 原料仓库仓库编码
        /// </summary>
        public string F_OutStockCode { get; set; }

        /// <summary>
        /// 原料仓库仓库
        /// </summary>
        public string F_OutStockName { get; set; }



        /// <summary>
        /// 计划数量
        /// </summary>
        public float? F_PlanQty { get; set; }

        /// <summary>
        /// 建议数量
        /// </summary>
        public float? F_ProposeQty { get; set; }



        /// <summary>
        /// 物料级别
        /// </summary>
        public int F_Level { get; set; }


    }

    /// <summary>
    /// 车间物料库存
    /// </summary>
    public class GoodsInventory
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string F_GoodsCode { get; set; }

        /// <summary>
        /// 物料数量
        /// </summary>
        public float? F_Qty { get; set; }
    }

    /// <summary>
    /// 领料数量
    /// </summary>
    public class GoodsOut
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string F_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string F_GoodsName { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public int F_Kind { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string F_Unit { get; set; }


        /// <summary>
        ///包装单位
        /// </summary>
        public string F_Unit2 { get; set; }

        /// <summary>
        ///包装数量
        /// </summary>
        public float F_UnitQty { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public float? F_Price { get; set; }


        /// <summary>
        /// 车班入库编码仓库
        /// </summary>
        public string F_InStockCode { get; set; }

        /// <summary>
        /// 原料仓库仓库名称
        /// </summary>
        public string F_InStockName { get; set; }


        /// <summary>
        /// 原料仓库仓库编码
        /// </summary>
        public string F_OutStockCode { get; set; }

        /// <summary>
        /// 原料仓库仓库
        /// </summary>
        public string F_OutStockName { get; set; }




        /// <summary>
        /// 计划数量
        /// </summary>
        public float? F_PlanQty { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public float? F_InventoryQty { get; set; }


        /// <summary>
        /// 建议数量
        /// </summary>
        public float? F_ProposeQty { get; set; }
    }
}