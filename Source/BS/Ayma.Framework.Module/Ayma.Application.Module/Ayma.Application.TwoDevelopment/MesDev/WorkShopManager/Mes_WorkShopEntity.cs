using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 12:03
    /// 描 述：车间管理
    /// </summary>
    public partial class Mes_WorkShopEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        [Column("W_CODE")]
        public string W_Code { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        [Column("W_NAME")]
        public string W_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("W_REMARK")]
        public string W_Remark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateUserId = LoginUserInfo.Get().userId;
            this.CreateDate = DateTime.Now;
            this.CreateUserName = LoginUserInfo.Get().realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyUserId = LoginUserInfo.Get().userId;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserName = LoginUserInfo.Get().realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

