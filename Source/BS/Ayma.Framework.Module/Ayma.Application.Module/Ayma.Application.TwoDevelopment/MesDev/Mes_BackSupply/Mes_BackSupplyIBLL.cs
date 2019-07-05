using Ayma.Util;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单制作
    /// </summary>
    public interface Mes_BackSupplyIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_BackSupplyHeadEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取退供应商物料数据
        /// </summary>
        /// <returns></returns>
        DataTable GetBackGoodsList(Pagination pagination, string queryJson, string keyword, string stockCode);

        /// <summary>
        /// 获取Mes_BackSupplyDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_BackSupplyDetailEntity> GetMes_BackSupplyDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_BackSupplyHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BackSupplyHeadEntity GetMes_BackSupplyHeadEntity(string keyValue);   
        /// <summary>
        /// 根据入库单Id获取退供应商表头数据
        /// </summary>
        /// <param name="materInKeyValue">入库单Id</param>
        /// <returns></returns>
        DataTable GetMes_BackSupplyHeadModel(string materInKeyValue);
        /// <summary>
        /// 获取Mes_BackSupplyDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BackSupplyDetailEntity GetMes_BackSupplyDetailEntity(string keyValue); 
        /// <summary>
        /// 根据入库单号 制作退供应商详情
        /// </summary>
        /// <param name="materInNo">入库单号</param>
        /// <returns></returns>
        DataTable GetMes_BackSupplyList(string materInNo);
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
        void SaveEntity(string keyValue, Mes_BackSupplyHeadEntity entity,List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList);
        #endregion

    }
}
