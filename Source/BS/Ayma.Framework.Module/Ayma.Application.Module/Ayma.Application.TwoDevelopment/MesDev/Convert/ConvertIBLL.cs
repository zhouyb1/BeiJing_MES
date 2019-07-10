using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-20 09:36
    /// 描 述：物料转换对应表
    /// </summary>
    public interface ConvertIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ConvertEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_Convert表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ConvertEntity GetMes_ConvertEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ConvertEntity entity);
        #endregion

        #region 验证重复
        /// <summary>
        ///检查转换的编码重复性
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="code">原编码</param>
        /// <param name="convertCode">转换后的编码</param>
        /// <returns></returns>
        bool ExistCode(string keyValue, string code,string convertCode);
        #endregion
    }
}
