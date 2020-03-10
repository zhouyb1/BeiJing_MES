using Ayma.Util;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-04 16:14
    /// 描 述：强制使用记录单据
    /// </summary>
    public interface CompUseHeadIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_CompUseHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        ///     /// <summary>
        ///  获取强制使用记录单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_CompUseHeadEntity> CompUseHeadList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_CompUseDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_CompUseDetailEntity> GetMes_CompUseDetailList(string keyValue,string state);
        /// <summary>
        /// 获取Mes_CompUseHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_CompUseHeadEntity GetMes_CompUseHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_CompUseDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_CompUseDetailEntity GetMes_CompUseDetailEntity(string keyValue);
        /// <summary>
        /// 获取强制使用单据物料数据
        /// </summary>
        /// <returns></returns>
        DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword, string stockCode);
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
        void SaveEntity(string keyValue, Mes_CompUseHeadEntity entity,List<Mes_CompUseDetailEntity> mes_CompUseDetailList);
        #endregion

    }
}
