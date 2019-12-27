using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Util;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-21 17:39
    /// 描 述：车间物料扫描
    /// </summary>
    public partial class Mes_WorkShopScanBLL : Mes_WorkShopScanIBLL
    {
        private Mes_WorkShopScanService mes_WorkShopScanService = new Mes_WorkShopScanService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_WorkShopScanEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return mes_WorkShopScanService.GetPageList(pagination, queryJson);
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
        /// 获取Mes_WorkShopScan表实体数据
        /// </summary>
        /// <returns></returns>
        public Mes_WorkShopScanEntity GetMes_WorkShopScanEntity(string goodsCode,string batch)
        {
            try
            {
                return mes_WorkShopScanService.GetMes_WorkShopScanEntity(goodsCode,batch);
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
                mes_WorkShopScanService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_WorkShopScanEntity entity)
        {
            try
            {
                mes_WorkShopScanService.SaveEntity(keyValue, entity);
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

