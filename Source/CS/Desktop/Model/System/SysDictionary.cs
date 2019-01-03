using System;

namespace Model
{
    public class SysDictionary
    {
        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string D_Code { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string D_Name { set; get; }


        /// <summary>
        /// 排序
        /// </summary>
        public int D_Seq { get; set; }


        /// <summary>
        /// 字典类型
        /// </summary>
        public string D_Type { set; get; }



        /// <summary>
        /// 创建人
        /// </summary>
        public string D_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? D_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string D_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? D_UpdateDate { set; get; }
        #endregion 
    }
}