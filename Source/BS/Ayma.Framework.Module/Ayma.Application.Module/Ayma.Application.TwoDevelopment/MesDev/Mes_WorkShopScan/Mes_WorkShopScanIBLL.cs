using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ayma.Util;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-21 17:39
    /// 描 述：车间物料扫描
    /// </summary>
    public interface Mes_WorkShopScanIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_WorkShopScanEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_WorkShopScan表list实体数据
        /// </summary>
        /// <returns></returns>
        Mes_WorkShopScanEntity GetMes_WorkShopScanEntity(string goodsCode);
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
        void SaveEntity(string keyValue, Mes_WorkShopScanEntity entity);
        #endregion
    }
}
