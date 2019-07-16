using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：车间入库到线边仓
    /// </summary>
    public partial class InWorkShopManagerBLL : InWorkShopManagerIBLL
    {
        private InWorkShopManagerService inWorkShopManagerService = new InWorkShopManagerService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InWorkShopHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return inWorkShopManagerService.GetPageList(pagination, queryJson);
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
        /// 车间入库到线边仓查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_InWorkShopHeadEntity> GetSearchIndex(Pagination pagination, string queryJson)
        {
            try
            {
                return inWorkShopManagerService.GetSearchIndex(pagination, queryJson);
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
        /// 获取Mes_InWorkShopHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InWorkShopHeadEntity GetMes_InWorkShopHeadEntity(string keyValue)
        {
            try
            {
                return inWorkShopManagerService.GetMes_InWorkShopHeadEntity(keyValue);
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
        /// 获取Mes_InWorkShopDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_InWorkShopDetailEntity> GetMes_InWorkShopDetailList(string keyValue)
        {
            try
            {
                return inWorkShopManagerService.GetMes_InWorkShopDetailList(keyValue);
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
        /// 获取仓库物料列表
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetInventoryMaterList(Pagination paginationobj,string stockCode)
        {
            try
            {
                return inWorkShopManagerService.GetInventoryMaterList(paginationobj, stockCode);
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
        /// 获取物料列表(半成品和成品)
        /// </summary>
        /// <param name="paginationobj">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetGoodsList(Pagination paginationobj, string queryJson)
        {
            try
            {
                return inWorkShopManagerService.GetGoodsList(paginationobj, queryJson);
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
                inWorkShopManagerService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_InWorkShopHeadEntity entity, List<Mes_InWorkShopDetailEntity> mes_InWorkShopDetailList)
        {
            try
            {
                inWorkShopManagerService.SaveEntity(keyValue, entity, mes_InWorkShopDetailList);
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
