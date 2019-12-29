using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 20:54
    /// 描 述：抽检记录
    /// </summary>
    public partial class Mes_InspectEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 抽检单号
        /// </summary>
        [Column("I_INSPECTNO")]
        public string I_InspectNo { get; set; }
        /// <summary>
        /// 抽检时间
        /// </summary>
        [Column("I_DATE")]
        public DateTime? I_Date { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("I_ORDERNO")]
        public string I_OrderNo { get; set; }
        /// <summary>
        /// 抽检类型，1人为抽检，2，机器生产线
        /// </summary>
        [Column("I_KIND")]
        public int? I_Kind { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [Column("I_STOCKNAME")]
        public string I_StockName { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("I_STOCKCODE")]
        public string I_StockCode { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [Column("I_BATCH")]
        public string I_Batch { get; set; }
        /// <summary>
        /// 状态(1=生成抽检；2=完成抽检)
        /// </summary>
        [Column("I_STATUS")]
        public int? I_Status { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("I_GOODSCODE")]
        public string I_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("I_GOODSNAME")]
        public string I_GoodsName { get; set; }
        /// <summary>
        /// 抽检数量
        /// </summary>
        [Column("I_GOODSQTY")]
        public double? I_GoodsQty { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        [Column("I_QUALIFIEDQTY")]
        public double? I_QualifiedQty { get; set; }
        /// <summary>
        /// 不合格原因
        /// </summary>
        [Column("I_RESON")]
        public string I_Reson { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("I_CREATEBY")]
        public string I_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("I_CREATEDATE")]
        public DateTime? I_CreateDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.I_CreateDate = DateTime.Now;
            this.I_CreateBy = userInfo.userId;
            this.I_Status = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

