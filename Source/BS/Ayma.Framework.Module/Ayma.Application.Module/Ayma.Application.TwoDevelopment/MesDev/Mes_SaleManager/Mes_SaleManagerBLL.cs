using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public partial class Mes_SaleManagerBLL : Mes_SaleManagerIBLL
    {
        private Mes_SaleManagerService mes_SaleManagerService = new Mes_SaleManagerService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_SaleManagerService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_SaleHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_SaleHeadEntity GetMes_SaleHeadEntity(string keyValue)
        {
            try
            {
                return mes_SaleManagerService.GetMes_SaleHeadEntity(keyValue);
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
        /// 获取Mes_SaleDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable< Mes_SaleDetailEntity >GetMes_SaleDetail(string saleNo)
        {
            try
            {
                return mes_SaleManagerService.GetMes_SaleDetail(saleNo);
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
        /// 获取原物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetGoodsList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                return mes_SaleManagerService.GetGoodsList(pagination,queryJson,keyword);
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
        /// 获取已提交的原物料销售单据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleHeadEntity> GetPostList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_SaleManagerService.GetPostList(pagination, queryJson);
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
        /// 报表：原物料销售单据详情
        /// </summary>
        /// <param name="saleNo"></param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleDetailEntity> GetDetailList(string saleNo)
        {
            try
            {
                return mes_SaleManagerService.GetDetailList(saleNo);
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
                mes_SaleManagerService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_SaleHeadEntity entity, List<Mes_SaleDetailEntity> detail)
        {
            try
            {
                mes_SaleManagerService.SaveEntity(keyValue, entity,detail);
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
