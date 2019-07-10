using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class Mes_BomRecordEntity
    {
        #region 实体成员

        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        [Column("B_PARENTID")]
        public string B_ParentID { get; set; }

        /// <summary>
        /// 工艺代码
        /// </summary>
        [Column("B_RECORDCODE")]
        public string B_RecordCode { get; set; }

        /// <summary>
        /// 配方编码
        /// </summary>
        [Column("B_FORMULACODE")]
        public string B_FormulaCode { get; set; }

        /// <summary>
        /// 配方名称
        /// </summary>
        [Column("B_FORMULANAME")]
        public string B_FormulaName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("B_GOODSCODE")]
        public string B_GoodsCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("B_GOODSNAME")]
        public string B_GoodsName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("B_UNIT")]
        public string B_Unit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Column("B_QTY")]
        public decimal? B_Qty { get; set; }

        [NotMapped]
        public decimal? B_Total { get; set; }
        /// <summary>
        /// erp餐食编码
        /// </summary>
        [NotMapped]
        public string B_ErpCode { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [Column("B_CREATEBY")]
        public string B_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("B_CREATEDATE")]
        public DateTime? B_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("B_UPDATEBY")]
        public string B_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("B_UPDATEDATE")]
        public DateTime? B_UpdateDate { get; set; }    
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("B_AVAIL")]
        public ErpEnums.YesOrNoEnum? B_Avail { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("B_STARTTIME")]
        public DateTime? B_StartTime { get; set; } 
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("B_ENDTIME")]
        public DateTime? B_EndTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("B_REMARK")]
        public string B_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            var userInfo = LoginUserInfo.Get();
            this.B_CreateDate = DateTime.Now;
            this.B_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
            var userInfo = LoginUserInfo.Get();
            this.B_UpdateDate = DateTime.Now;
            this.B_UpdateBy = userInfo.realName;
        }
        #endregion
    }
}

