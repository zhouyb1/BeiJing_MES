using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 13:51
    /// 描 述：其它入库单
    /// </summary>
    public partial class OtherWarehouseReceiptBLL : OtherWarehouseReceiptIBLL
    {
        private OtherWarehouseReceiptService otherWarehouseReceiptService = new OtherWarehouseReceiptService();

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInHeadEntity> GetList( string queryJson )
        {
            try
            {
                return otherWarehouseReceiptService.GetList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return otherWarehouseReceiptService.GetPageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取列表分页数据(其它入库单查询)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return otherWarehouseReceiptService.GetPostPageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_OtherInHeadEntity GetEntity(string keyValue)
        {
            try
            {
                return otherWarehouseReceiptService.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                otherWarehouseReceiptService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_OtherInHeadEntity entity, List<Mes_OtherInDetailEntity> strmes_MaterOtherInDetailList)
        {
            try
            {
                otherWarehouseReceiptService.SaveEntity(keyValue, entity, strmes_MaterOtherInDetailList);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取非成品成品列表数据(非成品:原材料/半成品)
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetGoodsList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                return otherWarehouseReceiptService.GetGoodsList(pagination, queryJson, keyword);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取Mes_OtherInHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInDetailEntity> GetMes_OtherInDetaiEntity(string keyValue,string state)
        {
            try
            {
                return otherWarehouseReceiptService.GetMes_OtherInDetaiEntity(keyValue,state);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

    }
}
