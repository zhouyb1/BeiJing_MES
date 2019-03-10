using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-07 11:05
    /// 描 述：生产订单管理
    /// </summary>
    public interface ProductOrderManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProductOrderHeadEntity GetMes_ProductOrderHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_ProductOrderDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderDetailEntity> GetMes_ProductOrderDetaillist(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity, List<Mes_ProductOrderDetailEntity> mes_ProductOrderDetaillist);

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue"></param>
        void AuditingBill(string keyValue);

        /// <summary>
        /// 递归统计bom
        /// </summary>
        IEnumerable<Mes_BomRecordEntity> GetBomList(string parentId,int qty);

        /// <summary>
        /// 保存订单所需的原物料
        /// </summary>
        void SaveBomList(List<Mes_MaterEntity> entityList);

        #endregion

    }
}
