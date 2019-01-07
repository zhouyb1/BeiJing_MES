using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
    /// </summary>
    public partial class Mes_ProductOrderHeadBLL : Mes_ProductOrderHeadIBLL
    {
        private Mes_ProductOrderHeadService mes_ProductOrderHeadService = new Mes_ProductOrderHeadService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_ProductOrderHeadService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderHeadEntity GetMes_ProductOrderHeadEntity(string keyValue)
        {
            try
            {
                return mes_ProductOrderHeadService.GetMes_ProductOrderHeadEntity(keyValue);
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
        /// 获取Mes_ProductOrderDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderDetailEntity GetMes_ProductOrderDetailEntity(string keyValue)
        {
            try
            {
                return mes_ProductOrderHeadService.GetMes_ProductOrderDetailEntity(keyValue);
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
                mes_ProductOrderHeadService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity,Mes_ProductOrderDetailEntity mes_ProductOrderDetailEntity)
        {
            try
            {
                mes_ProductOrderHeadService.SaveEntity(keyValue, entity,mes_ProductOrderDetailEntity);
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
