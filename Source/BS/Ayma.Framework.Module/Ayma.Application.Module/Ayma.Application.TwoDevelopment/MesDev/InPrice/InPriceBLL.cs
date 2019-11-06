using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-08-06 10:54
    /// 描 述：原物料入库价格表
    /// </summary>
    public partial class InPriceBLL : InPriceIBLL
    {
        private InPriceService inPriceService = new InPriceService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InPriceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return inPriceService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_InPrice表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InPriceEntity GetMes_InPriceEntity(string keyValue)
        {
            try
            {
                return inPriceService.GetMes_InPriceEntity(keyValue);
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
        public void DeleteEntity(List<Mes_InPriceEntity> list)
        {
            try
            {
                inPriceService.DeleteEntity(list);
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
        public void SaveEntity(string keyValue, Mes_InPriceEntity entity, Mes_PriceEntity entity2)
        {
            try
            {
                inPriceService.SaveEntity(keyValue, entity, entity2);
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
        /// 批量保存
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entityList"></param>
        /// <param name="entityList2"></param>
        public void SaveEntity(List<Mes_InPriceEntity> entityList, List<Mes_PriceEntity> entityList2)
        {
            try
            {
                inPriceService.SaveEntity(entityList, entityList2);
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
