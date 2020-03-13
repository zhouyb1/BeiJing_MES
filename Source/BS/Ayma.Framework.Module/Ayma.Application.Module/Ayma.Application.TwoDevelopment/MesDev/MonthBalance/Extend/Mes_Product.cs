namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 配方
    /// </summary>
    public class Mes_Product
    {
        /// <summary>
        /// 配方ID
        /// </summary>
        public string M_ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string M_ParentID { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string M_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string M_GoodsName { get; set; }


        /// <summary>
        /// 物料类型
        /// </summary>
        public int M_Kind { get; set; }

        /// <summary>
        /// 物料级别
        /// </summary>
        public int M_Level { get; set; }

    }
}