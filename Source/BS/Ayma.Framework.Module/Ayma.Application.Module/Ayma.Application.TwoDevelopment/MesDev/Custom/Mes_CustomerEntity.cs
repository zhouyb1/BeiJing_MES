using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-06 16:30
    /// 描 述：客户表
    /// </summary>
    public partial class Mes_CustomerEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        [Column("C_CODE")]
        public string C_Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [Column("C_NAME")]
        public string C_Name { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("C_CREATEBY")]
        public string C_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("C_CREATEDATE")]
        public DateTime? C_CreateDate { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        [Column("S_PERSON")]
        public string S_Person { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        [Column("S_TELEPHONE")]
        public string S_Telephone { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        /// <returns></returns>
        [Column("S_CORP")]
        public string S_Corp { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        [Column("S_ADDRESS")]
        public string S_Address { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        /// <returns></returns>
        [Column("S_TAXCODE")]
        public string S_TaxCode { get; set; }
        /// <summary>
        /// 资质1
        /// </summary>
        /// <returns></returns>
        [Column("S_EFFECT1")]
        public string S_Effect1 { get; set; }
        /// <summary>
        /// 资质2
        /// </summary>
        /// <returns></returns>
        [Column("S_EFFECT2")]
        public string S_Effect2 { get; set; }
        /// <summary>
        /// 资质3
        /// </summary>
        /// <returns></returns>
        [Column("S_EFFECT3")]
        public string S_Effect3 { get; set; }
        /// <summary>
        /// 资质4
        /// </summary>
        /// <returns></returns>
        [Column("S_EFFECT4")]
        public string S_Effect4 { get; set; }
        /// <summary>
        /// 资质5
        /// </summary>
        /// <returns></returns>
        [Column("S_EFFECT5")]
        public string S_Effect5 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("S_REMARK")]
        public string S_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.C_CreateDate = DateTime.Now;
            this.C_CreateBy = userInfo.realName;
            this.ID = Guid.NewGuid().ToString();
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
    }
}

