using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- AM_Base_AnnexesFile表映射类
    /// </summary>
    public partial class AMBaseAnnexesFileEntity
    {
        #region 属性
        /// <summary>
        /// 文件主键
        /// </summary>
        public string F_Id{ set; get; }
        /// <summary>
        /// 附件夹主键
        /// </summary>
        public string F_FolderId{ set; get; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string F_FileName{ set; get; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string F_FilePath{ set; get; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string F_FileSize{ set; get; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string F_FileExtensions{ set; get; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string F_FileType{ set; get; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int F_DownloadCount{ set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime F_CreateDate{ set; get; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string F_CreateUserId{ set; get; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreateUserName{ set; get; }
        #endregion
    }
}
