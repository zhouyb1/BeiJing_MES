using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 10:37
    /// 描 述：退供应商单查询
    /// </summary>
    public partial class Mes_BackSupplyQueryBLL : Mes_BackSupplyQueryIBLL
    {
        private Mes_BackSupplyQueryService mes_BackSupplyQueryService = new Mes_BackSupplyQueryService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BackSupplyHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_BackSupplyQueryService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_BackSupplyDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<Mes_BackSupplyDetailEntity> GetMes_BackSupplyDetailList(string keyValue)
        {
            try
            {
                return mes_BackSupplyQueryService.GetMes_BackSupplyDetailList(keyValue);
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
        /// 获取Mes_BackSupplyHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackSupplyHeadEntity GetMes_BackSupplyHeadEntity(string keyValue)
        {
            try
            {
                return mes_BackSupplyQueryService.GetMes_BackSupplyHeadEntity(keyValue);
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
        /// 获取Mes_BackSupplyDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackSupplyDetailEntity GetMes_BackSupplyDetailEntity(string keyValue)
        {
            try
            {
                return mes_BackSupplyQueryService.GetMes_BackSupplyDetailEntity(keyValue);
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
                mes_BackSupplyQueryService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_BackSupplyHeadEntity entity,List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList)
        {
            try
            {
                mes_BackSupplyQueryService.SaveEntity(keyValue, entity,mes_BackSupplyDetailList);
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
