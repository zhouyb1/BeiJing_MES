using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-06-27 15:26
    /// 描 述：班组表
    /// </summary>
    public partial class Mes_TeamEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 班组编码
        /// </summary>
        /// <returns></returns>
        [Column("T_CODE")]
        public string T_Code { get; set; }
        /// <summary>
        /// 班组名称
        /// </summary>
        /// <returns></returns>
        [Column("T_NAME")]
        public string T_Name { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        /// <returns></returns>
        [Column("T_WORKSHOPCODE")]
        public string T_WorkShopCode { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        /// <returns></returns>
        [Column("T_USERNAME")]
        public string T_UserName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("T_REMARK")]
        public string T_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
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

