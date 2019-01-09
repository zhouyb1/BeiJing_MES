using Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
    /// </summary>
    public interface Mes_ProductOrderHeadIBLL
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
        Mes_ProductOrderDetailEntity GetMes_ProductOrderDetailEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity,Mes_ProductOrderDetailEntity mes_ProductOrderDetailEntity);
        #endregion


                /// <summary>
        /// 获取ERP的餐食计划清单
        /// </summary>
        /// <param name="useDate"></param>
        List<ERPFoodListModel> GetErpFoodList(string useDate, string timeStamp);


        /// <summary>
        /// 保存ERP餐食清单
        /// </summary>
        /// <param name="foodEntity"></param>
         void SaveERPFood(List<ERPFoodListModel> foodEntity, out int msgCode, out string msgInfo);



    }
}
