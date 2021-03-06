﻿using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class PickingMaterBLL : PickingMaterIBLL
    {
        private PickingMaterService pickingMaterService = new PickingMaterService();

        #region 获取数据
        /// <summary>
        /// 获取领料计划页面
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarHeadTempEntity> GetTempPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return pickingMaterService.GetTempPageList(pagination, queryJson);
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
        public IEnumerable<Mes_CollarHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return pickingMaterService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_CollarHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CollarHeadEntity GetMes_CollarHeadEntity(string keyValue)
        {
            try
            {
                return pickingMaterService.GetMes_CollarHeadEntity(keyValue);
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
        /// 获取Mes_CollarDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarDetailEntity> GetMes_CollarDetailEntityList(string keyValue)
        {
            try
            {
                return pickingMaterService.GetMes_CollarDetailEntity(keyValue);
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
        /// 获取Mes_CollarTempHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CollarHeadTempEntity GetMes_CollarHeadTempEntity(string keyValue)
        {
            try
            {
                return pickingMaterService.GetMes_CollarHeadTempEntity(keyValue);
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
        /// 获取Mes_CollarTempDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarDetailTempEntity> GetMes_CollarDetailTempEntity(string keyValue)
        {
            try
            {
                return pickingMaterService.GetMes_CollarDetailTempEntity(keyValue);
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
        /// 获取库存物料表
        /// </summary>
        public IEnumerable<Mes_InventoryEntity> GetMaterList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                return pickingMaterService.GetMaterList(pagination, queryJson, keyword);
            }
            catch (Exception ex)
            {
                if (ex is  ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
           
        }

        public DataTable GetProductReportData(string queryJson, out string message)
        {
            try
            {
                return pickingMaterService.GetProductReportData(queryJson, out  message);
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

        public IEnumerable<ColumnModel> GetProductReportTitle(string queryJson, out string message)
        {
            try
            {
                return pickingMaterService.GetProductReportTitle(queryJson, out  message);
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

        public DataTable GetProductReportDataEx(string queryJson, out string message)
        {
            try
            {
                return pickingMaterService.GetProductReportDataEx(queryJson, out message);
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

        public IEnumerable<ColumnModel> GetProductReportTitleEx(string queryJson, out string message)
        {
            try
            {
                return pickingMaterService.GetProductReportTitleEx(queryJson, out message);
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

        public DataTable GetCollarRport(string queryJson)
        {
            try
            {
                return pickingMaterService.GetCollarRport(queryJson);
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
        public DataTable GetOtherRport(string queryJson)
        {
            try
            {
                return pickingMaterService.GetOtherRport(queryJson);
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
                pickingMaterService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_CollarHeadEntity entity,List<Mes_CollarDetailEntity >mes_CollarDetailEntityList)
        {
            try
            {
                pickingMaterService.SaveEntity(keyValue, entity,mes_CollarDetailEntityList);
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
        /// 新增实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(List<Mes_CollarHeadEntity> headList,List<Mes_CollarDetailEntity> mes_CollarDetailEntityList)
        {
            try
            {
                pickingMaterService.SaveEntity(headList, mes_CollarDetailEntityList);
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
        ///  自动生成领料单
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AutoCreateOrder(string date,out string message)
        {
            try
            {
              return pickingMaterService.AutoCreateOrder(date, out message);
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
