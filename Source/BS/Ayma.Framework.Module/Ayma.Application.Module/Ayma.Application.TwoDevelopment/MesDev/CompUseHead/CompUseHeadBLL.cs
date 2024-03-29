﻿using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-04 16:14
    /// 描 述：强制使用记录单据
    /// </summary>
    public partial class CompUseHeadBLL : CompUseHeadIBLL
    {
        private CompUseHeadService compUseHeadService = new CompUseHeadService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CompUseHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return compUseHeadService.GetPageList(pagination, queryJson);
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
        /// 获取强制使用记录单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CompUseHeadEntity> CompUseHeadList(Pagination pagination, string queryJson)
        {
            try
            {
                return compUseHeadService.CompUseHeadList(pagination, queryJson);
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
        /// 获取Mes_CompUseDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<Mes_CompUseDetailEntity> GetMes_CompUseDetailList(string keyValue,string state)
        {
            try
            {
                return compUseHeadService.GetMes_CompUseDetailList(keyValue,state);
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
        /// 获取Mes_CompUseHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CompUseHeadEntity GetMes_CompUseHeadEntity(string keyValue)
        {
            try
            {
                return compUseHeadService.GetMes_CompUseHeadEntity(keyValue);
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
        /// 获取Mes_CompUseDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CompUseDetailEntity GetMes_CompUseDetailEntity(string keyValue)
        {
            try
            {
                return compUseHeadService.GetMes_CompUseDetailEntity(keyValue);
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
        /// 获取强制使用单据物料数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword, string stockCode)
        {
            try
            {
                return compUseHeadService.GetGoodsList(pagination, queryJson, keyword, stockCode);
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
                compUseHeadService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_CompUseHeadEntity entity,List<Mes_CompUseDetailEntity> mes_CompUseDetailList)
        {
            try
            {
                compUseHeadService.SaveEntity(keyValue, entity,mes_CompUseDetailList);
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
