﻿using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 20:21
    /// 描 述：商品二级分类
    /// </summary>
    public interface GoodKindIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_GoodKindEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_GoodKind表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_GoodKindEntity GetMes_GoodKindEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, Mes_GoodKindEntity entity);
        #endregion


        #region 验证数据
        /// <summary>
        /// 验证编码是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="G_Code">编码</param>
        /// <returns></returns>
        bool ExistCode(string keyValue, string G_Code); 
        #endregion


    }
}
