using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-02 17:05
    /// 描 述：成品出库单制作
    /// </summary>
    public partial class ProOutMakeBLL : ProOutMakeIBLL
    {
        private ProOutMakeService proOutMakeService = new ProOutMakeService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProOutHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return proOutMakeService.GetPageList(pagination, queryJson);
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
        /// 获取页面显示列表数据 单据完成状态
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProOutHeadEntity> GetSearchPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return proOutMakeService.GetSearchPageList(pagination, queryJson);
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
        /// 获取Mes_ProOutDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProOutDetailEntity> GetMes_ProOutDetailList(string keyValue)
        {
            try
            {
                return proOutMakeService.GetMes_ProOutDetailList(keyValue);
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
        /// 获取Mes_ProOutHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProOutHeadEntity GetMes_ProOutHeadEntity(string keyValue)
        {
            try
            {
                return proOutMakeService.GetMes_ProOutHeadEntity(keyValue);
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
        /// 获取Mes_ProOutDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProOutDetailEntity GetMes_ProOutDetailEntity(string keyValue)
        {
            try
            {
                return proOutMakeService.GetMes_ProOutDetailEntity(keyValue);
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
        /// 获取仓库成品物料列表
        /// </summary>
        /// <param name="stockCode">仓库编码</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetInventoryProMaterList(Pagination paginationobj, string stockCode)
        {
            try
            {
                return proOutMakeService.GetInventoryProMaterList(paginationobj, stockCode);
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
                proOutMakeService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_ProOutHeadEntity entity,List<Mes_ProOutDetailEntity> mes_ProOutDetailList)
        {
            try
            {
                proOutMakeService.SaveEntity(keyValue, entity,mes_ProOutDetailList);
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
