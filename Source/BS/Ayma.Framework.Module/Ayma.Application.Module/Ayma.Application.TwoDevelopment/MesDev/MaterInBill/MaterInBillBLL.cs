using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 14:58
    /// 描 述：入库单制作
    /// </summary>
    public partial class MaterInBillBLL : MaterInBillIBLL
    {
        private MaterInBillService materInBillService = new MaterInBillService();

        #region 获取数据

        /// <summary>
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return materInBillService.GetPostPageList(pagination, queryJson);
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
        public IEnumerable<Mes_MaterInHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return materInBillService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_MaterInDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInDetailEntity> GetMes_MaterInDetailList(string keyValue)
        {
            try
            {
                return materInBillService.GetMes_MaterInDetailList(keyValue);
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
        /// 获取Mes_MaterInHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MaterInHeadEntity GetMes_MaterInHeadEntity(string keyValue)
        {
            try
            {
                return materInBillService.GetMes_MaterInHeadEntity(keyValue);
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
        public Mes_MaterInDetailEntity GetMes_MaterInDetailEntity(string keyValue)
        {
            try
            {
                return materInBillService.GetMes_MaterInDetailEntity(keyValue);
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
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void AuditingBill(string keyValue)
        {
            try
            {
                materInBillService.AuditingBill(keyValue);
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
        /// 撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="errMsg">错误信息</param>
        public int CancelBill(string orderNo, out string errMsg)
        {
            try
            {
                return materInBillService.CancelBill(orderNo, out errMsg);
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
        /// 提交单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="errMsg">错误信息</param>
        public int PostBill(string orderNo,out string errMsg)
        {
            try
            {
                return materInBillService.PostBill(orderNo,out errMsg);
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                materInBillService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_MaterInHeadEntity entity,List<Mes_MaterInDetailEntity> mes_MaterInDetailList)
        {
            try
            {
                materInBillService.SaveEntity(keyValue, entity,mes_MaterInDetailList);
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
