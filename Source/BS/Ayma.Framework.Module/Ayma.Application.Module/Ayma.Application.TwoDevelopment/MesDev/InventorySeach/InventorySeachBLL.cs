using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public partial class InventorySeachBLL : InventorySeachIBLL
    {
        private InventorySeachService inventorySeachService = new InventorySeachService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return inventorySeachService.GetPageList(pagination, queryJson);
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
        /// 获取物料领用情况列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<PickOrUsedModel> GetPickPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return inventorySeachService.GetPickPageList(pagination, queryJson);
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
        /// 获取物料使用情况列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<PickOrUsedModel> GetUsedPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return inventorySeachService.GetUsedPageList(pagination, queryJson);
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
        /// 获取物料价值查询列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<GoodsPriceModel> GetPricePageList(Pagination pagination, string queryJson)
        {
            try
            {
                return inventorySeachService.GetPricePageList(pagination, queryJson);
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
        /// 获取Mes_Inventory表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InventoryEntity GetMes_InventoryEntity(string keyValue)
        {
            try
            {
                return inventorySeachService.GetMes_InventoryEntity(keyValue);
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
        /// 根据仓库编码和商品编码获取列表
        /// </summary>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="goodsCode">物料编码</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetListByStockAndCode(string stockCode, string goodsCode)
        {
            try
            {
                return inventorySeachService.GetListByStockAndCode(stockCode, goodsCode);
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
        /// 获取Mes_Inventory表实体数据 根据商品编码和仓库编码以及批次
        /// </summary>
        /// <param name="goodsCode">商品编码</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="batch">批次</param>
        /// <returns></returns>
        public Mes_InventoryEntity GetEntityBy(string goodsCode, string stockCode, string batch)
        {
            try
            {
                return inventorySeachService.GetEntityBy(goodsCode, stockCode, batch);
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
        ///根据goodsCode获取List
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public Mes_InventoryEntity GetListByParams(string goodsCode,string batch)
        {
            try
            {
                return inventorySeachService.GetListByParams(goodsCode, batch);
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
                inventorySeachService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_InventoryEntity entity)
        {
            try
            {
                inventorySeachService.SaveEntity(keyValue, entity);
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
