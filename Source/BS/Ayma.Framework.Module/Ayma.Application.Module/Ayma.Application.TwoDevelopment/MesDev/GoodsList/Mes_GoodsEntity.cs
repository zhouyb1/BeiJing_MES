using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public partial class Mes_GoodsEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("G_SUPPLYCODE")]
        public string G_SupplyCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("G_SUPPLYNAME")]
        public string G_SupplyName { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [Column("G_CODE")]
        public string G_Code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("G_NAME")]
        public string G_Name { get; set; }
        /// <summary>
        /// 商品类型 1=原材料 2=半成品 3=成品
        /// </summary>
        [Column("G_KIND")]
        public ErpEnums.GkindEnum? G_Kind { get; set; }
        /// <summary>
        /// 保质时间
        /// </summary>
        [Column("G_PERIOD")]
        public int? G_Period { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("G_PRICE")]
        public decimal? G_Price { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("G_UNIT")]
        public string G_Unit { get; set; } 
        /// <summary>
        /// 单位重量
        /// </summary>
        [Column("G_UNITWEIGHT")]
        public decimal? G_UnitWeight { get; set; }
        
        /// <summary>
        /// 上限预警比例
        /// </summary>
        [Column("G_SUPER")]
        public decimal? G_Super { get; set; }
        /// <summary>
        /// 下限预警比例
        /// </summary>
        [Column("G_LOWER")]
        public decimal? G_Lower { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("G_CREATEBY")]
        public string G_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("G_CREATEDATE")]
        public DateTime? G_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("G_UPDATEBY")]
        public string G_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("G_UPDATEDATE")]
        public DateTime? G_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("G_REMARK")]
        public string G_Remark { get; set; }     
        /// <summary>
        /// ERP中的编码(成品必填)
        /// </summary>
        [Column("G_ERPCODE")]
        public string G_Erpcode { get; set; }   
        /// <summary>
        /// 商品二级分类
        /// </summary>
        [Column("G_TKIND")]
        public string G_TKind { get; set; } 
        /// <summary>
        /// 包装规格
        /// </summary>
        [Column("G_UNITQTY")]
        public decimal? G_UnitQty { get; set; }   
        /// <summary>
        /// 包装单位
        /// </summary>
        [Column("G_UNIT2")]
        public string G_Unit2 { get; set; }  
        /// <summary>
        /// 是否自制(0-否 1-是)
        /// </summary>
        [Column("G_SELF")]
        public ErpEnums.YesOrNoEnum? G_Self { get; set; } 
        /// <summary>
        /// 是否还在使用这个物料(0-否 1-是)
        /// </summary>
        [Column("G_ONLINE")]
        public ErpEnums.YesOrNoEnum? G_Online { get; set; }
        /// <summary>
        /// 备料天数(当一级分类为原料，必填、程序控制)
        /// </summary>
        [Column("G_PREPAREDAY")]
        public int? G_Prepareday { get; set; }  
        /// <summary>
        /// 销售税率
        /// </summary>
        [Column("G_Otax")]
        public decimal? G_Otax { get; set; }
        /// <summary>
        /// 购进税率
        /// </summary>
        [Column("G_ITAX")]
        public decimal? G_Itax { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.G_CreateDate = DateTime.Now;
            this.G_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.G_UpdateDate = DateTime.Now;
            this.G_UpdateBy = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        /// <summary>
        /// 成品条码
        /// </summary>
        [Column("G_BARCODE")]
        public string G_Barcode { get; set; }
        /// <summary>
        /// 班组编号
        /// </summary>
        [Column("G_TeamCode")]
        public string G_TeamCode { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Column("G_StockCode")]
        public string G_StockCode { get; set; }
        /// <summary>
        /// 最新价格
        /// </summary>
         [NotMapped]
        public decimal? M_Price { get; set; }
        #endregion
    }
}

