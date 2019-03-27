using Ayma.Util;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-12 17:32
    /// 描 述：排班记录
    /// </summary>
    public interface Mes_ArrangeIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ArrangeEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetDataList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取页面显示子列表数据
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="time"></param>
        /// <param name="orderno"></param>
        /// <param name="workshopcode"></param>
        /// <param name="classcode"></param>
        /// <returns></returns>
        DataTable GetSubDataList(string datetime, string time, string orderno, string workshopcode, string classcode);
        /// <summary>
        /// 获取Mes_Arrange表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ArrangeEntity GetMes_ArrangeEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ArrangeEntity entity);
        #endregion

    }
}
