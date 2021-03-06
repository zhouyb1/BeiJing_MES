﻿using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public interface ProceManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ProceEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取页面显示树形列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ProceEntity> GetTreeList(string queryJson);

        
        /// <summary>
        /// 获取Mes_Proce表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProceEntity GetMes_ProceEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ProceEntity entity);
        #endregion

        #region 验证数据

        /// <summary>
        /// 工艺代码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="recordCode">工艺代码</param>
        /// <returns></returns>
        bool ExistRecordCode(string keyValue, string recordCode); 
        /// <summary>
        /// 工艺名称不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="recordName">工艺名称</param>
        /// <returns></returns>
        bool ExistRecordName(string keyValue, string recordName);
        
        /// <summary>
        /// 工序名称不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="proName">工序名称</param>
        /// <returns></returns>
        bool ExistProName(string keyValue, string proName);

        /// <summary>
        /// 工序号不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="proNo">工序号</param>
        /// <returns></returns>
        bool ExistProNo(string keyValue, string proNo); 
        #endregion

    }
}
