using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 20:34
    /// 描 述：用户表
    /// </summary>
    public partial class Sys_UsersEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Column("U_CODE")]
        public string U_Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column("U_NAME")]
        public string U_Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("U_PASS")]
        public string U_Pass { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Column("U_DEPARTMENT")]
        public string U_Department { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        [Column("U_POST")]
        public string U_Post { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Column("U_RALECODE")]
        public string U_Ralecode { get; set; }
        /// <summary>
        /// 员工类型，1.正式工，2、临时工，3劳务工
        /// </summary>
        [Column("U_KIND")]
        public int? U_Kind { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Column("U_TELEPHONE")]
        public string U_Telephone { get; set; }
        /// <summary>
        /// RFID芯片编码
        /// </summary>
        [Column("U_RFIDCODE")]
        public string U_RFIDCode { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        [Column("U_GROUP")]
        public string U_Group { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [Column("U_INDATE")]
        public DateTime? U_Indate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        [Column("U_OUTDATE")]
        public DateTime? U_Outdate { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        [Column("U_CERT")]
        public string U_Cert { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column("U_SEX")]
        public string U_Sex { get; set; }
        /// <summary>
        /// 名族
        /// </summary>
        [Column("U_NATION")]
        public string U_Nation { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [Column("U_RECORD")]
        public string U_Record { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [Column("U_ORIGIN")]
        public string U_Origin { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Column("U_ADDRESS")]
        public string U_Address { get; set; }
        /// <summary>
        /// 照片1地址
        /// </summary>
        [Column("U_PICTURE1")]
        public string U_Picture1 { get; set; }
        /// <summary>
        /// 照片2地址
        /// </summary>
        [Column("U_PICTURE2")]
        public string U_Picture2 { get; set; }
        /// <summary>
        /// 照片3地址
        /// </summary>
        [Column("U_PICTURE3")]
        public string U_Picture3 { get; set; }
        /// <summary>
        /// 照片4地址
        /// </summary>
        [Column("U_PICTURE4")]
        public string U_Picture4 { get; set; }
        /// <summary>
        /// 照片5地址
        /// </summary>
        [Column("U_PICTURE5")]
        public string U_Picture5 { get; set; }
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
        #region 扩展字段
        #endregion
    }
}

