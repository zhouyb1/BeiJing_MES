using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 09:31
    /// 描 述：供应商列表
    /// </summary>
    public partial class Mes_SupplyEntity 
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
        [Column("S_CODE")]
        public string S_Code { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("S_NAME")]
        public string S_Name { get; set; }
        /// <summary>
        /// 资质期限
        /// </summary>
        [Column("S_EFFECTTIME")]
        public DateTime? S_EffectTime { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("S_CREATEBY")]
        public string S_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("S_CREATEDATE")]
        public DateTime? S_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("S_UPDATEBY")]
        public string S_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("S_UPDATEDATE")]
        public DateTime? S_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
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
            this.ID = Guid.NewGuid().ToString();
            this.S_CreateDate=DateTime.Now;
            this.S_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.S_UpdateDate=DateTime.Now;
            this.S_UpdateBy = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

