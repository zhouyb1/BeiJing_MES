using System.Data;
using System.Security.Cryptography.X509Certificates;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 14:58
    /// 描 述：入库单制作
    /// </summary>
    public interface MaterInBillIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取成品入库商品列表（现用）
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetProductGoodsList(Pagination pagination, string queryJson, string keyword);
        /// <summary>
        /// 获取非成品入库商品列表（非成品:原材料/半成品）
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword);
        /// <summary>
        /// 获取成品入库已提交的成品入库
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_MaterInHeadEntity> GetPostProductPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取成品入库显示数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_MaterInHeadEntity> GetProductPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_MaterInHeadEntity> GetPostPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_MaterInHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_MaterInDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_MaterInDetailEntity> GetMes_MaterInDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_MaterInHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_MaterInHeadEntity GetMes_MaterInHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_MaterInDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_MaterInDetailEntity GetMes_MaterInDetailEntity(string keyValue);
        /// <summary>
        /// 原物料入库详细列表
        /// </summary>
        /// <returns></returns>
        DataTable GetMaterInDetailSum(string queryJson);

        /// <summary>
        /// 渲染前端表头
        /// </summary>
        /// <returns></returns>
        IEnumerable<ColumnModel> GetPageTitle(string queryJson);

        /// <summary>
        /// 供应商存货明细
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetSupplyGoodsList(string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void AuditingBill(string keyValue);
        /// <summary>
        /// 撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="errMsg">错误信息</param>
        int CancelBill(string orderNo, out string errMsg); 
        /// <summary>
        /// 提交单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="errMsg">错误信息</param>
        int PostBill(string orderNo,out string errMsg); 
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
        void SaveEntity(string keyValue, Mes_MaterInHeadEntity entity,List<Mes_MaterInDetailEntity> mes_MaterInDetailList);
        #endregion

    }
}
