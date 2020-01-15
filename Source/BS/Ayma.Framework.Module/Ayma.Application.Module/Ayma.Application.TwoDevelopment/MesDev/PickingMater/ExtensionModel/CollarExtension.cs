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
        public decimal? F_Qty { get; set; }
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
        /// 工序编码
        /// </summary>
        public string F_ProceCode { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string F_ProceName { get; set; }


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
        public decimal F_UnitQty { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal? F_Price { get; set; }


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
        /// 最小转化率
        /// </summary>
        public decimal? F_ConvertMin { get; set; }

        /// <summary>
        /// 最大转化率
        /// </summary>
        public decimal? F_ConvertMax { get; set; }





        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal? F_PlanQty { get; set; }

        /// <summary>
        /// 建议数量
        /// </summary>
        public decimal? F_ProposeQty { get; set; }



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
        public decimal? F_Qty { get; set; }
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
        public decimal F_UnitQty { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal? F_Price { get; set; }


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
        public decimal? F_PlanQty { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal? F_InventoryQty { get; set; }


        /// <summary>
        /// 建议数量
        /// </summary>
        public decimal? F_ProposeQty { get; set; }
    }


    /// <summary>
    /// 物料转换
    /// </summary>
    public class GoodsConvert
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
        /// 日期
        /// </summary>
        public string F_CreateDate { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string F_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string F_GoodsName { get; set; }


        /// <summary>
        /// 工序编码
        /// </summary>
        public string F_ProceCode { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string F_ProceName { get; set; }


        /// <summary>
        /// 物料级别
        /// </summary>
        public int F_Level { get; set; }


        /// <summary>
        /// 物料类型
        /// </summary>
        public int F_Kind { get; set; }


        /// <summary>
        /// 基本单位
        /// </summary>
        public string F_Unit { get; set; }

        /// <summary>
        /// 使用数量
        /// </summary>
        public decimal? F_Qty { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public decimal? F_SumQty { get; set; }

        /// <summary>
        /// 最小转化率
        /// </summary>
        public decimal? F_ConvertMin { get; set; }


        /// <summary>
        /// 最大转化率
        /// </summary>
        public decimal? F_ConvertMax { get; set; }


        /// <summary>
        /// 转化率范围
        /// </summary>
        public string F_ConvertRange { get; set; }

        /// <summary>
        /// 转化率
        /// </summary>
        public decimal? F_Convert { get; set; }
        

        /// <summary>
        /// 转换标识 -1低于下限 1高于上限 0合理
        /// </summary>
        public int F_ConvertTag { get; set; }
    }

    /// <summary>
    /// 物料转换数据
    /// </summary>
    public class GoodsOrg
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string F_GoodsCode { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        public string F_CreateDate { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal F_Qty { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public decimal F_Type { get; set; }
    }
}