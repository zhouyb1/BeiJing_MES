using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:40
    /// 描 述：其它出库单
    /// </summary>
    public partial class Mes_OtherOutHeadBLL : Mes_OtherOutHeadIBLL
    {
        private Mes_OtherOutHeadService mes_OtherOutHeadService = new Mes_OtherOutHeadService();

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OtherOutHeadEntity> GetList( string queryJson )
        {
            try
            {
                return mes_OtherOutHeadService.GetList(queryJson);
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
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherOutHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_OtherOutHeadService.GetPageList(pagination, queryJson);
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
        /// 获取查询列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherOutHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_OtherOutHeadService.GetPostPageList(pagination, queryJson);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_OtherOutHeadEntity GetEntity(string keyValue)
        {
            try
            {
                return mes_OtherOutHeadService.GetEntity(keyValue);
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
        /// 获取其它出库单从表数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherOutDetailEntity> GetOtherOutDetailEntity(string keyValue)
        {
            try
            {
                return mes_OtherOutHeadService.GetOtherOutDetailEntity(keyValue);
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
                mes_OtherOutHeadService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_OtherOutHeadEntity strEntity, List<Mes_OtherOutDetailEntity> mes_OtherOutDetailList)
        {
            try
            {
                mes_OtherOutHeadService.SaveEntity(keyValue, strEntity, mes_OtherOutDetailList);
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
