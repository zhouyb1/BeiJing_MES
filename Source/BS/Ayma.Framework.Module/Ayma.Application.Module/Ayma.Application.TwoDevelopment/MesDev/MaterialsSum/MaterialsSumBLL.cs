using System.Data;
using Ayma.Util;
using System;
using System.Collections.Generic;
using Ayma.Application.TwoDevelopment.MesDev.MaterialsSum.ViewModel;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-16 10:59
    /// 描 述：原物料统计(入库、出库、次品)
    /// </summary>
    public partial class MaterialsSumBLL : MaterialsSumIBLL
    {
        private MaterialsSumService materialsSumService = new MaterialsSumService();

        #region 获取数据
        /// <summary>
        /// 获取库存明细表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public List<InventoryViewModel> GetInventoryDetail(Pagination pagination, string queryJson)
        {
            try
            {
                return materialsSumService.GetInventoryDetail(pagination, queryJson);
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
        /// 获取选取的时间原物料入库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料出库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialOutDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialOutDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料退库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialBackDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialBackDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料销售详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialSaleDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialSaleDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料报废详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialScrapDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialScrapDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料其它入库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialOtherDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialOtherDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料其他出库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialOtherOutDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialOtherOutDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取选取的时间原物料退供应商详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialBackSupplyDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                return materialsSumService.GetMaterialBackSupplyDetailListByDate(pagination, queryJson, M_GoodsCode);
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
        /// 获取期初期末显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialSumListByDate(Pagination pagination, string queryJson)
        {
            try
            {
                return materialsSumService.GetMaterialSumListByDate(pagination, queryJson);
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialSumList(Pagination pagination, string queryJson)
        {
            try
            {
                return materialsSumService.GetMaterialSumList(pagination, queryJson);
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
        /// 获取Mes_MaterInDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DataTable GetMes_MaterInDetailList(Pagination pagination, string queryJson)
        {
            try
            {
                return materialsSumService.GetMes_MaterInDetailList( pagination,  queryJson);
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
